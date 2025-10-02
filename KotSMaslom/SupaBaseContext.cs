namespace KotApi
{
    public class SupaBaseContext
    {
        public SupaBaseContext()
        {
        }
        public async Task<List<User>> GetUsers(Supabase.Client _supabaseClient)
        {
            var result = await _supabaseClient.From<User>().Get();
            return result.Models;
        }

        public async Task<User> InsertUser(Supabase.Client _supabaseClient, User user)
        {
            var result = await _supabaseClient.From<User>().Insert(user);
            return result.Model;
        }
        public async Task<string> DeleteUser(Supabase.Client _supabaseClient, int user_id)
        {
            try
            {
                await _supabaseClient.From<User>().Where(x => x.Id == user_id).Delete();
                return "User Delete";
            }
            catch (Exception ex)
            {
                return $"User not delete with ex: {ex.Message}";

            }
        }
        public async Task<User> UpdateUser(Supabase.Client _supabaseClient, int user_id, User updateUser)
        {
            var user = await _supabaseClient.From<User>().Where(x => x.Id == user_id).Single();

            user.Name = updateUser.Name;
            user.Age = updateUser.Age;
            await user.Update<User>();
            return user;
        }
    }
}

