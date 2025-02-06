using FirstApi.Dto.User;
using FirstApi.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    
    private readonly IUserInterface _userInterface;
    
    public UserController(IUserInterface userInterface)
    {
        _userInterface = userInterface;
    }
    
    [HttpGet("users")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userInterface.GetAll();
        return Ok(users);
    }
    
    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userInterface.GetById(id);
        return Ok(user);
    }
    
    [HttpPost("user")]
    public async Task<IActionResult> Create(CreateUserDto user)
    {
        var createdUser = await _userInterface.Create(user);
        return Ok(createdUser);
    }
    
    [HttpPut("user/{id}")]
    public async Task<IActionResult> Update(UpdateUserDto user, int id)
    {
        var updatedUser = await _userInterface.Update(user, id);
        return Ok(updatedUser);
    }

    [HttpDelete("user/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedUser = await _userInterface.Delete(id);
        return Ok(deletedUser);
    }
    
}