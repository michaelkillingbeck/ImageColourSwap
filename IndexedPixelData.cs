using SixLabors.ImageSharp.PixelFormats;

public class IndexedPixelData
{
    public int Index { get; set; }
    public Rgba32 PixelData { get; set; }

    public IndexedPixelData(int index, Rgba32 pixelData)
    {
        Index = index;
        PixelData = pixelData;
    }
}