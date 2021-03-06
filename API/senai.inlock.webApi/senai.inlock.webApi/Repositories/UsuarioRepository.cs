using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string stringConexao = "Data Source=PEDRO-PC\\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=senai@123";
        public void atualizar(UsuarioDomain usuarioAtualizado, int idUsuarioAtualizado)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string cmdUpdate = $@"UPDATE USUARIO
                                      SET email = @email, senha = @senha
                                      WHERE idUsuario = @idUsuarioAtualizado";
                using (SqlCommand comando = new SqlCommand(cmdUpdate, conexao))
                {
                    comando.Parameters.AddWithValue("@email", usuarioAtualizado.email);
                    comando.Parameters.AddWithValue("@senha", usuarioAtualizado.senha);
                    comando.Parameters.AddWithValue("@idUsuarioAtualizado", idUsuarioAtualizado);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public UsuarioDomain buscarPorID(int idUsuarioBuscado)
        {
            UsuarioDomain usuarioBuscado = new UsuarioDomain();
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string cmdReadWhere = $@"SELECT titulo, email, idUsuario, USUARIO.idTipoUsuario FROM USUARIO
                                         LEFT JOIN TIPO_USUARIO
                                         ON USUARIO.idTipoUsuario = TIPO_USUARIO.idTipoUsuario
                                         WHERE USUARIO.idUsuario = @idUsuarioBuscado";
                SqlDataReader leitorDeDados;
                using (SqlCommand comando = new SqlCommand(cmdReadWhere, conexao))
                {
                    comando.Parameters.AddWithValue("@idUsuarioBuscado", idUsuarioBuscado);
                    leitorDeDados = comando.ExecuteReader();
                    if (leitorDeDados.Read())
                    {
                        usuarioBuscado.email = leitorDeDados[1].ToString();
                        usuarioBuscado.tipoUsuario = new TipoUsuarioDomain { 
                            titulo = leitorDeDados[0].ToString(),
                            idTipoUsuario = Convert.ToInt32(leitorDeDados[3])
                        };
                        usuarioBuscado.idUsuario = Convert.ToInt32(leitorDeDados[2]);
                        usuarioBuscado.idTipoUsuario = Convert.ToInt32(leitorDeDados[3]);
                    }
                    else return null;
                    return usuarioBuscado;
                }
            }
        }

        public void cadastrar(UsuarioDomain novoUsuario)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string cmdInsert = $@"INSERT INTO USUARIO(email, senha, idTipoUsuario)
                                      VALUES (@email, @senha, @idTipoUsuario)";
                using (SqlCommand comando = new SqlCommand(cmdInsert, conexao))
                {
                    comando.Parameters.AddWithValue("@email", novoUsuario.email);
                    comando.Parameters.AddWithValue("@senha", novoUsuario.senha);
                    comando.Parameters.AddWithValue("@idTipoUsuario", novoUsuario.idTipoUsuario);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void deletar(int idUsuarioDeletado)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string cmdDelete = $@"DELETE FROM USUARIO WHERE idUsuario = @idUsuarioDeletado";
                using (SqlCommand comando = new SqlCommand(cmdDelete, conexao))
                {
                    comando.Parameters.AddWithValue("@idUsuarioDeletado", idUsuarioDeletado);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<UsuarioDomain> listarTodos()
        {
            List<UsuarioDomain> usuarios = new List<UsuarioDomain>();
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string cmdRead = $@"SELECT titulo, email, idUsuario, USUARIO.idTipoUsuario FROM USUARIO
                                       LEFT JOIN TIPO_USUARIO
                                       ON USUARIO.idTipoUsuario = TIPO_USUARIO.idTipoUsuario";
                SqlDataReader leitorDeDados;
                using (SqlCommand comando = new SqlCommand(cmdRead, conexao))
                {
                    leitorDeDados = comando.ExecuteReader();
                    while (leitorDeDados.Read())
                    {
                        UsuarioDomain usuarioConsultado = new UsuarioDomain();
                        usuarioConsultado.email = leitorDeDados[1].ToString();
                        usuarioConsultado.tipoUsuario = new TipoUsuarioDomain { 
                            titulo = leitorDeDados[0].ToString(),
                            idTipoUsuario = Convert.ToInt32(leitorDeDados[3])
                        };
                        usuarioConsultado.idUsuario = Convert.ToInt32(leitorDeDados[2]);
                        usuarioConsultado.idTipoUsuario = Convert.ToInt32(leitorDeDados[3]);
                        usuarios.Add(usuarioConsultado);
                    }
                }
            }
            return usuarios;
        }

        public UsuarioDomain logar(string email, string senha)
        {
            UsuarioDomain usuarioLogin = new UsuarioDomain();
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string cmdVerificar = $@"SELECT email, titulo, idUsuario FROM USUARIO
                                         LEFT JOIN TIPO_USUARIO
                                         ON USUARIO.idTipoUsuario = TIPO_USUARIO.IdTipoUsuario
                                         WHERE email = @email AND senha = @senha";
                using (SqlCommand comando = new SqlCommand(cmdVerificar, conexao))
                {
                    SqlDataReader leitorDeDados;
                    comando.Parameters.AddWithValue("@email", email);
                    comando.Parameters.AddWithValue("@senha", senha);
                    leitorDeDados = comando.ExecuteReader();
                    if (leitorDeDados.Read())
                    {
                        usuarioLogin.email = leitorDeDados[0].ToString();
                        usuarioLogin.tipoUsuario = new TipoUsuarioDomain { 
                            titulo = leitorDeDados[1].ToString() 
                        };
                        usuarioLogin.idUsuario = Convert.ToInt32(leitorDeDados[2]);
                    }
                    else return null;
                    return usuarioLogin;
                }
            }
        }
    }
}