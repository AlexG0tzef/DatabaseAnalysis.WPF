using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DatabaseAnalysis.WPF.FireBird
{
    [Serializable]

    public class Form21 : Form2
    {
        public Form21() : base()
        {
            FormNum.Value = "2.1";
            //NumberOfFields.Value = 24;
            Validate_all();
        }
        private void Validate_all()
        {
            MachinePower_Validation(MachinePower);
            MachineCode_Validation(MachineCode);
            RefineMachineName_Validation(RefineMachineName);
            NumberOfHoursPerYear_Validation(NumberOfHoursPerYear);
            CodeRAOIn_Validation(CodeRAOIn);
            StatusRAOIn_Validation(StatusRAOIn);
            VolumeIn_Validation(VolumeIn);
            MassIn_Validation(MassIn);
            QuantityIn_Validation(QuantityIn);
            TritiumActivityIn_Validation(TritiumActivityIn);
            TritiumActivityOut_Validation(TritiumActivityOut);
            BetaGammaActivityIn_Validation(BetaGammaActivityIn);
            BetaGammaActivityOut_Validation(BetaGammaActivityOut);
            TransuraniumActivityIn_Validation(TransuraniumActivityIn);
            TransuraniumActivityOut_Validation(TransuraniumActivityOut);
            AlphaActivityIn_Validation(AlphaActivityIn);
            AlphaActivityOut_Validation(AlphaActivityOut);
            VolumeOut_Validation(VolumeOut);
            MassOut_Validation(MassOut);
            QuantityOZIIIout_Validation(QuantityOZIIIout);
            CodeRAOout_Validation(CodeRAOout);
            StatusRAOout_Validation(StatusRAOout);
        }
        public override bool Object_Validation()
        {
            return !(MachinePower.HasErrors ||
            MachineCode.HasErrors ||
            RefineMachineName.HasErrors ||
            NumberOfHoursPerYear.HasErrors ||
            CodeRAOIn.HasErrors ||
            StatusRAOIn.HasErrors ||
            VolumeIn.HasErrors ||
            MassIn.HasErrors ||
            QuantityIn.HasErrors ||
            TritiumActivityIn.HasErrors ||
            TritiumActivityOut.HasErrors ||
            BetaGammaActivityIn.HasErrors ||
            BetaGammaActivityOut.HasErrors ||
            TransuraniumActivityIn.HasErrors ||
            TransuraniumActivityOut.HasErrors ||
            AlphaActivityIn.HasErrors ||
            AlphaActivityOut.HasErrors ||
            VolumeOut.HasErrors ||
            MassOut.HasErrors ||
            QuantityOZIIIout.HasErrors ||
            CodeRAOout.HasErrors ||
            StatusRAOout.HasErrors);
        }

        #region NumberInOrder
        public string NumberInOrderSum_DB { get; set; } = "";

        [NotMapped]
        public RamAccess<string> NumberInOrderSum
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(NumberInOrderSum)))
                {
                    if (NumberInOrderSum_DB != "" && NumberInOrderSum_DB != null)
                    {
                        ((RamAccess<string>)Dictionary[nameof(NumberInOrderSum)]).Value = NumberInOrderSum_DB;
                    }
                    else
                    {
                        ((RamAccess<string>)Dictionary[nameof(NumberInOrderSum)]).Value = NumberInOrder_DB.ToString();
                    }
                    return (RamAccess<string>)Dictionary[nameof(NumberInOrderSum)];
                }
                else
                {
                    RamAccess<string> rm = null;
                    if (NumberInOrderSum_DB != "" && NumberInOrderSum_DB != null)
                    {
                        rm = new RamAccess<string>(NumberInOrderSum_Validation, NumberInOrderSum_DB);
                    }
                    else
                    {
                        rm = new RamAccess<string>(NumberInOrderSum_Validation, NumberInOrder_DB.ToString());
                    }
                    rm.PropertyChanged += NumberInOrderSumValueChanged;
                    Dictionary.Add(nameof(NumberInOrderSum), rm);
                    return (RamAccess<string>)Dictionary[nameof(NumberInOrderSum)];
                }
            }
            set
            {
                NumberInOrderSum_DB = value.Value;
                OnPropertyChanged(nameof(NumberInOrderSum));
            }
        }
        private void NumberInOrderSumValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                NumberInOrderSum_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool NumberInOrderSum_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        //RefineMachineName property
        #region  RefineMachineName
        public string RefineMachineName_DB { get; set; } = "";
        public bool _RefineMachineName_Hidden_Get { get; set; } = true;
        [NotMapped]
        public RefBool RefineMachineName_Hidden_Get
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(RefineMachineName_Hidden_Get)))
                {
                    ((RefBool)Dictionary[nameof(RefineMachineName_Hidden_Get)]).Set(_RefineMachineName_Hidden_Get);
                    return (RefBool)Dictionary[nameof(RefineMachineName_Hidden_Get)];

                }
                else
                {
                    var rm = new RefBool(_RefineMachineName_Hidden_Get);
                    Dictionary.Add(nameof(RefineMachineName_Hidden_Get), rm);
                    return rm;
                }
            }
            set
            {
                if (_RefineMachineName_Hidden_Get != value.Get())
                {
                    _RefineMachineName_Hidden_Get = value.Get();
                    var tmp = RefineMachineName;
                    OnPropertyChanged(nameof(RefineMachineName_Hidden_Get));
                }
            }
        }
        public bool _RefineMachineName_Hidden_Set { get; set; } = true;
        [NotMapped]
        public RefBool RefineMachineName_Hidden_Set
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(RefineMachineName_Hidden_Set)))
                {
                    ((RefBool)Dictionary[nameof(RefineMachineName_Hidden_Set)]).Set(_RefineMachineName_Hidden_Set);
                    return (RefBool)Dictionary[nameof(RefineMachineName_Hidden_Set)];

                }
                else
                {
                    var rm = new RefBool(_RefineMachineName_Hidden_Set);
                    Dictionary.Add(nameof(RefineMachineName_Hidden_Set), rm);
                    return rm;
                }
            }
            set
            {
                if (_RefineMachineName_Hidden_Set != value.Get())
                {
                    _RefineMachineName_Hidden_Set = value.Get();
                    var tmp = RefineMachineName;
                    OnPropertyChanged(nameof(RefineMachineName_Hidden_Set));
                }
            }
        }

        [NotMapped]
        public RamAccess<string> RefineMachineName
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(RefineMachineName)))
                {
                    ((RamAccess<string>)Dictionary[nameof(RefineMachineName)]).Value = RefineMachineName_DB;
                    return (RamAccess<string>)Dictionary[nameof(RefineMachineName)];
                }
                else
                {
                    var rm = new RamAccess<string>(RefineMachineName_Validation, RefineMachineName_DB, RefineMachineName_Hidden_Get, RefineMachineName_Hidden_Set);
                    rm.PropertyChanged += RefineMachineNameValueChanged;
                    Dictionary.Add(nameof(RefineMachineName), rm);
                    return (RamAccess<string>)Dictionary[nameof(RefineMachineName)];
                }
            }
            set
            {
                if (RefineMachineName.Value != value.Value)
                {
                    RefineMachineName_DB = value.Value;
                    OnPropertyChanged(nameof(RefineMachineName));
                }
            }

        }

        private void RefineMachineNameValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                if (_RefineMachineName_Hidden_Set)
                {
                    RefineMachineName_DB = ((RamAccess<string>)Value).Value;
                }
            }
        }

        private bool RefineMachineName_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            return true;
        }

        //RefineMachineName property
        #endregion

        //MachineCode property
        #region MachineCode 
        public byte? MachineCode_DB { get; set; } = null;
        public bool _MachineCode_Hidden_Get { get; set; } = true;
        [NotMapped]
        public RefBool MachineCode_Hidden_Get
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MachineCode_Hidden_Get)))
                {
                    ((RefBool)Dictionary[nameof(MachineCode_Hidden_Get)]).Set(_MachineCode_Hidden_Get);
                    return (RefBool)Dictionary[nameof(MachineCode_Hidden_Get)];

                }
                else
                {
                    var rm = new RefBool(_MachineCode_Hidden_Get);
                    Dictionary.Add(nameof(MachineCode_Hidden_Get), rm);
                    return rm;
                }
            }
            set
            {
                if (_MachineCode_Hidden_Get != value.Get())
                {
                    _MachineCode_Hidden_Get = value.Get();
                    var tmp = MachineCode;
                    OnPropertyChanged(nameof(MachineCode_Hidden_Get));
                }
            }
        }
        public bool _MachineCode_Hidden_Set { get; set; } = true;
        [NotMapped]
        public RefBool MachineCode_Hidden_Set
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MachineCode_Hidden_Set)))
                {
                    ((RefBool)Dictionary[nameof(MachineCode_Hidden_Set)]).Set(_MachineCode_Hidden_Set);
                    return (RefBool)Dictionary[nameof(MachineCode_Hidden_Set)];

                }
                else
                {
                    var rm = new RefBool(_MachineCode_Hidden_Set);
                    Dictionary.Add(nameof(MachineCode_Hidden_Set), rm);
                    return rm;
                }
            }
            set
            {
                if (_MachineCode_Hidden_Set != value.Get())
                {
                    _MachineCode_Hidden_Set = value.Get();
                    var tmp = MachineCode;
                    OnPropertyChanged(nameof(MachineCode_Hidden_Set));
                }
            }
        }

        [NotMapped]
        public RamAccess<byte?> MachineCode
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MachineCode)))
                {
                    ((RamAccess<byte?>)Dictionary[nameof(MachineCode)]).Value = MachineCode_DB;
                    return (RamAccess<byte?>)Dictionary[nameof(MachineCode)];
                }
                else
                {
                    var rm = new RamAccess<byte?>(MachineCode_Validation, MachineCode_DB, MachineCode_Hidden_Get, MachineCode_Hidden_Set);
                    rm.PropertyChanged += MachineCodeValueChanged;
                    Dictionary.Add(nameof(MachineCode), rm);
                    return (RamAccess<byte?>)Dictionary[nameof(MachineCode)];
                }
            }
            set
            {
                if (MachineCode.Value != value.Value)
                {
                    MachineCode_DB = value.Value;
                    OnPropertyChanged(nameof(MachineCode));
                }
            }
        }
        private void MachineCodeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                if (_MachineCode_Hidden_Set)
                {
                    MachineCode_DB = ((RamAccess<byte?>)Value).Value;
                }
            }
        }

        private bool MachineCode_Validation(RamAccess<byte?> value) //TODO
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }

            bool a = (value.Value >= 11) && (value.Value <= 17);
            bool b = (value.Value >= 21) && (value.Value <= 24);
            bool c = (value.Value >= 31) && (value.Value <= 32);
            bool d = (value.Value >= 41) && (value.Value <= 43);
            bool e = (value.Value >= 51) && (value.Value <= 56);
            bool f = (value.Value >= 61) && (value.Value <= 63);
            bool g = (value.Value >= 71) && (value.Value <= 73);
            bool h = (value.Value == 19) || (value.Value == 29) || (value.Value == 39) || (value.Value == 49) ||
                     (value.Value == 99) || (value.Value == 79);
            if (!(a || b || c || d || e || f || g || h))
            {
                value.AddError("Недопустимое значение");
                return false;
            }

            return true;
        }

        //MachineCode property
        #endregion

        //MachinePower property
        #region  MachinePower
        public bool _MachinePower_Hidden_Get { get; set; } = true;
        [NotMapped]
        public RefBool MachinePower_Hidden_Get
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MachinePower_Hidden_Get)))
                {
                    ((RefBool)Dictionary[nameof(MachinePower_Hidden_Get)]).Set(_MachinePower_Hidden_Get);
                    return (RefBool)Dictionary[nameof(MachinePower_Hidden_Get)];

                }
                else
                {
                    var rm = new RefBool(_MachinePower_Hidden_Get);
                    Dictionary.Add(nameof(MachinePower_Hidden_Get), rm);
                    return rm;
                }
            }
            set
            {
                if (_MachinePower_Hidden_Get != value.Get())
                {
                    _MachinePower_Hidden_Get = value.Get();
                    var tmp = MachinePower;
                    OnPropertyChanged(nameof(MachinePower_Hidden_Get));
                }
            }
        }
        public bool _MachinePower_Hidden_Set { get; set; } = true;
        [NotMapped]
        public RefBool MachinePower_Hidden_Set
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MachinePower_Hidden_Set)))
                {
                    ((RefBool)Dictionary[nameof(MachinePower_Hidden_Set)]).Set(_MachinePower_Hidden_Set);
                    return (RefBool)Dictionary[nameof(MachinePower_Hidden_Set)];

                }
                else
                {
                    var rm = new RefBool(_MachinePower_Hidden_Set);
                    Dictionary.Add(nameof(MachinePower_Hidden_Set), rm);
                    return rm;
                }
            }
            set
            {
                if (_MachinePower_Hidden_Set != value.Get())
                {
                    _MachinePower_Hidden_Set = value.Get();
                    var tmp = MachinePower;
                    OnPropertyChanged(nameof(MachinePower_Hidden_Set));
                }
            }
        }

        public string MachinePower_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> MachinePower
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MachinePower)))
                {
                    ((RamAccess<string>)Dictionary[nameof(MachinePower)]).Value = MachinePower_DB;
                    return ((RamAccess<string>)Dictionary[nameof(MachinePower)]);

                }
                else
                {
                    var rm = new RamAccess<string>(MachinePower_Validation, MachinePower_DB, MachinePower_Hidden_Get, MachinePower_Hidden_Set);
                    rm.PropertyChanged += MachinePowerValueChanged;
                    Dictionary.Add(nameof(MachinePower), rm);
                    return rm;
                }
            }
            set
            {
                if (MachinePower.Value != value.Value)
                {
                    MachinePower_DB = value.Value;
                    OnPropertyChanged(nameof(MachinePower));
                }
            }
        }

        private void MachinePowerValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                if (_MachinePower_Hidden_Set)
                {
                    var value1 = ((RamAccess<string>)Value).Value;
                    if (value1 != null)
                    {
                        value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                        if (value1.Equals("-"))
                        {
                            MachinePower_DB = value1;
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
                    MachinePower_DB = value1;
                }
            }
        }

        private bool MachinePower_Validation(RamAccess<string> value) //TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
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
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                {
                    value.AddError("Число должно быть больше нуля");
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

        //MachinePower property
        #endregion

        //NumberOfHoursPerYear property
        #region  NumberOfHoursPerYear
        public string NumberOfHoursPerYear_DB { get; set; } = "";
        public bool _NumberOfHoursPerYear_Hidden_Get { get; set; } = true;
        [NotMapped]
        public RefBool NumberOfHoursPerYear_Hidden_Get
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(NumberOfHoursPerYear_Hidden_Get)))
                {
                    ((RefBool)Dictionary[nameof(NumberOfHoursPerYear_Hidden_Get)]).Set(_NumberOfHoursPerYear_Hidden_Get);
                    return (RefBool)Dictionary[nameof(NumberOfHoursPerYear_Hidden_Get)];

                }
                else
                {
                    var rm = new RefBool(_NumberOfHoursPerYear_Hidden_Get);
                    Dictionary.Add(nameof(NumberOfHoursPerYear_Hidden_Get), rm);
                    return rm;
                }
            }
            set
            {
                if (_NumberOfHoursPerYear_Hidden_Get != value.Get())
                {
                    _NumberOfHoursPerYear_Hidden_Get = value.Get();
                    var tmp = NumberOfHoursPerYear;
                    OnPropertyChanged(nameof(NumberOfHoursPerYear_Hidden_Get));
                }
            }
        }
        public bool _NumberOfHoursPerYear_Hidden_Set { get; set; } = true;
        [NotMapped]
        public RefBool NumberOfHoursPerYear_Hidden_Set
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(NumberOfHoursPerYear_Hidden_Set)))
                {
                    ((RefBool)Dictionary[nameof(NumberOfHoursPerYear_Hidden_Set)]).Set(_NumberOfHoursPerYear_Hidden_Set);
                    return (RefBool)Dictionary[nameof(NumberOfHoursPerYear_Hidden_Set)];

                }
                else
                {
                    var rm = new RefBool(_NumberOfHoursPerYear_Hidden_Set);
                    Dictionary.Add(nameof(NumberOfHoursPerYear_Hidden_Set), rm);
                    return rm;
                }
            }
            set
            {
                if (_NumberOfHoursPerYear_Hidden_Set != value.Get())
                {
                    _NumberOfHoursPerYear_Hidden_Set = value.Get();
                    var tmp = NumberOfHoursPerYear;
                    OnPropertyChanged(nameof(NumberOfHoursPerYear_Hidden_Set));
                }
            }
        }

        [NotMapped]
        public RamAccess<string> NumberOfHoursPerYear
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(NumberOfHoursPerYear)))
                {
                    ((RamAccess<string>)Dictionary[nameof(NumberOfHoursPerYear)]).Value = NumberOfHoursPerYear_DB;
                    return (RamAccess<string>)Dictionary[nameof(NumberOfHoursPerYear)];
                }
                else
                {
                    var rm = new RamAccess<string>(NumberOfHoursPerYear_Validation, NumberOfHoursPerYear_DB, NumberOfHoursPerYear_Hidden_Get, NumberOfHoursPerYear_Hidden_Set);
                    rm.PropertyChanged += NumberOfHoursPerYearValueChanged;
                    Dictionary.Add(nameof(NumberOfHoursPerYear), rm);
                    return (RamAccess<string>)Dictionary[nameof(NumberOfHoursPerYear)];
                }
            }
            set
            {
                if (NumberOfHoursPerYear.Value != value.Value)
                {
                    NumberOfHoursPerYear_DB = value.Value;
                    OnPropertyChanged(nameof(NumberOfHoursPerYear));
                }
            }
        }
        private void NumberOfHoursPerYearValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                if (_NumberOfHoursPerYear_Hidden_Set)
                {
                    var value1 = ((RamAccess<string>)Value).Value;
                    if (value1 != null)
                    {
                        value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                        if (value1.Equals("-"))
                        {
                            NumberOfHoursPerYear_DB = value1;
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
                    NumberOfHoursPerYear_DB = value1;
                }
            }
        }
        private bool NumberOfHoursPerYear_Validation(RamAccess<string> value) //TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return false;
            }
            if (value.Value.Equals("прим.") || value.Value.Equals("0"))
            {
                return true;
                //TODO
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
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                {
                    value.AddError("Число должно быть больше нуля");
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

        //NumberOfHoursPerYear property
        #endregion

        //CodeRAOIn property
        #region  CodeRAOIn
        public string CodeRAOIn_DB { get; set; } = "";
        public bool CodeRAOIn_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool CodeRAOIn_Hidden
        {
            get => CodeRAOIn_Hidden_Priv;
            set
            {
                CodeRAOIn_Hidden_Priv = value;
            }
        }

        [NotMapped]
        public RamAccess<string> CodeRAOIn
        {
            get
            {
                if (!CodeRAOIn_Hidden)
                {
                    if (Dictionary.ContainsKey(nameof(CodeRAOIn)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(CodeRAOIn)]).Value = CodeRAOIn_DB;
                        return (RamAccess<string>)Dictionary[nameof(CodeRAOIn)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(CodeRAOIn_Validation, CodeRAOIn_DB);
                        rm.PropertyChanged += CodeRAOInValueChanged;
                        Dictionary.Add(nameof(CodeRAOIn), rm);
                        return (RamAccess<string>)Dictionary[nameof(CodeRAOIn)];
                    }
                }
                else
                {
                    var tmp = new RamAccess<string>(null, null);
                    return tmp;
                }
            }
            set
            {
                if (!CodeRAOIn_Hidden)
                {
                    CodeRAOIn_DB = value.Value;
                    OnPropertyChanged(nameof(CodeRAOIn));
                }
            }
        }

        private void CodeRAOInValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var tmp = ((RamAccess<string>)Value).Value.ToLower();
                tmp = tmp.Replace("х", "x");
                CodeRAOIn_DB = tmp;
            }
        }
        private bool CodeRAOIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return false;
            }
            var tmp = value.Value.ToLower();
            tmp = tmp.Replace("х", "x");
            Regex a = new Regex("^[0-9x+]{11}$");
            if (!a.IsMatch(tmp))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            if (tmp.Length == 11)
            {
                Regex a0 = new Regex("^[1-3x+]");
                if (!a0.IsMatch(tmp.Substring(0, 1)))
                {
                    value.AddError("Недопустимое агрегатное состояние - " + tmp.Substring(0, 1));
                }
                Regex a1 = new Regex("^[0-49x+]");
                if (!a1.IsMatch(tmp.Substring(1, 1)))
                {
                    value.AddError("Недопустимое категория РАО - " + tmp.Substring(1, 1));
                }
                Regex a2 = new Regex("^[0-6x+]");
                if (!a2.IsMatch(tmp.Substring(2, 1)))
                {
                    value.AddError("Недопустимый радионуклидный состав РАО - " + tmp.Substring(2, 1));
                }
                Regex a3 = new Regex("^[12x+]");
                if (!a3.IsMatch(tmp.Substring(3, 1)))
                {
                    value.AddError("Недопустимое содержание ядерных материалов - " + tmp.Substring(3, 1));
                }
                Regex a4 = new Regex("^[12x+]");
                if (!a4.IsMatch(tmp.Substring(4, 1)))
                {
                    value.AddError("Недопустимоый период полураспада - " + tmp.Substring(4, 1));
                }
                Regex a5 = new Regex("^[0-3x+]");
                if (!a5.IsMatch(tmp.Substring(5, 1)))
                {
                    value.AddError("Недопустимоый период потенциальной опасности РАО - " + tmp.Substring(5, 1));
                }
                Regex a6 = new Regex("^[0-49x+]");
                if (!a6.IsMatch(tmp.Substring(6, 1)))
                {
                    value.AddError("Недопустимоый способ переработки - " + tmp.Substring(6, 1));
                }
                Regex a7 = new Regex("^[0-79x+]");
                if (!a7.IsMatch(tmp.Substring(7, 1)))
                {
                    value.AddError("Недопустимоый класс РАО - " + tmp.Substring(7, 1));
                }
                Regex a89 = new Regex("^[1]{1}[1-9]{1}|^[0]{1}[1]{1}|^[2]{1}[1-69]{1}|^[3]{1}[1-9]{1}|^[4]{1}[1-6]{1}|^[5]{1}[1-9]{1}|^[6]{1}[1-9]{1}|^[7]{1}[1-9]{1}|^[8]{1}[1-9]{1}|^[9]{1}[1-9]{1}");
                if (!a89.IsMatch(tmp.Substring(8, 2)))
                {
                    value.AddError("Недопустимоый код типа РАО - " + tmp.Substring(8, 2));
                }
                Regex a10 = new Regex("^[12x+]");
                if (!a7.IsMatch(tmp.Substring(10, 1)))
                {
                    value.AddError("Недопустимая горючесть - " + tmp.Substring(10, 1));
                }
                if (value.HasErrors)
                {
                    return false;
                }
            }
            return true;
        }
        //CodeRAOIn property
        #endregion

        //StatusRAOIn property
        #region  StatusRAOIn
        public string StatusRAOIn_DB { get; set; } = "";
        public bool StatusRAOIn_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool StatusRAOIn_Hidden
        {
            get => StatusRAOIn_Hidden_Priv;
            set
            {
                StatusRAOIn_Hidden_Priv = value;
            }
        }
        [NotMapped]
        public RamAccess<string> StatusRAOIn  //1 cyfer or OKPO.
        {
            get
            {
                if (!StatusRAOIn_Hidden)
                {
                    if (Dictionary.ContainsKey(nameof(StatusRAOIn)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(StatusRAOIn)]).Value = StatusRAOIn_DB;
                        return (RamAccess<string>)Dictionary[nameof(StatusRAOIn)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(StatusRAOIn_Validation, StatusRAOIn_DB);
                        rm.PropertyChanged += StatusRAOInValueChanged;
                        Dictionary.Add(nameof(StatusRAOIn), rm);
                        return (RamAccess<string>)Dictionary[nameof(StatusRAOIn)];
                    }
                }
                else
                {
                    var tmp = new RamAccess<string>(null, null);
                    return tmp;
                }
            }
            set
            {
                if (!StatusRAOIn_Hidden)
                {
                    StatusRAOIn_DB = value.Value;
                    OnPropertyChanged(nameof(StatusRAOIn));
                }
            }
        }

        private void StatusRAOInValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                StatusRAOIn_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool StatusRAOIn_Validation(RamAccess<string> value)//TODO
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
            if (value.Value.Length == 1)
            {
                int tmp;
                try
                {
                    tmp = int.Parse(value.Value);
                    if ((tmp < 1) || ((tmp > 4) && (tmp != 6) && (tmp != 9)))
                    {
                        value.AddError("Недопустимое значение");
                        return false;
                    }
                }
                catch (Exception)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
                return true;
            }
            if ((value.Value.Length != 8) && (value.Value.Length != 14))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            Regex mask = new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$");
            if (!mask.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //StatusRAOIn property
        #endregion

        //VolumeIn property
        #region  VolumeIn
        public string VolumeIn_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> VolumeIn//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(VolumeIn)))
                {
                    ((RamAccess<string>)Dictionary[nameof(VolumeIn)]).Value = VolumeIn_DB;
                    return (RamAccess<string>)Dictionary[nameof(VolumeIn)];
                }
                else
                {
                    var rm = new RamAccess<string>(VolumeIn_Validation, VolumeIn_DB);
                    rm.PropertyChanged += VolumeInValueChanged;
                    Dictionary.Add(nameof(VolumeIn), rm);
                    return (RamAccess<string>)Dictionary[nameof(VolumeIn)];
                }
            }
            set
            {
                VolumeIn_DB = value.Value;
                OnPropertyChanged(nameof(VolumeIn));
            }
        }

        private void VolumeInValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        VolumeIn_DB = value1;
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
                VolumeIn_DB = value1;
            }
        }

        private bool VolumeIn_Validation(RamAccess<string> value) //TODO
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

            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                {
                    value.AddError("Число должно быть больше нуля");
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

        //VolumeIn property
        #endregion

        //MassIn Property
        #region  MassIn
        public string MassIn_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> MassIn//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MassIn)))
                {
                    ((RamAccess<string>)Dictionary[nameof(MassIn)]).Value = MassIn_DB;
                    return (RamAccess<string>)Dictionary[nameof(MassIn)];
                }
                else
                {
                    var rm = new RamAccess<string>(MassIn_Validation, MassIn_DB);
                    rm.PropertyChanged += MassInValueChanged;
                    Dictionary.Add(nameof(MassIn), rm);
                    return (RamAccess<string>)Dictionary[nameof(MassIn)];
                }
            }
            set
            {
                MassIn_DB = value.Value;
                OnPropertyChanged(nameof(MassIn));
            }
        }

        private void MassInValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        MassIn_DB = value1;
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
                MassIn_DB = value1;
            }
        }
        private bool MassIn_Validation(RamAccess<string> value)//TODO
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
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //MassIn property
        #endregion

        //QuantityIn property
        #region  QuantityIn
        public string QuantityIn_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> QuantityIn//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(QuantityIn)))
                {
                    ((RamAccess<string>)Dictionary[nameof(QuantityIn)]).Value = QuantityIn_DB;
                    return (RamAccess<string>)Dictionary[nameof(QuantityIn)];
                }
                else
                {
                    var rm = new RamAccess<string>(QuantityIn_Validation, QuantityIn_DB);
                    rm.PropertyChanged += QuantityInValueChanged;
                    Dictionary.Add(nameof(QuantityIn), rm);
                    return (RamAccess<string>)Dictionary[nameof(QuantityIn)];
                }
            }
            set
            {
                QuantityIn_DB = value.Value;
                OnPropertyChanged(nameof(QuantityIn));
            }
        }
        // positive int.
        private void QuantityInValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                QuantityIn_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool QuantityIn_Validation(RamAccess<string> value1)//Ready
        {
            value1.ClearErrors();
            if (string.IsNullOrEmpty(value1.Value))
            {
                return true;
            }
            if (value1.Value.Equals("прим.") || value1.Value.Equals("-"))
            {
                return true;
            }
            string tmp = value1.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            int value;
            try
            {
                value = int.Parse(tmp);
                if (value <= 0)
                {
                    value1.AddError("Число должно быть больше нуля"); return false;
                }
            }
            catch
            {
                value1.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //QuantityIn property
        #endregion

        //TritiumActivityIn property
        #region  TritiumActivityIn
        public string TritiumActivityIn_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> TritiumActivityIn//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(TritiumActivityIn)))
                {
                    ((RamAccess<string>)Dictionary[nameof(TritiumActivityIn)]).Value = TritiumActivityIn_DB;
                    return (RamAccess<string>)Dictionary[nameof(TritiumActivityIn)];
                }
                else
                {
                    var rm = new RamAccess<string>(TritiumActivityIn_Validation, TritiumActivityIn_DB);
                    rm.PropertyChanged += TritiumActivityInValueChanged;
                    Dictionary.Add(nameof(TritiumActivityIn), rm);
                    return (RamAccess<string>)Dictionary[nameof(TritiumActivityIn)];
                }
            }
            set
            {
                TritiumActivityIn_DB = value.Value;
                OnPropertyChanged(nameof(TritiumActivityIn));
            }
        }

        private void TritiumActivityInValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        TritiumActivityIn_DB = value1;
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
                TritiumActivityIn_DB = value1;
            }
        }
        private bool TritiumActivityIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value == "-")
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //TritiumActivityIn property
        #endregion

        //BetaGammaActivityIn property
        #region  BetaGammaActivityIn
        public string BetaGammaActivityIn_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> BetaGammaActivityIn//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(BetaGammaActivityIn)))
                {
                    ((RamAccess<string>)Dictionary[nameof(BetaGammaActivityIn)]).Value = BetaGammaActivityIn_DB;
                    return (RamAccess<string>)Dictionary[nameof(BetaGammaActivityIn)];
                }
                else
                {
                    var rm = new RamAccess<string>(BetaGammaActivityIn_Validation, BetaGammaActivityIn_DB);
                    rm.PropertyChanged += BetaGammaActivityInValueChanged;
                    Dictionary.Add(nameof(BetaGammaActivityIn), rm);
                    return (RamAccess<string>)Dictionary[nameof(BetaGammaActivityIn)];
                }
            }
            set
            {
                BetaGammaActivityIn_DB = value.Value;
                OnPropertyChanged(nameof(BetaGammaActivityIn));
            }
        }

        private void BetaGammaActivityInValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        BetaGammaActivityIn_DB = value1;
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
                BetaGammaActivityIn_DB = value1;
            }
        }
        private bool BetaGammaActivityIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value == "-")
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //BetaGammaActivity property
        #endregion

        //AlphaActivityIn property
        #region  AlphaActivityIn
        public string AlphaActivityIn_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> AlphaActivityIn//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(AlphaActivityIn)))
                {
                    ((RamAccess<string>)Dictionary[nameof(AlphaActivityIn)]).Value = AlphaActivityIn_DB;
                    return (RamAccess<string>)Dictionary[nameof(AlphaActivityIn)];
                }
                else
                {
                    var rm = new RamAccess<string>(AlphaActivityIn_Validation, AlphaActivityIn_DB);
                    rm.PropertyChanged += AlphaActivityInValueChanged;
                    Dictionary.Add(nameof(AlphaActivityIn), rm);
                    return (RamAccess<string>)Dictionary[nameof(AlphaActivityIn)];
                }
            }
            set
            {
                AlphaActivityIn_DB = value.Value;
                OnPropertyChanged(nameof(AlphaActivityIn));
            }
        }

        private void AlphaActivityInValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        AlphaActivityIn_DB = value1;
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
                AlphaActivityIn_DB = value1;
            }
        }
        private bool AlphaActivityIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            if (value.Value == "-")
            {
                return true;
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //AlphaActivityIn property
        #endregion

        //TransuraniumActivityIn property
        #region  TransuraniumActivityIn
        public string TransuraniumActivityIn_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> TransuraniumActivityIn//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(TransuraniumActivityIn)))
                {
                    ((RamAccess<string>)Dictionary[nameof(TransuraniumActivityIn)]).Value = TransuraniumActivityIn_DB;
                    return (RamAccess<string>)Dictionary[nameof(TransuraniumActivityIn)];
                }
                else
                {
                    var rm = new RamAccess<string>(TransuraniumActivityIn_Validation, TransuraniumActivityIn_DB);
                    rm.PropertyChanged += TransuraniumActivityInValueChanged;
                    Dictionary.Add(nameof(TransuraniumActivityIn), rm);
                    return (RamAccess<string>)Dictionary[nameof(TransuraniumActivityIn)];
                }
            }
            set
            {
                TransuraniumActivityIn_DB = value.Value;
                OnPropertyChanged(nameof(TransuraniumActivityIn));
            }
        }

        private void TransuraniumActivityInValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        TransuraniumActivityIn_DB = value1;
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
                TransuraniumActivityIn_DB = value1;
            }
        }
        private bool TransuraniumActivityIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value == "-")
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //TransuraniumActivityIn property
        #endregion

        //CodeRAOout property
        #region  CodeRAOout
        public string CodeRAOout_DB { get; set; } = "";
        public bool CodeRAOout_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool CodeRAOout_Hidden
        {
            get => CodeRAOout_Hidden_Priv;
            set
            {
                CodeRAOout_Hidden_Priv = value;
            }
        }

        [NotMapped]
        public RamAccess<string> CodeRAOout
        {
            get
            {
                if (!CodeRAOout_Hidden)
                {
                    if (Dictionary.ContainsKey(nameof(CodeRAOout)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(CodeRAOout)]).Value = CodeRAOout_DB;
                        return (RamAccess<string>)Dictionary[nameof(CodeRAOout)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(CodeRAOout_Validation, CodeRAOout_DB);
                        rm.PropertyChanged += CodeRAOoutValueChanged;
                        Dictionary.Add(nameof(CodeRAOout), rm);
                        return (RamAccess<string>)Dictionary[nameof(CodeRAOout)];
                    }
                }
                else
                {
                    var tmp = new RamAccess<string>(null, null);
                    return tmp;
                }
            }
            set
            {
                if (!CodeRAOout_Hidden)
                {
                    CodeRAOout_DB = value.Value;
                    OnPropertyChanged(nameof(CodeRAOout));
                }
            }
        }

        private void CodeRAOoutValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var tmp = ((RamAccess<string>)Value).Value.ToLower();
                tmp = tmp.Replace("х", "x");
                CodeRAOout_DB = tmp;
            }
        }
        private bool CodeRAOout_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return false;
            }
            var tmp = value.Value.ToLower();
            tmp = tmp.Replace("х", "x");
            Regex a = new Regex("^[0-9x+]{11}$");
            if (!a.IsMatch(tmp))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            if (tmp.Length == 11)
            {
                Regex a0 = new Regex("^[1-3x+]");
                if (!a0.IsMatch(tmp.Substring(0, 1)))
                {
                    value.AddError("Недопустимое агрегатное состояние - " + tmp.Substring(0, 1));
                }
                Regex a1 = new Regex("^[0-49x+]");
                if (!a1.IsMatch(tmp.Substring(1, 1)))
                {
                    value.AddError("Недопустимое категория РАО - " + tmp.Substring(1, 1));
                }
                Regex a2 = new Regex("^[0-6x+]");
                if (!a2.IsMatch(tmp.Substring(2, 1)))
                {
                    value.AddError("Недопустимый радионуклидный состав РАО - " + tmp.Substring(2, 1));
                }
                Regex a3 = new Regex("^[12x+]");
                if (!a3.IsMatch(tmp.Substring(3, 1)))
                {
                    value.AddError("Недопустимое содержание ядерных материалов - " + tmp.Substring(3, 1));
                }
                Regex a4 = new Regex("^[12x+]");
                if (!a4.IsMatch(tmp.Substring(4, 1)))
                {
                    value.AddError("Недопустимоый период полураспада - " + tmp.Substring(4, 1));
                }
                Regex a5 = new Regex("^[0-3x+]");
                if (!a5.IsMatch(tmp.Substring(5, 1)))
                {
                    value.AddError("Недопустимоый период потенциальной опасности РАО - " + tmp.Substring(5, 1));
                }
                Regex a6 = new Regex("^[0-49x+]");
                if (!a6.IsMatch(tmp.Substring(6, 1)))
                {
                    value.AddError("Недопустимоый способ переработки - " + tmp.Substring(6, 1));
                }
                Regex a7 = new Regex("^[0-79x+]");
                if (!a7.IsMatch(tmp.Substring(7, 1)))
                {
                    value.AddError("Недопустимоый класс РАО - " + tmp.Substring(7, 1));
                }
                Regex a89 = new Regex("^[1]{1}[1-9]{1}|^[0]{1}[1]{1}|^[2]{1}[1-69]{1}|^[3]{1}[1-9]{1}|^[4]{1}[1-6]{1}|^[5]{1}[1-9]{1}|^[6]{1}[1-9]{1}|^[7]{1}[1-9]{1}|^[8]{1}[1-9]{1}|^[9]{1}[1-9]{1}");
                if (!a89.IsMatch(tmp.Substring(8, 2)))
                {
                    value.AddError("Недопустимоый код типа РАО - " + tmp.Substring(8, 2));
                }
                Regex a10 = new Regex("^[12x+]");
                if (!a7.IsMatch(tmp.Substring(10, 1)))
                {
                    value.AddError("Недопустимая горючесть - " + tmp.Substring(10, 1));
                }
                if (value.HasErrors)
                {
                    return false;
                }
            }
            return true;
        }
        //CodeRAOout property
        #endregion

        //StatusRAOout property
        #region  StatusRAOout
        public string StatusRAOout_DB { get; set; } = "";
        public bool StatusRAOout_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool StatusRAOout_Hidden
        {
            get => StatusRAOout_Hidden_Priv;
            set
            {
                StatusRAOout_Hidden_Priv = value;
            }
        }

        [NotMapped]
        public RamAccess<string> StatusRAOout  //1 cyfer or OKPO.
        {
            get
            {
                if (!StatusRAOout_Hidden)
                {
                    if (Dictionary.ContainsKey(nameof(StatusRAOout)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(StatusRAOout)]).Value = StatusRAOout_DB;
                        return (RamAccess<string>)Dictionary[nameof(StatusRAOout)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(StatusRAOout_Validation, StatusRAOout_DB);
                        rm.PropertyChanged += StatusRAOoutValueChanged;
                        Dictionary.Add(nameof(StatusRAOout), rm);
                        return (RamAccess<string>)Dictionary[nameof(StatusRAOout)];
                    }
                }
                else
                {
                    var tmp = new RamAccess<string>(null, null);
                    return tmp;
                }
            }
            set
            {
                if (!StatusRAOout_Hidden)
                {
                    StatusRAOout_DB = value.Value;
                    OnPropertyChanged(nameof(StatusRAOout));
                }
            }
        }

        private void StatusRAOoutValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                StatusRAOout_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool StatusRAOout_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value.Length == 1)
            {
                int tmp;
                try
                {
                    tmp = int.Parse(value.Value);
                    if ((tmp < 1) || ((tmp > 4) && (tmp != 6) && (tmp != 9)))
                    {
                        value.AddError("Недопустимое значение");
                        return false;
                    }
                }
                catch (Exception)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
                return true;
            }
            if ((value.Value.Length != 8) && (value.Value.Length != 14))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            Regex mask = new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$");
            if (!mask.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //StatusRAOout property
        #endregion

        //VolumeOut property
        #region  VolumeOut
        public string VolumeOut_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> VolumeOut//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(VolumeOut)))
                {
                    ((RamAccess<string>)Dictionary[nameof(VolumeOut)]).Value = VolumeOut_DB;
                    return (RamAccess<string>)Dictionary[nameof(VolumeOut)];
                }
                else
                {
                    var rm = new RamAccess<string>(VolumeOut_Validation, VolumeOut_DB);
                    rm.PropertyChanged += VolumeOutValueChanged;
                    Dictionary.Add(nameof(VolumeOut), rm);
                    return (RamAccess<string>)Dictionary[nameof(VolumeOut)];
                }
            }
            set
            {
                VolumeOut_DB = value.Value;
                OnPropertyChanged(nameof(VolumeOut));
            }
        }

        private void VolumeOutValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        VolumeOut_DB = value1;
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
                VolumeOut_DB = value1;
            }
        }
        private bool VolumeOut_Validation(RamAccess<string> value)//TODO
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //VolumeOut property
        #endregion

        //MassOut Property
        #region  MassOut
        public string MassOut_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> MassOut//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(MassOut)))
                {
                    ((RamAccess<string>)Dictionary[nameof(MassOut)]).Value = MassOut_DB;
                    return (RamAccess<string>)Dictionary[nameof(MassOut)];
                }
                else
                {
                    var rm = new RamAccess<string>(MassOut_Validation, MassOut_DB);
                    rm.PropertyChanged += MassOutValueChanged;
                    Dictionary.Add(nameof(MassOut), rm);
                    return (RamAccess<string>)Dictionary[nameof(MassOut)];
                }
            }
            set
            {
                MassOut_DB = value.Value;
                OnPropertyChanged(nameof(MassOut));
            }
        }

        private void MassOutValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        MassOut_DB = value1;
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
                MassOut_DB = value1;
            }
        }
        private bool MassOut_Validation(RamAccess<string> value)//TODO
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
        //MassOut Property
        #endregion

        //QuantityOZIIIout property
        #region  QuantityOZIIIout
        public string QuantityOZIIIout_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> QuantityOZIIIout//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(QuantityOZIIIout)))
                {
                    ((RamAccess<string>)Dictionary[nameof(QuantityOZIIIout)]).Value = QuantityOZIIIout_DB;
                    return (RamAccess<string>)Dictionary[nameof(QuantityOZIIIout)];
                }
                else
                {
                    var rm = new RamAccess<string>(QuantityOZIIIout_Validation, QuantityOZIIIout_DB);
                    rm.PropertyChanged += QuantityOZIIIoutValueChanged;
                    Dictionary.Add(nameof(QuantityOZIIIout), rm);
                    return (RamAccess<string>)Dictionary[nameof(QuantityOZIIIout)];
                }
            }
            set
            {
                QuantityOZIIIout_DB = value.Value;
                OnPropertyChanged(nameof(QuantityOZIIIout));
            }
        }
        // positive int.
        private void QuantityOZIIIoutValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                QuantityOZIIIout_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool QuantityOZIIIout_Validation(RamAccess<string> value1)//Ready
        {
            value1.ClearErrors();
            if (string.IsNullOrEmpty(value1.Value))
            {
                value1.AddError("Поле не заполнено");
                return false;
            }
            if (value1.Value == "-")
            {
                return true;
            }
            if (value1.Equals("прим."))
            {
                return true;
            }
            string tmp = value1.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            int value;
            try
            {
                value = int.Parse(tmp);
                if (value <= 0)
                {
                    value1.AddError("Число должно быть больше нуля"); return false;
                }
            }
            catch
            {
                value1.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //QuantityOZIIIout property
        #endregion

        //TritiumActivityOut property
        #region  TritiumActivityOut
        public string TritiumActivityOut_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> TritiumActivityOut//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(TritiumActivityOut)))
                {
                    ((RamAccess<string>)Dictionary[nameof(TritiumActivityOut)]).Value = TritiumActivityOut_DB;
                    return (RamAccess<string>)Dictionary[nameof(TritiumActivityOut)];
                }
                else
                {
                    var rm = new RamAccess<string>(TritiumActivityOut_Validation, TritiumActivityOut_DB);
                    rm.PropertyChanged += TritiumActivityOutValueChanged;
                    Dictionary.Add(nameof(TritiumActivityOut), rm);
                    return (RamAccess<string>)Dictionary[nameof(TritiumActivityOut)];
                }
            }
            set
            {
                TritiumActivityOut_DB = value.Value;
                OnPropertyChanged(nameof(TritiumActivityOut));
            }
        }

        private void TritiumActivityOutValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        TritiumActivityOut_DB = value1;
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
                TritiumActivityOut_DB = value1;
            }
        }
        private bool TritiumActivityOut_Validation(RamAccess<string> value)//TODO
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //TritiumActivityOut property
        #endregion

        //BetaGammaActivityOut property
        #region  BetaGammaActivityOut
        public string BetaGammaActivityOut_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> BetaGammaActivityOut//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(BetaGammaActivityOut)))
                {
                    ((RamAccess<string>)Dictionary[nameof(BetaGammaActivityOut)]).Value = BetaGammaActivityOut_DB;
                    return (RamAccess<string>)Dictionary[nameof(BetaGammaActivityOut)];
                }
                else
                {
                    var rm = new RamAccess<string>(BetaGammaActivityOut_Validation, BetaGammaActivityOut_DB);
                    rm.PropertyChanged += BetaGammaActivityOutValueChanged;
                    Dictionary.Add(nameof(BetaGammaActivityOut), rm);
                    return (RamAccess<string>)Dictionary[nameof(BetaGammaActivityOut)];
                }
            }
            set
            {
                BetaGammaActivityOut_DB = value.Value;
                OnPropertyChanged(nameof(BetaGammaActivityOut));
            }
        }
        private void BetaGammaActivityOutValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        BetaGammaActivityOut_DB = value1;
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
                BetaGammaActivityOut_DB = value1;
            }
        }
        private bool BetaGammaActivityOut_Validation(RamAccess<string> value)//TODO
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //BetaGammaActivityOut property
        #endregion

        //AlphaActivityOut property
        #region  AlphaActivityOut
        public string AlphaActivityOut_DB { get; set; } = ""; [NotMapped]
        public RamAccess<string> AlphaActivityOut//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(AlphaActivityOut)))
                {
                    ((RamAccess<string>)Dictionary[nameof(AlphaActivityOut)]).Value = AlphaActivityOut_DB;
                    return (RamAccess<string>)Dictionary[nameof(AlphaActivityOut)];
                }
                else
                {
                    var rm = new RamAccess<string>(AlphaActivityOut_Validation, AlphaActivityOut_DB);
                    rm.PropertyChanged += AlphaActivityOutValueChanged;
                    Dictionary.Add(nameof(AlphaActivityOut), rm);
                    return (RamAccess<string>)Dictionary[nameof(AlphaActivityOut)];
                }
            }
            set
            {
                AlphaActivityOut_DB = value.Value;
                OnPropertyChanged(nameof(AlphaActivityOut));
            }
        }

        private void AlphaActivityOutValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        AlphaActivityOut_DB = value1;
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
                AlphaActivityOut_DB = value1;
            }
        }
        private bool AlphaActivityOut_Validation(RamAccess<string> value)//TODO
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //AlphaActivityOut property
        #endregion

        //TransuraniumActivityOut property
        #region  TransuraniumActivityOut
        public string TransuraniumActivityOut_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> TransuraniumActivityOut//SUMMARIZABLE
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(TransuraniumActivityOut)))
                {
                    ((RamAccess<string>)Dictionary[nameof(TransuraniumActivityOut)]).Value = TransuraniumActivityOut_DB;
                    return (RamAccess<string>)Dictionary[nameof(TransuraniumActivityOut)];
                }
                else
                {
                    var rm = new RamAccess<string>(TransuraniumActivityOut_Validation, TransuraniumActivityOut_DB);
                    rm.PropertyChanged += TransuraniumActivityOutValueChanged;
                    Dictionary.Add(nameof(TransuraniumActivityOut), rm);
                    return (RamAccess<string>)Dictionary[nameof(TransuraniumActivityOut)];
                }
            }
            set
            {
                TransuraniumActivityOut_DB = value.Value;
                OnPropertyChanged(nameof(TransuraniumActivityOut));
            }
        }

        private void TransuraniumActivityOutValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    value1 = value1.Replace(" ", "");
                    value1 = value1.Replace("\n", "");
                    value1 = value1.Replace("\t", "");
                    value1 = value1.Replace("\r", "");
                    if (value1.Equals("-"))
                    {
                        TransuraniumActivityOut_DB = value1;
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
                TransuraniumActivityOut_DB = value1;
            }
        }
        private bool TransuraniumActivityOut_Validation(RamAccess<string> value)//TODO
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
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            value1 = value1.Replace(" ", "");
            value1 = value1.Replace("\n", "");
            value1 = value1.Replace("\t", "");
            value1 = value1.Replace("\r", "");
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
        //TransuraniumActivityOut property
        #endregion
    }
}
