using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using LoginToken.Models.Custom;
using LoginToken.Services;





namespace LoginToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IAutorizacionService autorizacionService;

        public UsuarioController(IAutorizacionService autorizacionService)
        {
            this.autorizacionService = autorizacionService;
        }


        [HttpPost]
        [Route("/Autenticar")]
        public async Task<IActionResult> Autentificar([FromBody]AutorizacionRequest autorizacion)
        {


            string Noencontrado = "Credenciales incorrectas";
            var resultado_autentificacion = await autorizacionService.DevolverToken(autorizacion);


            if (resultado_autentificacion == null)
                return Unauthorized(Noencontrado);
            


            return Ok(resultado_autentificacion);

        }
        




    }
}
