using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Models;

namespace UsuariosAPI.Data
{
    public class UsuarioContext:IdentityDbContext<Usuario>
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> opts):base(opts)
        {
            
        }

    }
}
