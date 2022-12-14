using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DatabaseAnalysis.WPF.FireBird
{
    public abstract class Form : INotifyPropertyChanged, IKey, INumberInOrder
    {
        public int Id { get; set; }
        [NotMapped]
        protected Dictionary<string, RamAccess> Dictionary { get; set; } = new Dictionary<string, RamAccess>();

        #region FormNum
        public string FormNum_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> FormNum
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(FormNum)))
                {
                    ((RamAccess<string>)Dictionary[nameof(FormNum)]).Value = FormNum_DB;
                    return (RamAccess<string>)Dictionary[nameof(FormNum)];
                }
                else
                {
                    var rm = new RamAccess<string>(FormNum_Validation, FormNum_DB);
                    rm.PropertyChanged += FormNumValueChanged;
                    Dictionary.Add(nameof(FormNum), rm);
                    return (RamAccess<string>)Dictionary[nameof(FormNum)];
                }
            }
            set
            {
                FormNum_DB = value.Value;
                OnPropertyChanged(nameof(FormNum));
            }
        }
        private void FormNumValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                FormNum_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool FormNum_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        [NotMapped]
        public long Order
        {
            get
            {
                return NumberInOrder_DB;
            }
        }

        public void SetOrder(long index)
        {
            if (NumberInOrder_DB != (int)index)
            {
                NumberInOrder_DB = (int)index;
                OnPropertyChanged(nameof(NumberInOrder));
                OnPropertyChanged(nameof(Order));
            }
        }

        #region NumberInOrder
        public int NumberInOrder_DB { get; set; } = 0;

        [NotMapped]
        public RamAccess<int> NumberInOrder
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(NumberInOrder)))
                {
                    ((RamAccess<int>)Dictionary[nameof(NumberInOrder)]).Value = NumberInOrder_DB;
                    return (RamAccess<int>)Dictionary[nameof(NumberInOrder)];
                }
                else
                {
                    var rm = new RamAccess<int>(NumberInOrder_Validation, NumberInOrder_DB);
                    rm.PropertyChanged += NumberInOrderValueChanged;
                    Dictionary.Add(nameof(NumberInOrder), rm);
                    return (RamAccess<int>)Dictionary[nameof(NumberInOrder)];
                }
            }
            set
            {
                NumberInOrder_DB = value.Value;
                OnPropertyChanged(nameof(NumberInOrder));
            }
        }
        private void NumberInOrderValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                NumberInOrder_DB = ((RamAccess<int>)Value).Value;
            }
        }
        private bool NumberInOrder_Validation(RamAccess<int> value)//Ready
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        #region NumberOfFields
        public int NumberOfFields_DB { get; set; } = 0;
        [NotMapped]
        public RamAccess<int> NumberOfFields
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(NumberOfFields)))
                {
                    ((RamAccess<int>)Dictionary[nameof(NumberOfFields)]).Value = NumberOfFields_DB;
                    return (RamAccess<int>)Dictionary[nameof(NumberOfFields)];
                }
                else
                {
                    var rm = new RamAccess<int>(NumberOfFields_Validation, NumberOfFields_DB);
                    rm.PropertyChanged += NumberOfFieldsValueChanged;
                    Dictionary.Add(nameof(NumberOfFields), rm);
                    return (RamAccess<int>)Dictionary[nameof(NumberOfFields)];
                }
            }
            set
            {
                NumberOfFields_DB = value.Value;
                OnPropertyChanged(nameof(NumberOfFields));
            }
        }
        private void NumberOfFieldsValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                NumberOfFields_DB = ((RamAccess<int>)Value).Value;
            }
        }
        private bool NumberOfFields_Validation(RamAccess<int> value)
        {
            value.ClearErrors();
            return true;

        }
        #endregion

        #region For_Validation
        public abstract bool Object_Validation();
        #endregion

        #region INotifyPropertyChanged
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
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
