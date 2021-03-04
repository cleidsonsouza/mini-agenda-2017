using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MySql.Data.MySqlClient;

namespace MiniAgenda
{
    class clsContatos
    {
        public int    Codigo   { get; set; }
        public string Nome     { get; set; }
        public string Telefone { get; set; }

        public void Salvar()
        {                          
            string sql = "INSERT INTO contatos VALUES (@codigo, @nome, @telefone)";

            // MySqlCommand representa uma instrução ou procedimento armazenado para ser executado contra um banco de dados MySql
            MySqlCommand cmd = new MySqlCommand(sql, clsBancoDados.con);

            // Adiciona-se o parametro, indicando o nome e o tipo e atribuindo o respectivo valor
            cmd.Parameters.Add("@codigo", MySqlDbType.Int32).Value = Codigo;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = Nome;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = Telefone;

            // Tenta executar o comando
            try
            {
                // Invoca o método estático ExecutarComando() contido em clsBancoDados
                clsBancoDados.ExecutarComando(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Excluir()
        {
            string sql = "DELETE FROM contatos WHERE codigo = @codigo";
            
            MySqlCommand cmd = new MySqlCommand(sql, clsBancoDados.con);

            cmd.Parameters.Add("@codigo", MySqlDbType.Int32).Value = Codigo;

            try
            {
                clsBancoDados.ExecutarComando(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Atualizar() 
        {
            string sql = "UPDATE contatos SET nome = @nome, telefone = @telefone WHERE codigo = @codigo";

            MySqlCommand cmd = new MySqlCommand(sql, clsBancoDados.con);

            cmd.Parameters.Add("@codigo", MySqlDbType.Int32).Value = Codigo;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = Nome;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = Telefone;

            try
            {
                clsBancoDados.ExecutarComando(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VerificarCodigo()
        {           
            // Instancia um objeto do tipo DataSet
            DataSet ds = new DataSet();           

            // Tenta consultar o banco de dados
            try
            {
                // O objeto DataSet recebe o resultado da consulta SQL
                ds = clsBancoDados.Consultar("SELECT codigo FROM contatos WHERE codigo = '" + Codigo + "'");                

                // Verifica o numero de linhas contidas na tabela 0 (primeira tabela inserida em ds) após a consulta
                // para verificar se o codigo pesquisado já existe e de acordo com isso realizar update ou insert
                if (ds.Tables[0].Rows.Count == 0)                    
                    // Retorna 0 para que seja realizada a inserção do cadastro já que o código não existe
                    return 0;
                else
                    // Retorna 1 para que seja realizada a atualização do cadastro já que o código existe
                    return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}