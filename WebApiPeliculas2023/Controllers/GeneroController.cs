using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPeliculas2023.Models;

namespace WebApiPeliculas2023.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GeneroController : Controller
	{
		private readonly ApplicationDbContext context;
		public GeneroController(ApplicationDbContext context)
		{
			this.context = context;
		}

		//EndPoint dar de alta genero
		[HttpPost("RegistrarGenero")]
		public async Task<ActionResult> RegistrarGenero(Genero genero)
		{
			var existeGnero = await context.Generos.AnyAsync(x => x.Nombre == genero.Nombre);
			if (existeGnero)
			{
				return BadRequest($"El genero {genero.Nombre} ya existe");
			}

			context.Add(genero);
			await context.SaveChangesAsync();
			return Ok(genero);
		}

		/*08-11-2023*/
		//End point para listar las categorias 
		[HttpGet("ListarGeneros")]
		public async Task<ActionResult<List<Genero>>> ListarGeneros()
		{
			var generos = await context.Generos.ToListAsync();
			return Ok(generos);
		}

		//End point para buscar informacion de un genero en especifico
		[HttpGet("GeneroEspecifico/{nombre}")]
		public async Task<ActionResult<Genero>> GeneroEspecifico(string nombre)
		{
			var genero = await context.Generos.FirstOrDefaultAsync(x => x.Nombre == nombre);
			if(genero == null)
			{
				return NotFound();
			}
			return Ok(genero);
		}

		//End point Actualizar generos
		[HttpPut("ActualizarGenero/{id:int}")]
		public async Task<ActionResult> ActualizarGenero(int id, Genero generoActualizado)
		{
			var generoExistente = await context.Generos.FindAsync(id);
			if (generoExistente == null)
			{
				return NotFound($"El género con ID {id} no existe.");
			}
			generoExistente.Nombre = generoActualizado.Nombre;
			try
			{
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				// Error de concurrencia si es necesario
				return BadRequest("Error de concurrencia al actualizar el género.");
			}
			return Ok(generoExistente);
		}

		//End point para eliminar elemento
		[HttpDelete("EliminarGenero/{id:int}")]
		public async Task<ActionResult> EliminarGenero(int id)
		{
			var existe = await context.Generos.AnyAsync(z => z.Id == id);
			if (!existe) return NotFound();
			context.Remove(new Genero() { Id = id});
			await context.SaveChangesAsync();
			return Ok();
		}

		//End point para actualizar hecho por el profe
		[HttpPut("ModificarGenero/{id:int}")]
		public async Task<ActionResult> ModificarGenero(int id, Genero genero)
		{
			var existe = await context.Generos.AnyAsync(x => x.Id == id);
			if(!existe) return NotFound("El producto no existe");

			context.Update(genero);
			await context.SaveChangesAsync();
			return Ok(genero);
		}
	}
}
