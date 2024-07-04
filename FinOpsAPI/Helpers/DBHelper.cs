using FinOpsAPI.Models;

namespace FinOpsAPI.Helpers
{
    public class DBHelper
    {
        public static string GetConnectionString(Auth authModel)
        {
            return Program.ConnectionString + $"User Id={authModel.Login};" + $"Password={authModel.Password};";
        }
    }
}
