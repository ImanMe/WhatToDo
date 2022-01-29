using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WhatToDo.Application.Dtos;
using WhatToDo.Application.Errors;
using WhatToDo.Core.Contracts;
using WhatToDo.Core.Entities;

namespace WhatToDo.Application.Controllers;

public class ItemsController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IToDoItemRepository _repository;

    public ItemsController(IToDoItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await _repository.GetAllAsync();

        var itemsDto = _mapper.Map<IList<ItemResponseDto>>(items);

        return Ok(itemsDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null) return NotFound(new ApiResponse(404));

        var itemDto = _mapper.Map<ItemResponseDto>(item);

        return Ok(itemDto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateItemDto itemDto)
    {
        var item = _mapper.Map<ToDoItem>(itemDto);

        var createdItem = await _repository.AddAsync(item);

        if (createdItem == null) return BadRequest(new ApiResponse(400, "Problem with your item."));

        var itemResponseDto = _mapper.Map<ItemResponseDto>(createdItem);

        return CreatedAtAction("Get", new { id = itemResponseDto.Id }, itemResponseDto);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateItemDto itemDto)
    {
        var itemToBeUpdated = await _repository.GetByIdAsync(itemDto.Id);

        if (itemToBeUpdated == null)
            return BadRequest(new ApiResponse(400, "Item you attempted to edit doesn't exist"));

        if (itemToBeUpdated.Description == itemDto.Description && itemToBeUpdated.IsCompleted == itemDto.IsCompleted)
            return BadRequest(new ApiResponse(400, "Your update should result in duplicate entries"));

        itemToBeUpdated.Description = itemDto.Description;
        itemToBeUpdated.IsCompleted = itemDto.IsCompleted;

        await _repository.UpdateAsync(itemToBeUpdated);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var itemToBeDeleted = await _repository.GetByIdAsync(id);

        if (itemToBeDeleted == null)
            return BadRequest(new ApiResponse(400, "Item you attempted to delete doesn't exist"));

        await _repository.DeleteAsync(itemToBeDeleted);

        return NoContent();
    }
}