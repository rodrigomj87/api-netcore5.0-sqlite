using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testedev.Data;
using testedev.Enum;
using testedev.Models;
using testedev.Models.ViewModel;

namespace testedev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        [HttpGet("listar")]
        public async Task<IActionResult> GetListaMovimentacaoAsync([FromServices] AppDbContext context) 
        {
            var movimentacoes = await context.Movimentacoes.AsNoTracking()
                .Select(_ => new 
                {
                    _.Id,
                    NmCliente = _.Cliente.NmCliente,
                    ConteinerNumero = _.Conteiner.ConteinerNumero,
                    NmTipoMovimentacao = _.TipoMovimentacao.NmTipoMovimentacao,
                    _.DataHoraInicio,
                    _.DataHoraFim
                }).ToListAsync();

            return Ok(movimentacoes);
        }

        [HttpGet("relatorio")]
        public async Task<IActionResult> GetRelatorioAsync([FromServices] AppDbContext context)
        {
            var alvo = new List<object>();

            var movimentacao = await context.Movimentacoes
                .OrderBy(_ => _.Cliente.Id)
                .ThenBy(_ => _.Conteiner.CategoriaId)
                .Select(_ => new
                {
                    _.Id,
                    NmCliente = _.Cliente.NmCliente,
                    ConteinerNumero = _.Conteiner.ConteinerNumero,
                    TipoMovimentacao = _.TipoMovimentacao.NmTipoMovimentacao,
                    NmTipo = _.Conteiner.Categoria.NmCategoria,
                    stDataHoraInicio = _.DataHoraInicio.ToString(),
                    stDataHoraFim = (_.DataHoraFim == null) ? "Movimentação Aberta" : _.DataHoraFim.ToString()
                })
                .ToListAsync();
            if (movimentacao == null)
                return NotFound("Não foi encontrada nenhuma Movimentação");

            alvo.Add(movimentacao);

            var nImportacao = context.Movimentacoes.Where(_ => _.Conteiner.Categoria.NmCategoria == ECategoria.IMPORTACAO).Count();
            var txtImportacao = "Foram registradas " + nImportacao + " Importações";
            alvo.Add(txtImportacao);

            var nExportacao = context.Movimentacoes.Where(_ => _.Conteiner.Categoria.NmCategoria == ECategoria.EXPORTACAO).Count();
            var txtExportacao = "Foram registradas " + nExportacao + " Exportações";
            alvo.Add(txtExportacao);

            return Ok(alvo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovimentacaoByIdAsync([FromServices] AppDbContext context, [FromRoute]int id)
        {
            var movimentacao = await context.Movimentacoes
                .Where(_ => _.Id == id)
                .Select(_ => new
                {
                    _.Id,
                    _.ClienteId,
                    _.ConteinerId,
                    _.TipoMovimentacaoId,
                    nmCliente = _.Cliente.NmCliente,
                    ConteinerNUmero = _.Conteiner.ConteinerNumero,
                    NmTipoMovimentecao = _.TipoMovimentacao.NmTipoMovimentacao,
                    _.DataHoraInicio,
                    _.DataHoraFim
                }).FirstOrDefaultAsync();

            return movimentacao == null ? NotFound("Movimentação encontrada") : Ok(movimentacao);
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> PostMovimentacaodAsync([FromServices] AppDbContext context,[FromBody]MovimentacaoVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cliente = await context.Clientes.Where(_ => _.Id == model.ClienteId).AsNoTracking().FirstOrDefaultAsync();
            if (cliente == null)
                return BadRequest("Código de cliente inválido");

            var tipoMovimentacao = await context.TipoMovimentacao.Where(_ => _.Id == model.TipoMovimentacaoId).AsNoTracking().FirstOrDefaultAsync();
            if (tipoMovimentacao == null)
                return BadRequest("Tipo de movimentação não encontrada");

            var conteiner = await context.Conteiners.Where(_ => _.Id == model.ConteinerId).AsNoTracking().FirstOrDefaultAsync();
            if (conteiner == null)
                return BadRequest("Conteiner não encontrado");

            var temMovimentacao = await context.Movimentacoes.Where(_ => _.ConteinerId == model.ConteinerId)
                .Where(_ => _.DataHoraFim == null).AsNoTracking().AnyAsync();
            if (temMovimentacao)
                return BadRequest("Já existe uma movimentação em andamento com este conteiner");


            var movimentacao = new Movimentacao
            {
                ClienteId = cliente.Id,
                ConteinerId = conteiner.Id,
                TipoMovimentacaoId = tipoMovimentacao.Id,
                DataHoraInicio = System.DateTime.Now
            };

            try
            {
                await context.Movimentacoes.AddAsync(movimentacao);
                await context.SaveChangesAsync();
                return Created(uri: $"api/movimentacao/{movimentacao.Id}", movimentacao);
            }
            catch (Exception)
            {

                return BadRequest("Não foi possivel salvar o registro");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimentacaoAsync([FromServices] AppDbContext context, [FromBody] MovimentacaoVM model, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var movimentacao = await context.Movimentacoes.Where(_ => _.Id == id).FirstOrDefaultAsync();
            if (movimentacao == null)
                return NotFound();

            var cliente = await context.Clientes.Where(_ => _.Id == model.ClienteId).AsNoTracking().FirstOrDefaultAsync();
            if (cliente == null)
                return BadRequest("Código de cliente inválido");

            var conteiner = await context.Conteiners.Where(_ => _.Id == model.ConteinerId).AsNoTracking().FirstOrDefaultAsync();
            if (conteiner == null)
                return BadRequest("Conteiner não encontrado");

            try
            {
                movimentacao.ClienteId = model.ClienteId;
                movimentacao.ConteinerId = model.ConteinerId;
                movimentacao.TipoMovimentacaoId = model.TipoMovimentacaoId;

                context.Movimentacoes.Update(movimentacao);
                await context.SaveChangesAsync();

                return Ok(movimentacao);
            }
            catch (Exception)
            {

                return BadRequest("Não foi possivel alterar o registro");
            }

        }
        [HttpPut("finalizar/{id}")]
        public async Task<IActionResult> PutFinalizarMovimentacaoAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var movimentacao = await context.Movimentacoes.Where(_ => _.Id == id).FirstOrDefaultAsync();
            if (movimentacao == null)
                return NotFound();

           try
            {
                movimentacao.DataHoraFim = System.DateTime.Now;

                context.Movimentacoes.Update(movimentacao);
                await context.SaveChangesAsync();

                return Ok(movimentacao);
            }
            catch (Exception)
            {

                return BadRequest("Não foi possivel alterar o registro");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimentacaoAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
           
            var movimentacao = await context.Movimentacoes.FirstOrDefaultAsync(_ => _.Id == id);
            if (movimentacao == null)
                return NotFound();

            try
            {
                
                context.Movimentacoes.Remove(movimentacao);
                await context.SaveChangesAsync();

                return Ok("Movimentacao Excluida");
            }
            catch (Exception)
            {

                return BadRequest("Não foi possivel excluir o registro");
            }

        }




    }
}
