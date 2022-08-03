using System.ComponentModel;

namespace DatabaseAnalysis.WPF.FireBird
{
    public interface IKey : INotifyPropertyChanged, INumberInOrder
    {
        int Id { get; set; }
    }
}
