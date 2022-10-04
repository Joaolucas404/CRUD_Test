using Loja.Src.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Loja.Src.Modelos;
using Microsoft.AspNetCore.Authorization;

namespace Loja.Src.Controladores
{
    [ApiController]
    [Route("api/Produtos")]
    [Produces("application/json")]
    public class ProdutosControlador : ControllerBase
    {
        #region Atributos
        private readonly IProdutos _repositorio;
        #endregion
        #region Construtores
        public ProdutosControlador(IProdutos repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion
        #region Métodos
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> PegarTodosProdutosAsync()
        {
            var lista = await _repositorio.PegarTodosProdutosAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }
        [HttpGet("id/{idProduto}")]
        [Authorize]
        public async Task<ActionResult> PegarProdutosPeloIdAsync([FromRoute] int idProduto)
        {
            try
            {
                return Ok(await _repositorio.PegarProdutosPeloIdAsync(idProduto));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NovoProdutosAsync([FromBody] Produtos produtos)
        {
            await _repositorio.NovoProdutosAsync(produtos);
            return Created($"api/Produtos", produtos);
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> AtualizarProdutosAsync([FromBody] Produtos produto)
        {
            try
            {
                await _repositorio.AtualizarProdutosAsync(produto);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }
        [HttpDelete("deletar/{idProduto}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> DeletarProdutosAsync([FromRoute] int idProduto)
        {
            try
            {
                await _repositorio.DeletarProdutosAsync(idProduto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        #endregion
    }
}
