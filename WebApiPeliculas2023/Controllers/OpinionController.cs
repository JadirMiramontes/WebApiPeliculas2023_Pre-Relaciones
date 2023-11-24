using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPeliculas2023.Class;
using WebApiPeliculas2023.Models;

namespace WebApiPeliculas2023.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OpinionController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		public OpinionController(ApplicationDbContext context)
		{
			this.context = context;
		}

		/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// EndPoint para dar de alta una Opinión
		/*[HttpPost("RegistrarOpinion")]
		public async Task<ActionResult> RegistrarOpinion(Opinion opinion)
		{
			var existeOpinion = await context.Opiniones.AnyAsync(x => x.IdPelicula == opinion.IdPelicula && x.IdUsuario == opinion.IdUsuario);

			context.Add(opinion);
			await context.SaveChangesAsync();
			return Ok(opinion);
		}*/

		[HttpPost("RegistrarOpinion")]
		public async Task<ActionResult> RegistrarOpinion(RegistrarOpinionDTO registrarOpinionDTO)
		{
			var existeOpinion = await context.Opiniones.AnyAsync(x => x.IdPelicula == registrarOpinionDTO.IdPelicula && x.IdUsuario == registrarOpinionDTO.IdUsuario);

			if (existeOpinion)
			{
				return BadRequest($"La opinión para la película con ID {registrarOpinionDTO.IdPelicula} y usuario con ID {registrarOpinionDTO.IdUsuario} ya existe.");
			}

			var opinion = new Opinion
			{
				IdPelicula = registrarOpinionDTO.IdPelicula,
				IdUsuario = registrarOpinionDTO.IdUsuario,
				Comentario = registrarOpinionDTO.Comentario,
				Calificacion = registrarOpinionDTO.Calificacion,
				// Otros campos necesarios para registrar una opinión
			};

			context.Add(opinion);
			await context.SaveChangesAsync();
			return Ok(opinion);
		}


/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// End point para listar las opiniones
		[HttpGet("ListarOpiniones")]
		public async Task<ActionResult<List<Opinion>>> ListarOpiniones()
		{
			var opiniones = await context.Opiniones.ToListAsync();
			return Ok(opiniones);
		}

/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// End point para buscar información de una opinión en específico por ID
		[HttpGet("OpinionEspecifica/{id:int}")]
		public async Task<ActionResult<Opinion>> OpinionEspecifica(int id)
		{
			var opinion = await context.Opiniones.FirstOrDefaultAsync(x => x.Id == id);

			if (opinion == null)
			{
				return NotFound();
			}

			return Ok(opinion);
		}

		/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// End point para actualizar opiniones
		/*[HttpPut("ActualizarOpinion/{id:int}")]
		public async Task<ActionResult> ActualizarOpinion(int id, Opinion opinionActualizada)
		{
			var opinionExistente = await context.Opiniones.FindAsync(id);
			if (opinionExistente == null)
			{
				return NotFound($"La opinión con ID {id} no existe.");
			}

			opinionExistente.Comentario = opinionActualizada.Comentario;
			opinionExistente.Calificacion = opinionActualizada.Calificacion;
			opinionExistente.Fecha = opinionActualizada.Fecha;

			try
			{
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				// Error de concurrencia si es necesario
				return BadRequest("Error de concurrencia al actualizar la opinión.");
			}

			return Ok(opinionExistente);
		}*/

		[HttpPut("ActualizarOpinion/{id:int}")]
		public async Task<ActionResult> ActualizarOpinion(int id, ActualizarOpinionDTO actualizarOpinionDTO)
		{
			var opinionExistente = await context.Opiniones.FindAsync(id);

			if (opinionExistente == null)
			{
				return NotFound($"La opinión con ID {id} no existe.");
			}

			opinionExistente.Comentario = actualizarOpinionDTO.Comentario;
			opinionExistente.Calificacion = actualizarOpinionDTO.Calificacion;

			try
			{
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				// Manejo de errores de concurrencia si es necesario
				return BadRequest("Error de concurrencia al actualizar la opinión.");
			}

			return Ok(opinionExistente);
		}

		/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// End point para eliminar opinión
		/*[HttpDelete("EliminarOpinion/{id:int}")]
		public async Task<ActionResult> EliminarOpinion(int id)
		{
			var existe = await context.Opiniones.AnyAsync(z => z.Id == id);

			if (!existe)
			{
				return NotFound();
			}

			var opinion = new Opinion() { Id = id };
			context.Remove(opinion);
			await context.SaveChangesAsync();
			return Ok();
		}*/

		[HttpDelete("EliminarOpinion")]
		public async Task<ActionResult> EliminarOpinion(EliminarOpinionDTO eliminarOpinionDTO)
		{
			var existe = await context.Opiniones.AnyAsync(z => z.Id == eliminarOpinionDTO.Id);

			if (!existe)
			{
				return NotFound();
			}

			var opinion = new Opinion() { Id = eliminarOpinionDTO.Id };
			context.Remove(opinion);
			await context.SaveChangesAsync();
			return Ok();
		}

		/*---------------------------------------------------------------------------------------------------------------------------------------------*/
		// End point para actualizar opinión
		/*[HttpPut("ModificarOpinion/{id:int}")]
		public async Task<ActionResult> ModificarOpinion(int id, Opinion opinion)
		{
			var existe = await context.Opiniones.AnyAsync(x => x.Id == id);

			if (!existe)
			{
				return NotFound("La opinión no existe");
			}

			opinion.Id = id;
			context.Update(opinion);
			await context.SaveChangesAsync();
			return Ok(opinion);
		}*/

		[HttpPut("ModificarOpinion/{id:int}")]
		public async Task<ActionResult> ModificarOpinion(int id, ModificarOpinionDTO modificarOpinionDTO)
		{
			var existe = await context.Opiniones.AnyAsync(x => x.Id == id);

			if (!existe)
			{
				return NotFound("La opinión no existe");
			}

			var opinionExistente = await context.Opiniones.FindAsync(id);

			opinionExistente!.Comentario = modificarOpinionDTO.Comentario;
			opinionExistente.Calificacion = modificarOpinionDTO.Calificacion;

			context.Update(opinionExistente);
			await context.SaveChangesAsync();
			return Ok(opinionExistente);
		}

	}
}
