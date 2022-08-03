using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DatabaseAnalysis.WPF.FireBird
{
    public abstract class RamAccess : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
