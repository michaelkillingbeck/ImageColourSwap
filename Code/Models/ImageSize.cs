namespace ImageHelpers.Models;

public class ImageSize
{
    public int Height { get; set; }

    public int Width { get; set; }

    public int Size => Height * Width;

    public ImageSize(int height, int width)
    {
        Height = height;
        Width = width;
    }
}