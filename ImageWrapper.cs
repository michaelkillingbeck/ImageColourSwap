using Image_Colour_Swap.Interfaces;
using SixLabors.ImageSharp;

public class ImageHelper
{
    private IImageLoader _imageLoader;
    private IImageSaver _imageSaver;
    private IImageData _palletteImage;
    private IImageData _sourceImage;

    public ImageHelper()
    {
        _imageLoader = new ImageSharpImageLoader();
        _imageSaver = new AWSS3ImageSaver();

        _palletteImage = new InMemoryImageData("", 0, 0, new byte[1]);
        _sourceImage = new InMemoryImageData("", 0, 0, new byte[1]);
    }

    public async Task CreateOutputImage()
    {
        try
        {
            Console.Out.WriteLine($"Creating final image for: {_sourceImage.Filename}");
            _sourceImage.SortedPixels.Update(_palletteImage.SortedPixels.PixelData);
            _sourceImage.SortedPixels.SortByIndex();
            Console.Out.WriteLine("Updated and Sorted the SourceData...");

            MemoryStream stream = (MemoryStream)_imageLoader.GenerateStream(_sourceImage.SortedPixels.PixelData, _sourceImage);
            await _imageSaver.SaveAsync($"output_{_sourceImage.Filename}", stream);
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Error creating final image for: {_sourceImage.Filename}");
            Console.Error.WriteLine(ex.Message);
        }
    }

    public async Task CreateSortedImages()
    {
        var pixelData = _imageLoader.CreatePixelData(_sourceImage);
        _sourceImage.SortedPixels = new SortedPixelData(pixelData);
        MemoryStream stream = (MemoryStream)_imageLoader.GenerateStream(_sourceImage.SortedPixels.PixelData, _sourceImage);
        await _imageSaver.SaveAsync($"sorted_{_sourceImage.Filename}", stream);

        pixelData = _imageLoader.CreatePixelData(_palletteImage);
        _palletteImage.SortedPixels = new SortedPixelData(pixelData);
        stream = (MemoryStream)_imageLoader.GenerateStream(_palletteImage.SortedPixels.PixelData, _palletteImage);
        await _imageSaver.SaveAsync($"sorted_{_palletteImage.Filename}", stream);
    }    

    public void LoadImages(string sourceImage, string palletteImage)
    {
        _sourceImage = _imageLoader.LoadImage(sourceImage);
        _palletteImage = _imageLoader.LoadImage(palletteImage);
    }

    public void Resize()
    {
        if(_sourceImage.Size.Size < _palletteImage.Size.Size)
        {
            _sourceImage = _imageLoader.Resize(_sourceImage, _palletteImage);
        }
        else
        {
            _palletteImage = _imageLoader.Resize(_palletteImage, _sourceImage);
        }
    }

    public async Task<bool> SaveImagesAsync()
    {
        MemoryStream stream = (MemoryStream)_imageLoader.GenerateStream(_sourceImage);
        await _imageSaver.SaveAsync(_sourceImage.Filename, stream);

        stream = (MemoryStream)_imageLoader.GenerateStream(_palletteImage);
        await _imageSaver.SaveAsync(_palletteImage.Filename, stream);

        return true;
    }
}