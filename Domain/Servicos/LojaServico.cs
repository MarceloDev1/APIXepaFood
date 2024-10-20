using Domain.Entidades;
using Domain.Interfaces;

namespace Domain.Servicos
{
    public class LojaServico : ILojaServico
    {
        private readonly ILojaRepositorio _lojaRepositorio;
        public LojaServico(ILojaRepositorio lojaRepositorio)
        {
            _lojaRepositorio = lojaRepositorio;
        }
        public void CriarLoja(Loja loja)
        {
            _lojaRepositorio.CriarLoja(loja);
        }
        public Loja ObterLojaPorId(int idLoja)
        {
            Loja loja = _lojaRepositorio.ObterLojaPorId(idLoja);
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