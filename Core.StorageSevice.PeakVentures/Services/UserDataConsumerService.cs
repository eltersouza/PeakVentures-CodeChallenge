using Confluent.Kafka;
using Core.StorageSevice.PeakVentures.Interface;
using Core.StorageSevice.PeakVentures.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StorageSevice.PeakVentures.Services
{
    public class UserDataConsumerService
    {
        public IUserDataStorageService _userDataStorageService { get; }
        private readonly KafkaConfiguration _kafkaConfiguration;

        public UserDataConsumerService(IUserDataStorageService userDataStorageService, IOptions<KafkaConfiguration> options)
        {
            _userDataStorageService = userDataStorageService;
            _kafkaConfiguration = options.Value;
        }

        public void StartConsumerLoop()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaConfiguration.BootstrapServers,
                GroupId = _kafkaConfiguration.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(_kafkaConfiguration.Topic);

                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume(cts.Token);
                        Console.WriteLine($"Consumed event from topic {_kafkaConfiguration.Topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");
                        
                        _userDataStorageService.StoreUserData(cr.Message.Value);
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {
                    consumer.Close();
                }
            }
        }
    }
}
