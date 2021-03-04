using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace MiniAgenda
{
    public partial class frmCadastroContatos : Form
    {
        clsContatos c = new clsContatos();      

        public frmCadastroContatos()
        {
            InitializeComponent();
        }

        // Método para limpar todos os campos
        public void LimparCampos()
        {
            txtCodigo.Text = "";
            txtNome.Text = "";
            txtTelefone.Text = "";
        }

        // Método para buscar o próximo código do banco
        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                clsBancoDados.Conectar();

                DataSet ds = new DataSet();

                // Essa sql seleciona o codigo em ordem decrescente, ou seja, do maior para o menor com o limite de apenas um resultado
                // com a intenção obter o maior código já cadastrado
                ds = clsBancoDados.Consultar("SELECT * FROM contatos ORDER BY codigo DESC LIMIT 1");

                // Converte para string o que está contido na tabela 0, linha 0 e coluna 0 (ds.Tables[0].Rows[0][0].ToString())
                // Converte o resultado para inteiro e soma-se 1 e, por fim, converte-se tudo para string e exibe em txtCodigo
                txtCodigo.Text = Convert.ToString(Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar ler do banco:\n" + ex.Message);
            }
            finally
            {
                clsBancoDados.Desconectar();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            int cod;

            // Tenta converter o conteúdo de txtCodigo para inteiro
            if (int.TryParse(txtCodigo.Text, out cod))
            {
                // Os atributos do objeto c do tipo clsContatos recebam os valores contidos nos campos de texto
                c.Codigo = cod;
                c.Nome = txtNome.Text;
                c.Telefone = txtTelefone.Text;

                // Tenta inserir (salvar) ou atualizar um cadastro
                try
                {
                    clsBancoDados.Conectar();

                    // Verifica se o cadastro já existe
                    if (c.VerificarCodigo() > 0)
                    {
                        // Atualiza o cadastro
                        c.Atualizar();
                        MessageBox.Show("Dados atualizados com sucesso!");
                    }
                    else
                    {
                        // Salva o cadastro
                        c.Salvar();
                        MessageBox.Show("Dados salvos com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao tentar salvar os dados:\n" + ex.Message);
                }
                finally
                {
                    clsBancoDados.Desconectar();
                }

                // Invoca o método limpar campos após o término da tarefa
                LimparCampos();
                // Coloca o foco (cursor) no campo txtCodigo
                txtCodigo.Focus();
            }
            // Se não conseguir converter o conteúdo de txtCodigo para inteiro (ex.: caracteres especiais ou letras)
            else
            {
                MessageBox.Show("Código inválido!");
                txtCodigo.Focus();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int cod;

            if (int.TryParse(txtCodigo.Text, out cod))
            {
                c.Codigo = cod;

                try
                {
                    clsBancoDados.Conectar();

                    if (c.VerificarCodigo() > 0)
                    {
                        c.Excluir();
                        MessageBox.Show("Dados excluídos com sucesso!");
                    }
                    else
                        MessageBox.Show("Código Inexistente!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao tentar excluir os dados:\n" + ex.Message);
                }
                finally
                {
                    clsBancoDados.Desconectar();
                }

                LimparCampos();
                txtCodigo.Focus();
            }
            else
            {
                MessageBox.Show("Código inválido!");
                txtCodigo.Focus();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            int cod;

            if (int.TryParse(txtCodigo.Text, out cod))
            {
                c.Codigo = cod;

                try
                {
                    clsBancoDados.Conectar();

                    DataSet ds = new DataSet();
                    ds = clsBancoDados.Consultar("SELECT * FROM contatos WHERE codigo = '" + txtCodigo.Text + "'");

                    // Verifica se ds não é nulo
                    if (ds != null)
                        // Verifica se existe resultado na consulta (se o número de linhas obtidos na consulta é maior que zero)
                        if (ds.Tables[0].Rows.Count > 0)
                            // Percorre todas as linhas da tabela 0 (obtida durante a consulta) - Uma linha a cada loop
                            // No nosso caso a consulta só retornará uma linha que é a de código igual a txtCodigo.Text
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                // O conteúdo da coluna 1 (dr[1]) da tabela 0 (table[0]) é recebido por txtNome
                                txtNome.Text = dr[1].ToString();
                                // O conteúdo da coluna 2 (dr[2]) da tabela 0 (table[0]) é recebido por txtNome
                                txtTelefone.Text = dr[2].ToString();
                            }
                        else
                        {
                            txtNome.Text = "";
                            txtTelefone.Text = "";
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao tentar ler do banco:\n" + ex.Message);
                }
                finally
                {
                    clsBancoDados.Desconectar();
                }
            }
            else
            {
                LimparCampos();
            }
        }

        private void txtTelefone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }
    }
}