using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
 
 public class DataContext : DbContext
 {
   public DbSet<Blog> Blogs { get; set; }
   public DbSet<Post> Posts { get; set; }
 
   public void AddBlog(Blog blog)
   {
     this.Blogs.Add(blog);
     this.SaveChanges();
   }
 
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
     // *******************************************************************
     // * IT IS NOT A GOOD IDEA TO STORE USER ID AND/OR PASSWORD          *
     // * DIRECTLY IN A CLASS (ESPECIALLY IF IT WILL BE POSTED ON GITHUB) *
     // * WE WILL LEARN LATER HOW TO REMOVE THIS DATA FROM THE CLASS      *
     // *******************************************************************
     // ## last 2 digits of CRN
     // XXX your initials
     var configuration = new ConfigurationBuilder()
             .AddJsonFile($"appsettings.json");
 
     var config = configuration.Build();
     optionsBuilder.UseSqlServer(@config["Blogs:ConnectionString"]);
   }
 }