namespace ReturnCenter.Core.Domain
{
    public enum ProductCondition
    {
        A,
        B,
        C,
        D
    }

    public enum FaultType
    {
        None,
        CosmeticDamage,
        MissingAccessory,
        PowerFailure,
        MechanicalFailure
    }

    public enum DispositionRoute
    {
        Resell,
        Repair,
        Recycle
    }

    public enum ProductLifecycleState
    {
        Received,
        Inspected,
        Processed
    }
}
