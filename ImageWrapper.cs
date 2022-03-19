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
        Console.Out.WriteLine($"Retrieving ImageSize Data for: {filepath}");

        try
        {
            using (var image = Image.Load<Rgba32>(filepath))
            {
                int height = image.Height;
                int width = image.Width;

                return new ImageSize(height, width);
            }
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Image for {filepath} was not loaded correctly.");
            Console.Error.WriteLine(ex.Message);
            return new ImageSize(0, 0);
        }
    }

    public string CreateOutputImage(string originalFilepath, SortedPixelData sourceData, SortedPixelData palletteData)
    {
        try
        {
            Console.Out.WriteLine($"Creating final image for: {originalFilepath}");

            Console.Out.WriteLine("Getting pallette pixels...");
            var pallettePixels = palletteData.PixelData;
            Console.Out.WriteLine("Getting ImageSize Data...");
            var imageSize = GetImageSize(originalFilepath);

            sourceData.Update(pallettePixels);
            sourceData.SortByIndex();
            Console.Out.WriteLine("Updated and Sorted the SourceData...");

            using(var outputImage = Image.LoadPixelData<Rgba32>(sourceData.PixelData, imageSize.Width, imageSize.Height))
            {
                string originalFilename = Path.GetFileName(originalFilepath);
                string filepath = Path.Combine(_outputLocation, $"output_{originalFilename}");
                outputImage.SaveAsJpeg(filepath);

                return filepath;
            }
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Error creating final image for: {originalFilepath}");
            Console.Error.WriteLine(ex.Message);
            return String.Empty;
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