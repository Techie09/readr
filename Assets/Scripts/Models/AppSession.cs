using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Readr.Assets.Scripts.Models;
using UnityEngine;

public class AppSession: MonoBehaviour
{
    public AppUser AppUser { get; private set; }

    public object UserSession { get; private set; }

    private static AppSession _current;

    public static AppSession Current
    {
       get { return _current ?? (_current = new GameObject(nameof(AppSession)).AddComponent<AppSession>()); }
    }

    public void Awake()
    {
        if (!_current)
        {
            _current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public AppUser SetCurrentAppUser(AppUser appUser)
    {
        Debug.Log($"setting current appUser {appUser.id}");
        AppUser = appUser;
        return appUser;
    }

    public async Task<UserSession> SetCurrentSessionAsync(UserSession session)
    {
        Debug.Log($"setting current userSession {session.Id}");
        await Task.Run(() => { UserSession = session; });
        return session;
    }

    public async Task ClearSessionAsync()
    {
        await Task.Run(() => { AppUser = default; UserSession = default; });
    }

    public void OnDestroy()
    {
        Debug.LogWarning("AppSession is Destroyed");
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
