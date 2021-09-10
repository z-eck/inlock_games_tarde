using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class TipoUsuarioRepository : BaseRepository, ITipoUsuarioRepository
    {
        public TipoUsuarioDomain buscarPorId(int idTipoBuscado)
        {
            TipoUsuarioDomain tipoConsulta = new TipoUsuarioDomain();
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string cmdReadWhere = $@"SELECT titulo, idTipoUsuario FROM TIPO_USUARIO
                                         WHERE idTipoUsuario = @idTipoBuscado";
                SqlDataReader leitorDeDados;
                using (SqlCommand comando = new SqlCommand(cmdReadWhere, conexao))
                {
                    comando.Parameters.AddWithValue("@idTipoBuscado", idTipoBuscado);
                    leitorDeDados = comando.ExecuteReader();
                    if (leitorDeDados.Read())
                    {
                        tipoConsulta.titulo = leitorDeDados[0].ToString();
                        tipoConsulta.idTipoUsuario = Convert.ToInt32(leitorDeDados[1]);
                    }
                    else return null;
                    return tipoConsulta;
                }
            }
        }

        public List<TipoUsuarioDomain> listarTodos()
        {
            List<TipoUsuarioDomain> tiposUsuarios = new List<TipoUsuarioDomain>();
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string cmdRead = $@"SELECT titulo, idTipoUsuario FROM TIPO_USUARIO";
                SqlDataReader leitorDeDados;
                using (SqlCommand comando = new SqlCommand(cmdRead, conexao))
                {
                    leitorDeDados = comando.ExecuteReader();
                    while (leitorDeDados.Read())
                    {
                        TipoUsuarioDomain tpUsuarioConsultado = new TipoUsuarioDomain();
                        tpUsuarioConsultado.titulo = leitorDeDados[0].ToString();
                        tpUsuarioConsultado.idTipoUsuario = Convert.ToInt32(leitorDeDados[1]);
                        tiposUsuarios.Add(tpUsuarioConsultado);
                    }
                }
            }
            return tiposUsuarios;
        }
    }
}