using CadastroPessoas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


namespace CadastroPessoas.ViewModels
{
    public class PessoaViewModel
    {
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string DataNascimentoFormatada
        {
            get
            {
                return string.Format("{0:dd/MM/yyyy}", DataNascimento);
            }
        }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string CPF { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Celular { get; set; }

        public void Validar()
        {
            DateTime dataAtual = DateTime.Now.Date;
            DateTime dataMinima = new DateTime(1940, 1, 1);

            if (string.IsNullOrWhiteSpace(Nome))
                throw new ApplicationException("O campo Nome é obrigatório");
            if (Nome.Length > 200)
                throw new ApplicationException("O campo Nome só pode conter 200 caracteres");

            if (DataNascimento == null)
                throw new ApplicationException("O campo DataNascimento é obrigatório");
            if (DataNascimento > dataAtual)
                throw new ApplicationException(string.Format("O campo DataNascimento não pode ser maior que a data de hoje {0:dd/MM/yyyy}", dataAtual));
            if (DataNascimento < dataMinima)
                throw new ApplicationException(string.Format("O campo DataNascimento não pode ser menor que a data de hoje {0:dd/MM/yyyy}", dataMinima));

            if (string.IsNullOrWhiteSpace(Sexo))
                throw new ApplicationException("O campo Sexo é obrigatório");
            if (Sexo.Length > 1)
                throw new ApplicationException("O campo Sexo só pode conter 1 caracteres");

            if (string.IsNullOrWhiteSpace(EstadoCivil))
                throw new ApplicationException("O campo EstadoCivil é obrigatório");
            if (EstadoCivil.Length > 20)
                throw new ApplicationException("O campo EstadoCivil só pode conter 20 caracteres");

            if (string.IsNullOrWhiteSpace(CPF))
                throw new ApplicationException("O campo CPF é obrigatório");
            if (CPF.Length != 11)
                throw new ApplicationException("O campo CPF deve conter 11 caracteres");
            if (!ValidarCPF(CPF))
                throw new ApplicationException("CPF inválido");
            using (Conexao db = new Conexao())
            {
                if (db.Pessoa.Any(c => c.CPF == CPF))
                    throw new ApplicationException("CPF já cadastrado");
            }
            if (string.IsNullOrWhiteSpace(CEP))
                throw new ApplicationException("O campo CEP é obrigatório");
            if (CEP.Length != 8)
                throw new ApplicationException("O campo CEP deve conter 8 caracteres");

            if (string.IsNullOrWhiteSpace(Endereco))
                throw new ApplicationException("O campo Endereco é obrigatório");
            if (Endereco.Length > 100)
                throw new ApplicationException("O campo Endereco só pode conter 100 caracteres");

            if (string.IsNullOrWhiteSpace(Numero))
                throw new ApplicationException("O campo Numero é obrigatório");
            if (Numero.Length > 10)
                throw new ApplicationException("O campo Numero só pode conter 10 caracteres");

            if (!string.IsNullOrWhiteSpace(Complemento))
            {
                if (Complemento.Length > 30)
                    throw new ApplicationException("O campo Complemento só pode conter 30 caracteres");
            }

            if (string.IsNullOrWhiteSpace(Bairro))
                throw new ApplicationException("O campo Bairro é obrigatório");
            if (Bairro.Length > 50)
                throw new ApplicationException("O campo Bairro só pode conter 50 caracteres");

            if (string.IsNullOrWhiteSpace(Cidade))
                throw new ApplicationException("O campo Cidade é obrigatório");
            if (Cidade.Length > 50)
                throw new ApplicationException("O campo Cidade só pode conter 50 caracteres");

            if (string.IsNullOrWhiteSpace(UF))
                throw new ApplicationException("O campo UF é obrigatório");
            if (UF.Length > 2)
                throw new ApplicationException("O campo UF só pode conter 2 caracteres");
        }
        public void TratarDados()
        {
            Nome = Nome?.ToUpper().Trim();
            CPF = Regex.Replace(CPF, "[^0-9]", string.Empty);
            CEP = Regex.Replace(CEP, "[^0-9]", string.Empty);
            Endereco = Endereco?.ToUpper().Trim();
            Numero = Numero?.ToUpper().Trim();
            Complemento = Complemento?.ToUpper().Trim();
            Bairro = Bairro?.ToUpper().Trim();
            Cidade = Cidade?.ToUpper().Trim();
            Email = Email?.Trim();
        }
        public bool ValidarCPF(string cpf)
        {
            string valor = cpf.Replace(".", "");
            valor = valor.Replace("-", "");
            if (valor.Length != 11)
                return false;
            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;
            if (igual || valor == "12345678909")
                return false;
            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                  valor[i].ToString());
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;
            return true;

        }
    }
}