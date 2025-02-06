using FirstApi.Models;
using Microsoft.EntityFrameworkCore;
using Task = FirstApi.Models.Task;

namespace FirstApi.Data;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>()
            .HasOne(x => x.User)         // Relacionamento com o usuário
            .WithMany(u => u.Tasks)      // Um usuário tem várias tarefas
            .HasForeignKey(x => x.UserId) // Chave estrangeira
            .OnDelete(DeleteBehavior.Cascade); // Deletar tarefas ao deletar usuário
    }
    
}