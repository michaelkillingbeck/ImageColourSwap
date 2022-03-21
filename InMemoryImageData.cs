using Image_Colour_Swap.Interfaces;

public class InMemoryImageData : IImageData
{
    private byte[] _bytes;
    private string _filename = "";
    private int _height;
    private int _width;

    public byte[] Bytes => _bytes;
    public string Filename => _filename;
    public ImageSize Size => new ImageSize(_height, _width);

    public SortedPixelData SortedPixels { get; set; }

    public InMemoryImageData(string filename, int width, int height, byte[] bytes)
    {
        _bytes = bytes;
        _filename = filename;
        _height = height;
        _width = width;

        SortedPixels = new SortedPixelData(new RgbPixelData[0]);
    }
}