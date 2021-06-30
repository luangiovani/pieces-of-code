using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Domain.Model.CBL
{
    public class LocalMicrocomputadorViewModel
    {
        [Key]
        [Display(Name = "ID")]
        public int idLocalMicrocomputador { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição é obrigatório")]
        [StringLength(128, ErrorMessage = "O Campo {0} não deve exceder os {1} caracteres.")]
        public string descricao { get; set; }
        public DateTime dataCadastro { get; set; }
        [Display(Name = "Status")]
        public bool indAtivo { get; set; }

        public string userRegistration_id { get; set; }
        //[Display(Name = "ServiceOrderClouds")]
        public virtual ICollection<ServiceOrderCloud> serviceOrderClouds { get; set; }

        public static implicit operator LocalMicrocomputador(LocalMicrocomputadorViewModel obj)
        {

            return new LocalMicrocomputador
            {
                idLocalMicrocomputador = obj.idLocalMicrocomputador,
                descricao = obj.descricao,
                dataCadastro = obj.dataCadastro,
                indAtivo = obj.indAtivo,
                userRegistration_id = obj.userRegistration_id
            };
        }

        public static implicit operator LocalMicrocomputadorViewModel(LocalMicrocomputador obj)
        {

            return new LocalMicrocomputadorViewModel
            {
                idLocalMicrocomputador = obj.idLocalMicrocomputador,
                descricao = obj.descricao,
                dataCadastro = obj.dataCadastro,
                indAtivo = obj.indAtivo,
                userRegistration_id = obj.userRegistration_id
            };
        }
    }
}
