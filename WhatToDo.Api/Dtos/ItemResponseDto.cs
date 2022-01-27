namespace WhatToDo.Api.Dtos
{
    public class ItemResponseDto
    {
        public int Id { get; set; }
        public string CreatedDate { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
