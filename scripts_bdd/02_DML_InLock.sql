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
VALUES ('Diablo 3', 'é um jogo que contém bastante ação e é viciante, seja você um novato ou um fã. Seu estúdio é a Blizzard.', '15/05/2012', 99.90, 2), ('Red Dead Redemption II', 'jogo eletrônico de ação-aventura western', '26/10/2018', 120, 1);
GO