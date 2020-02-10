using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppUserController : WebRequestController
{
    public AppUserController() : base("http://localhost:5000/AppUser")
    {
        
    }

    public AppUser AddAppUser(string username)
    {
        return Post($"/add/{username}").GetData<AppUser>();
    }

    public AppUser LoginAppUser(string username)
    {
        return Post($"/login/{username}").GetData<AppUser>();
    }
}
