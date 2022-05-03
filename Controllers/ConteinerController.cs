using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testedev.Data;
using testedev.Models;
using testedev.Models.ViewModel;

namespace testedev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConteinerController : ControllerBase
    {
        [HttpGet]
        [Route("conteiners")]
        public async Task<IActionResult> GetListaConteinersAsync([FromServices] AppDbContext context) 
        {
            var conteiners = await context.Conteiners.AsNoTracking()
                .Select(_ => new 
                {
                    _.Id,
                    _.ConteinerNumero,
                    _.ConteinerTipo,
                    nmStatus = _.Status.NmStatus,
                    nmCategoria = _.Categoria.NmCategoria
                }).ToListAsync();

            return Ok(conteiners);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetConteinerByIdAsync([FromServices] AppDbContext context, [FromRoute]int id)
        {
            var conteiner = await context.Conteiners
                .Where(_ => _.Id == id)
                .Select(_ => new
                {
                    _.Id,
                    _.ConteinerNumero,
                    _.ConteinerTipo,
                    nmStatus = _.Status.NmStatus,
                    nmCategoria = _.Categoria.NmCategoria
                }).FirstOrDefaultAsync();

            return conteiner == null ? NotFound("Contêiner não encontrado") : Ok(conteiner);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> PostConteinerdAsync([FromServices] AppDbContext context,[FromBody]ConteinerVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var conteiner = new Conteiner
            {
                ConteinerNumero = model.ConteinerNumero,
                ConteinerTipo = model.ConteinerTipo,
                StatusId = model.StatusId,
                CategoriaId = model.CategoriaId
            };

            try
            {
                await context.Conteiners.AddAsync(conteiner);
                await context.SaveChangesAsync();
                return Created(uri: $"api/conteiner/{conteiner.Id}", conteiner);
            }
            catch (Exception)
            {

                return BadRequest("Não foi possivel salvar o registro");
            }

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutConteinerdAsync([FromServices] AppDbContext context, [FromBody] ConteinerVM model, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var conteiner = await context.Conteiners.Where(_ => _.Id == id).FirstOrDefaultAsync();
            if (conteiner == null)
                return NotFound();

            try
            {
                conteiner.ConteinerNumero = model.ConteinerNumero;
                conteiner.ConteinerTipo = model.ConteinerTipo;
                conteiner.StatusId = model.StatusId;
                conteiner.CategoriaId = model.CategoriaId;

                context.Conteiners.Update(conteiner);
                await context.SaveChangesAsync();

                return Ok(conteiner);
            }
            catch (Exception)
            {

                return BadRequest("Não foi possivel alterar o registro");
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteConteinerdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
           
            var conteiner = await context.Conteiners.FirstOrDefaultAsync(_ => _.Id == id);
            if (conteiner == null)
                return NotFound();

            try
            {
                
                context.Conteiners.Remove(conteiner);
                await context.SaveChangesAsync();

                return Ok("Contêiner Excluido");
            }
            catch (Exception)
            {

                return BadRequest("Não foi possivel excluir o registro");
            }

        }




    }
}
