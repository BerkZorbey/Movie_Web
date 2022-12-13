using Movie_Web.Models.Value_Object;

namespace Movie_Web.Models
{
    public class User : BaseEntity
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool isActivatedEmail { get; set; }
        public Token? Token { get; set; }
    }
}
