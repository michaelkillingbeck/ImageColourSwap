namespace ImageHelpers.Models;

public class IndexedPixelData
{
    public int Index { get; set; }

    public RgbPixelData PixelData { get; set; }

    public IndexedPixelData(int index, RgbPixelData pixelData)
    {
        Index = index;
        PixelData = pixelData;
    }
}