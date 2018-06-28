using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Areas.Admin.Models.Connection
{
    public class CidadeConnection : E_Vent.Connection.Connection
    {
        public void Create(Cidade cidade)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Cidades VALUES(@nome, @estado_id, @status)";
            cmd.Parameters.AddWithValue("@nome", cidade.Nome);
            cmd.Parameters.AddWithValue("@estado_id", cidade.Estado_Id);
            cmd.Parameters.AddWithValue("@status", cidade.Status);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM Cidades WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void Update(int id, Cidade cidade)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Cidades SET nome = @nome, estado_id = @estado_id, status = @status WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", cidade.Nome);
            cmd.Parameters.AddWithValue("@estado_id", cidade.Estado_Id);
            cmd.Parameters.AddWithValue("@status", cidade.Status);
            cmd.ExecuteNonQuery();
        }

        public Cidade Read(int id)
        {
            Cidade cidade = new Cidade();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Cidades WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                cidade.Id = (int)reader["id"];
                cidade.Nome = (string)reader["nome"];
                cidade.Estado_Id = (int)reader["estado_id"];
                cidade.Status = (Status)reader["status"];
            }
            return cidade;
        }

        public List<ListarCidades> ReadAll()
        {
            List<ListarCidades> lista = new List<ListarCidades>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            //cmd.CommandText = @"select * from Cidades";
            cmd.CommandText = @"select Cidades.id, Cidades.nome, Estados.nome as estado_nome , Estados.sigla as estado_sigla, Cidades.status from Cidades inner join Estados on Cidades.estado_id = Estados.id order by nome";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListarCidades cidade = new ListarCidades();
                cidade.Id = (int)reader["id"];
                cidade.Nome = (string)reader["nome"];
                cidade.Estado_Nome = (string)reader["estado_nome"];
                cidade.Estado_Sigla = (string)reader["estado_sigla"];
                cidade.Status = (Status)reader["status"];
                lista.Add(cidade);
            }
            return lista;
        }

        public bool Check(int id)
        {
            return true;
        }
    }
}