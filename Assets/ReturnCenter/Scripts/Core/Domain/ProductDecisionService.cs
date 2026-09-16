namespace ReturnCenter.Core.Domain
{
    public sealed class ProductDecisionService
    {
        private readonly ProductValuationService _valuation = new ProductValuationService();

        public DispositionRoute Recommend(Product product)
        {
            var asIs = _valuation.EstimateAsIsResale(product) - 2.0;
            var repaired = _valuation.EstimateRepairedResale(product) - product.RepairCost - 4.0;
            var recycled = _valuation.EstimateRecycleValue(product) - 1.0;

            if (product.Fault == FaultType.None && asIs >= repaired && asIs >= recycled)
                return DispositionRoute.Resell;

            if (repaired > asIs && repaired > recycled)
                return DispositionRoute.Repair;

            return asIs >= recycled ? DispositionRoute.Resell : DispositionRoute.Recycle;
        }
    }
}
