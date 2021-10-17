using ExercicioCrudBanco.Controller;
using ExercicioCrudBanco.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExercicioCrudBanco
{
    public partial class Tela_Inicio : Form
    {
        public Tela_Inicio()
        {
            InitializeComponent(); 
            lbIndice.Visible = false;
            btnCadastrar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        //metodo utilizado para cadastrar empresa
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCNPJ.Text != "  .   .   /    -") //verifica se o campo cnpj não esta vazio
            {
                if (!String.IsNullOrEmpty(txtRazao.Text))//verifica se o campo razao social não esta vazio
                {
                    if (!String.IsNullOrEmpty(txtNomeF.Text))//verifica se o campo nome fantasia não esta vazio
                    {
                        if (!String.IsNullOrEmpty(txtEndereco.Text))//verifica se o campo endereço não esta vazio
                        {
                            if (txtTelefone.Text != "(  )     -") //verifica se o campo telefone não esta vazio
                            {
                                //intancia a classe EmpresaEntity criando um objeto empresa e passando os campos verificados acima como parametro, foi utilizado ToUpper() para gravar os dados em maiusculo
                                EmpresaEntity empresa = new EmpresaEntity(0, txtRazao.Text.ToUpper(), txtNomeF.Text.ToUpper(), txtCNPJ.Text.ToUpper(), txtEndereco.Text.ToUpper(), txtTelefone.Text);
                                var resultado = empresa.Gravar();//chama o metodo gravar que esta na classe EmpresaEntity e a variavel resultado recebe o resultado bolleano da do metodo
                                if (resultado != false)//se resultado for igual a verdadeiro entra no if
                                {
                                    MessageBox.Show("Cadastro realizado com sucesso!", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtCNPJ.Clear(); 
                                    txtRazao.Clear();
                                    txtNomeF.Clear();
                                    txtEndereco.Clear();
                                    txtTelefone.Clear();
                                    txtCNPJ.Focus();
                                    btnCadastrar.Enabled = true;
                                    btnAlterar.Enabled = false;
                                    btnExcluir.Enabled = false;
                                }
                                else
                                    MessageBox.Show("Erro ao cadastrar!", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("Verifique o campo Telefone!");
                        }
                        else
                            MessageBox.Show("Verifique o campo Endereço!");
                    }
                    else
                        MessageBox.Show("Verifique o campo Nome Famtasia!");
                }
                else
                    MessageBox.Show("Verifique o campo Razão Social!");
            }
            else
                MessageBox.Show("Verifique o campo CNPJ!");
        }
        //metodo para buscar informações baseado no campo cnpj
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (txtCNPJ.Text != "  .   .   /    -")//verifica se campo cnpj esta vazio
            {
                EmpresaController controller = new EmpresaController(); // instancia classe EmpresaControler criando o objeto controller
                DataTable resultado = controller.BuscarEmpresa(txtCNPJ.Text);//cria um objeto tabela(resultado) e chama metodo BuscarEmpresa da classe EmpresaController passando o CNPJ como parametro
                if (resultado.Rows.Count > 0)
                {
                    //preenche os textBox com as informações capturadas na tabela resultado 
                    txtRazao.Text = resultado.Rows[0]["RazaoSocial"].ToString();
                    txtNomeF.Text = resultado.Rows[0]["NomeFantasia"].ToString();
                    txtEndereco.Text = resultado.Rows[0]["Endereco"].ToString();
                    txtTelefone.Text = resultado.Rows[0]["Telefone"].ToString();
                    lbIndice.Text = resultado.Rows[0]["Id"].ToString();
                    btnCadastrar.Enabled = false;
                    btnAlterar.Enabled = true;
                    btnExcluir.Enabled = true;
                }
                else
                {
                    MessageBox.Show("CNPJ não encontrado!", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRazao.Clear();
                    txtNomeF.Clear();
                    txtEndereco.Clear();
                    txtTelefone.Clear();
                    txtRazao.Focus();
                    btnCadastrar.Enabled = true;
                    btnAlterar.Enabled = false;
                    btnExcluir.Enabled = false;
                }
            }
            else
                MessageBox.Show("Preencha o campo do CNPJ!", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        //metodo para excluir registro do banco de dados
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            EmpresaController controller = new EmpresaController();
            if (int.Parse(lbIndice.Text) >= 0)
            {
                if (MessageBox.Show("Deseja realmente excluir esse registro", "Mensagem de Confirmação!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    if (controller.ExcluirEmpresa(int.Parse(lbIndice.Text)))
                    {
                        MessageBox.Show("Empresa Excluida!", "Mensagem do Sistema");
                        txtCNPJ.Clear();
                        txtRazao.Clear();
                        txtNomeF.Clear();
                        txtEndereco.Clear();
                        txtTelefone.Clear();
                        txtCNPJ.Focus();
                        btnCadastrar.Enabled = true;
                        btnAlterar.Enabled = false;
                        btnExcluir.Enabled = false;
                    }

                    else
                        MessageBox.Show("Não foi possivel excluir!", "Mensagem do Sistema");

            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtCNPJ.Text != "  .   .   /    -")
            {
                if (!String.IsNullOrEmpty(txtRazao.Text))
                {
                    if (!String.IsNullOrEmpty(txtNomeF.Text))
                    {
                        if (!String.IsNullOrEmpty(txtEndereco.Text))
                        {
                            if (txtTelefone.Text != "(  )     -")
                            {
                                EmpresaEntity empresa = new EmpresaEntity(0, txtRazao.Text.ToUpper(), txtNomeF.Text.ToUpper(), txtCNPJ.Text.ToUpper(), txtEndereco.Text.ToUpper(), txtTelefone.Text);
                                var resultado = empresa.Alterar(lbIndice.Text);
                                if (resultado != false)
                                    MessageBox.Show("Alteração de registro realizada com sucesso!", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Erro ao alterar registro!", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("Verifique o campo Telefone!");
                        }
                        else
                            MessageBox.Show("Verifique o campo Endereço!");
                    }
                    else
                        MessageBox.Show("Verifique o campo Nome Famtasia!");
                }
                else
                    MessageBox.Show("Verifique o campo Razão Social!");
            }
            else
                MessageBox.Show("Verifique o campo CNPJ!");

        }

    }
}
