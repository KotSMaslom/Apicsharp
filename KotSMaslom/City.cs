using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace KotApi
{
    [Table("city")]
    public class City : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
