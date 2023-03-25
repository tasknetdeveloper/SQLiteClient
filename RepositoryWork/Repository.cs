using SQLite;
using Model;
using LoggerSpace;
namespace RepositoryWork
{   
    public class Repository
    {
        private const string DbDir = "Data";
        private Log log = new(true);
        private string connection = "";
        private bool isConnectionError = false;
        public Repository(string connection, Log log) {
            if (string.IsNullOrEmpty(connection))
            {
                isConnectionError = true;
                return;
            }

            this.log = log;
            this.connection = connection;
            Ini();
        }
   
        private void Ini() {

            if (isConnectionError) return;

            var appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DbDir);
            Directory.CreateDirectory(appData);
            this.connection = Path.Combine(appData, this.connection);                    
        }

        public void CreateTable<T>()
        {
            try
            {
                using (var cnx = new SQLiteConnection(this.connection))
                {
                    cnx.CreateTable<T>();
                }
            }
            catch (SQLiteException exp)
            {
                log.Error($"CreateTable/{exp.Message}");
            }
            catch (Exception exp)
            {
                log.Error($"CreateTable/{exp.Message}");
            }
        }

        public bool Insert<T>(T item)
        {
            var result = false;
            if (isConnectionError) return result;
            try
            {
                using (var cnx = new SQLiteConnection(this.connection))
                {
                    cnx.Insert(item);
                    result = true;
                }
            }
            catch (Exception exp)
            {
                this.log.Error($"Insert/{exp.Message}");
            }
            return result;
        }
        
        public User? Get(string sqlQuery)
        {
            User? result = null;
            if (isConnectionError) return result;
            try
            {
                using (var cnx = new SQLiteConnection(this.connection))
                {
                    List<User> list = cnx.Query<User>(sqlQuery);
                    if (list != null && list.FirstOrDefault() != null)
                        result = list.FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                this.log.Error($"Get/{exp.Message}");
            }
            return result;
        }

        public bool Update<T>(T? item)
        {
            var result = false;
            if (isConnectionError || item == null) return result;

            try
            {
                using (var cnx = new SQLiteConnection(this.connection))
                {
                    cnx.Update(item);
                    result = true;
                }
            }
            catch (Exception exp)
            {
                this.log.Error($"Update/{exp.Message}");
            }
            return result;
        }

    }
}
