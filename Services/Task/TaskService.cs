using FirstApi.Data;
using FirstApi.Dto.Task;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Services.Task;

public class TaskService : ITaskInterface
{
    
    private readonly AppDbContext _context;
    public TaskService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Response<List<Models.Task>>> GetAll()
    {
        Response<List<Models.Task>> response = new Response<List<Models.Task>>();

        try
        {
            var tasks = await _context.Tasks.ToListAsync();
            
            response.Data = tasks;
            response.Message = "Tarefas recuperadas com sucesso.";
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

    public async Task<Response<Models.Task>> GetById(int id)
    {
        Response<Models.Task> response = new Response<Models.Task>();

        try
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(taskDb => taskDb.Id == id);

            if (task == null)
            {
                response.Message = "Nenhum registro encontrado.";
                response.Success = false;
                response.StatusCode = 404;
                return response;
            }
            
            response.Data = task;
            response.Message = "Tarefa localizada com sucesso.";
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

    public async Task<Response<Models.Task>> Create(CreateTaskDto task)
    {
        Response<Models.Task> response = new Response<Models.Task>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(userDb => userDb.Id == task.UserId);

            if (user == null || user.Deleted)
            {
                response.Message = "Nenhum registro de usuário encontrado.";
                response.Success = false;
                response.StatusCode = 404;
                return response;
            }
            
            var createTask = new Models.Task
            {
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                UserId = task.UserId
            };
            
            _context.Tasks.Add(createTask);
            await _context.SaveChangesAsync();
            
            response.Data = await _context.Tasks.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == createTask.Id);
            response.Message = "Tarefa criada com sucesso.";
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

    public async Task<Response<Models.Task>> Update(UpdateTaskDto task, int id)
    {
        Response<Models.Task> response = new Response<Models.Task>();

        try
        {
            var taskDb = await _context.Tasks.FirstOrDefaultAsync(taskDb => taskDb.Id == id);
            
            if (taskDb == null)
            {
                response.Message = "Nenhum registro encontrado.";
                response.Success = false;
                response.StatusCode = 404;
                return response;
            }
            
            taskDb.Title = string.IsNullOrWhiteSpace(task.Title) ? taskDb.Title : task.Title;
            taskDb.Description = string.IsNullOrWhiteSpace(task.Description) ? taskDb.Title : task.Description;
            taskDb.Priority = task.Priority;
            taskDb.Status = task.Status;
            
            _context.Tasks.Update(taskDb);
            await _context.SaveChangesAsync();
            
            response.Message = "Tarefa atualizada com sucesso.";
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

    public async Task<Response<Models.Task>> Delete(int id)
    {
        Response<Models.Task> response = new Response<Models.Task>();

        try
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(taskDb => taskDb.Id == id);
            
            if (task == null)
            {
                response.Message = "Nenhum registro encontrado.";
                response.Success = false;
                response.StatusCode = 404;
                return response;
            }
            
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            
            response.Message = "Tarefa deletada com sucesso.";
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