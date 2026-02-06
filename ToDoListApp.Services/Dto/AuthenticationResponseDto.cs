namespace ToDoListApp.Services.Dto
{
    public class AuthenticationResponseDto
    {
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public string Token { get; set; } = null!;
    }
}
