namespace FirstApi.Models;

public class Task
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }

    public enum EPriotity
    {
        Low,
        Medium,
        High,
    }
    public EPriotity Priority { get; set; }

    public enum EStatus
    {
        Open,
        InProgress,
        Done,
    }
    public EStatus Status { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
}