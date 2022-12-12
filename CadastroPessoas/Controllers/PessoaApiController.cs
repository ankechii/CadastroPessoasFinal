using CadastroPessoas.Controllers;
using CadastroPessoas.ViewModels;
using CadastroPessoas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace cadastropessoas.controllers
{
    [RoutePrefix("api/pessoa")]
    public class PessoaApiController : ApiController
    {
        [Route("verificar-cpf-ja-cadastrado")]
        [HttpGet]
        public IHttpActionResult VerificarCpfJaCadastrado(string cpf)
        {
            cpf = Regex.Replace(cpf, "[^0-9]", string.Empty);
            using (Conexao db = new Conexao())
            {
                bool existecpf = db.Pessoa.Any(c => c.CPF == cpf);
                return Ok(new { resultado = existecpf });
            }
        }
    }
    [RoutePrefix("api/pessoa")]
    public class PessoaApi2Controller : ApiController
    {
        [Route("verificar-email-ja-cadastrado")]
        [HttpGet]
        public IHttpActionResult VerificarEmailJaCadastrado(string email)
        {
            using (Conexao db = new Conexao())
            {
                bool existeemail = db.Pessoa.Any(c => c.Email == email);
                return Ok(new { resultado = existeemail });
            }
        }
    }
}