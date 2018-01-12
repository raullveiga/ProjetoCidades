using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ProjetoCidades.Models;

namespace ProjetoCidades.Repositorio
{
    public class CidadeRepositorio
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rd;

        string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=ProjetoCidades; user id = sa; password = senai@123;";

        public List<Cidade> Listar()
        {

            List<Cidade> listCidades = new List<Cidade>();

            con = new SqlConnection(connectionString);
            string SqlQuery = "select * from Cidades";
            cmd = new SqlCommand(SqlQuery, con);
            con.Open();
            rd = cmd.ExecuteReader();

            //loop para add dados na lista
            while (rd.Read())
            {
                Cidade cidade = new Cidade();
                cidade.Id = Convert.ToInt16(rd["id"]);
                cidade.Nome = rd["Nome"].ToString();
                cidade.Estado = rd["Estado"].ToString();
                cidade.Habitantes = Convert.ToInt16(rd["Habitantes"]);

                listCidades.Add(cidade);
            }

            con.Close();

            return listCidades;
        }
        public Cidade Listar(int id)
        {
            Cidade cidade;
            try
            {
                cidade = new Cidade();
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Cidades where Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    cidade.Id = Convert.ToInt16(rd["Id"]);
                    cidade.Nome = rd["Nome"].ToString();
                    cidade.Estado = rd["Estado"].ToString();
                    cidade.Habitantes = Convert.ToInt16(rd["Habitantes"]);
                }
            }
            catch (SqlException SqlEx)
            {
                throw new Exception("Erro ao tentar atualizar dados (SQL)" + SqlEx.Message);
            }
            catch (System.Exception e)
            {
                throw new Exception("Erro ao tentar atualizar dados (System)" + e.Message);
            }
            finally
            {
                con.Close();
            }

            return cidade;
        }

        public List<Cidade> Cadastrar(Cidade cidade)
        {

            List<Cidade> listCidades = new List<Cidade>();

            con = new SqlConnection(connectionString);
            //colocamos '+" para strings, se for habitantes nao precisa de '
            string SqlQuery = "insert into Cidades (Nome, Estado, Habitantes) values ('" + cidade.Nome + "','" + cidade.Estado + "'," + cidade.Habitantes + ")";
            cmd = new SqlCommand(SqlQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return listCidades;
        }
        public string Editar(Cidade cidade)
        {
            string msg = "Erro ao tentar atualizar";
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE Cidades SET Nome = @n, Estado = @e, Habitantes = @h WHERE Id = @id";
                cmd.Parameters.AddWithValue("@n", cidade.Nome);
                cmd.Parameters.AddWithValue("@e", cidade.Estado);
                cmd.Parameters.AddWithValue("@h", cidade.Habitantes);
                cmd.Parameters.AddWithValue("@id", cidade.Id);
                con.Open();

                if (cmd.ExecuteNonQuery() > 0)
                    msg = "Atualização Efetuada";
                else
                    msg = "Não foi possível atualizar, tente novamente";

                cmd.Parameters.Clear();
            }
            catch (SqlException SqlEx)
            {
                throw new Exception("Erro ao tentar atualizar dados (SQL)" + SqlEx.Message);
            }
            catch (System.Exception e)
            {
                throw new Exception("Erro ao tentar atualizar dados (System)" + e.Message);
            }
            finally
            {
                con.Close();
            }
            return msg;
        }
        public String Excluir(int id){
            string msg = "Erro ao tentar excluir";
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Delete from Cidades WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id",id);
                con.Open();

                if (cmd.ExecuteNonQuery() > 0)
                    msg = "Excluido com sucesso!";
                else
                    msg = "Não foi possível excluir, tente novamente";

                cmd.Parameters.Clear();
            }
            catch (SqlException SqlEx)
            {
                throw new Exception("Erro ao tentar excluir dados (SQL)" + SqlEx.Message);
            }
            catch (System.Exception e)
            {
                throw new Exception("Erro ao tentar excluir dados (System)" + e.Message);
            }
            finally
            {
                con.Close();
            }
            return msg;

        }
    }
}
