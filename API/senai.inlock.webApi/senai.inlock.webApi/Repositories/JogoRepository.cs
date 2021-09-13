using senai.inlock.webApi.Domain;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private string stringConexao = @"CAL\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=Senai@132";
        public void Atualizar(JogoDomain JogoAtualizado, int idJogoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE JOGO SET nomeJogo = @nomeJogo WHERE idJogo = @idJogo";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", JogoAtualizado.nomeJogo);
                    cmd.Parameters.AddWithValue("@idJogo", idJogoAtualizado);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public JogoDomain BuscarPorID(int idJogoBuscado, int idEstudioBuscado, string nomeJogoBuscado, string descricaoBuscado, decimal valorBuscado, DateTime dataBuscada)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idJogo, idEstudio, nomeJogo, descricao, valor, dataLancamento FROM JOGO WHERE idJogo = @idJogo";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", idJogoBuscado);
                    cmd.Parameters.AddWithValue("idEstudio", idEstudioBuscado);
                    cmd.Parameters.AddWithValue("nomeJogo", nomeJogoBuscado);
                    cmd.Parameters.AddWithValue("descricao", descricaoBuscado);
                    cmd.Parameters.AddWithValue("valor", valorBuscado);
                    cmd.Parameters.AddWithValue("dataLancamento", dataBuscada);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        JogoDomain jogoBuscado = new JogoDomain
                        {
                            idJogo = Convert.ToInt32(reader["idJogo"]),
                            idEstudio = Convert.ToInt32(reader["idEstudio"]),
                            nomeJogo = reader["nomeJogo"].ToString(),
                            descricao = reader["descricao"].ToString(),
                            valor = Convert.ToDecimal(reader["valor"]),
                            dataLancamento = Convert.ToDateTime(reader["dataLancamento"])
                        };

                        return jogoBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(JogoDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO JOGO (idEstudio, nomeJogo, descricao, valor, dataLancamento) VALUES (@idEstudio, @nomeJogo, @descricao, @valor, @dataLancamento)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idEstudio", novoJogo.idEstudio);
                    cmd.Parameters.AddWithValue("@nomeJogo", novoJogo.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", novoJogo.descricao);
                    cmd.Parameters.AddWithValue("@valor", novoJogo.valor);
                    cmd.Parameters.AddWithValue("@dataLancamento", novoJogo.dataLancamento);


                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void Deletar(int idJogoDeletado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM JOGO WHERE idJogo = @idJogo";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", idJogoDeletado);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogoDomain> ListarTodos()
        {
            List<JogoDomain> listaJogo = new List<JogoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idJogo, idEstudio, nomeJogo, descricao, valor, dataLancamento FROM JOGO";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        JogoDomain jogo = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr[0]),
                            idEstudio = Convert.ToInt32(rdr[1]),
                            nomeJogo = rdr[2].ToString(),
                            descricao = rdr[3].ToString(),
                            valor = Convert.ToDecimal(rdr[4]),
                            dataLancamento = Convert.ToDateTime(rdr[5]),
                        };

                        listaJogo.Add(jogo);
                    }
                }
            }

            return listaJogo;
        }
    }
}
