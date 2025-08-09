using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoAppSnowlyCode.Business.Interfaces;
using TodoAppSnowlyCode.Data.Models;
using TodoAppSnowlyCode.DTO;

namespace TodoAppSnowlyCode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly IToDoItemService _todoItemService;
        private readonly IMapper _mapper;

        public TodosController(IToDoItemService todoItemService, IMapper mapper)
        {
            _todoItemService = todoItemService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken ct)
            => Ok(await _todoItemService.GetAllAsync(ct));

        [HttpGet("{id}", Name = "GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var res = await _todoItemService.GetByIdAsync(id, new CancellationToken());

            if (res is null)
                return NotFound();
            
            return Ok(_mapper.Map<ToDoItem, GetToDoItemDto>(res));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateToDoItemDto todoItem, CancellationToken ct)
        {
            var itemToAdd = _mapper.Map<CreateToDoItemDto, ToDoItem>(todoItem);
            var newItem = await _todoItemService.AddAsync(itemToAdd, ct);
            
            return CreatedAtRoute(nameof(GetByIdAsync), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateToDoItemDto todoItem, CancellationToken ct)
        {
            var itemToUpdate = _mapper.Map<UpdateToDoItemDto, ToDoItem>(todoItem);
            var res = await _todoItemService.UpdateAsync(id, itemToUpdate, ct);

            if (res is null)
                return NotFound();

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken ct)
        {
            if (await _todoItemService.DeleteAsync(id, ct) is false)
                return NotFound();

            return Ok();
        }
    }
}
