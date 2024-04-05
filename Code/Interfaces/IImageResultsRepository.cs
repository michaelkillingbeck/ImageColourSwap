namespace ImageHelpers.Interfaces;

public interface IImageResultsRepository<T>
{
    Task<T> LoadResults(string id);

    Task<bool> SaveResults(T results);
}