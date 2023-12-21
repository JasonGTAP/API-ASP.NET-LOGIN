namespace LoginToken.Models.Custom
{
    public class AutorizacionResponse
    {

        public string Token { get; set; }
        public bool Resultado { get; set; }


        public string Msg { get; set; }
        public string Rol {  get; set; }
    }
}
