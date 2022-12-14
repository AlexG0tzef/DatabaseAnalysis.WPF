using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DatabaseAnalysis.WPF.FireBird
{
    [Serializable]
    public class Form24 : Form2
    {
        public Form24() : base()
        {
            FormNum.Value = "2.4";
            //NumberOfFields.Value = 26;
            Validate_all();
        }
        private void Validate_all()
        {
            CodeOYAT_Validation(CodeOYAT);
            FcpNumber_Validation(FcpNumber);
            QuantityFromAnothers_Validation(QuantityFromAnothers);
            QuantityFromAnothersImported_Validation(QuantityFromAnothersImported);
            QuantityCreated_Validation(QuantityCreated);
            QuantityRemovedFromAccount_Validation(QuantityRemovedFromAccount);
            MassCreated_Validation(MassCreated);
            MassFromAnothers_Validation(MassFromAnothers);
            MassFromAnothersImported_Validation(MassFromAnothersImported);
            MassRemovedFromAccount_Validation(MassRemovedFromAccount);
            QuantityTransferredToAnother_Validation(QuantityTransferredToAnother);
            MassAnotherReasons_Validation(MassAnotherReasons);
            MassTransferredToAnother_Validation(MassTransferredToAnother);
            QuantityAnotherReasons_Validation(QuantityAnotherReasons);
            QuantityRefined_Validation(QuantityRefined);
            MassRefined_Validation(MassRefined);
        }
        public override bool Object_Validation()
        {
            return !(CodeOYAT.HasErrors ||
            FcpNumber.HasErrors ||
            QuantityFromAnothers.HasErrors ||
            QuantityFromAnothersImported.HasErrors ||
            QuantityCreated.HasErrors ||
            QuantityRemovedFromAccount.HasErrors ||
            MassCreated.HasErrors ||
            MassFromAnothers.HasErrors ||
            MassFromAnothersImported.HasErrors ||
            MassRemovedFromAccount.HasErrors ||
            QuantityTransferredToAnother.HasErrors ||
            MassAnotherReasons.HasErrors ||
            MassTransferredToAnother.HasErrors ||
            QuantityAnotherReasons.HasErrors ||
            QuantityRefined.HasErrors ||
            MassRefined.HasErrors);
        }

        //CodeOYAT property
        #region  CodeOYAT
        public string CodeOYAT_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> CodeOYAT
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(CodeOYAT)))
                {
                    ((RamAccess<string>)Dictionary[nameof(CodeOYAT)]).Value = CodeOYAT_DB;
                    return (RamAccess<string>)Dictionary[nameof(CodeOYAT)];
                }
                else
                {
                    var rm = new RamAccess<string>(CodeOYAT_Validation, CodeOYAT_DB);
                    rm.PropertyChanged += CodeOYATValueChanged;
                    Dictionary.Add(nameof(CodeOYAT), rm);
                    return (RamAccess<string>)Dictionary[nameof(CodeOYAT)];
                }
            }
            set
            {
                CodeOYAT_DB = value.Value;
                OnPropertyChanged(nameof(CodeOYAT));
            }
        }

        private void CodeOYATValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                CodeOYAT_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool CodeOYAT_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено"); return false;
            }
            Regex a = new Regex("^[0-9]{5}$");
            if (!a.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //CodeOYAT property
        #endregion

        //FcpNumber property
        #region  FcpNumber
        public string FcpNumber_DB { get; set; } = ""; [NotMapped]
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

        //MassCreated Property
        #region  MassCreated
        public string MassCreated_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> MassCreated
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MassCreated)))
                {
                    ((RamAccess<string>)Dictionary[nameof(MassCreated)]).Value = MassCreated_DB;
                    return (RamAccess<string>)Dictionary[nameof(MassCreated)];
                }
                else
                {
                    var rm = new RamAccess<string>(MassCreated_Validation, MassCreated_DB);
                    rm.PropertyChanged += MassCreatedValueChanged;
                    Dictionary.Add(nameof(MassCreated), rm);
                    return (RamAccess<string>)Dictionary[nameof(MassCreated)];
                }
            }
            set
            {
                MassCreated_DB = value.Value;
                OnPropertyChanged(nameof(MassCreated));
            }
        }

        private void MassCreatedValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        MassCreated_DB = value1;
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
                MassCreated_DB = value1;
            }
        }
        private bool MassCreated_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
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
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
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
        //MassCreated Property
        #endregion

        //QuantityCreated property
        #region  QuantityCreated
        public string QuantityCreated_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> QuantityCreated
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(QuantityCreated)))
                {
                    ((RamAccess<string>)Dictionary[nameof(QuantityCreated)]).Value = QuantityCreated_DB;
                    return (RamAccess<string>)Dictionary[nameof(QuantityCreated)];
                }
                else
                {
                    var rm = new RamAccess<string>(QuantityCreated_Validation, QuantityCreated_DB);
                    rm.PropertyChanged += QuantityCreatedValueChanged;
                    Dictionary.Add(nameof(QuantityCreated), rm);
                    return (RamAccess<string>)Dictionary[nameof(QuantityCreated)];
                }
            }
            set
            {
                QuantityCreated_DB = value.Value;
                OnPropertyChanged(nameof(QuantityCreated));
            }
        }
        // positive int.
        private void QuantityCreatedValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                QuantityCreated_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool QuantityCreated_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
            {
                return true;
            }
            try
            {
                int k = int.Parse(value.Value);
                if (k <= 0)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //QuantityCreated property
        #endregion

        //MassFromAnothers Property
        #region  MassFromAnothers
        public string MassFromAnothers_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> MassFromAnothers
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MassFromAnothers)))
                {
                    ((RamAccess<string>)Dictionary[nameof(MassFromAnothers)]).Value = MassFromAnothers_DB;
                    return (RamAccess<string>)Dictionary[nameof(MassFromAnothers)];
                }
                else
                {
                    var rm = new RamAccess<string>(MassFromAnothers_Validation, MassFromAnothers_DB);
                    rm.PropertyChanged += MassFromAnothersValueChanged;
                    Dictionary.Add(nameof(MassFromAnothers), rm);
                    return (RamAccess<string>)Dictionary[nameof(MassFromAnothers)];
                }
            }
            set
            {
                MassFromAnothers_DB = value.Value;
                OnPropertyChanged(nameof(MassFromAnothers));
            }
        }

        private void MassFromAnothersValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        MassFromAnothers_DB = value1;
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
                MassFromAnothers_DB = value1;
            }
        }
        private bool MassFromAnothers_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
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
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
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
        //MassFromAnothers Property
        #endregion

        //QuantityFromAnothers property
        #region  QuantityFromAnothers
        public string QuantityFromAnothers_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> QuantityFromAnothers
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(QuantityFromAnothers)))
                {
                    ((RamAccess<string>)Dictionary[nameof(QuantityFromAnothers)]).Value = QuantityFromAnothers_DB;
                    return (RamAccess<string>)Dictionary[nameof(QuantityFromAnothers)];
                }
                else
                {
                    var rm = new RamAccess<string>(QuantityFromAnothers_Validation, QuantityFromAnothers_DB);
                    rm.PropertyChanged += QuantityFromAnothersValueChanged;
                    Dictionary.Add(nameof(QuantityFromAnothers), rm);
                    return (RamAccess<string>)Dictionary[nameof(QuantityFromAnothers)];
                }
            }
            set
            {
                QuantityFromAnothers_DB = value.Value;
                OnPropertyChanged(nameof(QuantityFromAnothers));
            }
        }
        // positive int.
        private void QuantityFromAnothersValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                QuantityFromAnothers_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool QuantityFromAnothers_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
            {
                return true;
            }
            try
            {
                int k = int.Parse(value.Value);
                if (k <= 0)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //QuantityFromAnothers property
        #endregion

        //MassFromAnothersImported Property
        #region  MassFromAnothersImported
        public string MassFromAnothersImported_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> MassFromAnothersImported
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MassFromAnothersImported)))
                {
                    ((RamAccess<string>)Dictionary[nameof(MassFromAnothersImported)]).Value = MassFromAnothersImported_DB;
                    return (RamAccess<string>)Dictionary[nameof(MassFromAnothersImported)];
                }
                else
                {
                    var rm = new RamAccess<string>(MassFromAnothersImported_Validation, MassFromAnothersImported_DB);
                    rm.PropertyChanged += MassFromAnothersImportedValueChanged;
                    Dictionary.Add(nameof(MassFromAnothersImported), rm);
                    return (RamAccess<string>)Dictionary[nameof(MassFromAnothersImported)];
                }
            }
            set
            {
                MassFromAnothersImported_DB = value.Value;
                OnPropertyChanged(nameof(MassFromAnothersImported));
            }
        }

        private void MassFromAnothersImportedValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        MassFromAnothersImported_DB = value1;
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
                MassFromAnothersImported_DB = value1;
            }
        }
        private bool MassFromAnothersImported_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
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
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
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
        //MassFromAnothersImported Property
        #endregion

        //QuantityFromAnothersImported property
        #region  QuantityFromAnothersImported
        public string QuantityFromAnothersImported_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> QuantityFromAnothersImported
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(QuantityFromAnothersImported)))
                {
                    ((RamAccess<string>)Dictionary[nameof(QuantityFromAnothersImported)]).Value = QuantityFromAnothersImported_DB;
                    return (RamAccess<string>)Dictionary[nameof(QuantityFromAnothersImported)];
                }
                else
                {
                    var rm = new RamAccess<string>(QuantityFromAnothersImported_Validation, QuantityFromAnothersImported_DB);
                    rm.PropertyChanged += QuantityFromAnothersImportedValueChanged;
                    Dictionary.Add(nameof(QuantityFromAnothersImported), rm);
                    return (RamAccess<string>)Dictionary[nameof(QuantityFromAnothersImported)];
                }
            }
            set
            {
                QuantityFromAnothersImported_DB = value.Value;
                OnPropertyChanged(nameof(QuantityFromAnothersImported));
            }
        }
        // positive int.
        private void QuantityFromAnothersImportedValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                QuantityFromAnothersImported_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool QuantityFromAnothersImported_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
            {
                return true;
            }
            try
            {
                int k = int.Parse(value.Value);
                if (k <= 0)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //QuantityFromAnothersImported property
        #endregion

        //MassAnotherReasons Property
        #region  MassAnotherReasons
        public string MassAnotherReasons_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> MassAnotherReasons
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MassAnotherReasons)))
                {
                    ((RamAccess<string>)Dictionary[nameof(MassAnotherReasons)]).Value = MassAnotherReasons_DB;
                    return (RamAccess<string>)Dictionary[nameof(MassAnotherReasons)];
                }
                else
                {
                    var rm = new RamAccess<string>(MassAnotherReasons_Validation, MassAnotherReasons_DB);
                    rm.PropertyChanged += MassAnotherReasonsValueChanged;
                    Dictionary.Add(nameof(MassAnotherReasons), rm);
                    return (RamAccess<string>)Dictionary[nameof(MassAnotherReasons)];
                }
            }
            set
            {
                MassAnotherReasons_DB = value.Value;
                OnPropertyChanged(nameof(MassAnotherReasons));
            }
        }

        private void MassAnotherReasonsValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        MassAnotherReasons_DB = value1;
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
                MassAnotherReasons_DB = value1;
            }
        }
        private bool MassAnotherReasons_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
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
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
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
        //MassAnotherReasons Property
        #endregion

        //QuantityAnotherReasons property
        #region  QuantityAnotherReasons
        public string QuantityAnotherReasons_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> QuantityAnotherReasons
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(QuantityAnotherReasons)))
                {
                    ((RamAccess<string>)Dictionary[nameof(QuantityAnotherReasons)]).Value = QuantityAnotherReasons_DB;
                    return (RamAccess<string>)Dictionary[nameof(QuantityAnotherReasons)];
                }
                else
                {
                    var rm = new RamAccess<string>(QuantityAnotherReasons_Validation, QuantityAnotherReasons_DB);
                    rm.PropertyChanged += QuantityAnotherReasonsValueChanged;
                    Dictionary.Add(nameof(QuantityAnotherReasons), rm);
                    return (RamAccess<string>)Dictionary[nameof(QuantityAnotherReasons)];
                }
            }
            set
            {
                QuantityAnotherReasons_DB = value.Value;
                OnPropertyChanged(nameof(QuantityAnotherReasons));
            }
        }
        // positive int.
        private void QuantityAnotherReasonsValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                QuantityAnotherReasons_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool QuantityAnotherReasons_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
            {
                return true;
            }
            try
            {
                int k = int.Parse(value.Value);
                if (k <= 0)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //QuantityAnotherReasons property
        #endregion

        //MassTransferredToAnother Property
        #region  MassTransferredToAnother
        public string MassTransferredToAnother_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> MassTransferredToAnother
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MassTransferredToAnother)))
                {
                    ((RamAccess<string>)Dictionary[nameof(MassTransferredToAnother)]).Value = MassTransferredToAnother_DB;
                    return (RamAccess<string>)Dictionary[nameof(MassTransferredToAnother)];
                }
                else
                {
                    var rm = new RamAccess<string>(MassTransferredToAnother_Validation, MassTransferredToAnother_DB);
                    rm.PropertyChanged += MassTransferredToAnotherValueChanged;
                    Dictionary.Add(nameof(MassTransferredToAnother), rm);
                    return (RamAccess<string>)Dictionary[nameof(MassTransferredToAnother)];
                }
            }
            set
            {
                MassTransferredToAnother_DB = value.Value;
                OnPropertyChanged(nameof(MassTransferredToAnother));
            }
        }

        private void MassTransferredToAnotherValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        MassTransferredToAnother_DB = value1;
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
                MassTransferredToAnother_DB = value1;
            }
        }
        private bool MassTransferredToAnother_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
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
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
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
        //MassTransferredToAnother Property
        #endregion

        //QuantityTransferredToAnother property
        #region  QuantityTransferredToAnother
        public string QuantityTransferredToAnother_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> QuantityTransferredToAnother
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(QuantityTransferredToAnother)))
                {
                    ((RamAccess<string>)Dictionary[nameof(QuantityTransferredToAnother)]).Value = QuantityTransferredToAnother_DB;
                    return (RamAccess<string>)Dictionary[nameof(QuantityTransferredToAnother)];
                }
                else
                {
                    var rm = new RamAccess<string>(QuantityTransferredToAnother_Validation, QuantityTransferredToAnother_DB);
                    rm.PropertyChanged += QuantityTransferredToAnotherValueChanged;
                    Dictionary.Add(nameof(QuantityTransferredToAnother), rm);
                    return (RamAccess<string>)Dictionary[nameof(QuantityTransferredToAnother)];
                }
            }
            set
            {
                QuantityTransferredToAnother_DB = value.Value;
                OnPropertyChanged(nameof(QuantityTransferredToAnother));
            }
        }
        // positive int.
        private void QuantityTransferredToAnotherValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                QuantityTransferredToAnother_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool QuantityTransferredToAnother_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
            {
                return true;
            }
            try
            {
                int k = int.Parse(value.Value);
                if (k <= 0)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //QuantityTransferredToAnother property
        #endregion

        //MassRefined Property
        #region  MassRefined
        public string MassRefined_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> MassRefined
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MassRefined)))
                {
                    ((RamAccess<string>)Dictionary[nameof(MassRefined)]).Value = MassRefined_DB;
                    return (RamAccess<string>)Dictionary[nameof(MassRefined)];
                }
                else
                {
                    var rm = new RamAccess<string>(MassRefined_Validation, MassRefined_DB);
                    rm.PropertyChanged += MassRefinedValueChanged;
                    Dictionary.Add(nameof(MassRefined), rm);
                    return (RamAccess<string>)Dictionary[nameof(MassRefined)];
                }
            }
            set
            {
                MassRefined_DB = value.Value;
                OnPropertyChanged(nameof(MassRefined));
            }
        }

        private void MassRefinedValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        MassRefined_DB = value1;
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
                MassRefined_DB = value1;
            }
        }
        private bool MassRefined_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
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
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
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
        //MassRefined Property
        #endregion

        //QuantityRefined property
        #region  QuantityRefined
        public string QuantityRefined_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> QuantityRefined
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(QuantityRefined)))
                {
                    ((RamAccess<string>)Dictionary[nameof(QuantityRefined)]).Value = QuantityRefined_DB;
                    return (RamAccess<string>)Dictionary[nameof(QuantityRefined)];
                }
                else
                {
                    var rm = new RamAccess<string>(QuantityRefined_Validation, QuantityRefined_DB);
                    rm.PropertyChanged += QuantityRefinedValueChanged;
                    Dictionary.Add(nameof(QuantityRefined), rm);
                    return (RamAccess<string>)Dictionary[nameof(QuantityRefined)];
                }
            }
            set
            {
                QuantityRefined_DB = value.Value;
                OnPropertyChanged(nameof(QuantityRefined));
            }
        }
        // positive int.
        private void QuantityRefinedValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                QuantityRefined_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool QuantityRefined_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
            {
                return true;
            }
            try
            {
                int k = int.Parse(value.Value);
                if (k <= 0)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //QuantityRefined property
        #endregion

        //MassRemovedFromAccount Property
        #region  MassRemovedFromAccount
        public string MassRemovedFromAccount_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> MassRemovedFromAccount
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MassRemovedFromAccount)))
                {
                    ((RamAccess<string>)Dictionary[nameof(MassRemovedFromAccount)]).Value = MassRemovedFromAccount_DB;
                    return (RamAccess<string>)Dictionary[nameof(MassRemovedFromAccount)];
                }
                else
                {
                    var rm = new RamAccess<string>(MassRemovedFromAccount_Validation, MassRemovedFromAccount_DB);
                    rm.PropertyChanged += MassRemovedFromAccountValueChanged;
                    Dictionary.Add(nameof(MassRemovedFromAccount), rm);
                    return (RamAccess<string>)Dictionary[nameof(MassRemovedFromAccount)];
                }
            }
            set
            {
                MassRemovedFromAccount_DB = value.Value;
                OnPropertyChanged(nameof(MassRemovedFromAccount));
            }
        }

        private void MassRemovedFromAccountValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        MassRemovedFromAccount_DB = value1;
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
                MassRemovedFromAccount_DB = value1;
            }
        }
        private bool MassRemovedFromAccount_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
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
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
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
        //MassRemovedFromAccount Property
        #endregion

        //QuantityRemovedFromAccount property
        #region  QuantityRemovedFromAccount
        public string QuantityRemovedFromAccount_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> QuantityRemovedFromAccount
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(QuantityRemovedFromAccount)))
                {
                    ((RamAccess<string>)Dictionary[nameof(QuantityRemovedFromAccount)]).Value = QuantityRemovedFromAccount_DB;
                    return (RamAccess<string>)Dictionary[nameof(QuantityRemovedFromAccount)];
                }
                else
                {
                    var rm = new RamAccess<string>(QuantityRemovedFromAccount_Validation, QuantityRemovedFromAccount_DB);
                    rm.PropertyChanged += QuantityRemovedFromAccountValueChanged;
                    Dictionary.Add(nameof(QuantityRemovedFromAccount), rm);
                    return (RamAccess<string>)Dictionary[nameof(QuantityRemovedFromAccount)];
                }
            }
            set
            {
                QuantityRemovedFromAccount_DB = value.Value;
                OnPropertyChanged(nameof(QuantityRemovedFromAccount));
            }
        }
        // positive int.
        private void QuantityRemovedFromAccountValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                QuantityRemovedFromAccount_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool QuantityRemovedFromAccount_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
            {
                return true;
            }
            try
            {
                int k = int.Parse(value.Value);
                if (k <= 0)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //QuantityRemovedFromAccount property
        #endregion
    }
}