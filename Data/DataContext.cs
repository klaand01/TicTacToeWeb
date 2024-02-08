using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using TicTacToeWeb.Models;

namespace TicTacToeWeb.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>().HasKey(e => new { e.Id });
        }
        public DbSet<Board> Board { get; set; }
    }
}
