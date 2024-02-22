using Core.PixelService.PeakVentures.Interfaces;
using ImageMagick;

namespace Core.PixelService.PeakVentures.Services
{
    public class ImageGeneratorService : IImageGeneratorService
    {
        public MemoryStream GenerateGif()
        {
            using (var image = new MagickImage(MagickColors.Red, 100, 100))
            {
                var stream = new MemoryStream();
                image.Format = MagickFormat.Gif;
                image.Write(stream);
                stream.Position = 0;

                return stream;
            }
        }
    }
}
