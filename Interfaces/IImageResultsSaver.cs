namespace Image_Colour_Swap.Interfaces;

public interface IImageSaver<T>
{
    bool SaveResults(T results);
}