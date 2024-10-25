using Domain.Entidades;
using Domain.Requests;

namespace Domain.Interfaces
{
    public interface ILojaRepositorio
    {
        void CriarLoja(LojaRequest loja);
        Loja ObterLojaPorId(int idLoja);
        List<Loja> ObterLojaPorIdUsuario(int idUsuario);
        List<Loja> ObterTodasLojas();
        void DeletarLoja(int idLoja);
        void AtualizarLoja(Loja novaLoja);
    }
}
