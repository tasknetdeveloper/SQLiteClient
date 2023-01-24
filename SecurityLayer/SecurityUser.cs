using RepositoryWork;
using LoggerSpace;
namespace SecurityLayer
{
    public class SecurityUser
    {
        public SecurityUser(string connectionDB, Log log)
        {
            var rep = new Repository(connectionDB, log);            
        }       
    }
}