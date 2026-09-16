namespace ReturnCenter.Core.Simulation
{
    public sealed class EconomyState
    {
        public double Cash { get; private set; }
        public int ProcessedCount { get; private set; }
        public double TotalRecoveredValue { get; private set; }

        public EconomyState(double startingCash)
        {
            Cash = startingCash;
        }

        public void Apply(ProcessingOutcome outcome)
        {
            Cash += outcome.NetProfit;
            ProcessedCount++;
            TotalRecoveredValue += outcome.Revenue;
        }
    }
}
