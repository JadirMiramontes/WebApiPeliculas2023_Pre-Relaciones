using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPeliculas2023.Class;
using WebApiPeliculas2023.Models;

namespace WebApiPeliculas2023.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	public class PeliculaController : Controller
	{
		private readonly ApplicationDbContext context;
		public PeliculaController(ApplicationDbContext context)
		{
			this.context = context;
		}

/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// EndPoint para dar de alta una Pelicula
		/*[HttpPost("RegistrarPelicula")]
		public async Task<ActionResult> RegistrarPelicula(Pelicula pelicula)
		{
			var existePelicula = await context.Peliculas.AnyAsync(x => x.Titulo == pelicula.Titulo);

			if (existePelicula)
			{
				return BadRequest($"La película {pelicula.Titulo} ya existe");
			}
			context.Add(pelicula);
			await context.SaveChangesAsync();
			return Ok(pelicula);

		}*/

		[HttpPost("RegistrarPelicula")]
		public async Task<ActionResult> RegistrarPelicula(RegistrarPeliculaDTO registrarPeliculaDTO)
		{
			var existePelicula = await context.Peliculas.AnyAsync(x => x.Titulo == registrarPeliculaDTO.Titulo);

			if (existePelicula)
			{
				return BadRequest($"La película {registrarPeliculaDTO.Titulo} ya existe");
			}

			var nuevaPelicula = new Pelicula
			{
				Titulo = registrarPeliculaDTO.Titulo,
				Descripcion = registrarPeliculaDTO.Descripcion,
				Calificacion = registrarPeliculaDTO.Calificacion,
				Duracion = registrarPeliculaDTO.Duracion,
				Imagen = registrarPeliculaDTO.Imagen
			};

			context.Add(nuevaPelicula);
			await context.SaveChangesAsync();
			return Ok(nuevaPelicula);
		}



/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// End point para listar las películas
		[HttpGet("ListarPeliculas")]
		public async Task<ActionResult<List<Pelicula>>> ListarPeliculas()
		{
			var peliculas = await context.Peliculas.ToListAsync();
			return Ok(peliculas);
		}

/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// End point para buscar información de una película en específico por título
		[HttpGet("PeliculaEspecifica/{titulo}")]
		public async Task<ActionResult<Pelicula>> PeliculaEspecifica(string titulo)
		{
			var pelicula = await context.Peliculas.FirstOrDefaultAsync(x => x.Titulo == titulo);

			if (pelicula == null)
			{
				return NotFound();
			}

			return Ok(pelicula);
		}

		/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// End point para actualizar películas
		/*[HttpPut("ActualizarPelicula/{id:int}")]
		public async Task<ActionResult> ActualizarPelicula(int id, Pelicula peliculaActualizada)
		{
			var peliculaExistente = await context.Peliculas.FindAsync(id);
			if (peliculaExistente == null)
			{
				return NotFound($"La película con ID {id} no existe.");
			}
			peliculaExistente.Titulo = peliculaActualizada.Titulo;
			peliculaExistente.Descripcion = peliculaActualizada.Descripcion;
			peliculaExistente.Calificacion = peliculaActualizada.Calificacion;
			peliculaExistente.Duracion = peliculaActualizada.Duracion;
			peliculaExistente.Imagen = peliculaActualizada.Imagen;
			try
			{
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				// Error de concurrencia si es necesario
				return BadRequest("Error de concurrencia al actualizar la película.");
			}
			return Ok(peliculaExistente);
		}*/

		[HttpPut("ActualizarPelicula/{id:int}")]
		public async Task<ActionResult> ActualizarPelicula(int id, ActualizarPeliculaDTO actualizarPeliculaDTO)
		{
			var peliculaExistente = await context.Peliculas.FindAsync(id);

			if (peliculaExistente == null)
			{
				return NotFound($"La película con ID {id} no existe.");
			}

			// Actualiza solo los campos necesarios
			peliculaExistente.Titulo = actualizarPeliculaDTO.Titulo;
			peliculaExistente.Descripcion = actualizarPeliculaDTO.Descripcion;
			peliculaExistente.Calificacion = actualizarPeliculaDTO.Calificacion;
			peliculaExistente.Duracion = actualizarPeliculaDTO.Duracion;
			peliculaExistente.Imagen = actualizarPeliculaDTO.Imagen;

			try
			{
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				// Manejo de errores de concurrencia si es necesario
				return BadRequest("Error de concurrencia al actualizar la película.");
			}

			return Ok(peliculaExistente);
		}

		/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// End point para eliminar película
		/*[HttpDelete("EliminarPelicula/{id:int}")]
		public async Task<ActionResult> EliminarPelicula(int id)
		{
			var existe = await context.Peliculas.AnyAsync(z => z.Id == id);

			if (!existe)
			{
				return NotFound();
			}

			var pelicula = new Pelicula() { Id = id };
			context.Remove(pelicula);
			await context.SaveChangesAsync();
			return Ok();
		}*/

		[HttpDelete("EliminarPelicula")]
		public async Task<ActionResult> EliminarPelicula(EliminarPeliculaDTO eliminarPeliculaDTO)
		{
			var existe = await context.Peliculas.AnyAsync(z => z.Id == eliminarPeliculaDTO.Id);

			if (!existe)
			{
				return NotFound();
			}

			var pelicula = new Pelicula() { Id = eliminarPeliculaDTO.Id };
			context.Remove(pelicula);
			await context.SaveChangesAsync();
			return Ok();
		}


		/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// End point para actualizar película
		/*[HttpPut("ModificarPelicula/{id:int}")]
		public async Task<ActionResult> ModificarPelicula(int id, Pelicula pelicula)
		{
			var existe = await context.Peliculas.AnyAsync(x => x.Id == id);

			if (!existe)
			{
				return NotFound("La película no existe");
			}

			pelicula.Id = id;
			context.Update(pelicula);
			await context.SaveChangesAsync();
			return Ok(pelicula);
		}*/

		[HttpPut("ModificarPelicula/{id:int}")]
		public async Task<ActionResult> ModificarPelicula(int id, ModificarPeliculaDTO modificarPeliculaDTO)
		{
			var existe = await context.Peliculas.AnyAsync(x => x.Id == id);

			if (!existe)
			{
				return NotFound("La película no existe");
			}

			var peliculaExistente = await context.Peliculas.FindAsync(id);

			// Actualiza solo los campos necesarios
			peliculaExistente!.Titulo = modificarPeliculaDTO.Titulo;
			peliculaExistente.Descripcion = modificarPeliculaDTO.Descripcion;
			peliculaExistente.Calificacion = modificarPeliculaDTO.Calificacion;
			peliculaExistente.Duracion = modificarPeliculaDTO.Duracion;
			peliculaExistente.Imagen = modificarPeliculaDTO.Imagen;

			context.Update(peliculaExistente);
			await context.SaveChangesAsync();
			return Ok(peliculaExistente);
		}

/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		/*End poitn para agregar genero a una pelicula*/
		/*[HttpPost("AgregarGeneroPelicula/{idPelicula}/{idGenero}")]
		public async Task<ActionResult> AgregarGeneroPelicula(int PeliculasId, int GenerosId)
		{
			var pelicula = await context.Peliculas.FindAsync(PeliculasId);
			if (pelicula == null)
			{
				return NotFound();
			}

			var genero = await context.Generos.FindAsync(GenerosId);
			if (genero == null)
			{
				return NotFound();
			}

			pelicula.Generos.Add(genero);

			await context.SaveChangesAsync();
			return Ok();
		}*/

		[HttpPost("AgregarGeneroPelicula")]
		public async Task<ActionResult> AgregarGeneroPelicula(AgregarGeneroPeliculaDTO agregarGeneroPeliculaDTO)
		{
			var pelicula = await context.Peliculas.FindAsync(agregarGeneroPeliculaDTO.IdPelicula);
			if (pelicula == null)
			{
				return NotFound();
			}

			var genero = await context.Generos.FindAsync(agregarGeneroPeliculaDTO.IdGenero);
			if (genero == null)
			{
				return NotFound();
			}

			pelicula.Generos.Add(genero);

			await context.SaveChangesAsync();
			return Ok();
		}

	}
}
