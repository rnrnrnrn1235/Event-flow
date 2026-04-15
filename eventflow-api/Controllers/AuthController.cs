using System.Runtime.ExceptionServices;

[ApiController]

[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;
    public AuthController(AuthService auth)
    {
        _auth = auth;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistereDto dto)
    {
        var user = await _auth.RegisterAsync(dto);
        return Ok(user);
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _auth.LoginAsync(dto);
        return Ok( new{token});
    }
    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> getProfile()
    {
        var userId = JwtHelper.GetUserId(User);
        var user = await _auth.getCurrentUserAsync(userId);
        return Ok(user);
    }
}