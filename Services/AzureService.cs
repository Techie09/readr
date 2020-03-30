using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Readr.Models;
using Readr.Services.Interfaces;
using Readr.Repositories.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace Readr.Services
{
    public class AzureService : IAzureService
    {
        private readonly IAzureSettings _azureSettings;
        private readonly ISessionDetailRepository _sessionDetailRepository;
        private readonly IUserSessionRepository _userSessionRepository;

        public string GetVisionAnalysisEndpoint(NameValueCollection queryString) => $"{_azureSettings.RootUrl}/vision/v1.0/analyze?{queryString}";
        public string GetOcrEndpoint(NameValueCollection queryString) => $"{_azureSettings.RootUrl}/vision/v2.0/ocr?{queryString}";
        public string GetBatchAnaylzeEndpoint(NameValueCollection queryString) => $"{_azureSettings.RootUrl}/vision/v2.0/read/core/asyncBatchAnalyze?{queryString}";

        public AzureService(IAzureSettings azureSettings, ISessionDetailRepository sessionDetailRepository, IUserSessionRepository userSessionRepository)
        {
            _azureSettings = azureSettings;
            _sessionDetailRepository = sessionDetailRepository;
            _userSessionRepository = userSessionRepository;
        }

        public async Task AddScan(string userSessionId, byte[] imageData)
        {
            //var taskResult = await BatchAnalyzeAsync(imageData);
            //taskResult.EnsureSuccessStatusCode();
            File.WriteAllBytes("screenshot.png", imageData);

            var ocrResult = new OcrResult();
            // Analyze an image to get features and other properties.
            using (var client = Authenticate(GetOcrEndpoint(null), _azureSettings.ApiKey))
            using (var memStream = new MemoryStream(imageData))
            {
                await client.RecognizePrintedTextInStreamAsync(true, memStream).ContinueWith(async (t) =>
                {
                    // After the request, get the operation location (operation ID)
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        ObjectId sessionId = ObjectId.Parse(userSessionId);
                        await _userSessionRepository.GetUserSessionByUserSessionId(sessionId).ContinueWith(async (r) =>
                        {
                            if(t.Status == TaskStatus.RanToCompletion)
                            {
                                ObjectId userId = r.Result.ModifiedById;
                                var sessionDetail = new SessionDetail()
                                {
                                    Id = ObjectId.GenerateNewId(),
                                    SessionId = sessionId,
                                    ModifiedById = userId,
                                    PrintedTextResult = t.Result,
                                };

                                await _sessionDetailRepository.UpsertSessionDetail(sessionDetail);
                            }
                        });
                    }
                    else
                    {
                        //log the error
                    }
                });
            }

            return;
        }

        //public async Task<double> MeasureConfidence(OcrResult currentOcrResult, SessionDetail[] previousOcrResults)
        //{
        //    //create a way to store confidences per page
        //    var confidences = new Dictionary<ObjectId, double>();
        //    foreach (var r in previousOcrResults)
        //    {
        //        confidences.Add(r.Id, 0);
        //    }

        //    //loop through each result from db
        //        //for each result, compare to current result
        //            //for page get avg confidence from regions
        //                //for regions get avg confidence from lines
        //                    //for lines get confidence (matched words/total words) from words
        //}

        public async Task BatchReadFileInStreamAsync(byte[] imageData)
        {
            //var taskResult = await BatchAnalyzeAsync(imageData);
            //taskResult.EnsureSuccessStatusCode();
            File.WriteAllBytes("screenshot.png", imageData);

            // Analyze an image to get features and other properties.
            using (var client = Authenticate(GetBatchAnaylzeEndpoint(null), _azureSettings.ApiKey))
            using (var memStream = new MemoryStream(imageData))
            {
                await client.BatchReadFileInStreamAsync(memStream).ContinueWith(async (t) =>
                {
                    // After the request, get the operation location (operation ID)
                    string operationLocation = t.Result.OperationLocation;

                    // Retrieve the URI where the recognized text will be stored from the Operation-Location header.
                    // We only need the ID and not the full URL
                    const int numberOfCharsInOperationId = 36;
                    string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

                    // Extract the text
                    // Delay is between iterations and tries a maximum of 10 times.
                    int i = 0;
                    int maxRetries = 10;
                    ReadOperationResult results;
                    //Console.WriteLine($"Extracting text from URL image {Path.GetFileName(urlImage)}...");
                    do
                    {
                        results = await client.GetReadOperationResultAsync(operationId);
                        //Console.WriteLine("Server status: {0}, waiting {1} seconds...", results.Status, i);
                        await Task.Delay(1000);
                        if (i == 9)
                        {
                            throw new System.Exception("Server Timed Out");
                            //Console.WriteLine("Server timed out.");
                        }
                    }
                    while ((results.Status == TextOperationStatusCodes.Running || results.Status == TextOperationStatusCodes.NotStarted) && i++ < maxRetries);

                    // Display the found text.
                    var textRecognitionLocalFileResults = results.RecognitionResults;
                    foreach (TextRecognitionResult recResult in textRecognitionLocalFileResults)
                    {
                        var pagenumber = recResult.Page;
                        foreach (Line line in recResult.Lines)
                        {
                            //Console.WriteLine(line.Text);

                        }
                    }
                });
            }

            return;
        }

        /*
         * AUTHENTICATE
         * Creates a Computer Vision client used by each example.
         */
        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }

        public HttpRequestHeaders AddSubscriptionHeader(HttpRequestHeaders headers)
        {
            headers.Add(_azureSettings.OcpApimSubscriptionKeyHeader, _azureSettings.ApiKey);
            return headers;
        }

        public HttpContentHeaders AddContentTypeOctetStream(HttpContentHeaders headers)
        {
            headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return headers;
        }

        public async Task<HttpResponseMessage> BatchAnalyzeAsync(byte[] imageData)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request body
            //byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            using(var client = new HttpClient())
            using (var content = new ByteArrayContent(imageData))
            {
                var uri = GetBatchAnaylzeEndpoint(queryString);
                AddSubscriptionHeader(client.DefaultRequestHeaders);
                AddContentTypeOctetStream(content.Headers);

                return await client.PostAsync(uri, content);
            }

        }

        
        ///// <summary>
        ///// Gets the text visible in the specified image file by using
        ///// the Computer Vision REST API.
        ///// </summary>
        ///// <param name="imageFilePath">The image file with printed text.</param>
        //static async Task MakeOCRRequest(string imageFilePath)
        //{
        //    try
        //    {
        //        HttpClient client = new HttpClient();

        //        // Request headers.
        //        client.DefaultRequestHeaders.Add(
        //            "Ocp-Apim-Subscription-Key", subscriptionKey);

        //        // Request parameters. 
        //        // The language parameter doesn't specify a language, so the 
        //        // method detects it automatically.
        //        // The detectOrientation parameter is set to true, so the method detects and
        //        // and corrects text orientation before detecting text.
        //        string requestParameters = "language=unk&detectOrientation=true";

        //        // Assemble the URI for the REST API method.
        //        string uri = uriBase + "?" + requestParameters;

        //        HttpResponseMessage response;

        //        // Read the contents of the specified local image
        //        // into a byte array.
        //        byte[] byteData = GetImageAsByteArray(imageFilePath);

        //        // Add the byte array as an octet stream to the request body.
        //        using (ByteArrayContent content = new ByteArrayContent(byteData))
        //        {
        //            // This example uses the "application/octet-stream" content type.
        //            // The other content types you can use are "application/json"
        //            // and "multipart/form-data".
        //            content.Headers.ContentType =
        //                new MediaTypeHeaderValue("application/octet-stream");

        //            // Asynchronously call the REST API method.
        //            response = await client.PostAsync(uri, content);
        //        }

        //        // Asynchronously get the JSON response.
        //        string contentString = await response.Content.ReadAsStringAsync();

        //        // Display the JSON response.
        //        Console.WriteLine("\nResponse:\n\n{0}\n",
        //            JToken.Parse(contentString).ToString());
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("\n" + e.Message);
        //    }
        //}
    }
}
