using ImageHelpers.Models;

namespace ImageHelpers.Interfaces;

public interface IImageHandler
{
    RgbPixelData[] CreatePixelData(IImageData imageData);

    Stream GenerateStream(IImageData imageData);

    Stream GenerateStream(RgbPixelData[] pixels, IImageData imageData);

    Stream GenerateStream(string base64EncodedString);

    IImageData LoadImage(string filepath);

    IImageData LoadImageFromBase64EncodedString(string base64EncodedString);

    IImageData Resize(IImageData sourceImage, IImageData targetImage);
}