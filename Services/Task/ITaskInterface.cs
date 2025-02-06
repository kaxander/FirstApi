using FirstApi.Dto.Task;
using FirstApi.Models;

namespace FirstApi.Services.Task;

public interface ITaskInterface
{
    Task<Response<List<Models.Task>>> GetAll();
    
    Task<Response<Models.Task>> GetById(int id);
    
    Task<Response<Models.Task>> Create(CreateTaskDto task);
    
    Task<Response<Models.Task>> Update(UpdateTaskDto task, int id);
    
    Task<Response<Models.Task>> Delete(int id);
    
}