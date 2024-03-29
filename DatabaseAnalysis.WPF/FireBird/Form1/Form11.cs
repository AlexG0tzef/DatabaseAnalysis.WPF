﻿using OfficeOpenXml;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace DatabaseAnalysis.WPF.FireBird
{
    [Serializable]
    public class Form11 : Form1
    {
        public Form11() : base()
        {
            FormNum.Value = "1.1";
            Validate_all();
        }
        public bool _autoRN = false;
        private void Validate_all()
        {
            Activity_Validation(Activity);
            Category_Validation(Category);
            CreationDate_Validation(CreationDate);
            CreatorOKPO_Validation(CreatorOKPO);
            FactoryNumber_Validation(FactoryNumber);
            Owner_Validation(Owner);
            PackName_Validation(PackName);
            PackNumber_Validation(PackNumber);
            PackType_Validation(PackType);
            PassportNumber_Validation(PassportNumber);
            PropertyCode_Validation(PropertyCode);
            ProviderOrRecieverOKPO_Validation(ProviderOrRecieverOKPO);
            Quantity_Validation(Quantity);
            Radionuclids_Validation(Radionuclids);
            SignedServicePeriod_Validation(SignedServicePeriod);
            TransporterOKPO_Validation(TransporterOKPO);
            Type_Validation(Type);
        }
        public override bool Object_Validation()
        {
            return !(Activity.HasErrors ||
            Category.HasErrors ||
            CreationDate.HasErrors ||
            CreatorOKPO.HasErrors ||
            FactoryNumber.HasErrors ||
            Owner.HasErrors ||
            PackName.HasErrors ||
            PackNumber.HasErrors ||
            PackType.HasErrors ||
            PassportNumber.HasErrors ||
            PropertyCode.HasErrors ||
            ProviderOrRecieverOKPO.HasErrors ||
            Quantity.HasErrors ||
            Radionuclids.HasErrors ||
            SignedServicePeriod.HasErrors ||
            TransporterOKPO.HasErrors ||
            Type.HasErrors);
        }


        #region PassportNumber
        public string PassportNumber_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> PassportNumber
        {
            get
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
            set
            {
                PassportNumber_DB = value.Value;
                OnPropertyChanged(nameof(PassportNumber));
            }
        }
        private void PassportNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                PassportNumber_DB = ((RamAccess<string>)Value).Value;
            }
        }
        protected bool PassportNumber_Validation(RamAccess<string> value)//Ready
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
            return true;
        }
        #endregion

        #region Type
        public string Type_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> Type
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Type)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Type)]).Value = Type_DB;
                    return (RamAccess<string>)Dictionary[nameof(Type)];
                }
                else
                {
                    var rm = new RamAccess<string>(Type_Validation, Type_DB);
                    rm.PropertyChanged += TypeValueChanged;
                    Dictionary.Add(nameof(Type), rm);
                    return (RamAccess<string>)Dictionary[nameof(Type)];
                }
            }
            set
            {
                Type_DB = value.Value;
                OnPropertyChanged(nameof(Type));
            }
        }
        private void TypeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                Type_DB = ((RamAccess<string>)Value).Value;
            }
        }

        protected bool Type_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (string.IsNullOrEmpty(Radionuclids.Value))
            {

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
        }
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
            if (_autoRN)
            {
                _autoRN = false;
                return true;
            }
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("прим."))
            {
                return true;
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

        #region FactoryNumber
        public string FactoryNumber_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> FactoryNumber
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(FactoryNumber)))
                {
                    ((RamAccess<string>)Dictionary[nameof(FactoryNumber)]).Value = FactoryNumber_DB;
                    return (RamAccess<string>)Dictionary[nameof(FactoryNumber)];
                }
                else
                {
                    var rm = new RamAccess<string>(FactoryNumber_Validation, FactoryNumber_DB);
                    rm.PropertyChanged += FactoryNumberValueChanged;
                    Dictionary.Add(nameof(FactoryNumber), rm);
                    return (RamAccess<string>)Dictionary[nameof(FactoryNumber)];
                }
            }
            set
            {
                FactoryNumber_DB = value.Value;
                OnPropertyChanged(nameof(FactoryNumber));
            }
        }
        private void FactoryNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                FactoryNumber_DB = ((RamAccess<string>)Value).Value;
            }
        }

        private bool FactoryNumber_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        #endregion

        #region Quantity
        public int? Quantity_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<int?> Quantity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Quantity)))
                {
                    ((RamAccess<int?>)Dictionary[nameof(Quantity)]).Value = Quantity_DB;
                    return (RamAccess<int?>)Dictionary[nameof(Quantity)];
                }
                else
                {
                    var rm = new RamAccess<int?>(Quantity_Validation, Quantity_DB);
                    rm.PropertyChanged += QuantityValueChanged;
                    Dictionary.Add(nameof(Quantity), rm);
                    return (RamAccess<int?>)Dictionary[nameof(Quantity)];
                }
            }
            set
            {
                Quantity_DB = value.Value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        private void QuantityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                Quantity_DB = ((RamAccess<int?>)Value).Value;
            }
        }
        private bool Quantity_Validation(RamAccess<int?> value)//Ready
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value <= 0)
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region Activity
        public string Activity_DB { get; set; } = "";
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
            if (value.Value.Equals("прим."))
            {
                return false;
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
        #endregion

        #region CreationDate
        public string CreationDate_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> CreationDate
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(CreationDate)))
                {
                    ((RamAccess<string>)Dictionary[nameof(CreationDate)]).Value = CreationDate_DB;
                    return (RamAccess<string>)Dictionary[nameof(CreationDate)];
                }
                else
                {
                    var rm = new RamAccess<string>(CreationDate_Validation, CreationDate_DB);
                    rm.PropertyChanged += CreationDateValueChanged;
                    Dictionary.Add(nameof(CreationDate), rm);
                    return (RamAccess<string>)Dictionary[nameof(CreationDate)];
                }
            }
            set
            {
                CreationDate_DB = value.Value;
                OnPropertyChanged(nameof(CreationDate));
            }
        }
        private void CreationDateValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var tmp = ((RamAccess<string>)Value).Value;
                Regex b = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{2}$");
                if (b.IsMatch(tmp))
                {
                    tmp = tmp.Insert(6, "20");
                }
                CreationDate_DB = tmp;
            }
        }
        private bool CreationDate_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("прим."))
            {
                //if ((CreationDateNote.Value == null) || (CreationDateNote.Value == ""))
                //    value.AddError("Заполните примечание");
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
            return true;
        }
        #endregion

        #region CreatorOKPO
        public string CreatorOKPO_DB { get; set; }
        [NotMapped]
        public RamAccess<string> CreatorOKPO
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(CreatorOKPO)))
                {
                    ((RamAccess<string>)Dictionary[nameof(CreatorOKPO)]).Value = CreatorOKPO_DB;
                    return (RamAccess<string>)Dictionary[nameof(CreatorOKPO)];
                }
                else
                {
                    var rm = new RamAccess<string>(CreatorOKPO_Validation, CreatorOKPO_DB);
                    rm.PropertyChanged += CreatorOKPOValueChanged;
                    Dictionary.Add(nameof(CreatorOKPO), rm);
                    return (RamAccess<string>)Dictionary[nameof(CreatorOKPO)];
                }
            }//OK
            set
            {
                CreatorOKPO_DB = value.Value;
                OnPropertyChanged(nameof(CreatorOKPO));
            }
        }
        private void CreatorOKPOValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                CreatorOKPO_DB = value1;
            }
        }
        private bool CreatorOKPO_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("прим."))
            {
                //if ((PassportNumberNote.Value == null) || (PassportNumberNote.Value == ""))
                //    value.AddError("Заполните примечание");
                return false;
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

        #region Category
        public short? Category_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<short?> Category
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Category)))
                {
                    ((RamAccess<short?>)Dictionary[nameof(Category)]).Value = Category_DB;
                    return (RamAccess<short?>)Dictionary[nameof(Category)];
                }
                else
                {
                    var rm = new RamAccess<short?>(Category_Validation, Category_DB);
                    rm.PropertyChanged += CategoryValueChanged;
                    Dictionary.Add(nameof(Category), rm);
                    return (RamAccess<short?>)Dictionary[nameof(Category)];
                }
            }//OK
            set
            {
                Category_DB = value.Value;
                OnPropertyChanged(nameof(Category));
            }
        }
        private void CategoryValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                Category_DB = ((RamAccess<short?>)Value).Value;
            }
        }
        private bool Category_Validation(RamAccess<short?> value)//TODO
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if ((value.Value < 1) || (value.Value > 5))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region SignedServicePeriod
        public float? SignedServicePeriod_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<float?> SignedServicePeriod
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(SignedServicePeriod)))
                {
                    ((RamAccess<float?>)Dictionary[nameof(SignedServicePeriod)]).Value = SignedServicePeriod_DB;
                    return (RamAccess<float?>)Dictionary[nameof(SignedServicePeriod)];
                }
                else
                {
                    var rm = new RamAccess<float?>(SignedServicePeriod_Validation, SignedServicePeriod_DB);
                    rm.PropertyChanged += SignedServicePeriodValueChanged;
                    Dictionary.Add(nameof(SignedServicePeriod), rm);
                    return (RamAccess<float?>)Dictionary[nameof(SignedServicePeriod)];
                }
            }//OK
            set
            {
                SignedServicePeriod_DB = value.Value;
                OnPropertyChanged(nameof(SignedServicePeriod));
            }
        }

        private void SignedServicePeriodValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                SignedServicePeriod_DB = ((RamAccess<float?>)Value).Value;
            }
        }
        private bool SignedServicePeriod_Validation(RamAccess<float?> value)//Ready
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value <= 0)
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region PropertyCode
        public byte? PropertyCode_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<byte?> PropertyCode
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(PropertyCode)))
                {
                    ((RamAccess<byte?>)Dictionary[nameof(PropertyCode)]).Value = PropertyCode_DB;
                    return (RamAccess<byte?>)Dictionary[nameof(PropertyCode)];
                }
                else
                {
                    var rm = new RamAccess<byte?>(PropertyCode_Validation, PropertyCode_DB);
                    rm.PropertyChanged += PropertyCodeValueChanged;
                    Dictionary.Add(nameof(PropertyCode), rm);
                    return (RamAccess<byte?>)Dictionary[nameof(PropertyCode)];
                }
            }//OK
            set
            {
                PropertyCode_DB = value.Value;
                OnPropertyChanged(nameof(PropertyCode));
            }
        }
        private void PropertyCodeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                PropertyCode_DB = ((RamAccess<byte?>)Value).Value;
            }
        }
        private bool PropertyCode_Validation(RamAccess<byte?> value)//Ready
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!((value.Value >= 1) && (value.Value <= 9)))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        #region Owner
        public string Owner_DB { get; set; }
        [NotMapped]
        public RamAccess<string> Owner
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Owner)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Owner)]).Value = Owner_DB;
                    return (RamAccess<string>)Dictionary[nameof(Owner)];
                }
                else
                {
                    var rm = new RamAccess<string>(Owner_Validation, Owner_DB);
                    rm.PropertyChanged += OwnerValueChanged;
                    Dictionary.Add(nameof(Owner), rm);
                    return (RamAccess<string>)Dictionary[nameof(Owner)];
                }
            }//OK
            set
            {
                Owner_DB = value.Value;
                OnPropertyChanged(nameof(Owner));
            }
        }
        //if change this change validation
        private void OwnerValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                string value1 = ((RamAccess<string>)Value).Value;
                Owner_DB = value1;
            }
        }
        private bool Owner_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("прим."))
            {
                //if ((OwnerNote == null) || OwnerNote.Equals(""))
                //    value.AddError("Заполните примечание");
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

        #region ProviderOrRecieverOKPO
        public string ProviderOrRecieverOKPO_DB { get; set; }
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
            }//OK
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
                var value1 = ((RamAccess<string>)Value).Value;
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
            if (value.Value.Equals("прим."))
            {
                //if ((ProviderOrRecieverOKPONote == null) || ProviderOrRecieverOKPONote.Equals(""))
                //    value.AddError("Заполните примечание");
                return true;
            }
            if ((value.Value.Length != 8) && (value.Value.Length != 14))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            Regex mask = new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$");
            if (!mask.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region TransporterOKPO
        public string TransporterOKPO_DB { get; set; }
        [NotMapped]
        public RamAccess<string> TransporterOKPO
        {
            get
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
            }//OK
            set
            {
                TransporterOKPO_DB = value.Value;
                OnPropertyChanged(nameof(TransporterOKPO));
            }
        }

        private void TransporterOKPOValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                TransporterOKPO_DB = value1;
            }
        }
        private bool TransporterOKPO_Validation(RamAccess<string> value)//TODO
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
            if (value.Value.Equals("прим."))
            {
                //if ((TransporterOKPONote == null) || TransporterOKPONote.Equals(""))
                //    value.AddError("Заполните примечание");
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

        #region PackName
        public string PackName_DB { get; set; }
        [NotMapped]
        public RamAccess<string> PackName
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(PackName)))
                {
                    ((RamAccess<string>)Dictionary[nameof(PackName)]).Value = PackName_DB;
                    return (RamAccess<string>)Dictionary[nameof(PackName)];
                }
                else
                {
                    var rm = new RamAccess<string>(PackName_Validation, PackName_DB);
                    rm.PropertyChanged += PackNameValueChanged;
                    Dictionary.Add(nameof(PackName), rm);
                    return (RamAccess<string>)Dictionary[nameof(PackName)];
                }
            }//OK
            set
            {
                PackName_DB = value.Value;
                OnPropertyChanged(nameof(PackName));
            }
        }

        private void PackNameValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                PackName_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool PackName_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("прим."))
            {
                //if ((PackNameNote == null) || PackNameNote.Equals(""))
                //    value.AddError("Заполните примечание");//to do note handling
                return true;
            }
            return true;
        }
        #endregion

        #region PackType
        public string PackType_DB { get; set; }
        [NotMapped]
        public RamAccess<string> PackType
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(PackType)))
                {
                    ((RamAccess<string>)Dictionary[nameof(PackType)]).Value = PackType_DB;
                    return (RamAccess<string>)Dictionary[nameof(PackType)];
                }
                else
                {
                    var rm = new RamAccess<string>(PackType_Validation, PackType_DB);
                    rm.PropertyChanged += PackTypeValueChanged;
                    Dictionary.Add(nameof(PackType), rm);
                    return (RamAccess<string>)Dictionary[nameof(PackType)];
                }
            }//OK
            set
            {
                PackType_DB = value.Value;
                OnPropertyChanged(nameof(PackType));
            }
        }
        //If change this change validation
        private void PackTypeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                PackType_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool PackType_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("прим."))
            {
                //if ((PackTypeNote == null) || PackTypeNote.Equals(""))
                //    value.AddError("Заполните примечание");//to do note handling
                return true;
            }
            return true;
        }
        #endregion

        #region PackNumber
        public string PackNumber_DB { get; set; }
        [NotMapped]
        public RamAccess<string> PackNumber
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(PackNumber)))
                {
                    ((RamAccess<string>)Dictionary[nameof(PackNumber)]).Value = PackNumber_DB;
                    return (RamAccess<string>)Dictionary[nameof(PackNumber)];
                }
                else
                {
                    var rm = new RamAccess<string>(PackNumber_Validation, PackNumber_DB);
                    rm.PropertyChanged += PackNumberValueChanged;
                    Dictionary.Add(nameof(PackNumber), rm);
                    return (RamAccess<string>)Dictionary[nameof(PackNumber)];
                }
            }//OK
            set
            {
                PackNumber_DB = value.Value;
                OnPropertyChanged(nameof(PackNumber));
            }
        }
        //If change this change validation
        private void PackNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                PackNumber_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool PackNumber_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))//ok
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("прим."))
            {
                //if ((PackNumberNote == null) || PackNumberNote.Equals(""))
                //    value.AddError("Заполните примечание");//to do note handling
                return true;
            }
            return true;
        }
        #endregion

        protected override bool DocumentNumber_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (value.Value == "прим.")
            {
                //if ((DocumentNumberNote.Value == null) || DocumentNumberNote.Value.Equals(""))
                //    value.AddError("Заполните примечание");
                return true;
            }
            if (string.IsNullOrEmpty(value.Value))//ok
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }

        protected override bool OperationCode_Validation(RamAccess<string> value)//OK
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if ((value.Value == "01") || (value.Value == "13") ||
                (value.Value == "14") || (value.Value == "16") ||
                (value.Value == "26") || (value.Value == "36") ||
                (value.Value == "44") || (value.Value == "45") ||
                (value.Value == "49") || (value.Value == "51") ||
                (value.Value == "52") || (value.Value == "55") ||
                (value.Value == "56") || (value.Value == "57") ||
                (value.Value == "59") || (value.Value == "76"))
            {
                value.AddError("Код операции не может быть использован для РВ");
                return false;
            }
            return true;
        }
    }
}
