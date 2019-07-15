using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;

namespace AppLogistics.Validators
{
    public class ServiceValidator : BaseValidator, IServiceValidator
    {
        public ServiceValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(ServiceCreateEditView view)
        {
            return AreRelatedRateAndClient(view.RateId, view.RateClientId) && ModelState.IsValid;
        }

        public bool CanEdit(ServiceCreateEditView view)
        {
            return AreRelatedRateAndClient(view.RateId, view.RateClientId) && ModelState.IsValid;
        }

        private bool AreRelatedRateAndClient(int rateId, int clientId)
        {
            var rate = UnitOfWork.Get<Rate>(rateId);
            if (rate.ClientId != clientId)
            {
                Alerts.AddError(Validation.For<ServiceCreateEditView>("RateAndClientUnrelated"));
                return false;
            }

            return true;
        }
    }
}
