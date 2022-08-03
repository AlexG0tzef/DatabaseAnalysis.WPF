using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DatabaseAnalysis.WPF.FireBird
{
    [Serializable]
    public class Form26 : Form2
    {
        public Form26() : base()
        {
            FormNum.Value = "2.6";
            //NumberOfFields.Value = 11;
            Validate_all();
        }
        private void Validate_all()
        {
            ObservedSourceNumber_Validation(ObservedSourceNumber);
            ControlledAreaName_Validation(ControlledAreaName);
            SupposedWasteSource_Validation(SupposedWasteSource);
            DistanceToWasteSource_Validation(DistanceToWasteSource);
            TestDepth_Validation(TestDepth);
            RadionuclidName_Validation(RadionuclidName);
            AverageYearConcentration_Validation(AverageYearConcentration);
        }

        public override bool Object_Validation()
        {
            return !(ObservedSourceNumber.HasErrors ||
            ControlledAreaName.HasErrors ||
            SupposedWasteSource.HasErrors ||
            DistanceToWasteSource.HasErrors ||
            TestDepth.HasErrors ||
            RadionuclidName.HasErrors ||
            AverageYearConcentration.HasErrors);
        }

        //ObservedSourceNumber property
        #region ObservedSourceNumber
        public string ObservedSourceNumber_DB { get; set; } = "";[NotMapped]
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
            value.ClearErrors(); return true;
        }
        //ObservedSourceNumber property
        #endregion

        //ControlledAreaName property
        #region ControlledAreaName
        public string ControlledAreaName_DB { get; set; } = "";[NotMapped]
        public RamAccess<string> ControlledAreaName
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(ControlledAreaName)))
                {
                    ((RamAccess<string>)Dictionary[nameof(ControlledAreaName)]).Value = ControlledAreaName_DB;
                    return (RamAccess<string>)Dictionary[nameof(ControlledAreaName)];
                }
                else
                {
                    var rm = new RamAccess<string>(ControlledAreaName_Validation, ControlledAreaName_DB);
                    rm.PropertyChanged += ControlledAreaNameValueChanged;
                    Dictionary.Add(nameof(ControlledAreaName), rm);
                    return (RamAccess<string>)Dictionary[nameof(ControlledAreaName)];
                }
            }
            set
            {
                ControlledAreaName_DB = value.Value;
                OnPropertyChanged(nameof(ControlledAreaName));
            }
        }
        //If change this change validation
        private void ControlledAreaNameValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                ControlledAreaName_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool ControlledAreaName_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value == "-")
            {
                return true;
            }
            List<string> spr = new List<string>()
            {
                "ПП",
                "СЗЗ",
                "ЗН",
                "прим."
            };
            if (!spr.Contains(value.Value))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //ControlledAreaName property
        #endregion

        //SupposedWasteSource property
        #region SupposedWasteSource
        public string SupposedWasteSource_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> SupposedWasteSource
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(SupposedWasteSource)))
                {
                    ((RamAccess<string>)Dictionary[nameof(SupposedWasteSource)]).Value = SupposedWasteSource_DB;
                    return (RamAccess<string>)Dictionary[nameof(SupposedWasteSource)];
                }
                else
                {
                    var rm = new RamAccess<string>(SupposedWasteSource_Validation, SupposedWasteSource_DB);
                    rm.PropertyChanged += SupposedWasteSourceValueChanged;
                    Dictionary.Add(nameof(SupposedWasteSource), rm);
                    return (RamAccess<string>)Dictionary[nameof(SupposedWasteSource)];
                }
            }
            set
            {
                SupposedWasteSource_DB = value.Value;
                OnPropertyChanged(nameof(SupposedWasteSource));
            }
        }

        private void SupposedWasteSourceValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                SupposedWasteSource_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool SupposedWasteSource_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();//done
            return true;
        }
        //SupposedWasteSource property
        #endregion

        //DistanceToWasteSource property
        #region DistanceToWasteSource
        public string DistanceToWasteSource_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> DistanceToWasteSource
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(DistanceToWasteSource)))
                {
                    ((RamAccess<string>)Dictionary[nameof(DistanceToWasteSource)]).Value = DistanceToWasteSource_DB;
                    return (RamAccess<string>)Dictionary[nameof(DistanceToWasteSource)];
                }
                else
                {
                    var rm = new RamAccess<string>(DistanceToWasteSource_Validation, DistanceToWasteSource_DB);
                    rm.PropertyChanged += DistanceToWasteSourceValueChanged;
                    Dictionary.Add(nameof(DistanceToWasteSource), rm);
                    return (RamAccess<string>)Dictionary[nameof(DistanceToWasteSource)];
                }
            }
            set
            {
                DistanceToWasteSource_DB = value.Value;
                OnPropertyChanged(nameof(DistanceToWasteSource));
            }
        }

        private void DistanceToWasteSourceValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        DistanceToWasteSource_DB = value1;
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
                DistanceToWasteSource_DB = value1;
            }
        }
        private bool DistanceToWasteSource_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value == "-")
            {
                return true;
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
        //DistanceToWasteSource property
        #endregion

        //TestDepth property
        #region TestDepth
        public string TestDepth_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> TestDepth
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(TestDepth)))
                {
                    ((RamAccess<string>)Dictionary[nameof(TestDepth)]).Value = TestDepth_DB;
                    return (RamAccess<string>)Dictionary[nameof(TestDepth)];
                }
                else
                {
                    var rm = new RamAccess<string>(TestDepth_Validation, TestDepth_DB);
                    rm.PropertyChanged += TestDepthValueChanged;
                    Dictionary.Add(nameof(TestDepth), rm);
                    return (RamAccess<string>)Dictionary[nameof(TestDepth)];
                }
            }
            set
            {
                TestDepth_DB = value.Value;
                OnPropertyChanged(nameof(TestDepth));
            }
        }

        private void TestDepthValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        TestDepth_DB = value1;
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
                TestDepth_DB = value1;
            }
        }
        private bool TestDepth_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value == "-")
            {
                return true;
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
        //TestDepth property
        #endregion

        //RadionuclidName property
        #region RadionuclidName
        public string RadionuclidName_DB { get; set; } = "";[NotMapped]
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

        //AverageYearConcentration property
        #region AverageYearConcentration 
        public string AverageYearConcentration_DB { get; set; } = null;[NotMapped]
        public RamAccess<string> AverageYearConcentration
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(AverageYearConcentration)))
                {
                    ((RamAccess<string>)Dictionary[nameof(AverageYearConcentration)]).Value = AverageYearConcentration_DB;
                    return (RamAccess<string>)Dictionary[nameof(AverageYearConcentration)];
                }
                else
                {
                    var rm = new RamAccess<string>(AverageYearConcentration_Validation, AverageYearConcentration_DB);
                    rm.PropertyChanged += AverageYearConcentrationValueChanged;
                    Dictionary.Add(nameof(AverageYearConcentration), rm);
                    return (RamAccess<string>)Dictionary[nameof(AverageYearConcentration)];
                }
            }
            set
            {
                AverageYearConcentration_DB = value.Value;
                OnPropertyChanged(nameof(AverageYearConcentration));
            }
        }

        private void AverageYearConcentrationValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        AverageYearConcentration_DB = value1;
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
                AverageYearConcentration_DB = value1;
            }
        }
        private bool AverageYearConcentration_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if (value1.Equals("-"))
            {
                return true;
            }
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
        //AverageYearConcentration property
        #endregion
    }
}
