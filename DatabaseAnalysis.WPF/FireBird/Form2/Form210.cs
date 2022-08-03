using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DatabaseAnalysis.WPF.FireBird
{
    [Serializable]
    public class Form210 : Form2
    {
        public Form210() : base()
        {
            FormNum.Value = "2.10";
            //NumberOfFields.Value = 12;
            Validate_all();
        }
        private void Validate_all()
        {
            IndicatorName_Validation(IndicatorName);
            PlotName_Validation(PlotName);
            PlotKadastrNumber_Validation(PlotKadastrNumber);
            PlotCode_Validation(PlotCode);
            InfectedArea_Validation(InfectedArea);
            AvgGammaRaysDosePower_Validation(AvgGammaRaysDosePower);
            MaxGammaRaysDosePower_Validation(MaxGammaRaysDosePower);
            WasteDensityAlpha_Validation(WasteDensityAlpha);
            WasteDensityBeta_Validation(WasteDensityBeta);
            FcpNumber_Validation(FcpNumber);
        }

        public override bool Object_Validation()
        {
            return !(IndicatorName.HasErrors ||
            PlotName.HasErrors ||
            PlotKadastrNumber.HasErrors ||
            PlotCode.HasErrors ||
            InfectedArea.HasErrors ||
            AvgGammaRaysDosePower.HasErrors ||
            MaxGammaRaysDosePower.HasErrors ||
            WasteDensityAlpha.HasErrors ||
            WasteDensityBeta.HasErrors ||
            FcpNumber.HasErrors);
        }

        //IndicatorName property
        #region  IndicatorName
        public string IndicatorName_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> IndicatorName
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(IndicatorName)))
                {
                    ((RamAccess<string>)Dictionary[nameof(IndicatorName)]).Value = IndicatorName_DB;
                    return (RamAccess<string>)Dictionary[nameof(IndicatorName)];
                }
                else
                {
                    var rm = new RamAccess<string>(IndicatorName_Validation, IndicatorName_DB);
                    rm.PropertyChanged += IndicatorNameValueChanged;
                    Dictionary.Add(nameof(IndicatorName), rm);
                    return (RamAccess<string>)Dictionary[nameof(IndicatorName)];
                }
            }
            set
            {
                IndicatorName_DB = value.Value;
                OnPropertyChanged(nameof(IndicatorName));
            }
        }

        private void IndicatorNameValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                IndicatorName_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool IndicatorName_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            var spr = new List<string> {
                "З","Р","Н"
            };
            if (!spr.Contains(value.Value))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //IndicatorName property
        #endregion

        //PlotName property
        #region  PlotName
        public string PlotName_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> PlotName
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(PlotName)))
                {
                    ((RamAccess<string>)Dictionary[nameof(PlotName)]).Value = PlotName_DB;
                    return (RamAccess<string>)Dictionary[nameof(PlotName)];
                }
                else
                {
                    var rm = new RamAccess<string>(PlotName_Validation, PlotName_DB);
                    rm.PropertyChanged += PlotNameValueChanged;
                    Dictionary.Add(nameof(PlotName), rm);
                    return (RamAccess<string>)Dictionary[nameof(PlotName)];
                }
            }
            set
            {
                PlotName_DB = value.Value;
                OnPropertyChanged(nameof(PlotName));
            }
        }

        private void PlotNameValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                PlotName_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool PlotName_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //PlotName property
        #endregion

        //PlotKadastrNumber property
        #region PlotKadastrNumber 
        public string PlotKadastrNumber_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> PlotKadastrNumber
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(PlotKadastrNumber)))
                {
                    ((RamAccess<string>)Dictionary[nameof(PlotKadastrNumber)]).Value = PlotKadastrNumber_DB;
                    return (RamAccess<string>)Dictionary[nameof(PlotKadastrNumber)];
                }
                else
                {
                    var rm = new RamAccess<string>(PlotKadastrNumber_Validation, PlotKadastrNumber_DB);
                    rm.PropertyChanged += PlotKadastrNumberValueChanged;
                    Dictionary.Add(nameof(PlotKadastrNumber), rm);
                    return (RamAccess<string>)Dictionary[nameof(PlotKadastrNumber)];
                }
            }
            set
            {
                PlotKadastrNumber_DB = value.Value;
                OnPropertyChanged(nameof(PlotKadastrNumber));
            }
        }

        private void PlotKadastrNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                PlotKadastrNumber_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool PlotKadastrNumber_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //PlotKadastrNumber property
        #endregion

        //PlotCode property
        #region  PlotCode
        public string PlotCode_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> PlotCode
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(PlotCode)))
                {
                    ((RamAccess<string>)Dictionary[nameof(PlotCode)]).Value = PlotCode_DB;
                    return (RamAccess<string>)Dictionary[nameof(PlotCode)];
                }
                else
                {
                    var rm = new RamAccess<string>(PlotCode_Validation, PlotCode_DB);
                    rm.PropertyChanged += PlotCodeValueChanged;
                    Dictionary.Add(nameof(PlotCode), rm);
                    return (RamAccess<string>)Dictionary[nameof(PlotCode)];
                }
            }
            set
            {
                PlotCode_DB = value.Value;
                OnPropertyChanged(nameof(PlotCode));
            }
        }
        //6 symbols code
        private void PlotCodeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                PlotCode_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool PlotCode_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            Regex a = new Regex("^[0-9]{6}$");
            if (!a.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //PlotCode property
        #endregion

        //InfectedArea property
        #region  InfectedArea
        public string InfectedArea_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string> InfectedArea
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(InfectedArea)))
                {
                    ((RamAccess<string>)Dictionary[nameof(InfectedArea)]).Value = InfectedArea_DB;
                    return (RamAccess<string>)Dictionary[nameof(InfectedArea)];
                }
                else
                {
                    var rm = new RamAccess<string>(InfectedArea_Validation, InfectedArea_DB);
                    rm.PropertyChanged += InfectedAreaValueChanged;
                    Dictionary.Add(nameof(InfectedArea), rm);
                    return (RamAccess<string>)Dictionary[nameof(InfectedArea)];
                }
            }
            set
            {
                InfectedArea_DB = value.Value;
                OnPropertyChanged(nameof(InfectedArea));
            }
        }

        private void InfectedAreaValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        InfectedArea_DB = value1;
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
                InfectedArea_DB = value1;
            }
        }
        private bool InfectedArea_Validation(RamAccess<string> value)//TODO
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
        //InfectedArea property
        #endregion

        //AvgGammaRaysDosePower property
        #region  AvgGammaRaysDosePower
        public string AvgGammaRaysDosePower_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string> AvgGammaRaysDosePower
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(AvgGammaRaysDosePower)))
                {
                    ((RamAccess<string>)Dictionary[nameof(AvgGammaRaysDosePower)]).Value = AvgGammaRaysDosePower_DB;
                    return (RamAccess<string>)Dictionary[nameof(AvgGammaRaysDosePower)];
                }
                else
                {
                    var rm = new RamAccess<string>(AvgGammaRaysDosePower_Validation, AvgGammaRaysDosePower_DB);
                    rm.PropertyChanged += AvgGammaRaysDosePowerValueChanged;
                    Dictionary.Add(nameof(AvgGammaRaysDosePower), rm);
                    return (RamAccess<string>)Dictionary[nameof(AvgGammaRaysDosePower)];
                }
            }
            set
            {
                AvgGammaRaysDosePower_DB = value.Value;
                OnPropertyChanged(nameof(AvgGammaRaysDosePower));
            }
        }

        private void AvgGammaRaysDosePowerValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        AvgGammaRaysDosePower_DB = value1;
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
                AvgGammaRaysDosePower_DB = value1;
            }
        }
        private bool AvgGammaRaysDosePower_Validation(RamAccess<string> value)//TODO
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
        //AvgGammaRaysDosePower property
        #endregion

        //MaxGammaRaysDosePower property
        #region  MaxGammaRaysDosePower
        public string MaxGammaRaysDosePower_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string> MaxGammaRaysDosePower
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MaxGammaRaysDosePower)))
                {
                    ((RamAccess<string>)Dictionary[nameof(MaxGammaRaysDosePower)]).Value = MaxGammaRaysDosePower_DB;
                    return (RamAccess<string>)Dictionary[nameof(MaxGammaRaysDosePower)];
                }
                else
                {
                    var rm = new RamAccess<string>(MaxGammaRaysDosePower_Validation, MaxGammaRaysDosePower_DB);
                    rm.PropertyChanged += MaxGammaRaysDosePowerValueChanged;
                    Dictionary.Add(nameof(MaxGammaRaysDosePower), rm);
                    return (RamAccess<string>)Dictionary[nameof(MaxGammaRaysDosePower)];
                }
            }
            set
            {
                MaxGammaRaysDosePower_DB = value.Value;
                OnPropertyChanged(nameof(MaxGammaRaysDosePower));
            }
        }

        private void MaxGammaRaysDosePowerValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        MaxGammaRaysDosePower_DB = value1;
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
                MaxGammaRaysDosePower_DB = value1;
            }
        }
        private bool MaxGammaRaysDosePower_Validation(RamAccess<string> value)//TODO
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
        //MaxGammaRaysDosePower property
        #endregion

        //WasteDensityAlpha property
        #region  WasteDensityAlpha
        public string WasteDensityAlpha_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string> WasteDensityAlpha
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(WasteDensityAlpha)))
                {
                    ((RamAccess<string>)Dictionary[nameof(WasteDensityAlpha)]).Value = WasteDensityAlpha_DB;
                    return (RamAccess<string>)Dictionary[nameof(WasteDensityAlpha)];
                }
                else
                {
                    var rm = new RamAccess<string>(WasteDensityAlpha_Validation, WasteDensityAlpha_DB);
                    rm.PropertyChanged += WasteDensityAlphaValueChanged;
                    Dictionary.Add(nameof(WasteDensityAlpha), rm);
                    return (RamAccess<string>)Dictionary[nameof(WasteDensityAlpha)];
                }
            }
            set
            {
                WasteDensityAlpha_DB = value.Value;
                OnPropertyChanged(nameof(WasteDensityAlpha));
            }
        }

        private void WasteDensityAlphaValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        WasteDensityAlpha_DB = value1;
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
                WasteDensityAlpha_DB = value1;
            }
        }
        private bool WasteDensityAlpha_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if (value.Value.Equals("-"))
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
        //WasteDensityAlpha property
        #endregion

        //WasteDensityBeta property
        #region  WasteDensityBeta
        public string WasteDensityBeta_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string> WasteDensityBeta
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(WasteDensityBeta)))
                {
                    ((RamAccess<string>)Dictionary[nameof(WasteDensityBeta)]).Value = WasteDensityBeta_DB;
                    return (RamAccess<string>)Dictionary[nameof(WasteDensityBeta)];
                }
                else
                {
                    var rm = new RamAccess<string>(WasteDensityBeta_Validation, WasteDensityBeta_DB);
                    rm.PropertyChanged += WasteDensityBetaValueChanged;
                    Dictionary.Add(nameof(WasteDensityBeta), rm);
                    return (RamAccess<string>)Dictionary[nameof(WasteDensityBeta)];
                }
            }
            set
            {
                WasteDensityBeta_DB = value.Value;
                OnPropertyChanged(nameof(WasteDensityBeta));
            }
        }
        private void WasteDensityBetaValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        WasteDensityBeta_DB = value1;
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
                WasteDensityBeta_DB = value1;
            }
        }
        private bool WasteDensityBeta_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if (value.Value.Equals("-"))
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
        //WasteDensityBeta property
        #endregion

        //FcpNumber property
        #region  FcpNumber
        public string FcpNumber_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> FcpNumber
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(FcpNumber)))
                {
                    ((RamAccess<string>)Dictionary[nameof(FcpNumber)]).Value = FcpNumber_DB;
                    return (RamAccess<string>)Dictionary[nameof(FcpNumber)];
                }
                else
                {
                    var rm = new RamAccess<string>(FcpNumber_Validation, FcpNumber_DB);
                    rm.PropertyChanged += FcpNumberValueChanged;
                    Dictionary.Add(nameof(FcpNumber), rm);
                    return (RamAccess<string>)Dictionary[nameof(FcpNumber)];
                }
            }
            set
            {
                FcpNumber_DB = value.Value;
                OnPropertyChanged(nameof(FcpNumber));
            }
        }

        private void FcpNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                FcpNumber_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool FcpNumber_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            return true;
        }
        //FcpNumber property
        #endregion
    }
}
