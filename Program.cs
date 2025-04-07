using NLog;
using System.Linq;

 string path = Directory.GetCurrentDirectory() + "//nlog.config";
 
 // create instance of Logger
 var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();
 logger.Info("Program started");
  var db = new DataContext();
 //Menu option

bool exit = false;
while (!exit)
{
  Console.WriteLine("\nChoose an option:");
    Console.WriteLine("1 - Display all blogs");
    Console.WriteLine("2 - Add Blog");
    Console.WriteLine("3 - Create Post");
    Console.WriteLine("4 - Display Posts");
    Console.WriteLine("5 - Exit");
    var input = Console.ReadLine();
     switch (input)
    {
        case "1":
            var blogs = db.Blogs.OrderBy(b => b.Name).ToList();
            if (blogs.Count == 0)
            {
                Console.WriteLine("No blogs found.");
                break;
            }

            Console.WriteLine("All blogs:");
            foreach (var blog in blogs)
            {
                Console.WriteLine($"ID: {blog.BlogId}, Name: {blog.Name}");
            }
            break;
             case "2":
            Console.Write("Enter a name for a new Blog: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Blog name cannot be empty.");
                break;
            }

            var newBlog = new Blog { Name = name };
            db.AddBlog(newBlog);
            logger.Info("Blog added - {name}", name);
            break;
            case "3":
            var blogList = db.Blogs.OrderBy(b => b.BlogId).ToList();
            if (blogList.Count == 0)
            {
                Console.WriteLine("No blogs available. Please add one first.");
                break;
            }

            Console.WriteLine("Choose a Blog by ID:");
            foreach (var b in blogList)
            {
                Console.WriteLine($"ID: {b.BlogId}, Name: {b.Name}");
            }

            if (!int.TryParse(Console.ReadLine(), out int blogId))
            {
                Console.WriteLine("Invalid Blog ID.");
                break;
            }

            var selectedBlog = db.Blogs.FirstOrDefault(b => b.BlogId == blogId);
            if (selectedBlog == null)
            {
                Console.WriteLine("Blog not found.");
                break;
            }

            Console.Write("Enter post title: ");
            var title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Post title cannot be empty.");
                break;
            }

            Console.Write("Enter post content: ");
            var content = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Post content cannot be empty.");
                break;
            }

            var post = new Post { Title = title, Content = content, BlogId = selectedBlog.BlogId };
            db.Posts.Add(post);
            db.SaveChanges();
            logger.Info("Post created - BlogID: {blogId}, Title: {title}", blogId, title);
            break;

             case "4":
            var allBlogs = db.Blogs.OrderBy(b => b.BlogId).ToList();
            if (allBlogs.Count == 0)
            {
                Console.WriteLine("No blogs available.");
                break;
            }

            Console.WriteLine("Choose a Blog by ID:");
            foreach (var b in allBlogs)
            {
                Console.WriteLine($"ID: {b.BlogId}, Name: {b.Name}");
            }

            if (!int.TryParse(Console.ReadLine(), out int selectedId))
            {
                Console.WriteLine("Invalid Blog ID.");
                break;
            }

            var blogToView = db.Blogs.FirstOrDefault(b => b.BlogId == selectedId);
            if (blogToView == null)
            {
                Console.WriteLine("Blog not found.");
                break;
            }

            var posts = db.Posts.Where(p => p.BlogId == blogToView.BlogId).ToList();
            Console.WriteLine($"Posts for Blog: {blogToView.Name} ({posts.Count} total)");

            foreach (var p in posts)
            {
                Console.WriteLine($"\nTitle: {p.Title}");
                Console.WriteLine($"Content: {p.Content}");
            }

            logger.Info("Displayed posts for BlogID {id} - {count} post(s)", selectedId, posts.Count);
            break;
            case "5":
            exit = true;
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

logger.Info("Program ended");
