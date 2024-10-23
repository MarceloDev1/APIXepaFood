using Domain.Entidades;
using Domain.Requests;

namespace Domain.Interfaces
{
    public interface IUsuarioRepositorio
    {
        void CriarUsuario(UsuarioRequest usuario);
        int CriarUsuarioRetornaIdUsuario(UsuarioRequest usuario);
        Usuario ObterUsuarioPorEmail(string email);
        List<Usuario> ObterTodosUsuarios();
        Usuario ObterUsuarioPorId(int idUsuario);
        void AtualizarUsuarioPorId(Usuario novoUsuario);
        void DeletarUsuarioPorId(int idUsuario);
        Usuario ObterUsuarioPorEmailSenha(string email, string senha);
    }
}