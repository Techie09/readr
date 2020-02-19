using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Readr.Assets.Scripts.Models;
using UnityEngine;

public class AppUserController : WebRequestController
{
    public AppUserController() : base("http://localhost:5000/AppUser")
    {
        
    }

    public async Task<AppUser> AddAppUserAsync(string username)
    {
        return await await Post($"/add/{username}").ContinueWith((response) =>
            response.Result.GetData<AppUser>().ContinueWith((jsonResponse) =>
                AppSession.Current.SetCurrentAppUser(jsonResponse.Result)
            )
        );
    }

    public async Task<AppUser> LoginAppUserAsync(string username)
    {
        var postResult = await Post($"/login/{username}");
        var dataResult = await postResult.GetData<AppUser>();
        AppSession.Current.SetCurrentAppUser(dataResult);
        return dataResult;
        //return await ( await Post($"/login/{username}")).GetData<AppUser>();
    }
}
