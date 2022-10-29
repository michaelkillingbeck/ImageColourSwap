namespace Image_Colour_Swap.Interfaces;

public interface IImageResultsRepository<T>
{
    Task<T> LoadResults(string id);
    Task<bool> SaveResults(T results);
}