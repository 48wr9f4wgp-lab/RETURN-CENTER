namespace ReturnCenter.Core.Domain
{
    public sealed class ProductValuationService
    {
        public double EstimateAsIsResale(Product product)
        {
            var conditionMultiplier = product.Condition switch
            {
                ProductCondition.A => 0.85,
                ProductCondition.B => 0.68,
                ProductCondition.C => 0.45,
                ProductCondition.D => 0.18,
                _ => 0.25
            };

            var faultMultiplier = product.Fault switch
            {
                FaultType.None => 1.0,
                FaultType.CosmeticDamage => 0.82,
                FaultType.MissingAccessory => 0.72,
                FaultType.PowerFailure => 0.30,
                FaultType.MechanicalFailure => 0.25,
                _ => 0.50
            };

            return RoundMoney(product.BaseValue * conditionMultiplier * faultMultiplier);
        }

        public double EstimateRepairedResale(Product product)
        {
            var conditionMultiplier = product.Condition switch
            {
                ProductCondition.A => 0.88,
                ProductCondition.B => 0.78,
                ProductCondition.C => 0.65,
                ProductCondition.D => 0.45,
                _ => 0.50
            };

            return RoundMoney(product.BaseValue * conditionMultiplier);
        }

        public double EstimateRecycleValue(Product product)
        {
            return RoundMoney(product.BaseValue * 0.08);
        }

        private static double RoundMoney(double value) => System.Math.Round(value, 2);
    }
}
