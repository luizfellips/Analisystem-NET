using Analisystem.Models;
using Newtonsoft.Json;

namespace Analisystem.Helper
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserSession(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public UserModel GetUserSession()
        {
            string userSession = _httpContext.HttpContext.Session.GetString("LoggedSession");
            if(string.IsNullOrEmpty(userSession))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UserModel>(userSession);
        }
        public void CreateUserSession(UserModel user)
        {
            string value = JsonConvert.SerializeObject(user);
            _httpContext.HttpContext.Session.SetString("LoggedSession", value);
        }
        public void RemoveUserSession()
        {
            _httpContext.HttpContext.Session.Remove("LoggedSession");
        }
    }
}
