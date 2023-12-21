using System;
using System.Collections.Generic;

namespace LoginToken.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
