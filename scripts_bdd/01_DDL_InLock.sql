CREATE DATABASE inlock_games_tarde
GO

USE inlock_games_tarde
GO

CREATE TABLE ESTUDIO (

	idEstudio SMALLINT PRIMARY KEY IDENTITY(1,1),
	nomeEstudio VARCHAR(40) NOT NULL UNIQUE,

);
GO

CREATE TABLE JOGO (

  	idJogo INT PRIMARY KEY IDENTITY(1,1),
  	idEstudio SMALLINT FOREIGN KEY REFERENCES ESTUDIO(idEstudio),
  	nomeJogo VARCHAR(100) NOT NULL,
  	descricao VARCHAR(300) NOT NULL,
  	valor SMALLMONEY,
  	dataLancamento DATE

);
GO

CREATE TABLE TIPO_USUARIO (

	idTipoUsuario TINYINT PRIMARY KEY IDENTITY(1,1),
	titulo VARCHAR(100) NOT NULL

);
GO

CREATE TABLE USUARIO (

	idUsuario INT PRIMARY KEY IDENTITY(1,1),
  	idTipoUsuario TINYINT FOREIGN KEY REFERENCES TIPO_USUARIO(idTipoUsuario),
	email VARCHAR(300) NOT NULL UNIQUE,
  	senha VARCHAR(16) NOT NULL CONSTRAINT USUARIO_SENHA_CK CHECK (LEN(senha) >= 5)
  
);
GO
