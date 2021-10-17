using ExercicioCrudBanco.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExercicioCrudBanco.Model
{
    public class EmpresaEntity
    {
        //propriedades da tabela
        public int Id { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public string CNPJ { get; private set; }
        public string Endereco { get; private set; }
        public string Telefone { get; private set; }

        //construtores da tabela
        public EmpresaEntity()
        {

        }

        public EmpresaEntity(int id,string razaoSocial, string nomeFantasia, string cNPJ, string endereco, string telefone)
        {
            Id = id;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            CNPJ = cNPJ;
            Endereco = endereco;
            Telefone = telefone;
        }

        //metodo para cadastrar dados 
        internal bool Gravar()
        {
            EmpresaController controller = new EmpresaController(); //instancia a classe EmpresaController
            return controller.Gravar(this);//chama o metodo gravar que esta dentro da classe EmpresaController passando as propriedades dessa classe  "this"
        }

        //metodo para alterar dados
        internal bool Alterar(string id)
        {
            EmpresaController controller = new EmpresaController();//instancia a classe EmpresaController
            return controller.AlterarEmpresa(int.Parse(id), this);//chama o metodo alterar que esta dentro da classe EmpresaController passando o id que esta na label Indice e as propriedades dessa classe  "this"
        }
    }
}
