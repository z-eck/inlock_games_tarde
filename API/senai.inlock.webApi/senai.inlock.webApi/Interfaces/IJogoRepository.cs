using senai.inlock.webApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IJogoRepository
    {
        List<JogoDomain> ListarTodos();
        JogoDomain BuscarPorID(int idJogoBuscado);
        void Cadastrar(JogoDomain novoJogo);
        void Atualizar(JogoDomain JogoAtualizado, int idJogoAtualizado);
        void Deletar(int idJogoDeletado);
    }
}