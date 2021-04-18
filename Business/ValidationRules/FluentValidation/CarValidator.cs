using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(car => car.DailyPrice).NotEmpty();
            RuleFor(car => car.DailyPrice).GreaterThanOrEqualTo(0);
            RuleFor(car => car.Model).NotEmpty();
            RuleFor(car => car.Model).MinimumLength(3);
        }
    }
}
