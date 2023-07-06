namespace ForumSystem.Web.Helpers.Contracts
{
    public interface IAuthorizator
    {
        /// <summary>
        /// Check if there is logged user.>
        /// </summary>
        bool isLogged(string key);

        /// <summary>
        /// Check if session logged user is admin.>
        /// </summary>
        bool isAdmin(string key);

        /// <summary>
        /// Check if logged user ID matches the content creator ID>
        /// <param key="userId">Key paraim is userId from session.</param>
        /// </summary>
        public bool isContentCreator(string key, int contentCreatorId);

    }
}
