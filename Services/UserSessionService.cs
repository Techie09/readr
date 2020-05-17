using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using MongoDB.Bson;
using Readr.Models;
using Readr.Repositories.Interfaces;
using Readr.Services.Interfaces;

namespace Readr.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserSessionRepository _userSessionRepo;
        private readonly ISessionDetailRepository _sessionDetailRepo;

        public UserSessionService(IUserSessionRepository userSessionRepo, ISessionDetailRepository sessionDetailRepo)
        {
            _userSessionRepo = userSessionRepo;
            _sessionDetailRepo = sessionDetailRepo;
        }

        /// <summary>
        /// Creates new UserSession Asynchronously
        /// 
        /// Throws ArgumentException when Isbn and Description are Null or Whitespace
        /// Throws ArgumentException when Isbn is not or 13 digits long
        /// </summary>
        /// <param name="appUserId"></param>
        /// <param name="isbn"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<UserSessionDto> UpsertUserSessionAsync(UserSession userSession)
        {
            //validation error Messages
            var errorMessage_IsbnOrDescriptionRequired = "ISBN or description is required to continue";
            var errorMessage_IsbnInvalid = "Inavlid ISBN. ISBN must be 10 or 13 digits";

            //Define Validation 
            var isDescriptionNullOrWhitespace = String.IsNullOrWhiteSpace(userSession.Description);
            var isIsbnNullorwhiteSpace = String.IsNullOrWhiteSpace(userSession.Isbn);
            var isNumericRegex = new Regex(@"^\d+$");
            var isIsbn10or13 = new Func<string, bool>((i) => i.Length == 10 || i.Length == 13); 
            var isIsbnValid = false;

            //check if Isbn or Description exists
            if (isIsbnNullorwhiteSpace && isDescriptionNullOrWhitespace)
            {
                throw new ArgumentException(errorMessage_IsbnOrDescriptionRequired);
            }

            //If isbn is entered
            if (!isIsbnNullorwhiteSpace)
            {
                //try to normalize the data
                var isbn = userSession.Isbn.Replace("-", String.Empty).Trim();

                //verify that only numbers exist in ISBN and ISBN is either 10 or 13 digits long
                isIsbnValid = isNumericRegex.IsMatch(isbn) && isIsbn10or13(isbn);

                userSession.Isbn = isbn;
            }

            if(!isIsbnValid)
            {
                throw new ArgumentException(errorMessage_IsbnInvalid);
            }

            
            if(userSession != null)
            {
                await _userSessionRepo.UpsertUserSession(userSession);
            }

            return await userSession.MapToDto();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userSessionId"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        /// <remarks>
        /// I should update parameter for GetSessionDetailByUserSessionId to be a string and decouple dependency from MongoDb ObjectId Model
        /// I should also create my own "OcrResult" to decouple dependency from Microsoft Azure Models
        /// </remarks>
        public async Task<SearchResultsDto> SearchUserSession(string userSessionId, string searchText)
        {
            var result = new SearchResultsDto();
            var sessionDetailsFound = await _sessionDetailRepo.GetSessionDetailByUserSessionId(ObjectId.Parse(userSessionId)).ConfigureAwait(false);
            //var rawText = String.Empty;
            try
            {
                var detail = sessionDetailsFound.PrintedTextResult;
                var regions = detail.Regions;
                foreach(OcrRegion region in regions)
                {
                    var lines = region.Lines;
                    foreach(OcrLine line in lines)
                    {
                        var words = line.Words;
                        foreach(OcrWord word in words)
                        {
                            if (word.Text.ToLower().Contains(searchText.ToLower())) //Searh for match
                            {
                                //find how many regions to exclude
                                var regionFoundIndex = regions.IndexOf(region);
                                var skipLastCount = regions.Count - regionFoundIndex;
                                if (skipLastCount < 0)
                                {
                                    skipLastCount = 0;
                                }
                                //find haw many lines of text there are to find the word
                                var reg = regions.SkipLast(skipLastCount);
                                var linesInPriorRegions = 0;
                                if (reg.Any())
                                {
                                    linesInPriorRegions = reg.Sum(x => x.Lines.Count());
                                }
                                var lineNumber = linesInPriorRegions + region.Lines.IndexOf(line) + 1;

                                //find how many words to exclude
                                var wordsFoundIndex = words.IndexOf(word);
                                var skipLastWordCount = words.Count - wordsFoundIndex;
                                if (skipLastWordCount < 0)
                                {
                                    skipLastWordCount = 0;
                                }
                                //find how many characters to find the word in the line
                                var filteredWords = words.SkipLast(skipLastWordCount);
                                var charInPriorWords = 0;
                                if(filteredWords.Any())
                                {
                                    charInPriorWords = filteredWords.Sum(w => w.Text.Length);
                                }
                                var intPosition = charInPriorWords + filteredWords.Count(); //count number of words and spaces. will not include special characters. 

                                //Map from Ocr classes to custom classes (to reduce dependency on Ocr classes
                                var resultText = new SearchResultText()
                                {
                                    BoundingBox = word.BoundingBox,
                                    Text = word.Text
                                };

                                var resultLine = new SearchResultLine()
                                {
                                    BoundingBox = line.BoundingBox,
                                    Text = words.Select(w => new SearchResultText() { BoundingBox = w.BoundingBox, Text = w.Text }).ToList()
                                };

                                var resultRegion = new SearchResultRegion()
                                {
                                    BoundingBox = region.BoundingBox,
                                    Lines = new List<SearchResultLine>() { resultLine }
                                };

                                var resultDetail = new SearchResultDetail()
                                {
                                    Language = detail.Language,
                                    TextAngle = detail.TextAngle,
                                    Orientation = detail.Orientation,
                                    Regions = new List<SearchResultRegion>() { resultRegion }
                                };

                                //build result details
                                var resultDetails = new SearchResultDetails(resultText, resultLine, resultRegion, resultDetail, lineNumber, intPosition);

                                //add to results
                                result.ResultDetails.Add(resultDetails);
                            }
                        }
                    }
                }
            }
            finally
            {
                if (sessionDetailsFound != null)
                    sessionDetailsFound = null;
            }

            return result;
        }
    }
}
