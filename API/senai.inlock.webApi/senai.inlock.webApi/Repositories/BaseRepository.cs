using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    /// <summary>
    /// Classe criada para que a string de conexão não precise ser definida em todos os repositórios individualmente
    /// </summary>
    public class BaseRepository
    {
        protected string stringConexao = "Data Source=PEDRO-PC\\SQLEXPRESS; initial catalog=Rental; user Id=sa; pwd=senai@123";
    }
}