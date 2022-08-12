namespace DatabaseAnalysis.WPF.FireBird
{
    public interface INumberInOrder
    {
        long Order { get; }
        void SetOrder(long order);
    }
}
