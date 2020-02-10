using System;
using System.IO;
using System.Net;
using UnityEngine;

public abstract class WebRequestController
{
    private string _rootPath = String.Empty;

    public WebRequestController(string rootPath = "")
    {
        _rootPath = !String.IsNullOrWhiteSpace(rootPath) ? rootPath : "http://localhost:5000/";
    }

    public WebResponse Post(string uri)
    {
        try
        {
            var requestUrl = $"{_rootPath}{uri}";
            Debug.Log(requestUrl);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            Debug.Log(request.GetResponse());
            return request.GetResponse();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

public static class ExtensionMethods
{
    public static T GetData<T>(this WebResponse response)
    {
        try
        {
            string jsonResponse = String.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                jsonResponse = reader.ReadToEnd();
            }

            return !String.IsNullOrWhiteSpace(jsonResponse) ? JsonUtility.FromJson<T>(jsonResponse) : default;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
