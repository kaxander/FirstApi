using FirstApi.Data;
using FirstApi.Dto.User;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Services.User;

public class UserService : IUserInterface
{
    
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Response<List<Models.User>>> GetAll()
    {
        Response<List<Models.User>> response = new Response<List<Models.User>>();
        
        try
        {

            var users = await _context.Users.ToListAsync();
            
            response.Data = users;
            response.Message = "Usuários recuperados com sucesso.";
            response.Success = true;
            response.StatusCode = 200;
            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
            response.StatusCode = e.GetHashCode();
            return response;
        }
    }

    public async Task<Response<Models.User>> GetById(int id)
    {
        Response<Models.User> response = new Response<Models.User>();
        
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(userDb => userDb.Id == id);

            if (user == null || user.Deleted)
            {
                response.Message = "Nenhum registro encontrado.";
                response.Success = false;
                response.StatusCode = 404;
                return response;
            }

            response.Data = user;
            response.Message = "usuario localizado com sucesso.";
            response.Success = true;
            response.StatusCode = 200;

            return response;

        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
            response.StatusCode = e.GetHashCode();
            return response;
        }
    }

    public async Task<Response<Models.User>> Create(CreateUserDto user)
    {
        
        Response<Models.User> response = new Response<Models.User>();

        try
        {
            var createUser = new Models.User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password

            };
            
            _context.Users.Add(createUser);
            await _context.SaveChangesAsync();

            response.Data = createUser;
            response.Message = "Usuário criado com sucesso.";
            response.Success = true;
            response.StatusCode = 201;
            return response;
            
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
            response.StatusCode = e.GetHashCode();
            return response;
        }
    }

    public async Task<Response<Models.User>> Update(UpdateUserDto user, int id)
    {
        
        Response<Models.User> response = new Response<Models.User>();
        
        try
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(userDb => userDb.Id == id);

            if (userDb == null || userDb.Deleted)
            {
                response.Message = "Nenhum registro encontrado.";
                response.Success = false;
                response.StatusCode = 404;
                return response;
            }
            
            userDb.Name = string.IsNullOrEmpty(user.Name) ? userDb.Name : user.Name;
            userDb.Email = string.IsNullOrEmpty(user.Email) ? userDb.Email : user.Email;
            userDb.Password = string.IsNullOrEmpty(user.Password) ? userDb.Password : user.Password;
            
            _context.Users.Update(userDb);
            await _context.SaveChangesAsync();

            response.Message = "Usuário atualizado com sucesso.";
            response.Success = true;
            response.StatusCode = 200;
            return response;


        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
            response.StatusCode = e.GetHashCode();
            return response;
        }
    }

    public async Task<Response<Models.User>> Delete(int id)
    {
        Response<Models.User> response = new Response<Models.User>();

        try
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(userDb => userDb.Id == id);
            
            if (user == null || user.Deleted)
            {
                response.Message = "Nenhum registro encontrado.";
                response.Success = false;
                response.StatusCode = 404;
                return response;
            }
            
            user.Deleted = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            response.Message = "Usuário deletado com sucesso.";
            response.Success = true;
            response.StatusCode = 200;
            return response;


        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
            response.StatusCode = e.GetHashCode();
            return response;
        }
    }
}