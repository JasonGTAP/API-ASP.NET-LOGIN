using LoginToken.Models.Custom;



namespace LoginToken.Services
{
    public interface IAutorizacionService
    {

        Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);







    }
}
