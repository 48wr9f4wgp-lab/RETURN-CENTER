using ReturnCenter.Core.Domain;

namespace ReturnCenter.Core.Simulation
{
    public sealed class ProcessingOutcome
    {
        public DispositionRoute Route { get; }
        public double Revenue { get; }
        public double Cost { get; }
        public double NetProfit => Revenue - Cost;

        public ProcessingOutcome(DispositionRoute route, double revenue, double cost)
        {
            Route = route;
            Revenue = revenue;
            Cost = cost;
        }
    }
}
