using System.ComponentModel.DataAnnotations;

namespace Drones.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}