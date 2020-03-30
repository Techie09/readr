using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Readr.Assets.Scripts.Models;
using UnityEngine;

public class AppUserController : WebRequestController
{
    public AppUserController() : base("api/AppUsers")
    {
        
    }

    public async Task AddAppUserAsync(string username)
    {
        //localhost:5000/AppUser/add/test
        await Put($"add/{username}");
    }

    public async Task<AppUser> LoginAppUserAsync(string username)
    {
        var postResult = await Post($"login/{username}");
        var dataResult = await postResult.GetData<AppUser>();
        AppSession.Current.SetCurrentAppUser(dataResult);
        return dataResult;
        //return await ( await Post($"/login/{username}")).GetData<AppUser>();
    }
}
