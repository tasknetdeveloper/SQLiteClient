using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("User")]
    public class User
    {        
        public int id { get; set; } = 0;

        public string Name { get; set; } = "";

        public string PasswordSHA256 { get; set; } = "";

        public bool isAdmin { get; set; } = false;
    }
}