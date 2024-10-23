using Domain.Entidades;
using Domain.Requests;

namespace Domain.Interfaces
{
    public interface IUsuarioServico
    {
        void CriarUsuario(UsuarioRequest usuario);
        Usuario ObterUsuarioPorEmail(string email);
        List<Usuario> ObterTodosUsuarios();
        Usuario ObterUsuarioPorId(int idUsuario);
        void AtualizarUsuarioPorId(Usuario novoUsuario);
        void DeletarUsuarioPorId(int idUsuario);
        Usuario ObterUsuarioPorEmailSenha(string email, string senha);
    }
}