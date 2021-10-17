using ExercicioCrudBanco.DAO;
using ExercicioCrudBanco.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ExercicioCrudBanco.Controller
{
    public class EmpresaController
    {
        //metodo para gravar no banco de dados retorno variavel booleana
        public bool Gravar(EmpresaEntity empresa)
        {
            int id = 0; //variavel iniciando id com 0
            BancoInstance banco; // variavel para instanciar banco
            using (banco = new BancoInstance()) //cast instanciando banco 
            {
                bool ok = false;//inicia a variavel como false

                //executa a query no banco e retorna se true se der certo.
                ok = banco.Banco.ExecuteNonQuery(@"insert into Empresa(RazaoSocial, NomeFantasia, CNPJ, Endereco, Telefone) 
                values(@1, @2, @3, @4, @5)", "@1", empresa.RazaoSocial, "@2", empresa.NomeFantasia, "@3", empresa.CNPJ, "@4", empresa.Endereco, "@5", empresa.Telefone);
                if (ok && id == 0)// se ok for verdadeiro e id for igual a 0
                    id = banco.Banco.GetIdentity();//id recebe indice da linha 
                return ok;//retorna verdadeiro ou falso
            }
        }

        //metodo para retornar se a pessoa esta cadastrada no branco 
        public DataTable BuscarEmpresa(string cnpj)
        {
            DataTable dtResultado = new DataTable(); //cria uma variavel do tipo tabela
            BancoInstance banco;//variavel paa instanciar o banco
            using (banco = new BancoInstance())//cast que instancia o banco
            {
                //executa uma query no banco e armazena o resultado na variavel tabela dtResultado
                banco.Banco.ExecuteQuery(@"select * from Empresa where CNPJ = @1", out dtResultado, "@1", cnpj);
                return dtResultado;//retorna a tabela dtResultado
            }
        }
        //metodo para excluir uma pessoa do banco
        public bool ExcluirEmpresa(int id)
        {

            BancoInstance banco;//variavel para instanciar banco
            using (banco = new BancoInstance())//cast que inicia o banco
            {
                bool ok = false;//variavel boleana 
                //executa a query e retorna verdadeiro ou falso se conseguiu excluir ou não empresa do banco
                return ok = banco.Banco.ExecuteNonQuery(@"delete from Empresa where Id = @1", "@1", id);
            }
        }
        public bool AlterarEmpresa(int id,EmpresaEntity empresa)
        {

            BancoInstance banco;//variavel para instanciar banco
            using (banco = new BancoInstance())//cast que inicia o banco
            {
                bool ok = false;//variavel boleana 
                //executa a query e retorna verdadeiro ou falso se conseguiu alterar ou não dados do banco
                return ok = banco.Banco.ExecuteNonQuery(@"update Empresa set RazaoSocial = @1, NomeFantasia = @2, Endereco = @3, Telefone = @4 where Id = @5",
                    "@1",empresa.RazaoSocial,"@2",empresa.NomeFantasia,"@3",empresa.Endereco,"@4",empresa.Telefone,"@5",id);

            }
        }

    }
}
