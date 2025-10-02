using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace KotApi
{
    [Table("users")]
    public class User : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("password")]
        public string Password
        {
            get; set;
        }
        [Column("city_id")]
        public int City_id { get; set; }
    }
}
