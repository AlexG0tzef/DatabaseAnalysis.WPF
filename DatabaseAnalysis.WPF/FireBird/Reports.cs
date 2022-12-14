using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.FireBird
{
    public class Reports : IKey, INumberInOrder
    {
        [NotMapped]
        public long Order
        {
            get
            {
                try
                {
                    var num_str = "0";
                    if (Master_DB.RegNoRep.Value.Length >= 5)
                    {
                        num_str = Master_DB.RegNoRep.Value[..5];
                    }
                    else
                    {
                        num_str = Master_DB.RegNoRep.Value;
                    }
                    var num_int = Convert.ToInt64(num_str);
                    return num_int;
                }
                catch
                {
                    return 0;
                }
            }
        }
        public Reports()
        {
            Init();
        }
        private void Init()
        {
            Report_Collection = new ObservableCollectionWithItemPropertyChanged<Report>();
            Report_Collection.CollectionChanged += CollectionChanged;
        }

        public Report Master_DB { get; set; }

        [NotMapped]
        public Report Master
        {
            get
            {
                return Master_DB;
            }
            set
            {
                Master_DB = value;
                OnPropertyChanged(nameof(Master));
            }
        }

        ObservableCollectionWithItemPropertyChanged<Report> Report_Collection_DB;

        public ObservableCollectionWithItemPropertyChanged<Report> Report_Collection
        {
            get
            {
                return Report_Collection_DB;
            }
            set
            {
                Report_Collection_DB = value;
                OnPropertyChanged(nameof(Report_Collection));
            }
        }

        public void Sort()
        {
            Report_Collection.QuickSort();
        }
        public async Task SortAsync()
        {
            await Report_Collection.QuickSortAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }

        public void SetOrder(long index) { }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            OnPropertyChanged(nameof(Report_Collection));
        }

        public void CleanIds()
        {
            Id = 0;
            Master.CleanIds();
            foreach (Report item in Report_Collection)
            {
                item.CleanIds();
            }
        }

        private bool Master_Validation(RamAccess<Report> value)
        {
            return true;
        }

        private bool Report_Collection_Validation(RamAccess<ObservableCollectionWithItemPropertyChanged<Report>> value)
        {
            return true;
        }

        //Property Changed
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        //Property Changed
    }
}