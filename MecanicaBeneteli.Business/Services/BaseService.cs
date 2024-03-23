using MecanicaBeneteli.Business.Interfaces.Notificacoes;
using MecanicaBeneteli.Business.Notificacoes;
using MecanicaBeneteli.Business.Models;
using FluentValidation;


namespace MecanicaBeneteli.Business.Services
{
    public abstract class BaseService
    {

        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            foreach (var error in validator.Errors)
                Notificar(error.ErrorMessage);

            return false;
        }
        protected bool TemNotificacao()
        {
            return _notificador.TemNotificacao();
        }
    }
}
