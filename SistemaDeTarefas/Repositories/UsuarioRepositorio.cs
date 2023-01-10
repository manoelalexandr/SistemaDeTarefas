using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }
        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            _dbContext.SaveChanges();
            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
           UsuarioModel usuarioModel = await BuscarPorId(id);

            if(usuarioModel == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados");
            }

            usuarioModel.Name = usuario.Name;
            usuarioModel.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioModel);
            _dbContext.SaveChanges();

            return usuarioModel;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioModel = await BuscarPorId(id);

            if (usuarioModel == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados");
            }
        }

    }
}
