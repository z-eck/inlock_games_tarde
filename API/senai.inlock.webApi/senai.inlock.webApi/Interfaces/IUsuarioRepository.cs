using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IUsuarioRepository
    {
        List<UsuarioDomain> listarTodos();
        UsuarioDomain buscarPorID(int idUsuarioBuscado);
        void cadastrar(UsuarioDomain novoUsuario);
        void atualizar(UsuarioDomain usuarioAtualizado, int idUsuarioAtualizado);
        void deletar(int idUsuarioDeletado);
    }
}