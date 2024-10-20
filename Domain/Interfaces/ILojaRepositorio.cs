using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface ILojaRepositorio
    {
        void CriarLoja(Loja loja);
        Loja ObterLojaPorId(int idLoja);
        List<Loja> ObterTodasLojas();
        void DeletarLoja(int idLoja);
        void AtualizarLoja(Loja novaLoja);
    }
}
