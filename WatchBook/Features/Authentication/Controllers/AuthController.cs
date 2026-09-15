using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchBook.Application.Features.Authentication.Services;

namespace WatchBook.Web.Features.Authentication.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(
    IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid email",
                detail: "Email is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid password",
                detail: "Password is required.");
        }

        await authService.RegisterAsync(
            request.Email,
            request.Password,
            request.DisplayName,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "User registered successfully."
        });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email)
            || string.IsNullOrWhiteSpace(request.Password))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid credentials",
                detail: "Email and password are required.");
        }

        await authService.LoginAsync(
            request.Email,
            request.Password,
            request.RememberMe,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Login successful."
        });
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(
        CancellationToken cancellationToken)
    {
        await authService.LogoutAsync(cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Logout successful."
        });
    }

    public sealed record RegisterRequest(
        string Email,
        string Password,
        string? DisplayName);

    public sealed record LoginRequest(
        string Email,
        string Password,
        bool RememberMe = false);
}