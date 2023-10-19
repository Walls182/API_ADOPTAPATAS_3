using API_ADOPTAPATAS_3.Dtos;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Utility;

namespace API_ADOPTAPATAS_3.Repositories.Repository
{
    public class LoginRepository
    {


        

                public bool validarUsuario(RequestLoginDto loginDto)
                {
                    Encrip _encrip = new Encrip();
                    BdadoptapatasContext _dbContext = new BdadoptapatasContext();

                    var passs = _encrip.HashPassword(loginDto.Contrasena);
                    var usuario = _dbContext.Logins.FirstOrDefault(u => u.Usuario == loginDto.Usuario);


                     //Verifica si usuario o contrasena son nulos
                    if (usuario == null || usuario.Contrasena != passs)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
    }
}
