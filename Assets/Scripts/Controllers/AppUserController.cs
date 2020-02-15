using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Readr.Assets.Scripts.DataObjects;
using UnityEngine;

public class AppUserController : WebRequestController
{
    public AppUserController() : base("http://localhost:5000/AppUser")
    {
        
    }

    public async Task<AppUser> AddAppUser(string username)
    {
        var response = await Post($"/add/{username}"); 
        return await response.GetData<AppUser>();
    }

    public async Task<AppUser> LoginAppUser(string username)
    {
        return await ( await Post($"/login/{username}")).GetData<AppUser>();
    }
}
