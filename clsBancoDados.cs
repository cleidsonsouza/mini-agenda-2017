using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace que contém classes e métodos necessários para verificar o estado das conexões e trabalhar com DataSet
using System.Data;
// Namespace que contém classes e métodos necessários para realizar manipulações no banco de dados MySql
using MySql.Data.MySqlClient;

namespace MiniAgenda
{
    /// <summary>
    /// Classe responsável pela conexão com o banco de dados
    /// </summary>
    class clsBancoDados
    {
        // Representa o caminho para acessar o banco de dados
        static string strConexao = "server=localhost;userid=root;password='123456';database=miniAgenda";

        // MySqlConnection representa uma conexão com o banco de dados MySql
        public static MySqlConnection con = new MySqlConnection(strConexao);

        // Método que cria uma conexão com o banco de dados
        public static void Conectar()
        {
            // Verifica se a conexão com o banco de dados está fechada (para não tentar abrir uma conexão que já está aberta)
            if (con.State == ConnectionState.Closed)
            {
                // Tenta abrir a conexão com o banco de dados
                try
                {
                    // Abre a conexão com o banco de dados utilizando a string de conexão fornecida ao objeto "con", 
                    // no momento da sua criação, por meio do seu método construtor
                    con.Open();
                }
                // Caso haja algum erro ao se tentar abrir a conexão, uma exceção é capturada por esse bloco                 
                catch (MySqlException ex)
                {
                    // Lança uma exceção
                    throw ex;
                }            
            }
        }

        // Método que executa uma instrução SQL
        public static void ExecutarComando(MySqlCommand cmd)
        {           
            // Verifica se a conexão com o banco de dados está aberta
            if (con.State == ConnectionState.Open)
            {         
                // Tenta executar a instrução SQL (comando) no banco de dados
                try
                {
                    // Executa uma instrução SQL contida em cmd (cmd.CommandText) utilizando a conexão "con" estabelecida 
                    cmd.ExecuteNonQuery();                           
                }
                // Caso haja algum erro ao tentar executar a instrução SQL, uma exceção é capturada por esse bloco                 
                catch (MySqlException ex)
                {                
                    // Lança uma exceção
                    throw ex;
                }              
            }            
        }
        
        // Método que fecha a conexão com o banco de dados
        public static void Desconectar()
        {
            // Verifica se a conexão com o banco de dados está aberta
            if (con.State == ConnectionState.Open)
            {               
                // Fecha a conexão com o banco de dados
                con.Close();
                // Garante a liberação da memória que está sendo utilizada pelo objeto "con"
                con.Dispose();
            }
        }
        
        // Método que consulta o banco de dados e retorna as informações gravando-as em um DataSet
        public static DataSet Consultar(string sql)
        {
            // Verifica se a conexão com o banco de dados está aberta
            if (con.State == ConnectionState.Open)
            {
                // Tenta ler do banco de dados
                try
                {
                    // DataSet representa dados em um cache de memória
                    DataSet ds = new DataSet();
                    // MySqlDataAdapter representa um conjunto de comandos (SelectCommand, UpdateCommand, InsertCommand, DeleteCommand) 
                    // e uma conexão de banco de dados que são usados para preencher o DataSet e atualizar a fonte de dados
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                    // Preenche o DataSet com o resultado da consulta ao banco de dados, a tabela "contatos" contida no banco de dados "miniAgenda"
                    da.Fill(ds); // Outra forma de escrever seria -> da.Fill(ds, "um nome p/ tabela q esta sendo inserida em ds"). Ex.: da.Fill(ds, "tb1");
                                 // mas no momento de referenciar o dataset, no método chamador, poderia ser itilizado esse nome ao invés de 
                                 // ds.Tables[0].Rows.Count == 0. Ex.: ds.Tables["tb1"].Rows.Count == 0

                    // retorna o dataSet preenchido com as informações procedentes da consulta
                    return ds; 
                }
                // Caso haja algum erro ao tentar executar o comando, uma exceção é capturada por esse bloco                 
                catch (MySqlException ex)
                {
                    // Lança uma exceção
                    throw ex;
                }
            }
            return null;
        }        
    }
}