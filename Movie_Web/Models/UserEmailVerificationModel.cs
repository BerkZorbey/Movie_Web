using Movie_Web.Models.Value_Object;

namespace Movie_Web.Models
{
    public class UserEmailVerificationModel : BaseEntity
    {
        public string? UserId { get; set; }
        public Token? EmailVerificationToken { get; set; }
    }
}
