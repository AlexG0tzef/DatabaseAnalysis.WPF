using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace DatabaseAnalysis.WPF.FireBird
{
    public abstract class Form1 : Form
    {
        [NotMapped]
        public bool flag = false;
        public Form1() : base()
        {

        }
        protected void InPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            OnPropertyChanged(args.PropertyName);
        }

        #region OperationCode
        public string OperationCode_DB { get; set; } = "";
        public bool OperationCode_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool OperationCode_Hidden
        {
            get => OperationCode_Hidden_Priv;
            set
            {
                OperationCode_Hidden_Priv = value;
            }
        }

        [NotMapped]
        public RamAccess<string> OperationCode
        {
            get
            {
                if (!OperationCode_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(OperationCode)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(OperationCode)]).Value = OperationCode_DB;
                        return (RamAccess<string>)Dictionary[nameof(OperationCode)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(OperationCode_Validation, OperationCode_DB);
                        rm.PropertyChanged += OperationCodeValueChanged;
                        Dictionary.Add(nameof(OperationCode), rm);
                        return (RamAccess<string>)Dictionary[nameof(OperationCode)];
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
                if (!OperationCode_Hidden_Priv)
                {
                    OperationCode_DB = value.Value;
                    OnPropertyChanged(nameof(OperationCode));
                }
            }
        }
        private void OperationCodeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                OperationCode_DB = ((RamAccess<string>)Value).Value;
            }
        }
        protected virtual bool OperationCode_Validation(RamAccess<string> value)//Ready
        {

            value.ClearErrors();
            return true;
        }
        #endregion

        #region OperationDate
        public string OperationDate_DB { get; set; } = "";
        public bool OperationDate_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool OperationDate_Hidden
        {
            get => OperationDate_Hidden_Priv;
            set
            {
                OperationDate_Hidden_Priv = value;
            }
        }

        [NotMapped]
        public RamAccess<string> OperationDate
        {
            get
            {
                if (!OperationDate_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(OperationDate)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(OperationDate)]).Value = OperationDate_DB;
                        return (RamAccess<string>)Dictionary[nameof(OperationDate)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(OperationDate_Validation, OperationDate_DB);
                        rm.PropertyChanged += OperationDateValueChanged;
                        Dictionary.Add(nameof(OperationDate), rm);
                        return (RamAccess<string>)Dictionary[nameof(OperationDate)];
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
                if (!OperationDate_Hidden_Priv)
                {
                    OperationDate_DB = value.Value;
                    OnPropertyChanged(nameof(OperationDate));
                }
            }
        }
        private void OperationDateValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var tmp = ((RamAccess<string>)Value).Value;
                Regex b = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{2}$");
                if (b.IsMatch(tmp))
                {
                    tmp = tmp.Insert(6, "20");
                }
                OperationDate_DB = tmp;
            }
        }
        protected virtual bool OperationDate_Validation(RamAccess<string> value)//Ready
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
            return true;
        }
        #endregion

        #region DocumentVid

        public byte? DocumentVid_DB { get; set; } = null;
        public bool DocumentVid_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool DocumentVid_Hidden
        {
            get => DocumentVid_Hidden_Priv;
            set
            {
                DocumentVid_Hidden_Priv = value;
            }
        }

        [NotMapped]
        public RamAccess<byte?> DocumentVid
        {
            get
            {
                if (!DocumentVid_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(DocumentVid)))
                    {
                        ((RamAccess<byte?>)Dictionary[nameof(DocumentVid)]).Value = DocumentVid_DB;
                        return (RamAccess<byte?>)Dictionary[nameof(DocumentVid)];
                    }
                    else
                    {
                        var rm = new RamAccess<byte?>(DocumentVid_Validation, DocumentVid_DB);
                        rm.PropertyChanged += DocumentVidValueChanged;
                        Dictionary.Add(nameof(DocumentVid), rm);
                        return (RamAccess<byte?>)Dictionary[nameof(DocumentVid)];
                    }
                }
                else
                {
                    var tmp = new RamAccess<byte?>(null, null);
                    return tmp;
                }
            }
            set
            {
                DocumentVid_DB = value.Value;
                OnPropertyChanged(nameof(DocumentVid));
            }
        }

        private void DocumentVidValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                DocumentVid_DB = ((RamAccess<byte?>)Value).Value;
            }
        }
        protected virtual bool DocumentVid_Validation(RamAccess<byte?> value)//Ready
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            value.AddError("Недопустимое значение");
            return false;
        }
        #endregion

        #region DocumentNumber
        public string DocumentNumber_DB { get; set; } = "";
        public bool DocumentNumber_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool DocumentNumber_Hidden
        {
            get => DocumentNumber_Hidden_Priv;
            set
            {
                DocumentNumber_Hidden_Priv = value;
            }
        }

        [NotMapped]
        public RamAccess<string> DocumentNumber
        {
            get
            {
                if (!DocumentNumber_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(DocumentNumber)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(DocumentNumber)]).Value = DocumentNumber_DB;
                        return (RamAccess<string>)Dictionary[nameof(DocumentNumber)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(DocumentNumber_Validation, DocumentNumber_DB);
                        rm.PropertyChanged += DocumentNumberValueChanged;
                        Dictionary.Add(nameof(DocumentNumber), rm);
                        return (RamAccess<string>)Dictionary[nameof(DocumentNumber)];
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
                if (!DocumentNumber_Hidden_Priv)
                {
                    DocumentNumber_DB = value.Value;
                    OnPropertyChanged(nameof(DocumentNumber));
                }
            }
        }
        private void DocumentNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                DocumentNumber_DB = ((RamAccess<string>)Value).Value;
            }
        }
        protected virtual bool DocumentNumber_Validation(RamAccess<string> value)//Ready
        { return true; }
        #endregion

        #region DocumentDate
        public string DocumentDate_DB { get; set; } = "";
        public bool DocumentDate_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool DocumentDate_Hidden
        {
            get => DocumentDate_Hidden_Priv;
            set
            {
                DocumentDate_Hidden_Priv = value;
            }
        }

        [NotMapped]
        public RamAccess<string> DocumentDate
        {
            get
            {
                if (!DocumentDate_Hidden_Priv)
                {
                    if (Dictionary.ContainsKey(nameof(DocumentDate)))
                    {
                        ((RamAccess<string>)Dictionary[nameof(DocumentDate)]).Value = DocumentDate_DB;
                        return (RamAccess<string>)Dictionary[nameof(DocumentDate)];
                    }
                    else
                    {
                        var rm = new RamAccess<string>(DocumentDate_Validation, DocumentDate_DB);
                        rm.PropertyChanged += DocumentDateValueChanged;
                        Dictionary.Add(nameof(DocumentDate), rm);
                        return (RamAccess<string>)Dictionary[nameof(DocumentDate)];
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
                if (!DocumentDate_Hidden_Priv)
                {
                    DocumentDate_DB = value.Value;
                    OnPropertyChanged(nameof(DocumentDate));
                }
            }
        }
        private void DocumentDateValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var tmp = ((RamAccess<string>)Value).Value;
                Regex b = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{2}$");
                if (b.IsMatch(tmp))
                {
                    tmp = tmp.Insert(6, "20");
                }
                DocumentDate_DB = tmp;
            }
        }
        protected virtual bool DocumentDate_Validation(RamAccess<string> value)//Ready
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
            return true;
        }
        #endregion

    }
}
