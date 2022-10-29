namespace Image_Colour_Swap.Interfaces;

public interface IImageResultsSaver<T>
{
    Task<bool> SaveResults(T results);
}