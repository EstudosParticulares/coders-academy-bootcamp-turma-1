using System;
using System.ComponentModel.DataAnnotations;

namespace CodersAcademy.API.ViewModel.Request
{
    public class SignRequest
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }
    }
}
