using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DatabaseAnalysis.WPF.FireBird
{
    [Serializable]
    public class Form212 : Form2
    {
        public Form212() : base()
        {
            FormNum.Value = "2.12";
            //NumberOfFields.Value = 8;
            Validate_all();
        }
        private void Validate_all()
        {
            Radionuclids_Validation(Radionuclids);
            OperationCode_Validation(OperationCode);
            ObjectTypeCode_Validation(ObjectTypeCode);
            Activity_Validation(Activity);
            ProviderOrRecieverOKPO_Validation(ProviderOrRecieverOKPO);
        }
        public override bool Object_Validation()
        {
            return !(Radionuclids.HasErrors ||
            OperationCode.HasErrors ||
            ObjectTypeCode.HasErrors ||
            Activity.HasErrors ||
            ProviderOrRecieverOKPO.HasErrors);
        }

        //OperationCode property
        #region  OperationCode
        public short? OperationCode_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<short?> OperationCode
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(OperationCode)))
                {
                    ((RamAccess<short?>)Dictionary[nameof(OperationCode)]).Value = OperationCode_DB;
                    return (RamAccess<short?>)Dictionary[nameof(OperationCode)];
                }
                else
                {
                    var rm = new RamAccess<short?>(OperationCode_Validation, OperationCode_DB);
                    rm.PropertyChanged += OperationCodeValueChanged;
                    Dictionary.Add(nameof(OperationCode), rm);
                    return (RamAccess<short?>)Dictionary[nameof(OperationCode)];
                }
            }
            set
            {
                OperationCode_DB = value.Value;
                OnPropertyChanged(nameof(OperationCode));
            }
        }

        private void OperationCodeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                OperationCode_DB = ((RamAccess<short?>)Value).Value;
            }
        }

        private bool OperationCode_Validation(RamAccess<short?> value)
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //OperationCode property
        #endregion

        //ObjectTypeCode property
        #region 
        public short? ObjectTypeCode_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<short?> ObjectTypeCode
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(ObjectTypeCode)))
                {
                    ((RamAccess<short?>)Dictionary[nameof(ObjectTypeCode)]).Value = ObjectTypeCode_DB;
                    return (RamAccess<short?>)Dictionary[nameof(ObjectTypeCode)];
                }
                else
                {
                    var rm = new RamAccess<short?>(ObjectTypeCode_Validation, ObjectTypeCode_DB);
                    rm.PropertyChanged += ObjectTypeCodeValueChanged;
                    Dictionary.Add(nameof(ObjectTypeCode), rm);
                    return (RamAccess<short?>)Dictionary[nameof(ObjectTypeCode)];
                }
            }
            set
            {
                ObjectTypeCode_DB = value.Value;
                OnPropertyChanged(nameof(ObjectTypeCode));
            }
        }
        //2 digit code

        private void ObjectTypeCodeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                ObjectTypeCode_DB = ((RamAccess<short?>)Value).Value;
            }
        }
        private bool ObjectTypeCode_Validation(RamAccess<short?> value)//TODO
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //ObjectTypeCode property
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
        //Radionuclids property
        #endregion

        //Activity property
        #region  Activity
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
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //Activity property
        #endregion

        //ProviderOrRecieverOKPO property
        #region  ProviderOrRecieverOKPO
        public string ProviderOrRecieverOKPO_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> ProviderOrRecieverOKPO
        {
            get
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
            set
            {
                ProviderOrRecieverOKPO_DB = value.Value;
                OnPropertyChanged(nameof(ProviderOrRecieverOKPO));
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
                value.AddError("Поле не заполнено");
                return false;
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
        //ProviderOrRecieverOKPO property
        #endregion
    }
}
