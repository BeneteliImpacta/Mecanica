namespace MecanicaBeneteli.WebApp.Extensions.Exceptions
{
    public class ErroInternoApiException : Exception
    {
        public ErroInternoApiException() { }

        public ErroInternoApiException(string message, Exception innerException) : base(message, innerException) { }
    }
}
