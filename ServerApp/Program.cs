var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors();

// Add response caching for performance optimization
builder.Services.AddResponseCaching();

// Register ProductService as a singleton for efficient data management
builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

// Enable response caching middleware
app.UseResponseCaching();

// Enable CORS - Allow any origin for development
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

// Optimized product list endpoint with caching
app.MapGet("/api/productlist", (ProductService productService, HttpContext context) =>
{
    // Set cache headers for browser and intermediate caching (5 minutes)
    context.Response.Headers["Cache-Control"] = "public, max-age=300";

    return productService.GetProducts();
})
.WithName("GetProducts")
.WithOpenApi();

app.Run();

// Product Service class for centralized data management
public class ProductService
{
    private readonly List<Product> _products;

    public ProductService()
    {
        // Initialize products once (simulating database cache)
        _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 1200.50,
                Stock = 25,
                Category = new Category { Id = 101, Name = "Electronics" }
            },
            new Product
            {
                Id = 2,
                Name = "Headphones",
                Price = 50.00,
                Stock = 100,
                Category = new Category { Id = 102, Name = "Accessories" }
            }
        };
    }

    public IEnumerable<Product> GetProducts() => _products;
}

// Strongly-typed models for better performance and maintainability
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Stock { get; set; }
    public Category Category { get; set; } = null!;
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

