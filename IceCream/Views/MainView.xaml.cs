using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using IceCream.ViewModels;

namespace IceCream.Views;

public partial class MainView : Window
{
    private MainViewModel _viewModel;
    public MainView()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        DataContext = _viewModel;
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _viewModel.SaveIceCreamStation();
            MessageBox.Show("Success, stations was added!");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Empty Station Id");
        }
        
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _viewModel.DeleteIceCreamStation();
            MessageBox.Show("Success");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Nothing chosen");
        }
    }

    private void Selector_OnSelected(object sender, RoutedEventArgs e)
    {
        _viewModel.Select();
    }

    private void Variance_OnClick(object sender, RoutedEventArgs e)
    {
        var colour = _viewModel.CalculateVariance();
        switch (colour)
        {
            case 1:
                VarianceBox.Foreground = Brushes.Green;
                break;
            case 0:
                VarianceBox.Foreground= Brushes.Red;
                break;
            case -1:
                VarianceBox.Foreground = Brushes.Black;
                break;
        }
    }

    private void ChangeMode_OnClick(object sender, RoutedEventArgs e)
    {
        var currentMode = _viewModel.SwitchMode();
        switch (currentMode)
        {
            case 0:
                MainGrid.Background = Brushes.White;
                TextBlock1.Foreground = Brushes.Black;
                TextBlock2.Foreground = Brushes.Black;
                TextBlock3.Foreground = Brushes.Black;
                TextBlock4.Foreground = Brushes.Black;
                TextBlock5.Foreground = Brushes.Black;
                ListView.Foreground = Brushes.Black;
                ListView.Background = Brushes.White;
                break;
            case 1:
                MainGrid.Background = Brushes.Black;
                TextBlock1.Foreground = Brushes.White;
                TextBlock2.Foreground = Brushes.White;
                TextBlock3.Foreground = Brushes.White;
                TextBlock4.Foreground = Brushes.White;
                TextBlock5.Foreground = Brushes.White;
                ListView.Foreground = Brushes.White;
                ListView.Background = Brushes.Black;
                break;
            case 2:
                MainGrid.Background = Brushes.Purple;
                TextBlock1.Foreground = Brushes.White;
                TextBlock2.Foreground = Brushes.White;
                TextBlock3.Foreground = Brushes.White;
                TextBlock4.Foreground = Brushes.White;
                TextBlock5.Foreground = Brushes.White;
                ListView.Foreground = Brushes.White;
                ListView.Background = Brushes.Violet;
                break;
                
        }
    }

    private void MainView_OnKeyDown(object sender, KeyEventArgs e)
    {
        ListView.SelectedItem = null;
        _viewModel.ListViewUnselection();
    }
}