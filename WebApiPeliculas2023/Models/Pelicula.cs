using System.ComponentModel.DataAnnotations;

namespace WebApiPeliculas2023.Models
{
	public class Pelicula
	{
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength(maximumLength:50, ErrorMessage ="El campo {0} no debe de tener más de {1} caracteres")]

        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public double Calificacion { get; set; }
        public double Duracion { get; set; }
        public string Imagen { get; set; }
        public List<Genero> Generos { get; } = new();//muchos a muchos
        public ICollection<Opinion> Opiniones { get;} = new List<Opinion>();//Uno a muchos, opinion obligatoria
    }
}
