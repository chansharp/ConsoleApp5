using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConsoleApp5.Models;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; } = default!;

    public DbSet<Post> Posts { get; set; } = default!;

    private string DbPath { get; }

    public BloggingContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        string path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "blogging.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
            .UseSqlite($"Data Source={DbPath}")
            .LogTo(Console.WriteLine, LogLevel.Information);
            // .UseLazyLoadingProxies();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}

public class Blog
{
    private string _test = default!;

    public string Name { get; set; } = default!;
    
    public string Url { get; set; } = default!;

    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; private set; } = default!;

    public string Title { get; set; } = default!;

    public string Content { get; set; } = default!;

    public int BlogId { get; set; }

    public Blog Blog { get; set; } = default!;
}