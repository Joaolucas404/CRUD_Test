using System.Threading.Tasks;
using Loja.Src.Modelos;

namespace Loja.Src.Repositorios
{
    public interface IUsuarios
    {
        Task<Usuario> PegarUsuarioPeloEmailAsync(string email);
        Task NovoUsuarioAsync(Usuario usuario);

    }
}
