using Domain.Entidades;
using Domain.Interfaces;

namespace Domain.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public void CriarUsuario(Usuario usuario)
        {
            _usuarioRepositorio.CriarUsuario(usuario);
        }

        public Usuario ObterUsuarioPorEmail(string email)
        {
            Usuario usuario = _usuarioRepositorio.ObterUsuarioPorEmail(email);
            return usuario;
        }
        public List<Usuario> ObterTodosUsuarios()
        {
            List<Usuario> listaUsuarios = _usuarioRepositorio.ObterTodosUsuarios();
            return listaUsuarios;
        }

        public Usuario ObterUsuarioPorId(int idUsuario)
        {
            Usuario usuario = _usuarioRepositorio.ObterUsuarioPorId(idUsuario);
            return usuario;
        }

        public void AtualizarUsuarioPorId(Usuario novoUsuario)
        {
            _usuarioRepositorio.AtualizarUsuarioPorId(novoUsuario);
        }

        public void DeletarUsuarioPorId(int idUsuario)
        {
            _usuarioRepositorio.DeletarUsuarioPorId(idUsuario);
        }
    }
}