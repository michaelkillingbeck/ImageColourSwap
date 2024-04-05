namespace ImageHelpers.Models;

public class SortedPixelData
{
    public IndexedPixelData[] SortedPixels { get; set; }

    public SortedPixelData(RgbPixelData[] sortedPixels)
    {
        SortedPixels = CreateSortedPixels(sortedPixels);
    }

    public RgbPixelData[] GetPixelData()
    {
        return SortedPixels.Select(pixel => pixel.PixelData).ToArray();
    }

    public void SortByIndex()
    {
        IndexedPixelData[] sortedArray = SortedPixels.OrderBy(pixel => pixel.Index).ToArray();
        SortedPixels = sortedArray;
    }

    public void Update(RgbPixelData[] incomingPixels)
    {
        for (int index = 0; index < SortedPixels.Length; index++)
        {
            SortedPixels[index].PixelData = incomingPixels[index];
        }
    }

    private static IndexedPixelData[] CreateSortedPixels(RgbPixelData[] pixels)
    {
        if (!pixels.Any())
        {
            return Array.Empty<IndexedPixelData>();
        }

        IndexedPixelData[] tempArray = new IndexedPixelData[pixels.Length];

        for (int index = 0; index < pixels.Length; index++)
        {
            tempArray[index] = new IndexedPixelData(index, pixels[index]);
        }

        IndexedPixelData[] sortedArray = tempArray.OrderBy(indexedPixel => indexedPixel.PixelData.R)
            .ThenBy(indexedPixel => indexedPixel.PixelData.G)
            .ThenBy(indexedPixel => indexedPixel.PixelData.B)
            .ToArray();

        return sortedArray;
    }
}