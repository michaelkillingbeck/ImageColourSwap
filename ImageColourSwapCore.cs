public class ImageColourSwapCore
{
    public async Task<string> Run(string sourceImage, string palletteImage)
    {
        var imageHelper = new ImageHelper();

        imageHelper.LoadImages(sourceImage, palletteImage);
        imageHelper.Resize();

        var result = await imageHelper.SaveImagesAsync();

        await imageHelper.CreateSortedImages();
        return await imageHelper.CreateOutputImage();
    }
}