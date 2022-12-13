using System.ComponentModel.DataAnnotations;

namespace Movie_Web.Models.ApiResponse
{
    public class RegisterApiResponse : BaseEntity
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
