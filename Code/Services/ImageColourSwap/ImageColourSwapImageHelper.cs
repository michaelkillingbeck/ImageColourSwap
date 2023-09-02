using ImageHelpers.Interfaces;
using ImageHelpers.Models;

namespace ImageHelpers.Services.ImageColourSwap;

public class ImageColourSwapImageHelper
{
    readonly IImageHandler _imageHandler;
    readonly IImageSaver _imageSaver;
    private IImageData _palletteImage;
    private IImageData _sourceImage;

    public ImageColourSwapImageHelper(
        IImageHandler imageHandler,
        IImageSaver imageSaver)
    {
        _imageHandler = imageHandler;
        _imageSaver = imageSaver;

        _palletteImage = new InMemoryImageData("", 0, 0, new byte[1]);
        _sourceImage = new InMemoryImageData("", 0, 0, new byte[1]);
    }

    public async Task<string> CreateOutputImage()
    {
        try
        {
            _sourceImage.SortedPixels.Update(_palletteImage.SortedPixels.PixelData);
            _sourceImage.SortedPixels.SortByIndex();

            MemoryStream stream = (MemoryStream)_imageHandler.GenerateStream(_sourceImage.SortedPixels.PixelData, _sourceImage);
            string outputFileName = $"output_{_sourceImage.Filename}";
            await _imageSaver.SaveAsync(outputFileName, stream);

            return outputFileName;
        }
        catch(Exception)
        {
            return string.Empty;
        }
    }

    public async Task CreateSortedImages()
    {
        RgbPixelData[] pixelData = _imageHandler.CreatePixelData(_sourceImage);
        _sourceImage.SortedPixels = new SortedPixelData(pixelData);

        using(MemoryStream stream = (MemoryStream)_imageHandler.GenerateStream(_sourceImage.SortedPixels.PixelData, _sourceImage))
        {
            await _imageSaver.SaveAsync($"sorted_{_sourceImage.Filename}", stream);
        }

        pixelData = _imageHandler.CreatePixelData(_palletteImage);
        _palletteImage.SortedPixels = new SortedPixelData(pixelData);

        using(MemoryStream stream = (MemoryStream)_imageHandler.GenerateStream(_palletteImage.SortedPixels.PixelData, _palletteImage))
        {
            await _imageSaver.SaveAsync($"sorted_{_palletteImage.Filename}", stream);
        }
    }    

    public Tuple<string, string> GetSourceAndPalletteImageFilenames()
    {
        return new (_sourceImage.Filename, _palletteImage.Filename);
    }

    public void LoadImages(string sourceImage, string palletteImage)
    {
        _sourceImage = _imageHandler.LoadImage(sourceImage);
        _palletteImage = _imageHandler.LoadImage(palletteImage);
    }

    public void Resize()
    {
        if(_sourceImage.Size.Size < _palletteImage.Size.Size)
        {
            _sourceImage = _imageHandler.Resize(_sourceImage, _palletteImage);
        }
        else
        {
            _palletteImage = _imageHandler.Resize(_palletteImage, _sourceImage);
        }
    }

    public async Task<bool> SaveImagesAsync()
    {
        MemoryStream stream = (MemoryStream)_imageHandler.GenerateStream(_sourceImage);
        await _imageSaver.SaveAsync(_sourceImage.Filename, stream);

        stream = (MemoryStream)_imageHandler.GenerateStream(_palletteImage);
        await _imageSaver.SaveAsync(_palletteImage.Filename, stream);

        return true;
    }
}