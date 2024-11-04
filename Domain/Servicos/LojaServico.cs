﻿using Domain.Entidades;
using Domain.Interfaces;
using Domain.Requests;

namespace Domain.Servicos
{
    public class LojaServico : ILojaServico
    {
        private readonly ILojaRepositorio _lojaRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public LojaServico(ILojaRepositorio lojaRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _lojaRepositorio = lojaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }
        public void CriarLoja(LojaRequest loja)
        {
            _lojaRepositorio.CriarLoja(loja);
        }
        public void CriarLojaEUsuario(LojaEUsuarioRequest novaLoja)
        {
            UsuarioRequest usuario = new UsuarioRequest
            {
                Nome = novaLoja.Nome,
                Email = novaLoja.Email,
                Senha = novaLoja.Senha,
                CEP = novaLoja.CEPUsuario,
                Logradouro = novaLoja.LogradouroUsuario,
                Bairro = novaLoja.BairroUsuario,
                UF = novaLoja.UFUsuario,
                Cidade = novaLoja.CidadeUsuario,
                Telefone = novaLoja.Telefone,
                Feirante = novaLoja.Feirante
            };

            var idUsuario = _usuarioRepositorio.CriarUsuarioRetornaIdUsuario(usuario);
            
            LojaRequest loja = new LojaRequest
            {
                NomeLoja = novaLoja.NomeLoja,
                CEP = novaLoja.CEPLoja,
                Logradouro = novaLoja.LogradouroLoja,
                Bairro = novaLoja.BairroLoja,
                UF = novaLoja.UFLoja,
                Cidade = novaLoja.CidadeLoja,
                IdUsuario = idUsuario
            };
            _lojaRepositorio.CriarLoja(loja);
        }
        public Loja ObterLojaPorId(int idLoja)
        {
            Loja loja = _lojaRepositorio.ObterLojaPorId(idLoja);
            return loja;
        }

        public List<Loja> ObterLojaPorIdUsuario(int idUsuario)
        {
            List<Loja> loja = _lojaRepositorio.ObterLojaPorIdUsuario(idUsuario);
            return loja;
        }
        public List<Loja> ObterTodasLojas()
        {
            List<Loja> listaDeLojas = _lojaRepositorio.ObterTodasLojas();
            return listaDeLojas;
        }
        public void AtualizarLoja(Loja novaLoja)
        {
            _lojaRepositorio.AtualizarLoja(novaLoja);
        }       

        public void DeletarLoja(int idLoja)
        {
            _lojaRepositorio.DeletarLoja(idLoja);
        }

    }
}