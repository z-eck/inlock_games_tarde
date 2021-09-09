using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface ITipoUsuarioRepository
    {
        List<TipoUsuarioDomain> listarTodos();
        TipoUsuarioDomain buscarPorId(int idTipoBuscado);
        void cadastrar(TipoUsuarioDomain novoTipo);
        void atualizar(TipoUsuarioDomain tipoAtualizado, int idTipoAtualizado);
        void deletar(int idTipoDeletado);
    }
}