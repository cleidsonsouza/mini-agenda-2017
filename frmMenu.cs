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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void contatosToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            new frmCadastroContatos().ShowDialog();

            // Também pode ser feito da seguinte forma:
            // frmCadastroContatos cc = new frmCadastroContatos();
            // cc.ShowDialog();
            // ou:
            // frmCadastroContatos cc = new frmCadastroContatos();
            // cc.Show();
        }               

        private void contatosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmPesquisaContatos().ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            toolStripStatusLabel2.Text = DateTime.Now.ToString("dd/MM/yyyy");            
            toolStripStatusLabel3.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {            
            timer1.Start();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new frmCadastroContatos().ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            new frmPesquisaContatos().ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
