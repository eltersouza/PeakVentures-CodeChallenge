using Core.PixelService.PeakVentures.Interfaces;
using Core.PixelService.PeakVentures.Models;
using Core.PixelService.PeakVentures.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IImageGeneratorService, ImageGeneratorService>();
builder.Services.AddTransient<IUserDataPublisherService, UserDataPublisherService>();

var configuration = builder.Configuration;
builder.Services.Configure<KafkaConfiguration>(configuration.GetSection(nameof(KafkaConfiguration)));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/track", (HttpRequest request, IImageGeneratorService imageGeneratorService, IUserDataPublisherService userDataPublisherService, IOptions<KafkaConfiguration> options) =>
{
    var kafkaConfiguration = options.Value;
    var userData = UserData.FromRequest(request);
    var isPublished = userDataPublisherService.PublishUserData(userData, kafkaConfiguration);
    if (!isPublished)
        return Results.BadRequest();

    return Results.File(imageGeneratorService.GenerateGif(), contentType: "image/gif");
});

app.Run();