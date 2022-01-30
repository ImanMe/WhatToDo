namespace WhatToDo.Api.Dtos
{
    public class UpdateItemDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
