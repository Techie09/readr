using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Readr.Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.XR;

public class AppSession
{
    public AppUser AppUser { get; private set; }

    public UserSession UserSession { get; private set; }

    public SessionState sessionState { get; set; }

    public string ApiServerPath { get; private set; }

    private static AppSession _current;

    //public AppSession()
    //{
    //    ApiServerPath = "http://localhost:5000";
    //}

    public struct Capabilities
    {
        public static string deviceName_WindowsMR = "WindowsMR";
        public static string deviceName_Hololens = "Hololens";

        public static bool isWindowsMR => XRSettings.loadedDeviceName.ToLower() == deviceName_WindowsMR.ToLower();
        public static bool isHololens => XRSettings.loadedDeviceName.ToLower() == deviceName_Hololens.ToLower();
    }

    public static AppSession Current
    {
        get {return _current ?? (_current = new AppSession());}
    }

    public AppSession()
    {
        sessionState = SessionState.Initial;
    }

    public AppUser SetCurrentAppUser(AppUser appUser)
    {
        Debug.Log($"setting current appUser {appUser.id}");
        AppUser = appUser;
        return appUser;
    }

    public UserSession SetCurrentSession(UserSession session)
    {
        Debug.Log($"setting current userSession {session.Id}");
        UserSession = session;
        return session;
    }

    public string SetCurrentApiServerPath(string path)
    {
        Debug.Log($"setting current ApiServer path {path}");
        ApiServerPath = path;
        return path;
    }

    public async Task ClearSessionAsync()
    {
        await Task.Run(() => { AppUser = default; UserSession = default; });
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
}

public enum SessionState
{
    Initial,
    Running,
    Paused
}
