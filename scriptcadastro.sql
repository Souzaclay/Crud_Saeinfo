use cadastro;

CREATE TABLE aluno(
	[alunoid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](200) NULL,
	[rg] [varchar](7) NULL,
	[cpf] [varchar](11) NULL,
	[datanascimento] [datetime] NULL,
	[enderecoid] [int] NULL,
	[matricula] [varchar](20) NULL,
	[idade] [int] NULL,
	[sexo] [varchar](1) NULL,
	[email] [varchar](50) NULL,
	[datacadastro] [datetime] NULL,
	[dataalteracao] [datetime] NULL,
	[telefone] [varchar](15) NULL,
	[usuarioalteracao] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[alunoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE cidade (
	[cidadeid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](200) NULL,
	[estado] [varchar](2) NULL,
	[cep] [varchar](8) NULL,
PRIMARY KEY CLUSTERED 
(
	[cidadeid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE endereco (
	[enderecoid] [int] IDENTITY(1,1) NOT NULL,
	[logradouro] [varchar](200) NULL,
	[bairro] [varchar](100) NULL,
	[complemento] [varchar](200) NULL,
	[numero] [int] NULL,
	[cep] [varchar](8) NULL,
	[cidadeid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[enderecoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE responsavel (
	[responsavelid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](200) NULL,
	[rg] [varchar](7) NULL,
	[cpf] [varchar](11) NULL,
	[profissao] [varchar](100) NULL,
	[datacadastro] [datetime] NULL,
	[dataalteracao] [datetime] NULL,
	[alunoid] [int] NULL,
	[celular] [varchar](15) NULL,
 CONSTRAINT [Pk_responsavel] PRIMARY KEY CLUSTERED 
(
	[responsavelid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE usuario (
	[usuarioid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](200) NULL,
	[cpf] [varchar](11) NULL,
	[sexo] [varchar](1) NULL,
	[datacadastro] [datetime] NULL,
	[email] [varchar](50) NULL,
	[senha] [varchar](20) NULL,
	[telefone] [varchar](15) NULL,
	[cidadeid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[usuarioid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[aluno]  WITH CHECK ADD  CONSTRAINT [Fk_aluno_endereco_enderecoid] FOREIGN KEY([enderecoid])
REFERENCES [dbo].[endereco] ([enderecoid])


ALTER TABLE [dbo].[endereco]  WITH CHECK ADD  CONSTRAINT [Fk_endereco_cidade_cidadeid] FOREIGN KEY([cidadeid])
REFERENCES [dbo].[cidade] ([cidadeid])

ALTER TABLE [dbo].[responsavel]  WITH CHECK ADD  CONSTRAINT [Fk_responsavel_aluno_alunoid] FOREIGN KEY([alunoid])
REFERENCES [dbo].[aluno] ([alunoid])

ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [Fk_usuario_cidade_cidadeid] FOREIGN KEY([cidadeid])
REFERENCES [dbo].[cidade] ([cidadeid])

