USE inlock_games_tarde;
GO

INSERT INTO TIPO_USUARIO(titulo)
VALUES ('Administrador'), ('Cliente');
GO

INSERT INTO USUARIO(idTipoUsuario, email, senha)
VALUES (1, 'admin@admin.com', 'admin'), (2, 'cliente@cliente.com', 'cliente');
GO

INSERT INTO ESTUDIO(nomeEstudio)
VALUES ('Rockstar Studios'), ('Blizzard'), ('Square Enix');
GO

INSERT INTO JOGO(nomeJogo, descricao, dataLancamento, valor, idEstudio)
VALUES ('Diablo 3', '� um jogo que cont�m bastante a��o e � viciante, seja voc� um novato ou um f�. Seu est�dio � a Blizzard.', '15/05/2012', 99.90, 2), ('Red Dead Redemption II', 'jogo eletr�nico de a��o-aventura western', '26/10/2018', 120, 1);
GO