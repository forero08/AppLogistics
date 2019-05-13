using AppLogistics.Objects;
using System;

namespace AppLogistics.Validators
{
    public interface IEpsValidator : IValidator
    {
        bool CanCreate(EpsView view);
        bool CanEdit(EpsView view);
    }
}
