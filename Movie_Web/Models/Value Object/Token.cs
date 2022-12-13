namespace Movie_Web.Models.Value_Object
{
    public class Token
    {
        public string? AccessToken { get; set; }
        public DateTime TokenExpiration { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
