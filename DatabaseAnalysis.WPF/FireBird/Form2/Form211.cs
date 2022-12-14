using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DatabaseAnalysis.WPF.FireBird
{
    [Serializable]
    public class Form211 : Form2
    {
        public Form211() : base()
        {
            FormNum.Value = "2.11";
            //NumberOfFields.Value = 11;
            Validate_all();
        }
        private void Validate_all()
        {
            Radionuclids_Validation(Radionuclids);
            PlotName_Validation(PlotName);
            PlotKadastrNumber_Validation(PlotKadastrNumber);
            PlotCode_Validation(PlotCode);
            InfectedArea_Validation(InfectedArea);
            SpecificActivityOfPlot_Validation(SpecificActivityOfPlot);
            SpecificActivityOfLiquidPart_Validation(SpecificActivityOfLiquidPart);
            SpecificActivityOfDensePart_Validation(SpecificActivityOfDensePart);
        }

        public override bool Object_Validation()
        {
            return !(Radionuclids.HasErrors ||
            PlotName.HasErrors ||
            PlotKadastrNumber.HasErrors ||
            PlotCode.HasErrors ||
            InfectedArea.HasErrors ||
            SpecificActivityOfPlot.HasErrors ||
            SpecificActivityOfLiquidPart.HasErrors ||
            SpecificActivityOfDensePart.HasErrors);
        }

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
        #region  PlotKadastrNumber
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

        //Radionuclids property
        #region  Radionuclids
        public string Radionuclids_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> Radionuclids
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Radionuclids)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Radionuclids)]).Value = Radionuclids_DB;
                    return (RamAccess<string>)Dictionary[nameof(Radionuclids)];
                }
                else
                {
                    var rm = new RamAccess<string>(Radionuclids_Validation, Radionuclids_DB);
                    rm.PropertyChanged += RadionuclidsValueChanged;
                    Dictionary.Add(nameof(Radionuclids), rm);
                    return (RamAccess<string>)Dictionary[nameof(Radionuclids)];
                }
            }
            set
            {
                Radionuclids_DB = value.Value;
                OnPropertyChanged(nameof(Radionuclids));
            }
        }
        //If change this change validation
        private void RadionuclidsValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                Radionuclids_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool Radionuclids_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            value.Value.Replace(" ", "");
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            string[] nuclids = value.Value.Split("; ");
            bool flag = true;
            return true;
        }
        //Radionuclids property
        #endregion

        //SpecificActivityOfPlot property
        #region  SpecificActivityOfPlot
        public string SpecificActivityOfPlot_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> SpecificActivityOfPlot
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(SpecificActivityOfPlot)))
                {
                    ((RamAccess<string>)Dictionary[nameof(SpecificActivityOfPlot)]).Value = SpecificActivityOfPlot_DB;
                    return (RamAccess<string>)Dictionary[nameof(SpecificActivityOfPlot)];
                }
                else
                {
                    var rm = new RamAccess<string>(SpecificActivityOfPlot_Validation, SpecificActivityOfPlot_DB);
                    rm.PropertyChanged += SpecificActivityOfPlotValueChanged;
                    Dictionary.Add(nameof(SpecificActivityOfPlot), rm);
                    return (RamAccess<string>)Dictionary[nameof(SpecificActivityOfPlot)];
                }
            }
            set
            {
                SpecificActivityOfPlot_DB = value.Value;
                OnPropertyChanged(nameof(SpecificActivityOfPlot));
            }
        }

        private void SpecificActivityOfPlotValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        SpecificActivityOfPlot_DB = value1;
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
                SpecificActivityOfPlot_DB = value1;
            }
        }
        private bool SpecificActivityOfPlot_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
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
        //SpecificActivityOfPlot property
        #endregion

        //SpecificActivityOfLiquidPart property
        #region  SpecificActivityOfLiquidPart
        public string SpecificActivityOfLiquidPart_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> SpecificActivityOfLiquidPart
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(SpecificActivityOfLiquidPart)))
                {
                    ((RamAccess<string>)Dictionary[nameof(SpecificActivityOfLiquidPart)]).Value = SpecificActivityOfLiquidPart_DB;
                    return (RamAccess<string>)Dictionary[nameof(SpecificActivityOfLiquidPart)];
                }
                else
                {
                    var rm = new RamAccess<string>(SpecificActivityOfLiquidPart_Validation, SpecificActivityOfLiquidPart_DB);
                    rm.PropertyChanged += SpecificActivityOfLiquidPartValueChanged;
                    Dictionary.Add(nameof(SpecificActivityOfLiquidPart), rm);
                    return (RamAccess<string>)Dictionary[nameof(SpecificActivityOfLiquidPart)];
                }
            }
            set
            {
                SpecificActivityOfLiquidPart_DB = value.Value;
                OnPropertyChanged(nameof(SpecificActivityOfLiquidPart));
            }
        }

        private void SpecificActivityOfLiquidPartValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        SpecificActivityOfLiquidPart_DB = value1;
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
                SpecificActivityOfLiquidPart_DB = value1;
            }
        }
        private bool SpecificActivityOfLiquidPart_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
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
        //SpecificActivityOfLiquidPart property
        #endregion

        //SpecificActivityOfDensePart property
        #region SpecificActivityOfDensePart 
        public string SpecificActivityOfDensePart_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> SpecificActivityOfDensePart
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(SpecificActivityOfDensePart)))
                {
                    ((RamAccess<string>)Dictionary[nameof(SpecificActivityOfDensePart)]).Value = SpecificActivityOfDensePart_DB;
                    return (RamAccess<string>)Dictionary[nameof(SpecificActivityOfDensePart)];
                }
                else
                {
                    var rm = new RamAccess<string>(SpecificActivityOfDensePart_Validation, SpecificActivityOfDensePart_DB);
                    rm.PropertyChanged += SpecificActivityOfDensePartValueChanged;
                    Dictionary.Add(nameof(SpecificActivityOfDensePart), rm);
                    return (RamAccess<string>)Dictionary[nameof(SpecificActivityOfDensePart)];
                }
            }
            set
            {
                SpecificActivityOfDensePart_DB = value.Value;
                OnPropertyChanged(nameof(SpecificActivityOfDensePart));
            }
        }

        private void SpecificActivityOfDensePartValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        SpecificActivityOfDensePart_DB = value1;
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
                SpecificActivityOfDensePart_DB = value1;
            }
        }
        private bool SpecificActivityOfDensePart_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
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
        //SpecificActivityOfDensePart property
        #endregion
    }
}
