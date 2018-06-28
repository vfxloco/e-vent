using E_Vent.Models.Pessoa;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Models.Connection
{
    public class UsuariosConnection : E_Vent.Connection.Connection
    {
        public void Create(PessoaViewModel pessoa)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            if (pessoa.Tipo_Pessoa == 1)
            {
                cmd.CommandText = @"execute fisicas @nome, @email, @senha, @website, @imagem, @rg, @cpf, @dataNascimento";
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
                cmd.CommandText = @"execute juridicas @nome, @email, @senha, @website, @imagem, @inscEstadual, @cnpj, @dataFundacao";
                cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                cmd.Parameters.AddWithValue("@email", pessoa.Email);
                cmd.Parameters.AddWithValue("@senha", pessoa.Senha);
                cmd.Parameters.AddWithValue("@website", (string.IsNullOrEmpty(pessoa.Website) ? "null" : pessoa.Website));
                cmd.Parameters.AddWithValue("@inscEstadual", pessoa.Insc_Estadual);
                cmd.Parameters.AddWithValue("@cnpj", pessoa.CNPJ);
                cmd.Parameters.AddWithValue("@dataFundacao", pessoa.Data_Fundacao.Date.ToString("yyyy-MM-dd"));
            }
            cmd.ExecuteNonQuery();
        }
        
        public void Delete(int id)
        {
            
        }

        public void Status(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"execute update_Status @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void Update(int id, PessoaViewModel pessoaViewModel)
        {
            
        }

        public PessoaViewModel Read(int id)
        {
            
            return null;
        }

        public List<PessoaViewModel> ReadAll_PessoasFisicas()
        {
            List<PessoaViewModel> lista = new List<PessoaViewModel>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            //cmd.CommandText = @"select * from Cidades";
            cmd.CommandText = @"select * from v_Pessoas_Fisicas";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PessoaViewModel pessoaViewModel = new PessoaViewModel();
                pessoaViewModel.Id = (int)reader["id"];
                pessoaViewModel.Nome = (string)reader["nome"];
                pessoaViewModel.Email = (string)reader["email"];
                pessoaViewModel.Website = (string)reader["website"];
                pessoaViewModel.CPF = (string)reader["cpf"];
                pessoaViewModel.RG = (string)reader["rg"];
                pessoaViewModel.Data_Cadastro = (DateTime)reader["data_cadastro"];
                pessoaViewModel.Data_Nascimento = (DateTime)reader["data_nascimento"];
                pessoaViewModel.Status = (int)reader["status"];
                lista.Add(pessoaViewModel);
            }
            reader.Close();
            return lista;
        }

        public List<PessoaViewModel> ReadAll_PessoasJuridica()
        {
            List<PessoaViewModel> lista = new List<PessoaViewModel>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            //cmd.CommandText = @"select * from Cidades";
            cmd.CommandText = @"select * from v_Pessoas_Juridicas";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PessoaViewModel pessoaViewModel = new PessoaViewModel();
                pessoaViewModel.Id = (int)reader["id"];
                pessoaViewModel.Nome = (string)reader["nome"];
                pessoaViewModel.Email = (string)reader["email"];
                pessoaViewModel.Website = (string)reader["website"];
                pessoaViewModel.CNPJ = (string)reader["cnpj"];
                pessoaViewModel.Insc_Estadual = (string)reader["insc_estadual"];
                pessoaViewModel.Data_Cadastro = (DateTime)reader["data_cadastro"];
                pessoaViewModel.Data_Fundacao = (DateTime)reader["data_fundacao"];
                pessoaViewModel.Status = (int)reader["status"];
                lista.Add(pessoaViewModel);
            }
            reader.Close();
            return lista;
        }

        public bool Check(int id)
        {
            return true;
        }
    }
}