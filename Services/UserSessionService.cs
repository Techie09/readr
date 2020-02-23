using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Readr.Models;
using Readr.Models;
using Readr.Repositories.Interfaces;
using Readr.Services.Interfaces;

namespace Readr.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserSessionRepository _userSessionRepo;

        public UserSessionService(IUserSessionRepository userSessionRepo)
        {
            _userSessionRepo = userSessionRepo;
        }

        /// <summary>
        /// Creates new UserSession Asynchronously
        /// 
        /// Throws ArgumentException when Isbn and Description are Null or Whitespace
        /// Throws ArgumentException when Isbn is not or 13 digits long
        /// </summary>
        /// <param name="appUserId"></param>
        /// <param name="isbn"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<UserSessionDto> UpsertUserSessionAsync(UserSession userSession)
        {
            //validation error Messages
            var errorMessage_IsbnOrDescriptionRequired = "ISBN or desciption is required to continue";
            var errorMessage_IsbnInvalid = "Inavlid ISBN. ISBN must be 10 or 13 digits";

            //Define Validation 
            var isDescriptionNullOrWhitespace = String.IsNullOrWhiteSpace(userSession.Description);
            var isIsbnNullorwhiteSpace = String.IsNullOrWhiteSpace(userSession.Isbn);
            var isNumericRegex = new Regex(@"^\d+$");
            var isIsbn10or13 = new Func<string, bool>((i) => i.Length == 10 || i.Length == 13); 
            var isIsbnValid = false;

            //check if Isbn or Description exists
            if (isIsbnNullorwhiteSpace && isDescriptionNullOrWhitespace)
            {
                throw new ArgumentException(errorMessage_IsbnOrDescriptionRequired);
            }

            //If isbn is entered
            if (!isIsbnNullorwhiteSpace)
            {
                //try to normalize the data
                var isbn = userSession.Isbn.Replace("-", String.Empty).Trim();

                //verify that only numbers exist in ISBN and ISBN is either 10 or 13 digits long
                isIsbnValid = isNumericRegex.IsMatch(isbn) && isIsbn10or13(isbn);

                userSession.Isbn = isbn;
            }

            if(!isIsbnValid)
            {
                throw new ArgumentException(errorMessage_IsbnInvalid);
            }

            
            if(userSession != null)
            {
                await _userSessionRepo.UpsertUserSession(userSession);
            }

            return await userSession.MapToDto();
        }
    }
}
