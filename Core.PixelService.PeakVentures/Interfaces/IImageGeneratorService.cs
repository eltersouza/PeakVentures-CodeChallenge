using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PixelService.PeakVentures.Interfaces
{
    public interface IImageGeneratorService
    {
        MemoryStream GenerateGif();
    }
}
