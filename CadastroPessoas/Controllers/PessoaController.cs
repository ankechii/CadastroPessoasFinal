using CadastroPessoas.Models;
using CadastroPessoas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadastroPessoas.Controllers
{
    public class PessoaController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Cadastrar()
        {
            return View();
        }
        public ActionResult Listar()
        {
            using(Conexao db = new Conexao())
            {
                List<Pessoa> pessoasModels = db.Pessoa.ToList();
                List<PessoaViewModel> pessoasVms = new List<PessoaViewModel>();

                foreach (Pessoa item in pessoasModels)
                {
                    PessoaViewModel pessoaVm = new PessoaViewModel();
                    pessoaVm.Nome = item.Nome;
                    pessoaVm.DataNascimento = item.DataNascimento;
                    pessoaVm.Email = item.Email;

                    pessoasVms.Add(pessoaVm);
                }
                       return View(pessoasVms); 
            }
        }

        [HttpPost]
        public ActionResult CadastrarPost(PessoaViewModel dados)
        {  
            using (Conexao db = new Conexao())
            {
                dados.TratarDados();
                dados.Validar();

                Pessoa model = new Pessoa();
                model.Nome = dados.Nome;
                model.DataNascimento = dados.DataNascimento.Value;
                model.Sexo = dados.Sexo;
                model.EstadoCivil = dados.EstadoCivil;
                model.CPF = dados.CPF;
                model.CEP = dados.CEP;
                model.Endereco = dados.Endereco;
                model.Numero = dados.Numero;
                model.Complemento = dados.Complemento;
                model.Bairro = dados.Bairro;
                model.Cidade = dados.Cidade;
                model.UF = dados.UF;
                model.Email = dados.Email;
                model.Senha = dados.Senha = BCrypt.Net.BCrypt.HashPassword(dados.Senha);
                model.Celular = dados.Celular;

                db.Pessoa.Add(model);
                db.SaveChanges();
            }

            return RedirectToAction("Home");
        }
    }
}