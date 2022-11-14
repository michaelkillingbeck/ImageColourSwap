using ImageHelpers.Models;

namespace ImageHelpers.Interfaces;

public interface IImageData
{
    byte[] Bytes { get; }
    string Filename { get; }
    ImageSize Size { get; }
    SortedPixelData SortedPixels { get; set; }
}