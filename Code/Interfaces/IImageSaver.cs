namespace ImageHelpers.Interfaces;

public interface IImageSaver
{
    Task<bool> SaveAsync(string filename, Stream imageStream);
}