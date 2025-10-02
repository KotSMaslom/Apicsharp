using KotApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Supabase.Gotrue;

namespace KotApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Supabase.Client _supabaseClient;
        private readonly SupaBaseContext _supabaseContext;

        public WeatherForecastController(Supabase.Client supabaseClient, SupaBaseContext supaBaseContext)
        {
            _supabaseClient = supabaseClient;
            _supabaseContext = supaBaseContext;
        }
        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        public async Task<string> GetAllUsers(
            [FromQuery] int? city_id = null,
            [FromQuery] int? minAge = null,
            [FromQuery] int? maxAge = null,
            [FromQuery] string? name = null)
        {
            try
            {
                var result = await _supabaseContext.GetUsers(_supabaseClient);

                if (city_id.HasValue)
                    result = result.Where(u => u.City_id == city_id.Value).ToList();

                if (minAge.HasValue)
                    result = result.Where(u => u.Age >= minAge.Value).ToList();

                if (maxAge.HasValue)
                    result = result.Where(u => u.Age <= maxAge.Value).ToList();

                if (!string.IsNullOrEmpty(name))
                    result = result.Where(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.Indented);
            }

        }
        [HttpPost("AddUser", Name = "AddUser")]
        public async Task<string> InsertUser([FromBody] User createData)
        {

            if (createData == null)
            {
                return JsonConvert.SerializeObject(new { error = "User data is required" }, Formatting.Indented);
            }

            try
            {
                var result = await _supabaseContext.InsertUser(_supabaseClient, createData);

                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception)
            {
                return "";
            }
        }

        [HttpPost("AddUsersBatch", Name = "AddUsersBatch")]
        public async Task<string> InsertUsers([FromBody] List<User> createDataList)
        {
            if (createDataList == null || !createDataList.Any())
                return JsonConvert.SerializeObject(new { error = "User data is required" }, Formatting.Indented);

            var results = new List<User>();
            foreach (var user in createDataList)
            {
                var result = await _supabaseContext.InsertUser(_supabaseClient, user);
                results.Add(result);
            }

            return JsonConvert.SerializeObject(results, Formatting.Indented);
        }

        [HttpDelete("DeleteUser/{user_id}", Name ="DeleteUser")]
        public async Task<string> DeleteUser(int user_id)
        {
            var result = await _supabaseContext.DeleteUser(_supabaseClient, user_id);
            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }


        [HttpPatch("UpdateUser/{user_id}", Name = "UpdateUSer")]
        public async Task<string> UpdateUser(int user_id, [FromBody] User updateUser)
        {
            try
            {
                var result = await _supabaseContext.UpdateUser(_supabaseClient, user_id, updateUser);
                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message, Formatting.Indented);
            }
        }
    }
}
