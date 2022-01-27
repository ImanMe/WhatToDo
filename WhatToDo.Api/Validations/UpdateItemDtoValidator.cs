using FluentValidation;
using WhatToDo.Api.Dtos;
using WhatToDo.Core.Contracts;

namespace WhatToDo.Api.Validations
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
            return await _repository.IsDuplicateAsync(description);
        }
    }
}
