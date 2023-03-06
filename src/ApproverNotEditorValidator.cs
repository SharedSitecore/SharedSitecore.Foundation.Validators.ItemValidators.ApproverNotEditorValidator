using Sitecore.Data.Validators;
using Sitecore.Data.Items;

namespace SharedSitecore.Foundation.Validators.ItemValidators.ApproverNotEditorValidator {
    public class ApproverNotEditorValidation : StandardValidator
    {
        public override string Name => "Prevent self-approval validator";

        protected override ValidatorResult Evaluate() {
            Item item = GetItem();
            if (item != null && item.State != null && item.State.GetWorkflow() != null) {
                Sitecore.Workflows.IWorkflow workflow = item.State.GetWorkflow();
                if (workflow.GetState(item) != null && workflow.GetState(item).FinalState) {
                    if (item.Statistics.UpdatedBy == Sitecore.Context.User.Name) {
                        Text = "You cannot approve your own content item";
                        return GetFailedResult(ValidatorResult.Error);
                    }
                }
            }
            return ValidatorResult.Valid;
        }

        protected override ValidatorResult GetMaxValidatorResult() {
            throw new System.NotImplementedException();
        }
    }
}