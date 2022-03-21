namespace Image_Colour_Swap.Interfaces;

public interface IImageData
{
    byte[] Bytes { get; }
    string Filename { get; }
    ImageSize Size { get; }
    SortedPixelData SortedPixels { get; set; }
}