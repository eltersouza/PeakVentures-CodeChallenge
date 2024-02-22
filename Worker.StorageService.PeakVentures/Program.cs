using Core.StorageSevice.PeakVentures.Interface;
using Core.StorageSevice.PeakVentures.Models;
using Core.StorageSevice.PeakVentures.Services;
using Worker.StorageService.PeakVentures;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.Configure<KafkaConfiguration>(configuration.GetSection(nameof(KafkaConfiguration)));
        services.AddTransient<UserDataConsumerService>();
        services.AddSingleton<IUserDataStorageService, UserDataStorageService>();

        services.AddHostedService<WorkerApp>();
    })
    .Build();

await host.RunAsync();
