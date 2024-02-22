using Core.PixelService.PeakVentures.Models;
using Microsoft.AspNetCore.Http;

namespace Core.PixelService.PeakVentures.Interfaces
{
    public interface IUserDataPublisherService
    {
        bool PublishUserData(UserData userData, KafkaConfiguration kafkaConfiguration);
    }
}
