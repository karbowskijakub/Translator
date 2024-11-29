using Translator.Interfaces;
using Translator.Models;
using Microsoft.EntityFrameworkCore;

namespace Translator.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
             : base(options)
        {
        }
        public AppDbContext()
        {
        }

        public DbSet<Translate> TranslationFields { get; set; }

    }
}