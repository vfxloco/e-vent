using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Areas.Admin.Models.Connection
{
    public class EstadoConnection : E_Vent.Connection.Connection
    {
        public void Create(Estado estado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Estados VALUES(@Sigla, @Nome, @Status)";
            cmd.Parameters.AddWithValue("@Sigla", estado.Sigla.ToUpper());
            cmd.Parameters.AddWithValue("@Nome", estado.Nome);
            cmd.Parameters.AddWithValue("@Status", estado.Status);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM Estados WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void Update(int id,Estado estado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Estados SET nome = @nome, sigla = @sigla, status = @status WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@sigla", estado.Sigla.ToUpper());
            cmd.Parameters.AddWithValue("@nome", estado.Nome);
            cmd.Parameters.AddWithValue("@status", estado.Status);
            cmd.ExecuteNonQuery();
        }

        public Estado Read(int id)
        {
            Estado estado = new Estado();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Estados WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                estado.Id = (int)reader["id"];
                estado.Sigla = (string)reader["sigla"];
                estado.Nome = (string)reader["nome"];
                estado.Status = (Status)reader["status"];
            }
            return estado;
        }

        public List<Estado> ReadAll()
        {
            List<Estado> lista = new List<Estado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from Estados order by nome";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Estado estado = new Estado();
                estado.Id = (int)reader["id"];
                estado.Sigla = (string)reader["sigla"];
                estado.Nome = (string)reader["nome"];
                estado.Status = (Status)reader["status"];
                lista.Add(estado);
            }
            return lista;
        }

        public bool Check(int id)
        {
            List<Estado> lista = new List<Estado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from Cidades where estado_id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                reader.Close();
                return true;
            }
            return false;
        }
    }
}