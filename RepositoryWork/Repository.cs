using SQLite;
using Model;
using LoggerSpace;
namespace RepositoryWork
{   
    public class Repository
    {
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

            var appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sqlite_hello_world");
            Directory.CreateDirectory(appData);
            this.connection = Path.Combine(appData, this.connection);
        
            try
            {
                using (var cnx = new SQLiteConnection(this.connection))
                {
                    cnx.CreateTable<User>();
                }
            }
            catch (SQLiteException exp)
            {
                log.Error($"Ini/{exp.Message}");
            }
            catch (Exception exp)
            {
                log.Error($"Ini/{exp.Message}");
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

        //script = $"SELECT * FROM User WHERE Name = '{Name}'"
        public User? Get(string script)
        {
            User? result = null;
            if (isConnectionError) return result;
            try
            {
                using (var cnx = new SQLiteConnection(this.connection))
                {
                    List<User> list = cnx.Query<User>(script);
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
