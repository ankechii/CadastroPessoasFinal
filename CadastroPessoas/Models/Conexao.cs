using CadastroPessoas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CadastroPessoas.Models
{
    public class Conexao : DbContext
    {
        public Conexao() : base("CadastroPessoas")
        {

        }
        public DbSet<Pessoa> Pessoa { get; set; }
    }
}