using System;
using ReturnCenter.Core.Domain;
using ReturnCenter.Core.Simulation;

namespace ReturnCenter.Application
{
    public sealed class ManualProcessingSession
    {
        private readonly ProductProcessingService _processing = new ProductProcessingService();
        private readonly ProductDecisionService _decision = new ProductDecisionService();

        public Product CurrentProduct { get; }
        public EconomyState Economy { get; }
        public ProcessingOutcome LastOutcome { get; private set; }

        public ManualProcessingSession(Product product, double startingCash)
        {
            CurrentProduct = product ?? throw new ArgumentNullException(nameof(product));
            Economy = new EconomyState(startingCash);
        }

        public void Inspect()
        {
            CurrentProduct.MarkInspected();
        }

        public DispositionRoute GetRecommendation()
        {
            if (CurrentProduct.State != ProductLifecycleState.Inspected)
                throw new InvalidOperationException("Inspect before requesting a recommendation.");

            return _decision.Recommend(CurrentProduct);
        }

        public ProcessingOutcome Process(DispositionRoute route)
        {
            LastOutcome = _processing.Process(CurrentProduct, route);
            Economy.Apply(LastOutcome);
            return LastOutcome;
        }
    }
}
