using FluentValidation;
using WhatToDo.Api.Dtos;
using WhatToDo.Core.Contracts;

namespace WhatToDo.Api.Validations
{
    public class UpdateItemDtoValidator : AbstractValidator<UpdateItemDto>
    {
        // TODO: Confirm the accepted length of description with product team
        public UpdateItemDtoValidator(IToDoItemRepository repository)
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty")
                .Length(1, 200).WithMessage("Description should be between 2 and 200 characters");
        }
    }
}
