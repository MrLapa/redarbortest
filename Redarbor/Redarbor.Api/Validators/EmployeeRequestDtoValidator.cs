using FluentValidation;
using Redarbor.Core.DTOs.Request;
using System;

namespace Redarbor.Api.Validators
{
    public class EmployeeRequestDtoValidator : AbstractValidator<EmployeeRequestDto>
    {
        private const string DATE_FORMAT_MESSAGE = "Date format incorrect must be yyyy-MM-dd hh-mm-ss";
        public EmployeeRequestDtoValidator()
        {
            RuleFor(m => m.CreatedOn)
                .NotNull()
                .Must(isValidDate)
                .WithMessage(DATE_FORMAT_MESSAGE)
                .Length(1, 200);

            RuleFor(m => m.DeletedOn)
                .NotNull()
                .Must(isValidDate)
                .WithMessage(DATE_FORMAT_MESSAGE)
                .Length(1, 200);

            RuleFor(m => m.Email)
                .Length(1, 254);

            RuleFor(m => m.Fax)
                .Length(1, 50);

            RuleFor(m => m.Name)
                .NotNull()
                .Length(1, 50);

            RuleFor(m => m.Lastlogin)
                .NotNull()
                .Must(isValidDate)
                .WithMessage(DATE_FORMAT_MESSAGE)
                .Length(1, 200);

            RuleFor(m => m.Password)
                .NotNull()
                .Length(1, 50);

            RuleFor(m => m.Telephone)
                .Length(1, 50);

            RuleFor(m => m.UpdatedOn)
                .NotNull()
                .Must(isValidDate)
                .WithMessage(DATE_FORMAT_MESSAGE)
                .Length(1, 200);

            RuleFor(m => m.Username)
                .NotNull()
                .Length(1, 50);
        }

        private bool isValidDate(string date)
        {
            var result = DateTime.TryParse(date, out _);
            
            return result;
        }
    }
}
