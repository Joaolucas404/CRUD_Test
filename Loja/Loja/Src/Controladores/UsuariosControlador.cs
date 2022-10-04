using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Loja.Src.Modelos;
using Loja.Src.Repositorios;
using Loja.Src.Servicos;
using System;
using System.Threading.Tasks;

namespace Loja.Src.Controladores
{
    [ApiController]
    [Route("api/Usuarios")]
    [Produces("application/json")]
    public class UsuarioControlador : ControllerBase
    {
        #region Atributos
        private readonly IUsuarios _repositorio;
        private readonly IAutenticacao _servicos;
        #endregion

        #region Construtores
        public UsuarioControlador(IUsuarios repositorio, IAutenticacao servicos)
        {
            _repositorio = repositorio;
            _servicos = servicos;
        }
        #endregion
        #region Métodos

        [HttpGet("email/{emailUsuario}")]
        [Authorize]
        public async Task<ActionResult> PegarUsuarioPeloEmailAsync([FromRoute] string emailUsuario)
        {
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync(emailUsuario);
            if (usuario == null) return NotFound(new { Mensagem = "Usuario não encontrado" });
            return Ok(usuario);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> NovoUsuarioAsync([FromBody] Usuario usuario)
        {
            try
            {
                await _servicos.CriarUsuarioSemDuplicarAsync(usuario);
                return Created($"api/Usuarios/email/{usuario.Email}", usuario);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("logar")]
        [AllowAnonymous]
        public async Task<ActionResult> LogarAsync([FromBody] Usuario usuario)
        {
            var auxiliar = await _repositorio.PegarUsuarioPeloEmailAsync(usuario.Email);

            if (auxiliar == null) return Unauthorized(new { Mensagem = "E-mail não encontrado" });

            if (auxiliar.Senha != _servicos.CodificarSenha(usuario.Senha))
                return Unauthorized(new { Mensagem = "Senha incorreta" });

            var token = "Bearer " + _servicos.GerarToken(auxiliar);

            return Ok(new { Usuario = auxiliar, Token = token });
        }

        #endregion
    }
}
