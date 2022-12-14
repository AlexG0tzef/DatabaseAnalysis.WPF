using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DatabaseAnalysis.WPF.FireBird
{
    [Serializable]
    public class Form28 : Form2
    {
        public Form28() : base()
        {
            FormNum.Value = "2.8";
            //NumberOfFields.Value = 24;
            Validate_all();
        }
        private void Validate_all()
        {
            WasteSourceName_Validation(WasteSourceName);
            WasteRecieverName_Validation(WasteRecieverName);
            RecieverTypeCode_Validation(RecieverTypeCode);
            AllowedWasteRemovalVolume_Validation(AllowedWasteRemovalVolume);
            RemovedWasteVolume_Validation(RemovedWasteVolume);
            PoolDistrictName_Validation(PoolDistrictName);
        }
        public override bool Object_Validation()
        {
            return !(WasteSourceName.HasErrors ||
            WasteRecieverName.HasErrors ||
            RecieverTypeCode.HasErrors ||
            AllowedWasteRemovalVolume.HasErrors ||
            RemovedWasteVolume.HasErrors ||
            PoolDistrictName.HasErrors);
        }

        //WasteSourceName property
        #region WasteSourceName
        public string WasteSourceName_DB { get; set; } = "";
        [NotMapped]
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

        //WasteRecieverName property
        #region WasteRecieverName
        public string WasteRecieverName_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> WasteRecieverName
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(WasteRecieverName)))
                {
                    ((RamAccess<string>)Dictionary[nameof(WasteRecieverName)]).Value = WasteRecieverName_DB;
                    return (RamAccess<string>)Dictionary[nameof(WasteRecieverName)];
                }
                else
                {
                    var rm = new RamAccess<string>(WasteRecieverName_Validation, WasteRecieverName_DB);
                    rm.PropertyChanged += WasteRecieverNameValueChanged;
                    Dictionary.Add(nameof(WasteRecieverName), rm);
                    return (RamAccess<string>)Dictionary[nameof(WasteRecieverName)];
                }
            }
            set
            {
                WasteRecieverName_DB = value.Value;
                OnPropertyChanged(nameof(WasteRecieverName));
            }
        }

        private void WasteRecieverNameValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                WasteRecieverName_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool WasteRecieverName_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //WasteRecieverName property
        #endregion

        //RecieverTypeCode property
        #region RecieverTypeCode
        public string RecieverTypeCode_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> RecieverTypeCode
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(RecieverTypeCode)))
                {
                    ((RamAccess<string>)Dictionary[nameof(RecieverTypeCode)]).Value = RecieverTypeCode_DB;
                    return (RamAccess<string>)Dictionary[nameof(RecieverTypeCode)];
                }
                else
                {
                    var rm = new RamAccess<string>(RecieverTypeCode_Validation, RecieverTypeCode_DB);
                    rm.PropertyChanged += RecieverTypeCodeValueChanged;
                    Dictionary.Add(nameof(RecieverTypeCode), rm);
                    return (RamAccess<string>)Dictionary[nameof(RecieverTypeCode)];
                }
            }
            set
            {
                RecieverTypeCode_DB = value.Value;
                OnPropertyChanged(nameof(RecieverTypeCode));
            }
        }

        private void RecieverTypeCodeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                RecieverTypeCode_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool RecieverTypeCode_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            value.AddError("Недопустимое значение");
            return false;
        }
        //RecieverTypeCode property
        #endregion

        //PoolDistrictName property
        #region PoolDistrictName
        public string PoolDistrictName_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> PoolDistrictName
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(PoolDistrictName)))
                {
                    ((RamAccess<string>)Dictionary[nameof(PoolDistrictName)]).Value = PoolDistrictName_DB;
                    return (RamAccess<string>)Dictionary[nameof(PoolDistrictName)];
                }
                else
                {
                    var rm = new RamAccess<string>(PoolDistrictName_Validation, PoolDistrictName_DB);
                    rm.PropertyChanged += PoolDistrictNameValueChanged;
                    Dictionary.Add(nameof(PoolDistrictName), rm);
                    return (RamAccess<string>)Dictionary[nameof(PoolDistrictName)];
                }
            }
            set
            {
                PoolDistrictName_DB = value.Value;
                OnPropertyChanged(nameof(PoolDistrictName));
            }
        }

        private void PoolDistrictNameValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                PoolDistrictName_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool PoolDistrictName_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
            //List<string> spr = new List<string>();
            //if (spr.Contains(value.Value))
            //{
            //    return true;
            //}
            //value.AddError("Недопустимое значение");
            //return false;
        }
        //PoolDistrictName property
        #endregion

        //AllowedWasteRemovalVolume property
        #region AllowedWasteRemovalVolume
        public string AllowedWasteRemovalVolume_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> AllowedWasteRemovalVolume
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(AllowedWasteRemovalVolume)))
                {
                    ((RamAccess<string>)Dictionary[nameof(AllowedWasteRemovalVolume)]).Value = AllowedWasteRemovalVolume_DB;
                    return (RamAccess<string>)Dictionary[nameof(AllowedWasteRemovalVolume)];
                }
                else
                {
                    var rm = new RamAccess<string>(AllowedWasteRemovalVolume_Validation, AllowedWasteRemovalVolume_DB);
                    rm.PropertyChanged += AllowedWasteRemovalVolumeValueChanged;
                    Dictionary.Add(nameof(AllowedWasteRemovalVolume), rm);
                    return (RamAccess<string>)Dictionary[nameof(AllowedWasteRemovalVolume)];
                }
            }
            set
            {
                AllowedWasteRemovalVolume_DB = value.Value;
                OnPropertyChanged(nameof(AllowedWasteRemovalVolume));
            }
        }

        private void AllowedWasteRemovalVolumeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        AllowedWasteRemovalVolume_DB = value1;
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
                AllowedWasteRemovalVolume_DB = value1;
            }
        }
        private bool AllowedWasteRemovalVolume_Validation(RamAccess<string> value)
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
        //AllowedWasteRemovalVolume property
        #endregion

        //RemovedWasteVolume property
        #region RemovedWasteVolume
        public string RemovedWasteVolume_DB { get; set; } = null; [NotMapped]
        public RamAccess<string> RemovedWasteVolume
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(RemovedWasteVolume)))
                {
                    ((RamAccess<string>)Dictionary[nameof(RemovedWasteVolume)]).Value = RemovedWasteVolume_DB;
                    return (RamAccess<string>)Dictionary[nameof(RemovedWasteVolume)];
                }
                else
                {
                    var rm = new RamAccess<string>(RemovedWasteVolume_Validation, RemovedWasteVolume_DB);
                    rm.PropertyChanged += RemovedWasteVolumeValueChanged;
                    Dictionary.Add(nameof(RemovedWasteVolume), rm);
                    return (RamAccess<string>)Dictionary[nameof(RemovedWasteVolume)];
                }
            }
            set
            {
                RemovedWasteVolume_DB = value.Value;
                OnPropertyChanged(nameof(RemovedWasteVolume));
            }
        }

        private void RemovedWasteVolumeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        RemovedWasteVolume_DB = value1;
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
                RemovedWasteVolume_DB = value1;
            }
        }
        private bool RemovedWasteVolume_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
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
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //RemovedWasteVolume property
        #endregion
    }
}
