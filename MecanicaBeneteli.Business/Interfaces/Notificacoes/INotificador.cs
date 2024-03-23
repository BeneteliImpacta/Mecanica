using MecanicaBeneteli.Business.Notificacoes;

namespace MecanicaBeneteli.Business.Interfaces.Notificacoes
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
