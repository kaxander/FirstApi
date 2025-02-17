﻿using System.Text.Json.Serialization;

namespace FirstApi.Models;

public class User
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public bool Deleted { get; set; } = false;
    
    [JsonIgnore]
    public ICollection<Task> Tasks { get; set; }
}