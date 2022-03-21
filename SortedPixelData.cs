public class SortedPixelData
{
    public RgbPixelData[] PixelData => SortedPixels.Select(pixel => pixel.PixelData).ToArray<RgbPixelData>();
    public IndexedPixelData[] SortedPixels { get; set; }

    public SortedPixelData(RgbPixelData[] sortedPixels)
    {
        SortedPixels = CreateSortedPixels(sortedPixels);
    }

    public void SortByIndex()
    {
        var sortedArray = SortedPixels.OrderBy(pixel => pixel.Index).ToArray<IndexedPixelData>();
        SortedPixels = sortedArray;
    }

    public void Update(RgbPixelData[] incomingPixels)
    {
        for(int index = 0; index < SortedPixels.Length; index++)
        {
            SortedPixels[index].PixelData = incomingPixels[index];
        }
    }

    private IndexedPixelData[] CreateSortedPixels(RgbPixelData[] pixels)
    {
        if(pixels.Any() == false)
        {
            return new IndexedPixelData[0];
        }
        
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