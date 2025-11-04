namespace ClientApp.Services;

/// <summary>
/// Client-side caching service to reduce redundant API calls
/// Caches product data for 5 minutes to improve performance
/// </summary>
public class ProductCache
{
    private Product[]? _cachedProducts;
    private DateTime? _cacheTime;
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Check if the cache is valid (not expired)
    /// </summary>
    public bool HasValidCache()
    {
        return _cachedProducts != null &&
               _cacheTime.HasValue &&
               DateTime.Now - _cacheTime.Value < _cacheExpiration;
    }

    /// <summary>
    /// Get cached products
    /// </summary>
    public Product[]? GetProducts()
    {
        if (HasValidCache())
        {
            return _cachedProducts;
        }
        return null;
    }

    /// <summary>
    /// Store products in cache
    /// </summary>
    public void SetProducts(Product[] products)
    {
        _cachedProducts = products;
        _cacheTime = DateTime.Now;
    }

    /// <summary>
    /// Clear the cache (useful for refresh operations)
    /// </summary>
    public void ClearCache()
    {
        _cachedProducts = null;
        _cacheTime = null;
    }
}

// Product models
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Stock { get; set; }
    public Category? Category { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
