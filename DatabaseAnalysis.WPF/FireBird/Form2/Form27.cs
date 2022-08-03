using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DatabaseAnalysis.WPF.FireBird
{
    [Serializable]
    public class Form27 : Form2
    {
        public Form27() : base()
        {
            FormNum.Value = "2.7";
            //NumberOfFields.Value = 13;
            Validate_all();
        }
        private void Validate_all()
        {
            ObservedSourceNumber_Validation(ObservedSourceNumber);
            RadionuclidName_Validation(RadionuclidName);
            AllowedWasteValue_Validation(AllowedWasteValue);
            FactedWasteValue_Validation(FactedWasteValue);
            WasteOutbreakPreviousYear_Validation(WasteOutbreakPreviousYear);
        }
        public override bool Object_Validation()
        {
            return !(ObservedSourceNumber.HasErrors ||
            RadionuclidName.HasErrors ||
            AllowedWasteValue.HasErrors ||
            FactedWasteValue.HasErrors ||
            WasteOutbreakPreviousYear.HasErrors);
        }

        //ObservedSourceNumber property
        #region  ObservedSourceNumber
        public string ObservedSourceNumber_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> ObservedSourceNumber
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(ObservedSourceNumber)))
                {
                    ((RamAccess<string>)Dictionary[nameof(ObservedSourceNumber)]).Value = ObservedSourceNumber_DB;
                    return (RamAccess<string>)Dictionary[nameof(ObservedSourceNumber)];
                }
                else
                {
                    var rm = new RamAccess<string>(ObservedSourceNumber_Validation, ObservedSourceNumber_DB);
                    rm.PropertyChanged += ObservedSourceNumberValueChanged;
                    Dictionary.Add(nameof(ObservedSourceNumber), rm);
                    return (RamAccess<string>)Dictionary[nameof(ObservedSourceNumber)];
                }
            }
            set
            {
                ObservedSourceNumber_DB = value.Value;
                OnPropertyChanged(nameof(ObservedSourceNumber));
            }
        }
        //If change this change validation
        private void ObservedSourceNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                ObservedSourceNumber_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool ObservedSourceNumber_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //ObservedSourceNumber property
        #endregion

        //RadionuclidName property
        #region  RadionuclidName
        public string RadionuclidName_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> RadionuclidName
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(RadionuclidName)))
                {
                    ((RamAccess<string>)Dictionary[nameof(RadionuclidName)]).Value = RadionuclidName_DB;
                    return (RamAccess<string>)Dictionary[nameof(RadionuclidName)];
                }
                else
                {
                    var rm = new RamAccess<string>(RadionuclidName_Validation, RadionuclidName_DB);
                    rm.PropertyChanged += RadionuclidNameValueChanged;
                    Dictionary.Add(nameof(RadionuclidName), rm);
                    return (RamAccess<string>)Dictionary[nameof(RadionuclidName)];
                }
            }
            set
            {
                RadionuclidName_DB = value.Value;
                OnPropertyChanged(nameof(RadionuclidName));
            }
        }


        private void RadionuclidNameValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                RadionuclidName_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool RadionuclidName_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            var tmpstr = value.Value.ToLower().Replace(" ", "");
            return true;
        }
        //RadionuclidName property
        #endregion

        //AllowedWasteValue property
        #region  AllowedWasteValue
        public string AllowedWasteValue_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> AllowedWasteValue
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(AllowedWasteValue)))
                {
                    ((RamAccess<string>)Dictionary[nameof(AllowedWasteValue)]).Value = AllowedWasteValue_DB;
                    return (RamAccess<string>)Dictionary[nameof(AllowedWasteValue)];
                }
                else
                {
                    var rm = new RamAccess<string>(AllowedWasteValue_Validation, AllowedWasteValue_DB);
                    rm.PropertyChanged += AllowedWasteValueValueChanged;
                    Dictionary.Add(nameof(AllowedWasteValue), rm);
                    return (RamAccess<string>)Dictionary[nameof(AllowedWasteValue)];
                }
            }
            set
            {
                AllowedWasteValue_DB = value.Value;
                OnPropertyChanged(nameof(AllowedWasteValue));
            }
        }


        private void AllowedWasteValueValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        AllowedWasteValue_DB = value1;
                        return;
                    }
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
                    }
                    catch (Exception ex)
                    { }
                }
                AllowedWasteValue_DB = value1;
            }
        }
        private bool AllowedWasteValue_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("прим."))
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //AllowedWasteValue property
        #endregion

        //FactedWasteValue property
        #region  FactedWasteValue
        public string FactedWasteValue_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> FactedWasteValue
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(FactedWasteValue)))
                {
                    ((RamAccess<string>)Dictionary[nameof(FactedWasteValue)]).Value = FactedWasteValue_DB;
                    return (RamAccess<string>)Dictionary[nameof(FactedWasteValue)];
                }
                else
                {
                    var rm = new RamAccess<string>(FactedWasteValue_Validation, FactedWasteValue_DB);
                    rm.PropertyChanged += FactedWasteValueValueChanged;
                    Dictionary.Add(nameof(FactedWasteValue), rm);
                    return (RamAccess<string>)Dictionary[nameof(FactedWasteValue)];
                }
            }
            set
            {
                FactedWasteValue_DB = value.Value;
                OnPropertyChanged(nameof(FactedWasteValue));
            }
        }


        private void FactedWasteValueValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        FactedWasteValue_DB = value1;
                        return;
                    }
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
                    }
                    catch (Exception ex)
                    { }
                }
                FactedWasteValue_DB = value1;
            }
        }
        private bool FactedWasteValue_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("-"))
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            string tmp = value1;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //FactedWasteValue property
        #endregion

        //WasteOutbreakPreviousYear property
        #region  WasteOutbreakPreviousYear
        public string WasteOutbreakPreviousYear_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> WasteOutbreakPreviousYear
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(WasteOutbreakPreviousYear)))
                {
                    ((RamAccess<string>)Dictionary[nameof(WasteOutbreakPreviousYear)]).Value = WasteOutbreakPreviousYear_DB;
                    return (RamAccess<string>)Dictionary[nameof(WasteOutbreakPreviousYear)];
                }
                else
                {
                    var rm = new RamAccess<string>(WasteOutbreakPreviousYear_Validation, WasteOutbreakPreviousYear_DB);
                    rm.PropertyChanged += WasteOutbreakPreviousYearValueChanged;
                    Dictionary.Add(nameof(WasteOutbreakPreviousYear), rm);
                    return (RamAccess<string>)Dictionary[nameof(WasteOutbreakPreviousYear)];
                }
            }
            set
            {
                WasteOutbreakPreviousYear_DB = value.Value;
                OnPropertyChanged(nameof(WasteOutbreakPreviousYear));
            }
        }


        private void WasteOutbreakPreviousYearValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        WasteOutbreakPreviousYear_DB = value1;
                        return;
                    }
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
                    }
                    catch (Exception ex)
                    { }
                }
                WasteOutbreakPreviousYear_DB = value1;
            }
        }
        private bool WasteOutbreakPreviousYear_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("-"))
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            string tmp = value1;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //WasteOutbreakPreviousYear property
        #endregion
    }
}
