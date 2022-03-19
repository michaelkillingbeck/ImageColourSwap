using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public class ImageHelper
{
    private const string _outputLocation = "CreatedImages/";
    public ImageHelper()
    {
    }

    public SortedPixelData CreateSortedImage(string filepath)
    {
        string filename = $"sorted_{Path.GetFileName(filepath)}";

        using (var image = Image.Load<Rgba32>(filepath))
        {
            var pixelData = CreatePixelData(image);
            var height = image.Height;
            var width = image.Width;

            var sortedPixels = new SortedPixelData(filename, pixelData);

            using(var outputImage = Image.LoadPixelData<Rgba32>(sortedPixels.PixelData, width, height))
            {
                filepath = Path.Combine(_outputLocation, filename);
                outputImage.SaveAsJpeg(filepath);
            }

            return sortedPixels;
        }
    }

    public string ImportImage(string filepath)
    {
        Guid fileId = Guid.NewGuid();

        using (var image = Image.Load<Rgba32>(filepath))
        {
            int height = image.Height;
            int width = image.Width;

            Console.WriteLine($"{filepath}: The height is {height} pixels.");
            Console.WriteLine($"{filepath}: The width is {width} pixels.");

            string filename = Path.Combine(_outputLocation, (fileId.ToString() + ".jpg"));

            image.SaveAsJpeg(filename);

            return filename;
        }
    }    

    private Rgba32[] CreatePixelData(Image<Rgba32> image)
    {
        Rgba32[] pixels = new Rgba32[0];
        pixels = new Rgba32[image.Width * image.Height];
            image.CopyPixelDataTo(pixels);

        return pixels;
    }

    public ImageSize GetImageSize(string filepath)
    {
        using (var image = Image.Load<Rgba32>(filepath))
        {
            int height = image.Height;
            int width = image.Width;

            return new ImageSize(height, width);
        }
    }

    public string CreateOutputImage(string originalFilepath, SortedPixelData sourceData, SortedPixelData palletteData)
    {
        var pallettePixels = palletteData.PixelData;
        var imageSize = GetImageSize(originalFilepath);

        sourceData.Update(pallettePixels);
        sourceData.SortByIndex();

        using(var outputImage = Image.LoadPixelData<Rgba32>(sourceData.PixelData, imageSize.Width, imageSize.Height))
        {
            string originalFilename = Path.GetFileName(originalFilepath);
            string filepath = Path.Combine(_outputLocation, $"output_{originalFilename}");
            outputImage.SaveAsJpeg(filepath);

            return filepath;
        }
    }

    public void Resize(ImageSize imageSize, string filepath)
    {
        int height = imageSize.Height;
        int width = imageSize.Width;

        using (var image = Image.Load(filepath))
        {
            image.Mutate(img => img.Resize(imageSize.Width, imageSize.Height));

            image.SaveAsJpeg(filepath);
        }
    }
}