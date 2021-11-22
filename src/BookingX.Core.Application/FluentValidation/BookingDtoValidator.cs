using System;
using BookingX.Core.Application.Dtos;
using FluentValidation;

namespace BookingX.Core.Application.FluentValidation
{
    public class BookingDtoValidator : AbstractValidator<BookingDto>
    {
        public BookingDtoValidator()
        {
            RuleFor(b => b.Id).NotEmpty();
            
            RuleFor(b => b.CustomerId).NotEmpty();
            
            RuleFor(b => b.RoomId).NotEmpty();

            RuleFor(b => b.StartDate)
            .NotEmpty()
            .IsValidDateTime()
            .Custom(
                (x, context) =>
                {
                    DateTime startDate;

                    if (!DateTime.TryParse(x, out startDate))
                        return;

                    var today = DateTime.Today;

                    if ((startDate.Date == today))
                        context.AddFailure("StartDate", "The start date cannot be today's date");
                    
                    if ((startDate.Date - today).TotalDays > 30)
                        context.AddFailure("StartDate", "The start date must be within the next 30 days");
                }
            );

            RuleFor(b => b.EndDate)
            .NotEmpty()
            .IsValidDateTime()
            .Custom(
                (x, context) =>
                {
                    DateTime endDate;

                    if (!DateTime.TryParse(x, out endDate))
                        return;

                    if(endDate.Date <= DateTime.Today)
                        context.AddFailure("EndDate", "The end date must be a future day");
                }
            );;

            RuleFor( b => new { b.StartDate, b.EndDate }).Custom(
                (x, context) =>
                {
                    DateTime startDate;
                    DateTime endDate;

                    if (!DateTime.TryParse(x.StartDate, out startDate))
                        return;
                        
                    if (!DateTime.TryParse(x.EndDate, out endDate))
                        return;

                    var today = DateTime.Today;

                    if ((startDate > endDate))
                        context.AddFailure(nameof(x.StartDate), "The start date cannot be greater than the end date");
                    
                    if ((endDate.Date - startDate.Date).TotalDays > 3)
                        context.AddFailure(nameof(x.EndDate), "The end date cannot be more than 3 days greater than the start date");
                }
            );

        }
        private bool BeAValidDate(string value)
        {
            return DateTime.TryParse(value, out DateTime _);
        }
    }
}