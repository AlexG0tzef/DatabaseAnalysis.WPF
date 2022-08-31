using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.InnerLogger;
using DatabaseAnalysis.WPF.Interfaces;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.ViewModels.Progress;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using static DatabaseAnalysis.WPF.Resourses.StaticResourses;

namespace DatabaseAnalysis.WPF.MVVM.Views.Progress
{
    public partial class OnStartUpProgressBar : Window
    {
        public OnStartUpProgressBar()
        {
            InitializeComponent();
            DataContext = new OnStartUpProgressBarViewModel(this, new BackgroundLoader());
            Show();
        }
    }
}
