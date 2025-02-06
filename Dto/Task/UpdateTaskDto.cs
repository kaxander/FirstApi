namespace FirstApi.Dto.Task;

public class UpdateTaskDto
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public Models.Task.EPriotity Priority { get; set; }
    
    public Models.Task.EStatus Status { get; set; }
}