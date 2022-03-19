using SixLabors.ImageSharp.PixelFormats;

public class SortedPixelData
{
    public string Filename { get; set;}
    public Rgba32[] PixelData => SortedPixels.Select(pixel => pixel.PixelData).ToArray<Rgba32>();
    public IndexedPixelData[] SortedPixels { get; set; }

    public SortedPixelData(string filename, Rgba32[] sortedPixels)
    {
        Filename = filename;

        SortedPixels = CreateSortedPixels(sortedPixels);
    }

    public void SortByIndex()
    {
        var sortedArray = SortedPixels.OrderBy(pixel => pixel.Index).ToArray<IndexedPixelData>();
        SortedPixels = sortedArray;
    }

    public void Update(Rgba32[] incomingPixels)
    {
        for(int index = 0; index < SortedPixels.Length; index++)
        {
            SortedPixels[index].PixelData = incomingPixels[index];
        }
    }

    private IndexedPixelData[] CreateSortedPixels(Rgba32[] pixels)
    {
        var tempArray = new IndexedPixelData[pixels.Length];

        for(int index = 0; index < pixels.Length; index++)
        {
            tempArray[index] = new IndexedPixelData(index, pixels[index]);
        }

        var sortedArray = tempArray.OrderBy(indexedPixel => indexedPixel.PixelData.R  )
                                    .ThenBy(indexedPixel => indexedPixel.PixelData.G)
                                    .ThenBy(indexedPixel => indexedPixel.PixelData.B)
                                    .ToArray<IndexedPixelData>();

        return sortedArray;
    }
}