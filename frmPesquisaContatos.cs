using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniAgenda
{
    public partial class frmPesquisaContatos : Form
    {
        public frmPesquisaContatos()
        {
            InitializeComponent();
        }

        // Método preencher o DataGrid
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            try
            {
                clsBancoDados.Conectar();

                // Seleciona todos os contatos que começam com a letra contida em txtNome e terminadas com qualquer coisa
                ds = clsBancoDados.Consultar("SELECT * FROM contatos WHERE nome like '" + txtNome.Text + "%' ");

                // O DataGrid gdvContatos recebe o contéúdo da tabela 1 do DataSet ds que contém o resultado da consulta SQL
                gdvContatos.DataSource = ds.Tables[0];

                // Verifica se a tabela está vazia
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Nenhum registro encontrado!");
                    txtNome.Text = "";
                    txtNome.Focus();
                }
            }
            finally
            {
                clsBancoDados.Desconectar();
            }
        }
    }
}
