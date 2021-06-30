using Framework.Domain.Model.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Domain.Model
{
    /// <summary>
    /// reverter as traduçoes das classes acima
    /// </summary>
    public class RegisterViewModel
    {

        public string tipo { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "E-mail/Login")]
        public string email { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "CPF")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Telefone")]
        public string telefone { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "CEP")]
        public string cep { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Endereço")]
        public string endereco { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Número")]
        public string numEndereco { get; set; }

       
        [Display(Name = "Complemento")]
        public string complemento { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Bairro")]
        public string bairro { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Cidade")]
        public string cidade { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string senha { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        public string Confirmarsenha { get; set; }

       


    }
    
    public class RegisterServiceOrderViewModel
    {
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Marca")]
        public string manufacturer { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Mídia")]
        public string midia { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Modelo")]
        public string modelo { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Número de Série")]
        public string serie { get; set; }


        //[Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Tipo de Serviço")]
        public string tipoServico { get; set; }


        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Qual o problema o aparelho aparenta ter?")]
        [DataType(DataType.MultilineText)]
        public string problemaAparelho { get; set; }


        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Quais os arquivos e pastas mais importantes?")]
        [DataType(DataType.MultilineText)]
        public string arquivosImportantes { get; set; }

        
        [Display(Name = "Cliente")]
        public int customer { get; set; }

        public CustomerViewModel customerVM { get; set; }
    }

    public class ROSViewModel
    {
        public RegisterViewModel RegisterViewModel { get;set; }
        public RegisterServiceOrderViewModel RegisterServiceOrderViewModel { get;set; }
    }
}
