using ForumSystem.Web.Helpers.Contracts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ForumSystem.Web.Helpers
{
    public  class Authorizator : IAuthorizator
    {
        public const string notAthorized = "You are not Authorized to do this!";

        private readonly IHttpContextAccessor contextAccessor;

        public Authorizator(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        /// <summary>
        /// Check if there is logged user.>
        /// </summary>
        public bool isLogged(string key)
        {
            if (!this.contextAccessor.HttpContext.Session.Keys.Contains(key))
            {
                return false;
            }
            return true;
        }
       
        public bool isAdmin(string key)
        {
            if (this.contextAccessor.HttpContext.Session.GetInt32(key) != 3)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Check if logged user ID matches the content creator ID>
        /// <param key="userId">Key paraim is userId from session.</param>
        /// </summary>
        public bool isContentCreator(string key, int contentCreatorId)
        {
            if (this.contextAccessor.HttpContext.Session.GetInt32(key) != contentCreatorId)
            {
                return false;
            }
            return true;
        }

    }
}
