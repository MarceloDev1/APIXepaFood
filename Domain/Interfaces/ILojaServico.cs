using Domain.Entidades;
using Domain.Requests;

namespace Domain.Interfaces
{
    public interface ILojaServico
    {
        void CriarLoja(LojaRequest loja);
        void CriarLojaEUsuario(LojaEUsuarioRequest novaLoja);
        Loja ObterLojaPorId(int idLoja);
        List<Loja> ObterLojaPorIdUsuario(int idUsuario);
        List<Loja> ObterTodasLojas();
        void DeletarLoja(int idLoja);
        void AtualizarLoja(Loja novaLoja);
    }
}
