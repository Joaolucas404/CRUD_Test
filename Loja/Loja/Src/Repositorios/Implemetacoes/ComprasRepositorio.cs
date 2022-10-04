using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loja.Src.Contextos;
using Loja.Src.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Loja.Src.Repositorios.Implemetacoes
{
    public class ComprasRepositorio : ICompras
    {
        #region Atributos

        private readonly LojaContextos _contexto;
        
        #endregion Atributos

        #region Construtores
        public ComprasRepositorio(LojaContextos contexto)
        {
            _contexto = contexto;
        }
        #endregion Construtores

        #region Métodos
        public async Task<List<Compras>> PegarTodasComprasAsync()
        {
            return await _contexto.Compras
            .Include(p => p.Criador)
            .Include(p => p.Produto)
            .ToListAsync();
        }
        public async Task<Compras> PegarComprasPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id da postagem não encontrado");
            return await _contexto.Compras
            .Include(p => p.Criador)
            .Include(p => p.Produto)
            .FirstOrDefaultAsync(p => p.Id_Compra == id);
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Compras.FirstOrDefault(u => u.Id_Compra == id);
                return auxiliar != null;
            }
        }

        public async Task NovaComprasAsync(Compras compras)
        {
            if (!ExisteComprasId(compras.Criador.Id_Usuario)) throw new Exception("Id de compras não encontrado");
            if (!ExisteProdutoId(compras.Produto.Id_Produto)) throw new Exception("Id do produto não encontrado");
            await _contexto.Compras.AddAsync(
                new Compras
                {

                    Id_Compra = compras.Id_Compra,
                    DataHora = compras.DataHora,
                    Criador = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id_Usuario == compras.Criador.Id_Usuario),
                    Produto = await _contexto.Produtos.FirstOrDefaultAsync(t => t.Id_Produto == compras.Produto.Id_Produto)

                });

            await _contexto.SaveChangesAsync();

            bool ExisteComprasId(int id)
            {
                var auxiliar = _contexto.Compras.FirstOrDefault(u => u.Id_Compra == id);
                return auxiliar != null;
            }
            bool ExisteProdutoId(int id)
            {
                var auxiliar = _contexto.Produtos.FirstOrDefault(t => t.Id_Produto == id);
                return auxiliar != null;
            }
        }

        public async Task AtualizarComprasAsync(Compras compras)
        {
            if (!ExisteProdutoId(compras.Produto.Id_Produto)) throw new Exception("Id do Produto não encontrado");
            var comprasExistente = await PegarComprasPeloIdAsync(compras.Id_Compra);
            comprasExistente.Id_Compra = compras.Id_Compra;
            comprasExistente.DataHora = compras.DataHora;
            comprasExistente.Criador = compras.Criador;
            comprasExistente.Produto = await _contexto.Produtos.FirstOrDefaultAsync(t => t.Id_Produto == compras.Produto.Id_Produto);
            _contexto.Compras.Update(comprasExistente);
            await _contexto.SaveChangesAsync();

            bool ExisteProdutoId(int id)
            {
                var auxiliar = _contexto.Compras.FirstOrDefault(t => t.Id_Compra == id);
                return auxiliar != null;

            }
        }

        public async Task DeletarComprasAsync(int id)
        {
            _contexto.Compras.Remove(await PegarComprasPeloIdAsync(id));
            await _contexto.SaveChangesAsync();



            #endregion Métodos
        }
    } 
}


    