﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DatabaseAnalysis.WPF.FireBird
{
    public class DBObservable : INotifyPropertyChanged
    {
        [NotMapped] private bool _isChanged = true;

        public int Id { get; set; }

        public DBObservable()
        {
            Reports_Collection_DB = new ObservableCollectionWithItemPropertyChanged<Reports>();
            Reports_Collection.CollectionChanged += CollectionChanged;
        }

        #region Reports_Collection
        ObservableCollectionWithItemPropertyChanged<Reports> Reports_Collection_DB;
        public virtual ObservableCollectionWithItemPropertyChanged<Reports> Reports_Collection
        {
            get
            {
                return Reports_Collection_DB;
            }
            set
            {
                Reports_Collection_DB = value;
                OnPropertyChanged(nameof(Reports_Collection));
            }
        }
        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            OnPropertyChanged(nameof(Reports_Collection));
            OnPropertyChanged(nameof(Report_Collection));
            OnPropertyChanged(nameof(Reports_Collection10));
            OnPropertyChanged(nameof(Reports_Collection20));
        }
        #endregion

        #region Reports_Collection10
        [NotMapped]
        public virtual ObservableCollectionWithItemPropertyChanged<Reports> Reports_Collection10
        {
            get
            {
                var sm = from Reports t in Reports_Collection_DB where t.Master.FormNum.Value == "1.0" select t;
                var obj = new ObservableCollectionWithItemPropertyChanged<Reports>(sm);
                return obj;
            }
        }
        #endregion

        #region Reports_Collection20
        [NotMapped]
        public virtual ObservableCollectionWithItemPropertyChanged<Reports> Reports_Collection20
        {
            get
            {
                var sm = from Reports t in Reports_Collection_DB where t.Master.FormNum.Value == "2.0" select t;

                var obj = new ObservableCollectionWithItemPropertyChanged<Reports>(sm);
                return obj;
            }
        }
        #endregion

        #region Report_Collection
        [NotMapped]
        public virtual ObservableCollectionWithItemPropertyChanged<Report> Report_Collection
        {
            get
            {
                ObservableCollectionWithItemPropertyChanged<Report> lst = new();
                foreach (var reps in Reports_Collection)
                {
                    var rep = ((DatabaseAnalysis.WPF.FireBird.Reports)reps).Report_Collection;
                    lst.AddRange(rep);
                }
                return lst;
            }
        }
        #endregion

        private bool Reports_Collection_Validation(DbSet<Reports> value)
        {
            return true;
        }

        //Property Changed
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}