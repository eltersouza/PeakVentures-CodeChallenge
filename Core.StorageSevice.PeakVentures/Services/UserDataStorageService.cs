using Core.StorageSevice.PeakVentures.Interface;
using Microsoft.Extensions.Configuration;

namespace Core.StorageSevice.PeakVentures.Services
{
    public class UserDataStorageService : IUserDataStorageService
    {
        private readonly string loggingPath;
        public UserDataStorageService(IConfiguration configuration)
        {
            var section = configuration.GetSection("Logging:FilePath");
            loggingPath = !string.IsNullOrEmpty(section.Value) ? section.Value : "/tmp/visits.log";
        }

        public bool StoreUserData(string userData)
        {
            if (string.IsNullOrEmpty(userData))
                return false;

            try
            {
                using (var writer = new StreamWriter(loggingPath, true))
                {
                    writer.WriteLine(userData);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}