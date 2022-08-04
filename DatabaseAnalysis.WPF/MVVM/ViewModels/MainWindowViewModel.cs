﻿using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.State.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public Navigator Navigator { get; set; } = new Navigator();

        public ICommand SearchCommand { get; set; }


        private TextBlock _selectedSearch;
        public TextBlock SelectedSearch
        {
            get => _selectedSearch;
            set
            {
                if (_selectedSearch != value)
                {
                    _selectedSearch = value;
                    OnPropertyChanged(nameof(SelectedSearch));
                    StringSearch = "";
                }
            }
        }
        private string _stringSearch;
        public string StringSearch
        {
            get => _stringSearch;
            set
            {
                _stringSearch = value;
                OnPropertyChanged(nameof(StringSearch));
                SearchCommand?.Execute(null);
            }
        }


        public ICommand ExportExcel { get; set; }

        public MainWindowViewModel()
        {
            Navigator.CurrentViewModel = new OperReportsViewModel(Navigator);
            SearchCommand = new SearchReportsAsyncCommand(Navigator, this);
            ExportExcel = new ExportExcelReports(Navigator);
        }
    }
}