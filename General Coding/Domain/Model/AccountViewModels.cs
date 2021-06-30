using Framework.Domain.Model.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Domain.Model
{

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        //[EmailAddress(ErrorMessage = "The {0} field is not a valid {0} address.")]
        [Display(Name = "Email")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Stay connected?")]
        public bool RememberMe { get; set; }
    }

    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
            ativo = true;
        }

        [Key]
        [Display(Name = "Código")]
        public string id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(60, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Display(Name = "Name")]
        public string nome { get; set; }

        [StringLength(16, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Display(Name = "CPF")]
        public string cpf { get; set; }

        [Phone]
        [StringLength(18, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Display(Name = "Phone")]
        public string telefone { get; set; }

        [Phone]
        [StringLength(18, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Display(Name = "Cell phone")]
        public string celular { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(15, ErrorMessage = "The field {0} must contain at least {2} and at most {1} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password confirmation is not correct.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// URL File
        /// Url do Arquivo do Documento
        /// </summary>
        [Display(Name = "File")]
        public string url_file { get; set; }

        [Display(Name = "Active")]
        public bool ativo { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Date register")]
        [ScaffoldColumn(false)]
        public DateTime? dt_cadastro { get; set; }

        [Display(Name = "Perfil")]
        public string perfil { get; set; }

        [Display(Name = "Resend Password Mail")]
        public bool resendMail { get; set; }

        [Display(Name="Token To Reset Password")]
        public string tokenCode { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int location_id { get; set; }

        public virtual LocationsViewModel location { get; set; }
        
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(15, ErrorMessage = "The field {0} must contain at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password confirmation is not correct.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

        public bool isError { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(15, ErrorMessage = "The field {0} must contain at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(15, ErrorMessage = "The field {0} must contain at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NovaSenha", ErrorMessage = "The password confirmation is not correct.")]
        public string ConfirmarSenha { get; set; }
    }
}
