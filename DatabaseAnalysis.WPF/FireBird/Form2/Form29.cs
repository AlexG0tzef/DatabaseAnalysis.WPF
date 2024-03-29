﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DatabaseAnalysis.WPF.FireBird
{
    [Serializable]
    public class Form29 : Form2
    {
        public Form29() : base()
        {
            FormNum.Value = "2.9";
            //NumberOfFields.Value = 8;
            Validate_all();
        }
        private void Validate_all()
        {
            WasteSourceName_Validation(WasteSourceName);
            RadionuclidName_Validation(RadionuclidName);
            AllowedActivity_Validation(AllowedActivity);
            FactedActivity_Validation(FactedActivity);
        }
        public override bool Object_Validation()
        {
            return !(WasteSourceName.HasErrors ||
            RadionuclidName.HasErrors ||
            AllowedActivity.HasErrors ||
            FactedActivity.HasErrors);
        }

        //WasteSourceName property
        #region WasteSourceName 
        public string WasteSourceName_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> WasteSourceName
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(WasteSourceName)))
                {
                    ((RamAccess<string>)Dictionary[nameof(WasteSourceName)]).Value = WasteSourceName_DB;
                    return (RamAccess<string>)Dictionary[nameof(WasteSourceName)];
                }
                else
                {
                    var rm = new RamAccess<string>(WasteSourceName_Validation, WasteSourceName_DB);
                    rm.PropertyChanged += WasteSourceNameValueChanged;
                    Dictionary.Add(nameof(WasteSourceName), rm);
                    return (RamAccess<string>)Dictionary[nameof(WasteSourceName)];
                }
            }
            set
            {
                WasteSourceName_DB = value.Value;
                OnPropertyChanged(nameof(WasteSourceName));
            }
        }
        private void WasteSourceNameValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                WasteSourceName_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool WasteSourceName_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //WasteSourceName property
        #endregion
        //RadionuclidName property
        #region RadionuclidName
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
        //If change this change validation
        private void RadionuclidNameValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                RadionuclidName_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool RadionuclidName_Validation(RamAccess<string> value)//TODO
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
        //AllowedActivity property
        #region AllowedActivity
        public string AllowedActivity_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> AllowedActivity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(AllowedActivity)))
                {
                    ((RamAccess<string>)Dictionary[nameof(AllowedActivity)]).Value = AllowedActivity_DB;
                    return (RamAccess<string>)Dictionary[nameof(AllowedActivity)];
                }
                else
                {
                    var rm = new RamAccess<string>(AllowedActivity_Validation, AllowedActivity_DB);
                    rm.PropertyChanged += AllowedActivityValueChanged;
                    Dictionary.Add(nameof(AllowedActivity), rm);
                    return (RamAccess<string>)Dictionary[nameof(AllowedActivity)];
                }
            }
            set
            {
                AllowedActivity_DB = value.Value;
                OnPropertyChanged(nameof(AllowedActivity));
            }
        }

        private void AllowedActivityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        AllowedActivity_DB = value1;
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
                AllowedActivity_DB = value1;
            }
        }
        private bool AllowedActivity_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value == "прим.")
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
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                {
                    value.AddError("Число должно быть больше нуля"); return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //AllowedActivity property
        #endregion
        //FactedActivity property
        #region FactedActivity
        public string FactedActivity_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string> FactedActivity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(FactedActivity)))
                {
                    ((RamAccess<string>)Dictionary[nameof(FactedActivity)]).Value = FactedActivity_DB;
                    return (RamAccess<string>)Dictionary[nameof(FactedActivity)];
                }
                else
                {
                    var rm = new RamAccess<string>(FactedActivity_Validation, FactedActivity_DB);
                    rm.PropertyChanged += FactedActivityValueChanged;
                    Dictionary.Add(nameof(FactedActivity), rm);
                    return (RamAccess<string>)Dictionary[nameof(FactedActivity)];
                }
            }
            set
            {
                FactedActivity_DB = value.Value;
                OnPropertyChanged(nameof(FactedActivity));
            }
        }
        private void FactedActivityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        FactedActivity_DB = value1;
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
                FactedActivity_DB = value1;
            }
        }

        private bool FactedActivity_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
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
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                {
                    value.AddError("Число должно быть больше нуля"); return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //FactedActivity property
        #endregion
    }
}
