using ItCollege.Models.Recources;
using ItCollege.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ItCollege.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Course> Courses { get; set; }
}