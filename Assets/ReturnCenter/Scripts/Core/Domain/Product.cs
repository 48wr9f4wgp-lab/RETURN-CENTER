using System;

namespace ReturnCenter.Core.Domain
{
    [Serializable]
    public sealed class Product
    {
        public string Id { get; }
        public string Archetype { get; }
        public double BaseValue { get; }
        public ProductCondition Condition { get; }
        public FaultType Fault { get; }
        public double RepairCost { get; }
        public ProductLifecycleState State { get; private set; }

        public Product(
            string id,
            string archetype,
            double baseValue,
            ProductCondition condition,
            FaultType fault,
            double repairCost)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Product id is required.", nameof(id));
            if (string.IsNullOrWhiteSpace(archetype)) throw new ArgumentException("Product archetype is required.", nameof(archetype));
            if (baseValue <= 0) throw new ArgumentOutOfRangeException(nameof(baseValue));
            if (repairCost < 0) throw new ArgumentOutOfRangeException(nameof(repairCost));

            Id = id;
            Archetype = archetype;
            BaseValue = baseValue;
            Condition = condition;
            Fault = fault;
            RepairCost = repairCost;
            State = ProductLifecycleState.Received;
        }

        public void MarkInspected()
        {
            if (State == ProductLifecycleState.Processed)
                throw new InvalidOperationException("Processed products cannot be inspected again.");

            State = ProductLifecycleState.Inspected;
        }

        public void MarkProcessed()
        {
            if (State != ProductLifecycleState.Inspected)
                throw new InvalidOperationException("Product must be inspected before processing.");

            State = ProductLifecycleState.Processed;
        }
    }
}
