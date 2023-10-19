using API_ADOPTAPATAS_3.Dtos;
using API_ADOPTAPATAS_3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ADOPTAPATAS_3.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly JwtSettingsDto _jwtSettingsDto;

        public UserController(JwtSettingsDto jwtSettingsDto)
        {
            _jwtSettingsDto = jwtSettingsDto;


        }
        
        [HttpPost("/login")]
        public async Task<IActionResult> InicioSesion([FromBody] RequestLoginDto request)
        {
            UserService userService = new UserService(_jwtSettingsDto);
            if (!ModelState.IsValid)
            {
                return BadRequest("Respuesta invalida- Invalid request");
            }
            return Ok(userService.InicioSesion(request));
        }
        [HttpPost("/registro")]
        public async Task<IActionResult> RegistroUsuario([FromBody] RequestRegisterDto registroDto)
        {
            UserService userService = new UserService(_jwtSettingsDto);

            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud de registro no válida");
            }

            return Ok(userService.CreacionUsuario(registroDto));
        }

        /*
                // GET: api/Login
                [HttpGet]
                public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
                {
                  if (_context.Logins == null)
                  {
                      return NotFound();
                  }
                    return await _context.Logins.ToListAsync();
                }

                // GET: api/Login/5
                [HttpGet("{id}")]
                public async Task<ActionResult<Login>> GetLogin(int id)
                {
                  if (_context.Logins == null)
                  {
                      return NotFound();
                  }
                    var login = await _context.Logins.FindAsync(id);

                    if (login == null)
                    {
                        return NotFound();
                    }

                    return login;
                }

                // PUT: api/Login/5
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPut("{id}")]
                public async Task<IActionResult> PutLogin(int id, Login login)
                {
                    if (id != login.IdLogin)
                    {
                        return BadRequest();
                    }

                    _context.Entry(login).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!LoginExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return NoContent();
                }

                // POST: api/Login
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPost]
                public async Task<ActionResult<Login>> PostLogin(Login login)
                {
                  if (_context.Logins == null)
                  {
                      return Problem("Entity set 'BdadoptapatasContext.Logins'  is null.");
                  }
                    _context.Logins.Add(login);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        if (LoginExists(login.IdLogin))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return CreatedAtAction("GetLogin", new { id = login.IdLogin }, login);
                }

                // DELETE: api/Login/5
                [HttpDelete("{id}")]
                public async Task<IActionResult> DeleteLogin(int id)
                {
                    if (_context.Logins == null)
                    {
                        return NotFound();
                    }
                    var login = await _context.Logins.FindAsync(id);
                    if (login == null)
                    {
                        return NotFound();
                    }

                    _context.Logins.Remove(login);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }

                private bool LoginExists(int id)
                {
                    return (_context.Logins?.Any(e => e.IdLogin == id)).GetValueOrDefault();
                }*/   
    }
}
