using FluentValidation;
using MovieAppCase.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Validations
{
    public class MovieVoteDtoValidator:AbstractValidator<MovieVoteDto>
    {
        public MovieVoteDtoValidator()
        {
            RuleFor(x => x.SourceId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Vote).NotNull().NotEmpty().WithMessage("{PropertyName} zorunludur. ").GreaterThanOrEqualTo(1).LessThanOrEqualTo(10).WithMessage("{PropertyName} 1 ile 10 arasında olmalıdır.");
            RuleFor(x => x.Note).NotNull().NotEmpty().WithMessage("{PropertyName} zorunludur. ");
        }
    }
}
