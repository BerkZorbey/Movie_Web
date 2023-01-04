namespace Movie_Web.Models.ApiResponse
{
    public class LoginApiResponse : BaseEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
