using FluentValidation;

namespace SealabAPI.DataAccess.Models
{
    public abstract class AbstractModelValidator<T> : AbstractValidator<T> where T : class { }
}
