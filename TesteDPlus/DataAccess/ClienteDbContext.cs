using System;
using TesteDPlus.Model;
using TesteDPlus.Db;
using Microsoft.EntityFrameworkCore;

namespace TesteDPlus.DataAccess
{
	public class ClienteDbContext : DbContext
	{
		public DbSet<Cliente> CLientes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string conexaoDB = $"Filename={ConexaoDB.DevolverRota("clientes.db")}";
			optionsBuilder.UseSqlite(conexaoDB);
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Cliente>(entity =>
			{
				entity.HasKey(col => col.IDCliente);
				entity.Property(col => col.IDCliente).IsRequired().ValueGeneratedOnAdd();
			});
		}
	}
}

