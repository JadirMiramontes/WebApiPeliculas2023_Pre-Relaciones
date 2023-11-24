namespace WebApiPeliculas2023.Class
{
	public class RegistrarPeliculaDTO
	{
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public int Calificacion { get; set; }
		public int Duracion { get; set; }
		public string Imagen { get; set; }
	}
	public class ActualizarPeliculaDTO
	{
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public int Calificacion { get; set; }
		public int Duracion { get; set; }
		public string Imagen { get; set; }
	}
	public class ModificarPeliculaDTO
	{
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public int Calificacion { get; set; }
		public int Duracion { get; set; }
		public string Imagen { get; set; }
	}

	public class EliminarPeliculaDTO
	{
		public int Id { get; set; }
	}
	public class AgregarGeneroPeliculaDTO
	{
		public int IdPelicula { get; set; }
		public int IdGenero { get; set; }
	}

}
