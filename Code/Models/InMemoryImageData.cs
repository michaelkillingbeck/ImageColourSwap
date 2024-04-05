using ImageHelpers.Interfaces;

namespace ImageHelpers.Models;

public class InMemoryImageData : IImageData
{
    private readonly int _height;
    private readonly int _width;

    public byte[] Bytes { get; }

    public string Filename { get; }

    public ImageSize Size => new(_height, _width);

    public SortedPixelData SortedPixels { get; set; }

    public InMemoryImageData(string filename, int width, int height, byte[] bytes)
    {
        Bytes = bytes;
        Filename = filename;
        _height = height;
        _width = width;

        SortedPixels = new SortedPixelData(Array.Empty<RgbPixelData>());
    }
}