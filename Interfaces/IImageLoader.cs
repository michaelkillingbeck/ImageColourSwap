namespace Image_Colour_Swap.Interfaces;

public interface IImageLoader
{
    RgbPixelData[] CreatePixelData(IImageData imageData);
    Stream GenerateStream(IImageData imageData);
    Stream GenerateStream(RgbPixelData[] pixels, IImageData imageData);
    Stream GenerateStream(string base64EncodedString);
    IImageData LoadImage(string filepath);
    IImageData Resize(IImageData sourceImage, IImageData targetImage);
}