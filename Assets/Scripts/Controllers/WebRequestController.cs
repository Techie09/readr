using System;
using System.Net.Http;
using System.Threading.Tasks;
using Readr.Assets.Scripts.Models;
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
            Debug.Log($"Post to { _rootPath}{ uri}");

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
            Debug.LogError(ex);
            throw;
        }
    }
}

public static class ExtensionMethods
{
    public static async Task<AppUser> GetAppUserData(this HttpResponseMessage response)
    {
        try
        {
            Debug.Log($"Getting Data for {typeof(AppUser)} from {response.Content}");

            var json = await response.Content.ReadAsStringAsync();
            Debug.Log($"from http response {response.RequestMessage.RequestUri} and produced result: {Environment.NewLine}{json}");
            if(!String.IsNullOrWhiteSpace(json))
            {
                AppUser jsonResult = new AppUser();
                //T jsonResult = JsonUtility.FromJson<T>(json.Result);
                JsonUtility.FromJsonOverwrite(json.Replace(@"\",""), jsonResult);
                return jsonResult;
            }
            return default;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }

    public static async Task<UserSession> GetUserSessionData(this HttpResponseMessage response)
    {
        try
        {
            Debug.Log($"Getting Data for {typeof(UserSession)} from {response.Content}");

            var json = await response.Content.ReadAsStringAsync();
            Debug.Log($"from http response {response.RequestMessage.RequestUri} and produced result: {Environment.NewLine}{json}");
            if (!String.IsNullOrWhiteSpace(json))
            {
                UserSession jsonResult = new UserSession();
                //T jsonResult = JsonUtility.FromJson<T>(json.Result);
                JsonUtility.FromJsonOverwrite(json, jsonResult);
                return jsonResult;
            }
            return default;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }
}
