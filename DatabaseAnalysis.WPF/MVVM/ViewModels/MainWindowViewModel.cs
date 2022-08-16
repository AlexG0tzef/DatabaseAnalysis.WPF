﻿using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.State.Navigation;
using System.Windows;
using System.Windows.Controls;
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

        private double _valueBar = 100;
        public double ValueBar
        {
            get => _valueBar;
            set
            {
                _valueBar = value;
                if (value <= 100)
                    ValueBarVisible = Visibility.Visible;
                else
                    ValueBarVisible = Visibility.Hidden;
                OnPropertyChanged(nameof(ValueBar));
            }
        }

        private Visibility _valueBarVisible = Visibility.Hidden;
        public Visibility ValueBarVisible
        {
            get => _valueBarVisible;
            set
            {
                _valueBarVisible = value;
                OnPropertyChanged(nameof(ValueBarVisible));
            }
        }


        public ICommand ExportExcel { get; set; }

        public MainWindowViewModel()
        {
            Navigator.CurrentViewModel = new OperReportsViewModel(Navigator);
            SearchCommand = new SearchReportsAsyncCommand(Navigator, this);
            ExportExcel = new ExportExcelAsyncCommand(Navigator, this);
        }
    }
}