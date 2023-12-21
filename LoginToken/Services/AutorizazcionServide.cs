using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginToken.Models;
using LoginToken.Models.Custom;
using Microsoft.Extensions.Caching;
using Microsoft.AspNetCore.Mvc;

namespace LoginToken.Services
{
    public class AutorizazcionServide : IAutorizacionService
    {
        private readonly UsuDbContext context;
        private readonly IConfiguration configuration;

        public AutorizazcionServide(UsuDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }



        private string GenerarToken(String idUsuario) { 
        
            
            var key = configuration.GetValue<String>("JwtSetting:key");

            var KeyBytes= Encoding.ASCII.GetBytes(key);


            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(KeyBytes),
                SecurityAlgorithms.HmacSha256Signature
                
      
                );


            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject=claims,
                Expires=DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credencialesToken


            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);


            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;


        }













        //validar que el usuario existe en la base de datos para generar el token

        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
           

            var usuario_encontrado = context.Usuarios.FirstOrDefault(x =>
            x.Nombre == autorizacion.Email &&
            x.Clave ==autorizacion.Clave
           
            
            
            
            );


            if (usuario_encontrado == null) {


                return await Task.FromResult<AutorizacionResponse>(null);
            }

            string tokenCreado = GenerarToken(usuario_encontrado.IdUsuario.ToString());
            string rol = usuario_encontrado.Rol;
            return new AutorizacionResponse() { Token = tokenCreado, Resultado = true, Msg = "usuario encontrado",Rol =rol };


        }
    }
}
