using E_Vent.Models.Pessoa;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Models.Connection
{
    public class UsuarioConnection : E_Vent.Connection.Connection
    {
        public void Create(PessoaViewModel pessoa)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            if (pessoa.Tipo_Pessoa == 1)
            {
                cmd.CommandText = @"execute fisicas @nome, @email, @senha, @website, @rg, @cpf, @dataNascimento";
                cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                cmd.Parameters.AddWithValue("@email", pessoa.Email);
                cmd.Parameters.AddWithValue("@senha", pessoa.Senha);
                cmd.Parameters.AddWithValue("@website", (string.IsNullOrEmpty(pessoa.Website) ? "null" : pessoa.Website));
                cmd.Parameters.AddWithValue("@rg", pessoa.RG);
                cmd.Parameters.AddWithValue("@cpf", pessoa.CPF);
                cmd.Parameters.AddWithValue("@dataNascimento", pessoa.Data_Nascimento.Date.ToString("yyyy-MM-dd"));    
            }
            else if(pessoa.Tipo_Pessoa == 2)
            {
                cmd.CommandText = @"execute juridicas @nome, @email, @senha, @website, @inscEstadual, @cnpj, @dataFundacao";
                cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                cmd.Parameters.AddWithValue("@email", pessoa.Email);
                cmd.Parameters.AddWithValue("@senha", pessoa.Senha);
                cmd.Parameters.AddWithValue("@website", (string.IsNullOrEmpty(pessoa.Website) ? "null" : pessoa.Website));
                cmd.Parameters.AddWithValue("@inscEstadual", pessoa.Insc_Estadual);
                cmd.Parameters.AddWithValue("@cnpj", pessoa.CNPJ);
                cmd.Parameters.AddWithValue("@dataFundacao", pessoa.Data_Fundacao.Date.ToString("yyyy-MM-dd"));
            }
            else
            {
                throw new Exception(pessoa.Tipo_Pessoa.ToString());
            }

            cmd.ExecuteNonQuery();
        }

        public PessoaSession Login(PessoaViewLogin login)
        {
            PessoaSession usuario = null;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"execute check_login @email, @senha";
            cmd.Parameters.AddWithValue("@email", login.Email);
            cmd.Parameters.AddWithValue("@senha", login.Senha);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                usuario = new PessoaSession();
                usuario.Id = (int)reader["id"];
                usuario.Nome = (string)reader["nome"];
                usuario.Email = (string)reader["email"];
                usuario.Tipo_Pessoa = (TipoPessoa)reader["tipopessoa"];
                usuario.Status = (Status)reader["status"];
            }
            return usuario;
        }
    }
}