using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiPeliculas2023.Models;

namespace WebApiPeliculas2023
{
	/*Los : se utilizas para heredar una clase y se utilizan para
	 mapear las clases o modelos a las identidades de las BD (llaver foraneas, etc)*/
	public class ApplicationDbContext:IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions options):base(options) 
		{ 
		}

        public DbSet<Pelicula> Peliculas { get; set; }
		public DbSet<Genero> Generos { get; set; }
		public DbSet<GeneroPelicula> GenerosPeliculas { get; set; }
		public DbSet<Opinion> Opiniones { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
