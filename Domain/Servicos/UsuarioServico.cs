using Domain.Entidades;
using Domain.Interfaces;
using Domain.Requests;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public void CriarUsuario(UsuarioRequest usuario)
        {
            usuario.Senha = GerarMD5Hash(usuario.Senha);
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

        public Usuario ObterUsuarioPorEmailSenha(string email, string senha)
        {
            senha = GerarMD5Hash(senha.Trim());
            var usuario = _usuarioRepositorio.ObterUsuarioPorEmailSenha(email, senha);
            return usuario;
        }

        private static string GerarMD5Hash(string senha)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(senha);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }
}