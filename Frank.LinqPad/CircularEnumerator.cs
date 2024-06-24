namespace LINQPadQuery;

/// <summary>
/// A circular enumerator that wraps around a collection allowing for infinite iteration over the collection in the same order as the original collection.
/// </summary>
/// <typeparam name="T"></typeparam>
public class CircularEnumerator<T>
{
    private readonly IList<T> _items;
    private int _currentIndex = -1;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    /// <summary>
    /// Initializes a new instance of the <see cref="CircularEnumerator{T}"/> class.
    /// </summary>
    /// <param name="items"></param>
    /// <exception cref="ArgumentException">Empty collection is not allowed</exception>
    public CircularEnumerator(IEnumerable<T> items)
    {
        _items = new List<T>(items);
        
        if (_items.Count == 0)
            throw new ArgumentException("The collection is empty.", nameof(items));
    }

    public static CircularEnumerator<T> Create(IEnumerable<T> items) => new CircularEnumerator<T>(items);

    /// <summary>
    /// Gets the next item in the collection.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public T GetNext()
    {
        _semaphore.Wait();
        try
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("The collection is empty.");

            _currentIndex = (_currentIndex + 1) % _items.Count;
            return _items[_currentIndex];
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Gets the next item in the collection asynchronously.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<T> GetNextAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("The collection is empty.");

            _currentIndex = (_currentIndex + 1) % _items.Count;
            return _items[_currentIndex];
        }
        finally
        {
            _semaphore.Release();
        }
    }
}