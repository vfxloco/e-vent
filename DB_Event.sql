create database DB_EVent7
go

use DB_Event7
go

create table Tipos_Pessoas(
	id				int				not null primary key identity,
	nome			varchar(60)		not null
)

insert into Tipos_Pessoas values ('Pessoa Física'),('Pessoa Jurídica')

create table Tipos_Permissoes(
	id				int			not null primary key identity,
	nome			varchar(60) not null
)

create table Telefones(
	pessoa_id		int			not null references Pessoas,
	numero			int			not null,
	whats			bit				null,
	primary key(pessoa_id, numero)
)
go

create table Pessoas(
	id				int				not null primary key identity,
	nome			varchar(120)	not null,
	email			varchar(120)	not null unique,
	senha			varchar(60)		not null,
	website			varchar(60)		    null,
	data_cadastro	date		not null default GETDATE(),
	status			int				not null default 1,
	imagem			varchar(120)		null,
	tipo_pessoa_id	int				not	null references Tipos_Pessoas
)

create table Pessoas_Fisicas(
	pessoa_id		int			not null primary key references Pessoas,
	rg				varchar(20) not null ,
	cpf				varchar(20) not null unique,
	data_nascimento	date	not null
)

create table Pessoas_Juridicas(
	pessoa_id		int			not null primary key references Pessoas,
	insc_estadual	varchar(20) not null ,
	cnpj			varchar(20) not null unique,
	data_fundacao	date	not null
)

create table Pessoas_Permissoes(
	pessoa_id		int		not null references Pessoas,
	permissao_id	int		not null references Tipos_Permissoes,
	primary key(pessoa_id, permissao_id)
	-- Terminar
)
go

--

create table Tipos_Imoveis(
	id				int			not null primary key identity,
	nome			varchar(60) not null,
	status			int				null
)
go

create table Estados(
	id				int			not null primary key identity,
	sigla			varchar(2)	not null,
	nome			varchar(60) not null,
	status			int				null default 1
)
go

create table Cidades(
	id				int			not null primary key identity,
	nome			varchar(60) not null,
	estado_id		int		not null references Estados,
	status			int				null default 1
)
go

create table Enderecos(
	id				int			not null primary key identity,
	rua				varchar(80)	not null,
	bairro			varchar(80) not null,
	numero			int			not null,
	complemento		varchar(200)	null,
	cep				varchar(20)	not null,
	cidade_id		int			not null references Cidades
)
go

create table Publicacoes(
	id				int		 not null primary key identity,
	pessoa_id		int		 not null references Pessoas,
	endereco_id		int		 not null references Enderecos,
	tipo_imovel_id	int		 not null references Tipos_Imoveis,
	status			int		 not null default 1,
	data_publicacao	date not null default GETDATE(),
	descricao		varchar(MAX) null
)
go


-- Select
select * from Estados order by nome
select * from Cidades
select * from Enderecos
select * from Tipos_Imoveis order by nome

select * from Pessoas
select * from Pessoas_Fisicas
select * from Pessoas_Juridicas
go

-- procedure
--create
create procedure juridicas
(
	@nome varchar(120),
	@email varchar(120),
	@senha varchar(60),
	@website varchar(60),
	@imagem varchar(120),
	@insc_estadual varchar(20),
	@cnpj varchar(20),
	@data_fundacao date
)
as
begin
	insert into Pessoas values (@nome, @email, @senha, @website, GETDATE(), 1, @imagem, 2)
	insert into Pessoas_Juridicas values (@@IDENTITY, @insc_estadual, @cnpj, @data_fundacao)
end
go

execute juridicas 'Teste', 'teste@email.com', '123', 'www.teste.com.br', 'teste caminho imagem', '000001', '000001', '2018-03-20'
go

--delete
create procedure juridicasDelete
(
	@id int
)
as
begin
	delete from Pessoas_Juridicas where pessoa_id = @id
	delete from Pessoas where id = @id
end
go

execute juridicasDelete 7
go

-- Create
create procedure fisicas
(
	@nome varchar(120),
	@email varchar(120),
	@senha varchar(60),
	@website varchar(60),
	@imagem varchar(120),
	@rg varchar(20),
	@cpf varchar(20),
	@data_nascimento date
)
as
begin
	insert into Pessoas values (@nome, @email, @senha, @website, GETDATE(), 1, @imagem, 1)
	insert into Pessoas_Fisicas values (@@IDENTITY, @rg, @cpf, @data_nascimento)
end
go

execute fisicas 'Vinícius Francisco Xavier', 'viniciu2s@gmail.com', '123', null, 'teste caminho imagem', '10401010', '10401010', '1998-01-24'
go

-- Delete
create procedure fisicasDelete
(
	@id int
)
as
begin
	delete from Pessoas_Fisicas where pessoa_id = @id
	delete from Pessoas where id = @id
end
go

execute fisicasDelete 7
go

-- Delete any pessoa
create procedure pessoasDelete
(
	@id int
)
as
begin
	if exists (select * from Pessoas_Fisicas where pessoa_id = @id)
	begin
		execute fisicasDelete @id
	end
	else
	begin
		execute juridicasDelete @id
	end
end
go

execute pessoasDelete 3
go

-- View
create view peassoasReadAll
as
	select 
	p.id, p.nome, p.email, p.website, p.data_cadastro, tp.id as 'tipopessoa', 
	case	
		when p.id = pf.pessoa_id then pf.cpf
		else pj.cnpj
	end as cpfcnpj,
	case	
		when p.id = pf.pessoa_id then pf.rg
		else pj.insc_estadual
	end as rgies,
	p.status
from
	Pessoas p
join 
	Tipos_Pessoas tp on p.tipo_pessoa_id = tp.id
left join
	Pessoas_Fisicas pf on p.id = pf.pessoa_id
left join
	Pessoas_Juridicas pj on p.id = pj.pessoa_id

select * from peassoasReadAll	


create view v_Pessoas_Fisicas
as
	select 
		p.id, p.nome, p.email, p.imagem, p.website, pf.cpf, pf.rg, p.data_cadastro, pf.data_nascimento, p.status
	from
		Pessoas p, Pessoas_Fisicas pf
	where
		p.id = pf.pessoa_id
go

select * from v_Pessoas_Fisicas


create view v_Pessoas_Juridicas
as
	select 
		p.id, p.nome, p.email, p.imagem, p.website, pj.cnpj, pj.insc_estadual, p.data_cadastro, pj.data_fundacao, p.status
	from
		Pessoas p, Pessoas_Juridicas pj
	where
		p.id = pj.pessoa_id
go

select * from v_Pessoas_Juridicas


select * from Pessoas
select * from Pessoas_Fisicas
select * from Pessoas_Juridicas

/*
  Select All de Cidades - View da Lista de Cidades
  Retorna: Cidade_ID, Cidade_Nome, Estado_Nome, Estado_Sigla, Cidade_Status
*/
select Cidades.id, Cidades.nome, Estados.nome as estado_nome , Estados.sigla as estado_sigla, Cidades.status from Cidades 
inner join Estados on Cidades.estado_id = Estados.id order by nome

-- Procedure
-- Login
create procedure check_login
(
	@email			varchar(120),
	@senha			varchar(60)
)
as
begin
	if exists(select * from Pessoas where email = @email and senha = @senha)
	begin
		select * from peassoasReadAll where email = @email
	end
end
go

execute check_login 'tesste1@email.com', '123123'
execute check_login 'tesste2@email.com', '123123'

execute check_login 'tlala', '123123'

-- Procedure
-- Update Status
create procedure update_Status
(
	@id int
)
as 
begin
	if(select status from Pessoas where id = @id) = 1
	begin
		update Pessoas set status = 0 where id = @id
	end
	else
	begin
		update Pessoas set status = 1 where id = @id
	end
end
go

execute update_Status 27

-- Pegar valor de um Banco e colocar no outro
-- Estados
set identity_insert Estados on

insert into Estados (id, nome , sigla, status)
select id, nome , sigla, status from DB_EVent.dbo.Estados

set identity_insert Estados off
go

select * from Estados

-- Cidades
set identity_insert Cidades on

insert into Cidades (id, nome, estado_id, status)
select id, nome, estado_id, status from DB_EVent.dbo.Cidades

set identity_insert Cidades off
go

select * from Cidades

-- Tipo Imovel
set identity_insert Tipos_Imoveis on

insert into Tipos_Imoveis (id, nome, status)
select id, nome, status from DB_EVent.dbo.Tipos_Imoveis

set identity_insert Tipos_Imoveis off
go

select * from Tipos_Imoveis