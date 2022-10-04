using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loja.Src.Contextos;
using Loja.Src.Modelos;
using Microsoft.EntityFrameworkCore;

    namespace Loja.Src.Repositorios.Implementacoes
    {
        public class UsuarioRepositorio : IUsuarios
        {
            #region Atributos
            private readonly LojaContextos _contexto;
            #endregion
            #region Construtores
            public UsuarioRepositorio(LojaContextos contexto)
            {
                _contexto = contexto;
            }
            #endregion
            #region Métodos
        public async Task<Usuario> PegarUsuarioPeloEmailAsync(string email)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task NovoUsuarioAsync(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(
            new Usuario
            {
                Id_Usuario = usuario.Id_Usuario,
                Nome = usuario.Nome,
                Log_in = usuario.Log_in,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Documento = usuario.Documento,
                Tipo = usuario.Tipo
            });
            await _contexto.SaveChangesAsync();
        }


        #endregion
    }
}


