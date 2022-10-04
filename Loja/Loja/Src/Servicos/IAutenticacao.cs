using System.Threading.Tasks;
using Loja.Src.Modelos;

namespace Loja.Src.Servicos
{
    public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(Usuario usuario);
        string GerarToken(Usuario usuario);
    }

}
