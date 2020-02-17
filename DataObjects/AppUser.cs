using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Readr.Models
{
    public class AppUser : IBsonModel
    {
        public ObjectId Id { get; set; }
        public string Username { get; set; }

        public static async Task<AppUser> CreateUserAsync(string username)
        {
            return await CreateUser(username).ConfigureAwait(false);
        }

        protected static Task<AppUser> CreateUser(string username)
        {
            return Task.FromResult(new AppUser
            {
                Id = ObjectId.GenerateNewId(),
                Username = username
            });
        }

    }

    public static partial class ExtensionMethods
    {
        public static async Task<AppUserDto> MapToDto(this AppUser appUser)
        {
            return await Task.FromResult(new AppUserDto()
            {
                Id = appUser.Id.ToString(),
                Username = appUser.Username
            });
        }
    }
}
