using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DatabaseAnalysis.WPF.FireBird
{
    public class Form18 : Form1
    {
        public Form18() : base()
        {
            FormNum.Value = "1.8";
            Validate_all();
        }
        private void Validate_all()
        {
            CodeRAO_Validation(CodeRAO);
            IndividualNumberZHRO_Validation(IndividualNumberZHRO);
            SpecificActivity_Validation(SpecificActivity);
            SaltConcentration_Validation(SaltConcentration);
            Radionuclids_Validation(Radionuclids);
            ProviderOrRecieverOKPO_Validation(ProviderOrRecieverOKPO);
            TransporterOKPO_Validation(TransporterOKPO);
            TritiumActivity_Validation(TritiumActivity);
            BetaGammaActivity_Validation(BetaGammaActivity);
            AlphaActivity_Validation(AlphaActivity);
            TransuraniumActivity_Validation(TransuraniumActivity);
            PassportNumber_Validation(PassportNumber);
            RefineOrSortRAOCode_Validation(RefineOrSortRAOCode);
            Subsidy_Validation(Subsidy);
            FcpNumber_Validation(FcpNumber);
            StatusRAO_Validation(StatusRAO);
            Volume6_Validation(Volume6);
            Mass7_Validation(Mass7);
            Volume20_Validation(Volume20);
            Mass21_Validation(Mass21);
            StoragePlaceName_Validation(StoragePlaceName);
            StoragePlaceCode_Validation(StoragePlaceCode);
        }
        public override bool Object_Validation()
        {
            return !(CodeRAO.HasErrors ||
            IndividualNumberZHRO.HasErrors ||
            SpecificActivity.HasErrors ||
            SaltConcentration.HasErrors ||
            Radionuclids.HasErrors ||
            ProviderOrRecieverOKPO.HasErrors ||
            TransporterOKPO.HasErrors ||
            TritiumActivity.HasErrors ||
            BetaGammaActivity.HasErrors ||
            AlphaActivity.HasErrors ||
            TransuraniumActivity.HasErrors ||
            PassportNumber.HasErrors ||
            RefineOrSortRAOCode.HasErrors ||
            Subsidy.HasErrors ||
            FcpNumber.HasErrors ||
            StatusRAO.HasErrors ||
            Volume6.HasErrors ||
            Mass7.HasErrors ||
            Volume20.HasErrors ||
            Mass21.HasErrors ||
            StoragePlaceName.HasErrors ||
            StoragePlaceCode.HasErrors);
        }

        #region IndividualNumberZHRO
        public string IndividualNumberZHRO_DB { get; set; } = "";
        public bool IndividualNumberZHRO_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool IndividualNumberZHRO_Hidden
        {
            get => IndividualNumberZHRO_Hidden_Priv;
            set
            {
                IndividualNumberZHRO_Hidden_Priv = value;
            }
        }
        [NotMapped]
        public RamAccess<string> IndividualNumberZHRO
        {
            get
            {
                if (!IndividualNumberZHRO_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(IndividualNumberZHRO)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(IndividualNumberZHRO)]).Value = IndividualNumberZHRO_DB;
                        return (RamAccess<string>)Dictionary[nameof(IndividualNumberZHRO)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(IndividualNumberZHRO_Validation, IndividualNumberZHRO_DB);
                        rm.PropertyChanged += IndividualNumberZHROValueChanged;
                        Dictionary.Add(nameof(IndividualNumberZHRO), rm);
                        return (RamAccess<string>)Dictionary[nameof(IndividualNumberZHRO)];
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
                if (!IndividualNumberZHRO_Hidden_Priv)
                {
                    IndividualNumberZHRO_DB = value.Value;
                    OnPropertyChanged(nameof(IndividualNumberZHRO));
                }
            }
        }
        private void IndividualNumberZHROValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                IndividualNumberZHRO_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool IndividualNumberZHRO_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        #endregion

        #region PassportNumber
        public string PassportNumber_DB { get; set; } = "";
        public bool PassportNumber_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool PassportNumber_Hidden
        {
            get => PassportNumber_Hidden_Priv;
            set
            {
                PassportNumber_Hidden_Priv = value;
            }
        }
        [NotMapped]
        public RamAccess<string> PassportNumber
        {
            get
            {
                if (!PassportNumber_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(PassportNumber)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(PassportNumber)]).Value = PassportNumber_DB;
                        return (RamAccess<string>)Dictionary[nameof(PassportNumber)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(PassportNumber_Validation, PassportNumber_DB);
                        rm.PropertyChanged += PassportNumberValueChanged;
                        Dictionary.Add(nameof(PassportNumber), rm);
                        return (RamAccess<string>)Dictionary[nameof(PassportNumber)];
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
                if (!PassportNumber_Hidden_Priv)
                {
                    PassportNumber_DB = value.Value;
                    OnPropertyChanged(nameof(PassportNumber));
                }
            }
        }
        private void PassportNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                PassportNumber_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool PassportNumber_Validation(RamAccess<string> value)
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
            if (value.Value.Equals("прим."))
            {
                //if ((PassportNumberNote.Value == null) || (PassportNumberNote.Value == ""))
                //{
                //    value.AddError("Поле не может быть пустым");//to do note handling
                //}
                return true;
            }
            return true;
        }
        #endregion

        #region Volume6
        public string Volume6_DB { get; set; } = null;
        public bool Volume6_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool Volume6_Hidden
        {
            get => Volume6_Hidden_Priv;
            set
            {
                Volume6_Hidden_Priv = value;
            }
        }
        [NotMapped]
        public RamAccess<string> Volume6
        {
            get
            {
                if (!Volume6_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(Volume6)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(Volume6)]).Value = Volume6_DB;
                        return (RamAccess<string>)Dictionary[nameof(Volume6)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(Volume6_Validation, Volume6_DB);
                        rm.PropertyChanged += Volume6ValueChanged;
                        Dictionary.Add(nameof(Volume6), rm);
                        return (RamAccess<string>)Dictionary[nameof(Volume6)];
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
                if (!Volume6_Hidden_Priv)
                {
                    Volume6_DB = value.Value;
                    OnPropertyChanged(nameof(Volume6));
                }
            }
        }
        private void Volume6ValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        Volume6_DB = value1;
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
                Volume6_DB = value1;
            }
        }
        private bool Volume6_Validation(RamAccess<string> value)//TODO
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
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch (Exception)
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region Mass7
        public string Mass7_DB { get; set; } = null;
        public bool Mass7_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool Mass7_Hidden
        {
            get => Mass7_Hidden_Priv;
            set
            {
                Mass7_Hidden_Priv = value;
            }
        }
        [NotMapped]
        public RamAccess<string> Mass7
        {
            get
            {
                if (!Mass7_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(Mass7)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(Mass7)]).Value = Mass7_DB;
                        return (RamAccess<string>)Dictionary[nameof(Mass7)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(Mass7_Validation, Mass7_DB);
                        rm.PropertyChanged += Mass7ValueChanged;
                        Dictionary.Add(nameof(Mass7), rm);
                        return (RamAccess<string>)Dictionary[nameof(Mass7)];
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
                if (!Mass7_Hidden_Priv)
                {
                    Mass7_DB = value.Value;
                    OnPropertyChanged(nameof(Mass7));
                }
            }
        }
        private void Mass7ValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        Mass7_DB = value1;
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
                Mass7_DB = value1;
            }
        }
        private bool Mass7_Validation(RamAccess<string> value)//TODO
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
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch (Exception)
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region SaltConcentration
        public string SaltConcentration_DB { get; set; } = null;
        public bool SaltConcentration_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool SaltConcentration_Hidden
        {
            get => SaltConcentration_Hidden_Priv;
            set
            {
                SaltConcentration_Hidden_Priv = value;
            }
        }
        [NotMapped]
        public RamAccess<string> SaltConcentration
        {
            get
            {
                if (!SaltConcentration_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(SaltConcentration)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(SaltConcentration)]).Value = SaltConcentration_DB;
                        return (RamAccess<string>)Dictionary[nameof(SaltConcentration)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(SaltConcentration_Validation, SaltConcentration_DB);
                        rm.PropertyChanged += SaltConcentrationValueChanged;
                        Dictionary.Add(nameof(SaltConcentration), rm);
                        return (RamAccess<string>)Dictionary[nameof(SaltConcentration)];
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
                if (!SaltConcentration_Hidden_Priv)
                {
                    SaltConcentration_DB = value.Value;
                    OnPropertyChanged(nameof(SaltConcentration));
                }
            }
        }
        private void SaltConcentrationValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        SaltConcentration_DB = value1;
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
                SaltConcentration_DB = value1;
            }
        }
        private bool SaltConcentration_Validation(RamAccess<string> value)
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
            }//OK
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
            string[] nuclids = value.Value.Split(";");
            for (int k = 0; k < nuclids.Length; k++)
            {
                nuclids[k] = nuclids[k].ToLower().Replace(" ", "");
            }
            bool flag = true;
            if (!flag)
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region SpecificActivity
        public string SpecificActivity_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string> SpecificActivity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(SpecificActivity)))
                {
                    ((RamAccess<string>)Dictionary[nameof(SpecificActivity)]).Value = SpecificActivity_DB;
                    return (RamAccess<string>)Dictionary[nameof(SpecificActivity)];
                }
                else
                {
                    var rm = new RamAccess<string>(SpecificActivity_Validation, SpecificActivity_DB);
                    rm.PropertyChanged += SpecificActivityValueChanged;
                    Dictionary.Add(nameof(SpecificActivity), rm);
                    return (RamAccess<string>)Dictionary[nameof(SpecificActivity)];
                }
            }
            set
            {
                SpecificActivity_DB = value.Value;
                OnPropertyChanged(nameof(SpecificActivity));
            }
        }
        private void SpecificActivityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        SpecificActivity_DB = value1;
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
                SpecificActivity_DB = value1;
            }
        }
        private bool SpecificActivity_Validation(RamAccess<string> value)//TODO
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
        #endregion

        #region ProviderOrRecieverOKPO
        public string ProviderOrRecieverOKPO_DB { get; set; } = "";
        public bool ProviderOrRecieverOKPO_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool ProviderOrRecieverOKPO_Hidden
        {
            get => ProviderOrRecieverOKPO_Hidden_Priv;
            set
            {
                ProviderOrRecieverOKPO_Hidden_Priv = value;
            }
        }
        [NotMapped]
        public RamAccess<string> ProviderOrRecieverOKPO
        {
            get
            {
                if (!ProviderOrRecieverOKPO_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(ProviderOrRecieverOKPO)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(ProviderOrRecieverOKPO)]).Value = ProviderOrRecieverOKPO_DB;
                        return (RamAccess<string>)Dictionary[nameof(ProviderOrRecieverOKPO)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(ProviderOrRecieverOKPO_Validation, ProviderOrRecieverOKPO_DB);
                        rm.PropertyChanged += ProviderOrRecieverOKPOValueChanged;
                        Dictionary.Add(nameof(ProviderOrRecieverOKPO), rm);
                        return (RamAccess<string>)Dictionary[nameof(ProviderOrRecieverOKPO)];
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
                if (!ProviderOrRecieverOKPO_Hidden_Priv)
                {
                    ProviderOrRecieverOKPO_DB = value.Value;
                    OnPropertyChanged(nameof(ProviderOrRecieverOKPO));
                }
            }
        }
        private void ProviderOrRecieverOKPOValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                string value1 = ((RamAccess<string>)Value).Value;
                ProviderOrRecieverOKPO_DB = value1;
            }
        }
        private bool ProviderOrRecieverOKPO_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value.Equals("Минобороны"))
            {
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
        #endregion

        #region TransporterOKPO
        public string TransporterOKPO_DB { get; set; } = "";
        public bool TransporterOKPO_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool TransporterOKPO_Hidden
        {
            get => TransporterOKPO_Hidden_Priv;
            set
            {
                TransporterOKPO_Hidden_Priv = value;
            }
        }
        [NotMapped]
        public RamAccess<string> TransporterOKPO
        {
            get
            {
                if (!TransporterOKPO_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(TransporterOKPO)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(TransporterOKPO)]).Value = TransporterOKPO_DB;
                        return (RamAccess<string>)Dictionary[nameof(TransporterOKPO)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(TransporterOKPO_Validation, TransporterOKPO_DB);
                        rm.PropertyChanged += TransporterOKPOValueChanged;
                        Dictionary.Add(nameof(TransporterOKPO), rm);
                        return (RamAccess<string>)Dictionary[nameof(TransporterOKPO)];
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
                if (!TransporterOKPO_Hidden_Priv)
                {
                    TransporterOKPO_DB = value.Value;
                    OnPropertyChanged(nameof(TransporterOKPO));
                }
            }
        }
        private void TransporterOKPOValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                TransporterOKPO_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool TransporterOKPO_Validation(RamAccess<string> value)//Done
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value.Equals("-") || value.Value.Equals("Минобороны"))
            {
                return true;
            }
            if (value.Value.Equals("прим."))
            {
                //if ((TransporterOKPONote == null) || TransporterOKPONote.Equals(""))
                //    value.AddError( "Заполните примечание");
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
        #endregion

        #region StoragePlaceName
        public string StoragePlaceName_DB { get; set; } = "";
        public bool StoragePlaceName_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool StoragePlaceName_Hidden
        {
            get => StoragePlaceName_Hidden_Priv;
            set
            {
                StoragePlaceName_Hidden_Priv = value;
            }
        }
        [NotMapped]
        public RamAccess<string> StoragePlaceName
        {
            get
            {
                if (!StoragePlaceName_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(StoragePlaceName)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(StoragePlaceName)]).Value = StoragePlaceName_DB;
                        return (RamAccess<string>)Dictionary[nameof(StoragePlaceName)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(StoragePlaceName_Validation, StoragePlaceName_DB);
                        rm.PropertyChanged += StoragePlaceNameValueChanged;
                        Dictionary.Add(nameof(StoragePlaceName), rm);
                        return (RamAccess<string>)Dictionary[nameof(StoragePlaceName)];
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
                if (!StoragePlaceName_Hidden_Priv)
                {
                    StoragePlaceName_DB = value.Value;
                    OnPropertyChanged(nameof(StoragePlaceName));
                }
            }
        }

        private void StoragePlaceNameValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                StoragePlaceName_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool StoragePlaceName_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            //List<string> spr = new List<string>();
            //if (!spr.Contains(value.Value))
            //{
            //    value.AddError("Недопустимое значение");
            //    return false;
            //}
            return true;
        }
        #endregion

        #region StoragePlaceCode
        public string StoragePlaceCode_DB { get; set; } = "";
        public bool StoragePlaceCode_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool StoragePlaceCode_Hidden
        {
            get => StoragePlaceCode_Hidden_Priv;
            set
            {
                StoragePlaceCode_Hidden_Priv = value;
            }
        }
        [NotMapped]
        public RamAccess<string> StoragePlaceCode //8 cyfer code or - .
        {
            get
            {
                if (!StoragePlaceCode_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(StoragePlaceCode)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(StoragePlaceCode)]).Value = StoragePlaceCode_DB;
                        return (RamAccess<string>)Dictionary[nameof(StoragePlaceCode)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(StoragePlaceCode_Validation, StoragePlaceCode_DB);
                        rm.PropertyChanged += StoragePlaceCodeValueChanged;
                        Dictionary.Add(nameof(StoragePlaceCode), rm);
                        return (RamAccess<string>)Dictionary[nameof(StoragePlaceCode)];
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
                if (!StoragePlaceCode_Hidden_Priv)
                {
                    StoragePlaceCode_DB = value.Value;
                    OnPropertyChanged(nameof(StoragePlaceCode));
                }
            }
        }

        private void StoragePlaceCodeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                StoragePlaceCode_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool StoragePlaceCode_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            //List<string> lst = new List<string>();//HERE binds spr
            //if (!lst.Contains(value.Value))
            //{
            //    value.AddError("Недопустимое значение"); return false;
            //}
            //return true;
            if (value.Value == "-") return true;
            Regex a = new Regex("^[0-9]{8}$");
            if (!a.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            var tmp = value.Value;
            if (tmp.Length == 8)
            {
                Regex a0 = new Regex("^[1-9]");
                if (!a0.IsMatch(tmp.Substring(0, 1)))
                {
                    value.AddError("Недопустимый вид пункта - " + tmp.Substring(0, 1));
                }
                Regex a1 = new Regex("^[1-3]");
                if (!a1.IsMatch(tmp.Substring(1, 1)))
                {
                    value.AddError("Недопустимое состояние пункта - " + tmp.Substring(1, 1));
                }
                Regex a2 = new Regex("^[1-2]");
                if (!a2.IsMatch(tmp.Substring(2, 1)))
                {
                    value.AddError("Недопустимая изоляция от окружающей среды - " + tmp.Substring(2, 1));
                }
                Regex a3 = new Regex("^[1-59]");
                if (!a3.IsMatch(tmp.Substring(3, 1)))
                {
                    value.AddError("Недопустимая зона нахождения пунтка - " + tmp.Substring(3, 1));
                }
                Regex a4 = new Regex("^[0-4]");
                if (!a4.IsMatch(tmp.Substring(4, 1)))
                {
                    value.AddError("Недопустимое значение пункта - " + tmp.Substring(4, 1));
                }
                Regex a5 = new Regex("^[1-49]");
                if (!a5.IsMatch(tmp.Substring(5, 1)))
                {
                    value.AddError("Недопустимое размещение пункта хранения относительно поверхности земли - " + tmp.Substring(5, 1));
                }
                Regex a67 = new Regex("^[1]{1}[1-9]{1}|^[2]{1}[1-69]{1}|^[3]{1}[1]{1}|^[4]{1}[1-49]{1}|^[5]{1}[1-69]{1}|^[6]{1}[1]{1}|^[7]{1}[1349]{1}|^[8]{1}[1-69]{1}|^[9]{1}[9]{1}");
                if (!a67.IsMatch(tmp.Substring(6, 2)))
                {
                    value.AddError("Недопустимоый код типа РАО - " + tmp.Substring(6, 2));
                }
                if (value.HasErrors)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region CodeRAO
        public string CodeRAO_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> CodeRAO
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(CodeRAO)))
                {
                    ((RamAccess<string>)Dictionary[nameof(CodeRAO)]).Value = CodeRAO_DB;
                    return (RamAccess<string>)Dictionary[nameof(CodeRAO)];
                }
                else
                {
                    var rm = new RamAccess<string>(CodeRAO_Validation, CodeRAO_DB);
                    rm.PropertyChanged += CodeRAOValueChanged;
                    Dictionary.Add(nameof(CodeRAO), rm);
                    return (RamAccess<string>)Dictionary[nameof(CodeRAO)];
                }
            }
            set
            {
                CodeRAO_DB = value.Value;
                OnPropertyChanged(nameof(CodeRAO));
            }
        }
        private void CodeRAOValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var tmp = ((RamAccess<string>)Value).Value.ToLower();
                tmp = tmp.Replace("х", "x");
                CodeRAO_DB = tmp;
            }
        }
        private bool CodeRAO_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            var tmp = value.Value.ToLower();
            tmp = tmp.Replace("х", "x");
            Regex a = new Regex("^[0-9x+]{11}$");
            if (!a.IsMatch(tmp))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region StatusRAO
        public string StatusRAO_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> StatusRAO  //1 cyfer or OKPO.
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(StatusRAO)))
                {
                    ((RamAccess<string>)Dictionary[nameof(StatusRAO)]).Value = StatusRAO_DB;
                    return (RamAccess<string>)Dictionary[nameof(StatusRAO)];
                }
                else
                {
                    var rm = new RamAccess<string>(StatusRAO_Validation, StatusRAO_DB);
                    rm.PropertyChanged += StatusRAOValueChanged;
                    Dictionary.Add(nameof(StatusRAO), rm);
                    return (RamAccess<string>)Dictionary[nameof(StatusRAO)];
                }
            }
            set
            {
                StatusRAO_DB = value.Value;
                OnPropertyChanged(nameof(StatusRAO));
            }
        }
        private void StatusRAOValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                StatusRAO_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool StatusRAO_Validation(RamAccess<string> value)//TODO
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
                        value.AddError("Недопустимое значение"); return false;
                    }
                    return true;
                }
                catch (Exception)
                {
                    value.AddError("Недопустимое значение"); return false;
                }
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
        #endregion

        #region Volume20
        public string Volume20_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string> Volume20
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Volume20)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Volume20)]).Value = Volume20_DB;
                    return (RamAccess<string>)Dictionary[nameof(Volume20)];
                }
                else
                {
                    var rm = new RamAccess<string>(Volume20_Validation, Volume20_DB);
                    rm.PropertyChanged += Volume20ValueChanged;
                    Dictionary.Add(nameof(Volume20), rm);
                    return (RamAccess<string>)Dictionary[nameof(Volume20)];
                }
            }
            set
            {
                Volume20_DB = value.Value;
                OnPropertyChanged(nameof(Volume20));
            }
        }
        private void Volume20ValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        Volume20_DB = value1;
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
                Volume20_DB = value1;
            }
        }
        private bool Volume20_Validation(RamAccess<string> value)
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

        #region Mass21
        public string Mass21_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string> Mass21
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Mass21)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Mass21)]).Value = Mass21_DB;
                    return (RamAccess<string>)Dictionary[nameof(Mass21)];
                }
                else
                {
                    var rm = new RamAccess<string>(Mass21_Validation, Mass21_DB);
                    rm.PropertyChanged += Mass21ValueChanged;
                    Dictionary.Add(nameof(Mass21), rm);
                    return (RamAccess<string>)Dictionary[nameof(Mass21)];
                }
            }
            set
            {
                Mass21_DB = value.Value;
                OnPropertyChanged(nameof(Mass21));
            }
        }
        private void Mass21ValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        Mass21_DB = value1;
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
                Mass21_DB = value1;
            }
        }
        private bool Mass21_Validation(RamAccess<string> value)//TODO
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
        #endregion

        #region TritiumActivity
        public string TritiumActivity_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> TritiumActivity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(TritiumActivity)))
                {
                    ((RamAccess<string>)Dictionary[nameof(TritiumActivity)]).Value = TritiumActivity_DB;
                    return (RamAccess<string>)Dictionary[nameof(TritiumActivity)];
                }
                else
                {
                    var rm = new RamAccess<string>(TritiumActivity_Validation, TritiumActivity_DB);
                    rm.PropertyChanged += TritiumActivityValueChanged;
                    Dictionary.Add(nameof(TritiumActivity), rm);
                    return (RamAccess<string>)Dictionary[nameof(TritiumActivity)];
                }
            }
            set
            {
                TritiumActivity_DB = value.Value;
                OnPropertyChanged(nameof(TritiumActivity));
            }
        }
        private void TritiumActivityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        TritiumActivity_DB = value1;
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
                TritiumActivity_DB = value1;
            }
        }
        private bool TritiumActivity_Validation(RamAccess<string> value)//TODO
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

        #region BetaGammaActivity
        public string BetaGammaActivity_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> BetaGammaActivity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(BetaGammaActivity)))
                {
                    ((RamAccess<string>)Dictionary[nameof(BetaGammaActivity)]).Value = BetaGammaActivity_DB;
                    return (RamAccess<string>)Dictionary[nameof(BetaGammaActivity)];
                }
                else
                {
                    var rm = new RamAccess<string>(BetaGammaActivity_Validation, BetaGammaActivity_DB);
                    rm.PropertyChanged += BetaGammaActivityValueChanged;
                    Dictionary.Add(nameof(BetaGammaActivity), rm);
                    return (RamAccess<string>)Dictionary[nameof(BetaGammaActivity)];
                }
            }
            set
            {
                BetaGammaActivity_DB = value.Value;
                OnPropertyChanged(nameof(BetaGammaActivity));
            }
        }
        private void BetaGammaActivityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        BetaGammaActivity_DB = value1;
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
                BetaGammaActivity_DB = value1;
            }
        }
        private bool BetaGammaActivity_Validation(RamAccess<string> value)//TODO
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

        #region AlphaActivity
        public string AlphaActivity_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> AlphaActivity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(AlphaActivity)))
                {
                    ((RamAccess<string>)Dictionary[nameof(AlphaActivity)]).Value = AlphaActivity_DB;
                    return (RamAccess<string>)Dictionary[nameof(AlphaActivity)];
                }
                else
                {
                    var rm = new RamAccess<string>(AlphaActivity_Validation, AlphaActivity_DB);
                    rm.PropertyChanged += AlphaActivityValueChanged;
                    Dictionary.Add(nameof(AlphaActivity), rm);
                    return (RamAccess<string>)Dictionary[nameof(AlphaActivity)];
                }
            }
            set
            {
                AlphaActivity_DB = value.Value;
                OnPropertyChanged(nameof(AlphaActivity));
            }
        }
        private void AlphaActivityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        AlphaActivity_DB = value1;
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
                AlphaActivity_DB = value1;
            }
        }
        private bool AlphaActivity_Validation(RamAccess<string> value)//TODO
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

        #region TransuraniumActivity
        public string TransuraniumActivity_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> TransuraniumActivity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(TransuraniumActivity)))
                {
                    ((RamAccess<string>)Dictionary[nameof(TransuraniumActivity)]).Value = TransuraniumActivity_DB;
                    return (RamAccess<string>)Dictionary[nameof(TransuraniumActivity)];
                }
                else
                {
                    var rm = new RamAccess<string>(TransuraniumActivity_Validation, TransuraniumActivity_DB);
                    rm.PropertyChanged += TransuraniumActivityValueChanged;
                    Dictionary.Add(nameof(TransuraniumActivity), rm);
                    return (RamAccess<string>)Dictionary[nameof(TransuraniumActivity)];
                }
            }
            set
            {
                TransuraniumActivity_DB = value.Value;
                OnPropertyChanged(nameof(TransuraniumActivity));
            }
        }
        private void TransuraniumActivityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        TransuraniumActivity_DB = value1;
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
                TransuraniumActivity_DB = value1;
            }
        }
        private bool TransuraniumActivity_Validation(RamAccess<string> value)//TODO
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

        #region RefineOrSortRAOCode
        public string RefineOrSortRAOCode_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> RefineOrSortRAOCode //2 cyfer code or empty.
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(RefineOrSortRAOCode)))
                {
                    ((RamAccess<string>)Dictionary[nameof(RefineOrSortRAOCode)]).Value = RefineOrSortRAOCode_DB;
                    return (RamAccess<string>)Dictionary[nameof(RefineOrSortRAOCode)];
                }
                else
                {
                    var rm = new RamAccess<string>(RefineOrSortRAOCode_Validation, RefineOrSortRAOCode_DB);
                    rm.PropertyChanged += RefineOrSortRAOCodeValueChanged;
                    Dictionary.Add(nameof(RefineOrSortRAOCode), rm);
                    return (RamAccess<string>)Dictionary[nameof(RefineOrSortRAOCode)];
                }
            }
            set
            {
                RefineOrSortRAOCode_DB = value.Value;
                OnPropertyChanged(nameof(RefineOrSortRAOCode));
            }
        }//If change this change validation

        private void RefineOrSortRAOCodeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                RefineOrSortRAOCode_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool RefineOrSortRAOCode_Validation(RamAccess<string> value)//TODO
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
            else if (string.IsNullOrEmpty(OperationCode.Value))
            {
                value.AddError("Не указан код операции");
                return false;
            }
            return true;
        }
        #endregion

        #region Subsidy
        public string Subsidy_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> Subsidy // 0<number<=100 or empty.
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Subsidy)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Subsidy)]).Value = Subsidy_DB;
                    return (RamAccess<string>)Dictionary[nameof(Subsidy)];
                }
                else
                {
                    var rm = new RamAccess<string>(Subsidy_Validation, Subsidy_DB);
                    rm.PropertyChanged += SubsidyValueChanged;
                    Dictionary.Add(nameof(Subsidy), rm);
                    return (RamAccess<string>)Dictionary[nameof(Subsidy)];
                }
            }
            set
            {
                Subsidy_DB = value.Value;
                OnPropertyChanged(nameof(Subsidy));
            }
        }
        private void SubsidyValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                Subsidy_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool Subsidy_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value.Equals("-"))
            {
                return true;
            }
            try
            {
                int tmp = int.Parse(value.Value);
                if (!((tmp > 0) && (tmp <= 100)))
                {
                    value.AddError("Недопустимое значение"); return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        #region FcpNumber
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
        #endregion

        protected override bool OperationCode_Validation(RamAccess<string> value)//OK
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                return true;
            }
            bool a0 = value.Value == "01";
            bool a2 = value.Value == "18";
            bool a3 = value.Value == "55";
            bool a4 = value.Value == "63";
            bool a5 = value.Value == "64";
            bool a6 = value.Value == "68";
            bool a7 = value.Value == "97";
            bool a8 = value.Value == "98";
            bool a9 = value.Value == "99";
            bool a12 = value.Value == "51";
            bool a13 = value.Value == "52";
            bool a14 = value.Value == "10";
            bool a10 = (int.Parse(value.Value) >= 21) && (int.Parse(value.Value) <= 29);
            bool a11 = (int.Parse(value.Value) >= 31) && (int.Parse(value.Value) <= 39);
            if (!(a0 || a2 || a3 || a4 || a5 || a6 || a7 || a8 || a9 || a10 || a11 || a12 || a13 || a14))
            {
                value.AddError("Код операции не может быть использован в форме 1.7");
                return false;
            }
            return true;
        }

        protected override bool DocumentNumber_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            return true;
        }

        protected override bool DocumentVid_Validation(RamAccess<byte?> value)
        {
            value.ClearErrors();
            value.AddError("Недопустимое значение");
            return false;
        }

        protected override bool DocumentDate_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value == "прим.")
            {
                return true;
            }
            var tmp = value.Value;
            Regex b1 = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{2}$");
            if (b1.IsMatch(tmp))
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
            bool b = (OperationCode.Value == "68");
            bool c = (OperationCode.Value == "52") || (OperationCode.Value == "55");
            bool d = (OperationCode.Value == "18") || (OperationCode.Value == "51");
            if (b || c || d)
            {
                if (!tmp.Equals(OperationDate))
                {
                    //value.AddError("Заполните примечание");//to do note handling
                    return true;
                }
            }
            return true;
        }
    }
}
