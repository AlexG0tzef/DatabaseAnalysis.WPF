using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DatabaseAnalysis.WPF.FireBird
{
    public class Form19 : Form1
    {
        public Form19() : base()
        {
            FormNum.Value = "1.9";
            OperationCode.Value = "10";
            Validate_all();
        }
        private void Validate_all()
        {
            //Quantity_Validation(Quantity);
            CodeTypeAccObject_Validation(CodeTypeAccObject);
            Activity_Validation(Activity);
            Radionuclids_Validation(Radionuclids);
        }
        public override bool Object_Validation()
        {
            return !(CodeTypeAccObject.HasErrors ||
            Activity.HasErrors ||
            Radionuclids.HasErrors);
        }

        #region CodeTypeAccObject
        public short? CodeTypeAccObject_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<short?> CodeTypeAccObject
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(CodeTypeAccObject)))
                {
                    ((RamAccess<short?>)Dictionary[nameof(CodeTypeAccObject)]).Value = CodeTypeAccObject_DB;
                    return (RamAccess<short?>)Dictionary[nameof(CodeTypeAccObject)];
                }
                else
                {
                    var rm = new RamAccess<short?>(CodeTypeAccObject_Validation, CodeTypeAccObject_DB);
                    rm.PropertyChanged += CodeTypeAccObjectValueChanged;
                    Dictionary.Add(nameof(CodeTypeAccObject), rm);
                    return (RamAccess<short?>)Dictionary[nameof(CodeTypeAccObject)];
                }
            }
            set
            {
                CodeTypeAccObject_DB = value.Value;
                OnPropertyChanged(nameof(CodeTypeAccObject));
            }
        }
        private void CodeTypeAccObjectValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                CodeTypeAccObject_DB = ((RamAccess<short?>)Value).Value;
            }
        }
        private bool CodeTypeAccObject_Validation(RamAccess<short?> value)//TODO
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        #endregion

        #region Radionuclids
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
        }//If change this change validation

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
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            string[] nuclids = value.Value.Split("; ");
            bool flag = true;
            if (!flag)
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region Activity
        public string Activity_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string> Activity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Activity)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Activity)]).Value = Activity_DB;
                    return (RamAccess<string>)Dictionary[nameof(Activity)];
                }
                else
                {
                    var rm = new RamAccess<string>(Activity_Validation, Activity_DB);
                    rm.PropertyChanged += ActivityValueChanged;
                    Dictionary.Add(nameof(Activity), rm);
                    return (RamAccess<string>)Dictionary[nameof(Activity)];
                }
            }
            set
            {
                Activity_DB = value.Value;
                OnPropertyChanged(nameof(Activity));
            }
        }
        private void ActivityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        Activity_DB = value1;
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
                Activity_DB = value1;
            }
        }
        private bool Activity_Validation(RamAccess<string> value)//Ready
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        protected override bool OperationCode_Validation(RamAccess<string> value)//OK
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value != "10")
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        protected override bool OperationDate_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            var tmp = value.Value;
            Regex b = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{2}$");
            if (b.IsMatch(tmp))
            {
                tmp = tmp.Insert(6, "20");
            }
            Regex a = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{4}$");
            if (!a.IsMatch(tmp))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            try { DateTimeOffset.Parse(tmp); }
            catch (Exception)
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            //DateTimeOffset date = DateTimeOffset.Parse(tmp);
            //if (date.Date > DateTimeOffset.Now.Date)
            //{
            //    value.AddError("Недопустимое значение");
            //    return false;
            //}
            return true;
        }
        protected override bool DocumentDate_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            var tmp = value.Value;
            Regex b = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{2}$");
            if (b.IsMatch(tmp))
            {
                tmp = tmp.Insert(6, "20");
            }
            Regex a = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{4}$");
            if (!a.IsMatch(tmp))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            try { DateTimeOffset.Parse(tmp); }
            catch (Exception)
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            //DateTimeOffset date = DateTimeOffset.Parse(tmp);
            //if (date.Date > DateTimeOffset.Now.Date)
            //{
            //    value.AddError("Недопустимое значение");
            //    return false;
            //}
            return true;
        }
        protected override bool DocumentNumber_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))//ok
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("прим."))
            {
                return true;
            }
            return true;
        }
    }
}
