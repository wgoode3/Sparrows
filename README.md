# C# Sparrows

Example Note taking app using SQLite with ASP.NET Core.

## HOW TO SQLITE WITH ENTITY FRAMEWORK IN ASP.NET CORE 

Create a new project using the ```dojo``` template.

```shell
# if it isn't installed already, install it using...
dotnet new -i Dojo.Mvc.Template
```

Run the following commands.

```shell
dotnet new dojo -o Sparrows
cd sparrow
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 2.1.4
```

### Create Models

If we're making a simple app for taking Notes, make a model like this. In ```Sparrows/Models/Notes.cs``` add...

```CS
using System;
using System.ComponentModel.DataAnnotations;

namespace Sparrows.Models
{
    public class Note
    {
        [Key]
        public int NoteId {get;set;}
        [Required (ErrorMessage = "Note Title is required!")]
        public string Title {get;set;}
        [Required (ErrorMessage = "Note Text is required!")]
        public string Text {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}
```

### Create a Context Class

In ```Sparrows/Models/DBContext.cs``` add...

```CS
using Microsoft.EntityFrameworkCore;
using Sparrows.Models;

namespace Sparrows.Models {
    public class DBContext : DbContext {
        public DBContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlite("Filename=mydb.db");
        }
        public DbSet<Note> Notes {get;set;}

    }
}
```

### Modify ```Sparrows/Startup.cs```

Add these name spaces:

```CS
using Microsoft.EntityFrameworkCore;
using Sparrows.Models;
```

Modify the ```public Startup( ... )``` constructor to look like...

```CS
public Startup(IConfiguration configuration)
{
    Configuration = configuration;
            
    using(var client = new DBContext(new DbContextOptions<DBContext>()))
    {
        client.Database.EnsureCreated();
    }
}
```

Modify the ```public void ConfigureServices( ... )``` method to look like...

```CS
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<DBContext>(options => options.UseSqlite("Data Source=mydb.db"));
    services.AddSession();            
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);;
}
```

### Don't forget to inject DBContext into HomeController

At the top of the ```HomeController``` class, add the following class member and constructor...

```CS
private static DBContext context;

public HomeController(DBContext DBContext)
{
    context = DBContext;
}
```

### Starting your project

When we start the project using ```dotnet watch run``` the ```mydb.db``` file will be created for us. 