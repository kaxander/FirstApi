using FirstApi.Dto.User;
using FirstApi.Models;
using Task = FirstApi.Models.Task;

namespace FirstApi.Services.User;

public interface IUserInterface
{
    
    Task<Response<List<Models.User>>> GetAll();
    
    Task<Response<Models.User>> GetById(int id);
    
    Task<Response<Models.User>> Create(CreateUserDto user);

    Task<Response<Models.User>> Update(UpdateUserDto user, int id);
    
    Task<Response<Models.User>> Delete(int id);

}