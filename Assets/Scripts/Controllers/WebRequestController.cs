using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public abstract class WebRequestController
{
    private string _rootPath = String.Empty;

    public WebRequestController(string rootPath = "")
    {
        _rootPath = !String.IsNullOrWhiteSpace(rootPath) ? rootPath : "http://localhost:5000/";
    }

    public async Task<HttpResponseMessage> Post(string uri)
    {
        try
        {
            var requestUrl = $"{_rootPath}{uri}";
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(requestUrl, new StringContent(""));
            return response;

            // Debug.Log(requestUrl);
            // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            // Debug.Log(request.GetResponse());
            // return request.GetResponse();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

public static class ExtensionMethods
{
    public static async Task<T> GetData<T>(this HttpResponseMessage response)
    {
        try
        {
            string jsonResponse = await (response.Content.ReadAsStringAsync());
            Debug.Log($"from http response {response.Headers.Location} and produced result: {Environment.NewLine}{jsonResponse}");
            return !String.IsNullOrWhiteSpace(jsonResponse) ? JsonUtility.FromJson<T>(jsonResponse) : default;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
