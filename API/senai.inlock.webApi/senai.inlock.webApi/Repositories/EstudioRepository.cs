using senai.inlock.webApi.Domain;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        private string stringConexao = "Data Source=PEDRO-PC\\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=senai@123";
        public EstudioDomain BuscarPorID(int idEstudioBuscado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT nomeEstudio, idEstudio FROM ESTUDIO WHERE idEstudio = @idEstudio";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idEstudio", idEstudioBuscado);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        EstudioDomain estudioBuscado = new EstudioDomain
                        {
                            idEstudio = Convert.ToInt32(reader["idEstudio"]),

                            nomeEstudio = reader["nomeEstudio"].ToString()
                        };

                        return estudioBuscado;
                    }

                    return null;
                }
            }
        }
        public List<EstudioDomain> ListarTodos()
        {
            List<EstudioDomain> listaEstudio = new List<EstudioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idEstudio, nomeEstudio FROM ESTUDIO";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstudioDomain estudio = new EstudioDomain()
                        {
                            idEstudio = Convert.ToInt32(rdr[0]),
                            nomeEstudio = rdr[1].ToString(),
                        };

                        listaEstudio.Add(estudio);
                    }
                }
            }

            return listaEstudio;
        }
    }
}
