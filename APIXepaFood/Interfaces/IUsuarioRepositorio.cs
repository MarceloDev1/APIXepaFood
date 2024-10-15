﻿using APIXepaFood.Models;

namespace APIXepaFood.Interfaces
{
    public interface IUsuarioRepositorio
    {
        void CriarUsuario(Usuario usuario);
        Usuario ObterUsuarioPorEmail(string email);
        List<Usuario> ObterTodosUsuarios();
        Usuario ObterUsuarioPorId(int idUsuario);
        void AtualizarUsuarioPorId(Usuario novoUsuario);
        void DeletarUsuarioPorId(int idUsuario);
    }
}