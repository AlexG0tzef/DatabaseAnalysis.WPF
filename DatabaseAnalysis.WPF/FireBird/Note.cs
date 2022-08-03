using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DatabaseAnalysis.WPF.FireBird
{
    public class Note : IKey, INumberInOrder
    {
        public Note()
        {
            Init();
        }
        public Note(string rowNumber, string graphNumber, string comment)
        {
            RowNumber.Value = rowNumber;
            GraphNumber.Value = graphNumber;
            Comment.Value = comment;
            Init();
        }
        protected void InPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            OnPropertyChanged(args.PropertyName);
        }
        [NotMapped]
        Dictionary<string, RamAccess> Dictionary { get; set; } = new Dictionary<string, RamAccess>();
        public void Init()
        {
            RowNumber_Validation(RowNumber);
            GraphNumber_Validation(GraphNumber);
            Comment_Validation(Comment);
        }

        public int Id { get; set; }

        public void SetOrder(long index) { }
        public long Order { get; set; }

        #region RowNUmber
        public string? RowNumber_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string?> RowNumber
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(RowNumber)))
                {
                    ((RamAccess<string?>)Dictionary[nameof(RowNumber)]).Value = RowNumber_DB;
                    return (RamAccess<string?>)Dictionary[nameof(RowNumber)];
                }
                else
                {
                    var rm = new RamAccess<string?>(RowNumber_Validation, RowNumber_DB);
                    rm.PropertyChanged += RowNumberValueChanged;
                    Dictionary.Add(nameof(RowNumber), rm);
                    return (RamAccess<string?>)Dictionary[nameof(RowNumber)];
                }
            }
            set
            {
                RowNumber_DB = value.Value;
                OnPropertyChanged(nameof(RowNumber));
            }
        }
        private void RowNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                RowNumber_DB = ((RamAccess<string?>)Value).Value;
            }
        }
        private bool RowNumber_Validation(RamAccess<string?> value)
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        #region GraphNumber
        public string? GraphNumber_DB { get; set; } = null;
        [NotMapped]
        public RamAccess<string?> GraphNumber
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(GraphNumber)))
                {
                    ((RamAccess<string?>)Dictionary[nameof(GraphNumber)]).Value = GraphNumber_DB;
                    return (RamAccess<string?>)Dictionary[nameof(GraphNumber)];
                }
                else
                {
                    var rm = new RamAccess<string?>(GraphNumber_Validation, GraphNumber_DB);
                    rm.PropertyChanged += GraphNumberValueChanged;
                    Dictionary.Add(nameof(GraphNumber), rm);
                    return (RamAccess<string?>)Dictionary[nameof(GraphNumber)];
                }
            }
            set
            {
                GraphNumber_DB = value.Value;
                OnPropertyChanged(nameof(GraphNumber));
            }
        }
        private void GraphNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                GraphNumber_DB = ((RamAccess<string?>)Value).Value;
            }
        }
        private bool GraphNumber_Validation(RamAccess<string?> value)
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        #region Comment
        public string? Comment_DB { get; set; } = "";
        [NotMapped]
        public RamAccess<string> Comment
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Comment)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Comment)]).Value = Comment_DB;
                    return (RamAccess<string>)Dictionary[nameof(Comment)];
                }
                else
                {
                    var rm = new RamAccess<string>(Comment_Validation, Comment_DB);
                    rm.PropertyChanged += CommentValueChanged;
                    Dictionary.Add(nameof(Comment), rm);
                    return (RamAccess<string>)Dictionary[nameof(Comment)];
                }
            }
            set
            {
                Comment_DB = value.Value;
                OnPropertyChanged(nameof(Comment));
            }
        }
        private void CommentValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                Comment_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool Comment_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        //Для валидации
        public bool Object_Validation()
        {
            return true;
        }
        //Для валидации

        //Property Changed
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        //Property Changed

    }
}
