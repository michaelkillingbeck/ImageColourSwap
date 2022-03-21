namespace Image_Colour_Swap.Interfaces;

public interface IImageSaver
{
    Task<bool> SaveAsync(string filename, Stream imageStream);
}