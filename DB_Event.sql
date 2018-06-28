create database DB_EVent10
go

use DB_Event10
go

create table Tipos_Pessoas(
	id				int				not null primary key identity,
	nome			varchar(60)		not null
)

insert into Tipos_Pessoas values ('Pessoa Física'),('Pessoa Jurídica'),('Outros')

create table Tipos_Permissoes(
	id				int			not null primary key identity,
	nome			varchar(60) not null
)

create table Pessoas(
	id				int				not null primary key identity,
	nome			varchar(120)	not null,
	email			varchar(120)	not null unique,
	senha			varchar(60)		not null,
	website			varchar(60)		    null,
	data_cadastro	date			not null default GETDATE(),
	status			int				not null default 1,
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

drop table Enderecos
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

drop table Publicacoes
create table Publicacoes(
	id				int		 not null primary key references Enderecos,
	pessoa_id		int		 not null references Pessoas,
	tipo_imovel_id	int		 not null references Tipos_Imoveis,
	status			int		 not null default 1,
	data_publicacao	date	 not null default GETDATE(),
	descricao		varchar(MAX) null,
	imagem			varchar(250)  null,
)
go

drop table Calendario_Evento
create table Calendario_Evento(
	Calendario_Evento_ID	int				not null primary key identity,
	Publicacoes_ID			int				not null references Publicacoes,
	Titulo					varchar(100)	    null,
	Descricao				varchar(250)		null,
	Data_Inicio				date			    null,
	Data_Final				date				null,
	IsFullDay				bit					null,
	ThemeColor				varchar(20)			null
)
go
insert into Calendario_Evento values (1,'Em Manutenção', 'Não Disponivel', '2018-06-28', '2018-06-28', 1, 'red')
delete from Calendario_Evento where Calendario_Evento_ID = 19
update Calendario_Evento set IsFullDay = 1
select * from Calendario_Evento where Publicacoes_ID = 5
select * from Publicacoes
select * from Pessoas
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
	@insc_estadual varchar(20),
	@cnpj varchar(20),
	@data_fundacao date
)
as
begin
	insert into Pessoas values (@nome, @email, @senha, @website, GETDATE(), 1, 2)
	insert into Pessoas_Juridicas values (@@IDENTITY, @insc_estadual, @cnpj, @data_fundacao)
end
go

execute juridicas 'Teste', 'teste2@email.com', '123', 'www.teste.com.br', '000002', '000002', '2018-03-20'
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

execute juridicasDelete 3
go

-- Create
create procedure fisicas
(
	@nome varchar(120),
	@email varchar(120),
	@senha varchar(60),
	@website varchar(60),
	@rg varchar(20),
	@cpf varchar(20),
	@data_nascimento date
)
as
begin
	insert into Pessoas values (@nome, @email, @senha, @website, GETDATE(), 1, 1)
	insert into Pessoas_Fisicas values (@@IDENTITY, @rg, @cpf, @data_nascimento)
end
go

execute fisicas 'Vinícius Francisco Xavier', 'vinicius@gmail.com', '123123', '', '10101010', '10101010', '1998-01-24'
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

execute fisicasDelete 5
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

execute pessoasDelete 1
go

-- View
create view peassoasReadAll
as
	select 
	p.id, p.nome, p.email, p.website, p.data_cadastro, tp.id as 'tipopessoa', 
	case	
		when p.id = pf.pessoa_id then pf.cpf
		else pj.cnpj
	end as 'cpf_cnpj',
	case	
		when p.id = pf.pessoa_id then pf.rg
		else pj.insc_estadual
	end as 'rg_ies',
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
		p.id, p.nome, p.email, p.website, pf.cpf, pf.rg, p.data_cadastro, pf.data_nascimento, p.status
	from
		Pessoas p, Pessoas_Fisicas pf
	where
		p.id = pf.pessoa_id
go

select * from v_Pessoas_Fisicas


create view v_Pessoas_Juridicas
as
	select 
		p.id, p.nome, p.email, p.website, pj.cnpj, pj.insc_estadual, p.data_cadastro, pj.data_fundacao, p.status
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

select * from Pessoas where email = 'vfx@gmail.com' and senha = '123123'
execute check_login 'vfx@gmail.com', '123123'
execute check_login 'tesste@email.com', '123123'

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


------
drop procedure Cadastro_Publicacao
create procedure Cadastro_Publicacao
(
	-- Endereco
	@rua				varchar(80),
	@bairro				varchar(80),
	@numero				int,
	@complemento		varchar(200),
	@cep				varchar(20),
	@cidade_id			int,
	-- Publicação
	@pessoa_id			int,
	@tipo_imovel_id		int,
	@descricao			varchar(MAX),
	@imagem				varchar(250)				
)
as
begin
	insert into Enderecos values(@rua, @bairro, @numero, @complemento, @cep, @cidade_id) 
	insert into Publicacoes values (@@IDENTITY ,@pessoa_id, @tipo_imovel_id, 1, GETDATE(), @descricao, @imagem)
end

execute Cadastro_Publicacao 'São Paulo', 'Parque da Liberdade', 382, 'Condominio', '150000-000', 17, 1, 12, 'TEste de Descrição !!!', '2.jpg'
execute Cadastro_Publicacao 'Av. José da Silva Sé', 'Parque da Liberdade V', 382, 'Condominio', '150001-001', 17, 2, 12, 'Casa do Vinicius', '1.jpg'
execute Cadastro_Publicacao 'Teste', 'teste', 382, 'lalalal lalala', '150101-001', 17, 2, 12, 'ffg dfgfdg fdg dfgdfgdf gdf gdf g dfg df', '3.jpg'

select * from Enderecos
select * from Publicacoes
insert into Enderecos values('Av. José da Silva Sé', 'Parque da Liberdade V', 382, 'Condominio', '150001-001', 17) 
	insert into Publicacoes values (@@IDENTITY , 5, 12, 1, GETDATE(), 'Casa do Vinicius')

create procedure Deselete_Publicacao
(
	@id int
)
as
begin
delete Enderecos where id = @id
	delete Publicacoes where id = @id
	
end

-- execute Deselete_Publicacao 14
drop procedure Editar_Publicacao
create procedure Editar_Publicacao
(
	@id					int,
	-- Endereco
	@rua				varchar(80),
	@bairro				varchar(80),
	@numero				int,
	@complemento		varchar(200),
	@cep				varchar(20),
	@cidade_id			int,
	-- Publicação
	@tipo_imovel_id		int,
	@status				int,
	@descricao			varchar(MAX),
	@imagem				varchar(250)
)
as
begin
	update Enderecos set rua = @rua, bairro = @bairro, numero = @numero, complemento = @complemento, cep = @cep, cidade_id = @cidade_id where id = @id
	update Publicacoes set tipo_imovel_id = @tipo_imovel_id, status = @status, descricao = @descricao, imagem = @imagem where id = @id
end

execute Editar_Publicacao 3, 'São Paulo', 'Parque da Liberdade', 382, 'Condominio', '150000-000', 17, 12, 1, 'lalalal', 'imagem'

select * from Pessoas
select * from Cidades
select * from Tipos_Imoveis
select * from Enderecos
select * from Publicacoes
go

create view v_Read_Cidades as
	select c.id, c.nome as nomeCidade, e.nome as nomeEstado, c.status
	from Cidades c, Estados e
	where c.estado_id = e.id
go

select * from v_Read_Cidades
go

create view v_Publicacaoes as
	select e.id, e.cidade_id, p.tipo_imovel_id, p.status, p.data_publicacao, p.descricao, p.imagem 
	from Enderecos e, Publicacoes p 
	where e.id = p.id
go

drop view v_Publicacao 
create view v_Publicacao as
	select e.id, e.cidade_id, p.tipo_imovel_id, p.status, p.data_publicacao, p.descricao, p.imagem, e.rua, e.bairro, e.cep, e.complemento, e.numero, p.pessoa_id
	from Enderecos e, Publicacoes p
	where e.id = p.id
go

select * from v_Publicacao where id = 4
go

select * from Pessoas

update Publicacoes set descricao = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam lorem purus, egestas id scelerisque id, pellentesque vitae lorem. Morbi condimentum nulla nec mi tincidunt pretium in eu lorem. Morbi vitae lobortis justo. Praesent blandit elementum neque nec posuere. Ut tristique sed ante vel tincidunt. Nam id metus vitae nunc dignissim iaculis nec a est. Aenean mollis ut sapien ut elementum.' where id = 5

------------------------------------------------------------------------
-- Pessoas_Permissoes
------------------------------------------------------------------------
create procedure create_Pessoas_Permissoes(
	@pessoa_id		int,
	@permissao_id	int
)
as 
begin
	insert into Pessoas_Permissoes values (@pessoa_id, @permissao_id)
end
go

select * from Pessoas_Permissoes

create procedure update_Pessoas_Permissoes(
	@pessoa_id_novo		int,
	@permissao_id_novo	int,
	@pessoa_id			int,
	@permissao_id		int
)
as 
begin
	update Pessoas_Permissoes
	set pessoa_id = @pessoa_id_novo, permissao_id = @permissao_id_novo
	where pessoa_id = @pessoa_id and permissao_id = @permissao_id
end
go

create procedure delete_Pessoas_Permissoes(
	@pessoa_id				int,
	@permissao_id			int
)
as 
begin
	delete from Pessoas_Permissoes
	where pessoa_id = @pessoa_id and permissao_id = @permissao_id
end
go
