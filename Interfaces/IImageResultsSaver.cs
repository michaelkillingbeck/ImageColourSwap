namespace Image_Colour_Swap.Interfaces;

public interface IImageResultsSaver<T>
{
    bool SaveResults(T results);
}