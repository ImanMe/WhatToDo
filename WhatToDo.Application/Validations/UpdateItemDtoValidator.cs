using FluentValidation;
using WhatToDo.Application.Dtos;
using WhatToDo.Core.Contracts;

namespace WhatToDo.Application.Validations
{
    public class UpdateItemDtoValidator : AbstractValidator<UpdateItemDto>
    {
        private readonly IToDoItemRepository _repository;

        // TODO: Confirm the accepted length of description with product team
        public UpdateItemDtoValidator(IToDoItemRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty")
                .Length(2, 200).WithMessage("Description should be between 2 and 200 characters")
                .MustAsync((x, _) => HasDuplicateValue(x)).WithMessage("This item already exists.");
        }

        private async Task<bool> HasDuplicateValue(string description)
        {
            var result = await _repository.IsDuplicateAsync(description);
            return !result;
        }
    }
}
