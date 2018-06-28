using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Areas.Admin.Models.Connection
{
    public class TipoImovelConnection : E_Vent.Connection.Connection
    {
        public void Create(TipoImovel tipoImovel)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Tipos_Imoveis VALUES(@Nome, @Status)";
            cmd.Parameters.AddWithValue("@Nome", tipoImovel.Nome);
            cmd.Parameters.AddWithValue("@Status", tipoImovel.Status);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM Tipos_Imoveis WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void Update(int id, TipoImovel tipoImovel)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Tipos_Imoveis SET nome = @nome, status = @status WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", tipoImovel.Nome);
            cmd.Parameters.AddWithValue("@status", tipoImovel.Status);
            cmd.ExecuteNonQuery();
        }

        public TipoImovel Read(int id)
        {
            TipoImovel tipoImovel = new TipoImovel();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Tipos_Imoveis WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tipoImovel.Id = (int)reader["id"];
                tipoImovel.Nome = (string)reader["nome"];
                tipoImovel.Status = (Status)reader["status"];
            }
            return tipoImovel;
        }

        public List<TipoImovel> ReadAll()
        {
            List<TipoImovel> lista = new List<TipoImovel>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from Tipos_Imoveis order by nome";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TipoImovel tipoImovel = new TipoImovel();
                tipoImovel.Id = (int)reader["id"];
                tipoImovel.Nome = (string)reader["nome"];
                tipoImovel.Status = (Status)reader["status"];
                lista.Add(tipoImovel);
            }
            return lista;
        }

        public bool Check(int id)
        {
            return true;
        }
    }
}