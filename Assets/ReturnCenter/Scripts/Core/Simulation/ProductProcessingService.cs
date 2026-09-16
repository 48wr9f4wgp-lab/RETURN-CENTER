using System;
using ReturnCenter.Core.Domain;

namespace ReturnCenter.Core.Simulation
{
    public sealed class ProductProcessingService
    {
        private readonly ProductValuationService _valuation = new ProductValuationService();

        public ProcessingOutcome Process(Product product, DispositionRoute route)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (product.State != ProductLifecycleState.Inspected)
                throw new InvalidOperationException("Inspect the product before choosing a disposition route.");

            ProcessingOutcome outcome;
            switch (route)
            {
                case DispositionRoute.Resell:
                    outcome = new ProcessingOutcome(route, _valuation.EstimateAsIsResale(product), 2.0);
                    break;
                case DispositionRoute.Repair:
                    outcome = new ProcessingOutcome(route, _valuation.EstimateRepairedResale(product), product.RepairCost + 4.0);
                    break;
                case DispositionRoute.Recycle:
                    outcome = new ProcessingOutcome(route, _valuation.EstimateRecycleValue(product), 1.0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(route), route, null);
            }

            product.MarkProcessed();
            return outcome;
        }
    }
}
