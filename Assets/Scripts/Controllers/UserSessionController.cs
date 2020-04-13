using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Readr.Assets.Scripts.Models;
using UnityEngine;

namespace Readr.Assets.Scripts.Controllers
{
    public class UserSessionController : WebRequestController
    {
        public UserSessionController() : base("api/Sessions")
        {

        }

        public async Task<SearchResultsDto> SearchText(string searchWord)
        {
            try
            {
                var requestUrl = $"id/{AppSession.Current.UserSession.Id}/search/{searchWord}/";
                return await PostAsync<SearchResultsDto>(requestUrl, null);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                throw;
            }
        }

        public async Task<UserSession> CreateSessionAsync(UserSession newUserSession)
        {
            var id = newUserSession.Id != null ? newUserSession.Id : String.Empty;
            newUserSession.Id = id;
            //var response = await Post($"upsert/{id}", JsonConvert.SerializeObject(newUserSession));
            //return await response.GetData<UserSession>();
            return await PostAsync<UserSession>($"upsert/{id}", newUserSession);
        }
    }
}
