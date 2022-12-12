using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadastroPessoas.Models
{
    [Table("Pessoa")]
    public class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(100)]
        public string Sexo { get; set; }

        [Required]
        [StringLength(100)]
        public string EstadoCivil { get; set; }

        [Required]
        [StringLength(100)]
        public string CPF { get; set; }

        [Required]
        [StringLength(100)]
        public string CEP { get; set; }

        [Required]
        [StringLength(100)]
        public string Endereco { get; set; }

        [Required]
        [StringLength(100)]
        public string Numero { get; set; }

        [StringLength(100)]
        public string Complemento { get; set; }

        [Required]
        [StringLength(100)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(100)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(100)]
        public string UF { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Senha { get; set; }
        
        [StringLength(100)]
        public string Celular { get; set; }

    }
}