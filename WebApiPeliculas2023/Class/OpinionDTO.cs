namespace WebApiPeliculas2023.Class
{
	public class RegistrarOpinionDTO
	{
		public int IdPelicula { get; set; }
		public int IdUsuario { get; set; }
		public string Comentario { get; set; }
		public int Calificacion { get; set; }
	}

	public class ActualizarOpinionDTO
	{
		public string Comentario { get; set; }
		public int Calificacion { get; set; }
	}

	public class EliminarOpinionDTO
	{
		public int Id { get; set; }
	}
	public class ModificarOpinionDTO
	{
		public string Comentario { get; set; }
		public int Calificacion { get; set; }
	}

}
