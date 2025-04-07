using NLog;
using System.Linq;


 string path = Directory.GetCurrentDirectory() + "//nlog.config";
 
 // create instance of Logger
 var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();
 logger.Info("Program started");
 
 //Menu option

bool exit = false;
while (!exit){
  Console.WriteLine("\nChoose an option:");
    Console.WriteLine("1 - Display all blogs");
    Console.WriteLine("2 - Add Blog");
    Console.WriteLine("3 - Create Post");
    Console.WriteLine("4 - Display Posts");
    Console.WriteLine("5 - Exit");
    var input = Console.ReadLine();
}

 
 // Create and save a new Blog
 Console.Write("Enter a name for a new Blog: ");
 var name = Console.ReadLine();
 
 var blog = new Blog { Name = name };
 
 var db = new DataContext();
 db.AddBlog(blog);
 logger.Info("Blog added - {name}", name);
 
 // Display all Blogs from the database
 var query = db.Blogs.OrderBy(b => b.Name);
 
 Console.WriteLine("All blogs in the database:");
 foreach (var item in query)
 {
   Console.WriteLine(item.Name);
 }
 
 logger.Info("Program ended");