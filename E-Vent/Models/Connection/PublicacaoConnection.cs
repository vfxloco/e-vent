using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Models.Connection
{
    public class PublicacaoConnection : E_Vent.Connection.Connection
    {
        public void Create(PublicacaoView publicacao)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"execute Cadastro_Publicacao @rua, @bairro, @numero, @complemento, @cep, @cidade_id, @pessoa_id, @tipo_imovel_id, @descricao, @imagem";
            cmd.Parameters.AddWithValue("@rua", publicacao.Rua);
            cmd.Parameters.AddWithValue("@bairro", publicacao.Bairro);
            cmd.Parameters.AddWithValue("@numero", publicacao.Numero);
            cmd.Parameters.AddWithValue("@complemento", (string.IsNullOrEmpty(publicacao.Complemento) ? "" : publicacao.Complemento));
            cmd.Parameters.AddWithValue("@cep", publicacao.CEP);
            cmd.Parameters.AddWithValue("@cidade_id", publicacao.Cidade_Id);
            cmd.Parameters.AddWithValue("@pessoa_id", publicacao.Pessoa_Id);
            cmd.Parameters.AddWithValue("@tipo_imovel_id", publicacao.Tipo_Imovel_Id);
            cmd.Parameters.AddWithValue("@descricao", (string.IsNullOrEmpty(publicacao.Descricao) ? "" : publicacao.Descricao));
            cmd.Parameters.AddWithValue("@imagem", (string.IsNullOrEmpty(publicacao.Imagem) ? "" : publicacao.Imagem));
            cmd.ExecuteNonQuery();
        }

        public List<PublicacaoView> Publicacoes()
        {
            List<PublicacaoView> lista = new List<PublicacaoView>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_Publicacaoes";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PublicacaoView publicacao = new PublicacaoView();
                publicacao.Id = (int)reader["id"];
                publicacao.Tipo_Imovel_Id = (int)reader["tipo_imovel_id"];
                publicacao.Cidade_Id = (int)reader["cidade_id"];
                publicacao.Descricao = (string)reader["descricao"];
                publicacao.Imagem = (string)reader["imagem"];
                publicacao.Data_Publicacao = (DateTime)reader["data_publicacao"];
                publicacao.Status = (Status)reader["status"];
                lista.Add(publicacao);
            }
            return lista;
        }

        public PublicacaoView Read(int id)
        {
            PublicacaoView publicacao = new PublicacaoView();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_Publicacao where id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                publicacao.Id = (int)reader["id"];
                publicacao.Tipo_Imovel_Id = (int)reader["tipo_imovel_id"];
                publicacao.Cidade_Id = (int)reader["cidade_id"];
                publicacao.Descricao = (string)reader["descricao"];
                publicacao.Imagem = (string)reader["imagem"];
                publicacao.Data_Publicacao = (DateTime)reader["data_publicacao"];
                publicacao.Rua = (string)reader["rua"];
                publicacao.Bairro = (string)reader["bairro"];
                publicacao.CEP = (string)reader["cep"];
                publicacao.Complemento = (string)reader["complemento"];
                publicacao.Numero = (int)reader["numero"];
                publicacao.Pessoa_Id = (int)reader["pessoa_id"];
                publicacao.Status = (Status)reader["status"];
            }
            return publicacao;
        }

        public List<Event> Calendar_RadAll(int id)
        {
            List<Event> lista = new List<Event>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from Calendario_Evento where Publicacoes_ID = @id";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Event evento = new Event();
                evento.EventId = (int)reader["Calendario_Evento_ID"];
                evento.Publicacao_Id = (int)reader["Publicacoes_ID"];
                evento.Titulo = (string)reader["Titulo"];
                evento.Descricao = (string)reader["Descricao"];
                evento.Start = (DateTime)reader["Data_Inicio"];
                evento.End = (DateTime)reader["Data_Final"];
                evento.IsFullDay = (Boolean)reader["IsFullDay"];
                evento.ThemeColor = (string)reader["ThemeColor"];
                lista.Add(evento);
            }
            return lista; 
        }

        public void Editar(PublicacaoView publicacao)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"execute Editar_Publicacao @Publicacao_Id, @Rua, @Bairro, @Numero, @Complemento, @CEP, @Cidade_Id, @Tipo_Imovel_Id, @Status, @Descricao, @Imagem";
            cmd.Parameters.AddWithValue("@Publicacao_Id", publicacao.Id);
            cmd.Parameters.AddWithValue("@Rua", publicacao.Rua);
            cmd.Parameters.AddWithValue("@Bairro", publicacao.Bairro);
            cmd.Parameters.AddWithValue("@Numero", publicacao.Numero);
            cmd.Parameters.AddWithValue("@Complemento", publicacao.Complemento);
            cmd.Parameters.AddWithValue("@CEP", publicacao.CEP);
            cmd.Parameters.AddWithValue("@Cidade_Id", publicacao.Cidade_Id);
            cmd.Parameters.AddWithValue("@Tipo_Imovel_Id", publicacao.Tipo_Imovel_Id);
            cmd.Parameters.AddWithValue("@Status", publicacao.Status);
            cmd.Parameters.AddWithValue("@Descricao", publicacao.Descricao);
            cmd.Parameters.AddWithValue("@Imagem", (string.IsNullOrEmpty(publicacao.Imagem) ? "" : publicacao.Imagem));
            cmd.ExecuteNonQuery();
        }

        public bool Calendar_Create(int id, Event e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"insert into Calendario_Evento values (@Publicacao_Id, @Titulo, @Descricao, @Start, @End, 1, @ThemeColor)";
                cmd.Parameters.AddWithValue("@Publicacao_Id", id);
                cmd.Parameters.AddWithValue("@Titulo", e.Titulo);
                cmd.Parameters.AddWithValue("@Descricao", e.Descricao);
                cmd.Parameters.AddWithValue("@Start", e.Start);
                cmd.Parameters.AddWithValue("@End", e.End);
                cmd.Parameters.AddWithValue("@IsFullDay", e.IsFullDay);
                cmd.Parameters.AddWithValue("@ThemeColor", e.ThemeColor);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch{
                return false;
            }
        }

        public bool Calendar_Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"delete from Calendario_Evento where Calendario_Evento_ID = @id";
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}