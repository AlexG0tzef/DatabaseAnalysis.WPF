using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseAnalysis.WPF.FireBird
{
    public abstract class Form2 : Form
    {

        public Form2()
        {

        }
        protected void InPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            OnPropertyChanged(args.PropertyName);
        }

        #region CorrectionNumber

        public byte CorrectionNumber_DB { get; set; } = 0;

        [NotMapped]
        public RamAccess<byte> CorrectionNumber
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(CorrectionNumber)))
                {
                    ((RamAccess<byte>)Dictionary[nameof(CorrectionNumber)]).Value = CorrectionNumber_DB;
                    return (RamAccess<byte>)Dictionary[nameof(CorrectionNumber)];
                }
                else
                {
                    var rm = new RamAccess<byte>(CorrectionNumber_Validation, CorrectionNumber_DB);
                    rm.PropertyChanged += CorrectionNumberValueChanged;
                    Dictionary.Add(nameof(CorrectionNumber), rm);
                    return (RamAccess<byte>)Dictionary[nameof(CorrectionNumber)];
                }
            }
            set
            {
                CorrectionNumber_DB = value.Value;
                OnPropertyChanged(nameof(CorrectionNumber));
            }
        }

        private void CorrectionNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                CorrectionNumber_DB = ((RamAccess<byte>)Value).Value;
            }
        }

        private bool CorrectionNumber_Validation(RamAccess<byte> value)
        {
            value.ClearErrors();
            return true;
        }

        //CorrectionNumber property

        #endregion
    }
}
