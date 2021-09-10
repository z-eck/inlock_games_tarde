using senai.inlock.webApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IEstudioRepository
    {
        List<EstudioDomain> ListarTodos();
        EstudioDomain BuscarPorID(int idEstudioBuscado);
    }
}
