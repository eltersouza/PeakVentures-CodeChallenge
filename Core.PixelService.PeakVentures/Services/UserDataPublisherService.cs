using Confluent.Kafka;
using Core.PixelService.PeakVentures.Interfaces;
using Core.PixelService.PeakVentures.Models;
using Microsoft.AspNetCore.Http;

namespace Core.PixelService.PeakVentures.Services
{
    public class UserDataPublisherService: IUserDataPublisherService
    {
        public bool PublishUserData(UserData userData, KafkaConfiguration kafkaConfiguration)
        {
            bool result = false;

            if (userData == null || string.IsNullOrEmpty(userData.IpAddress))
                return result;

            var config = new ProducerConfig
            {
                BootstrapServers = kafkaConfiguration.BootstrapServers
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            
            var message = new Message<Null, string> { Value = userData.ToString() };
            
            producer.Produce(kafkaConfiguration.Topic, message, deliveryReport => {
                Console.WriteLine(deliveryReport.Message.Value);
                result = !deliveryReport.Error.IsError;
            });
            producer.Flush();

            return result;
        }
    }
}
