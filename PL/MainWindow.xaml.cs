using System.Windows;
using PL.Engineer;
using PL.Task;


namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Main window - init
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Show engineers window 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnEngineers_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }

    /// <summary>
    /// Show tasks window 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnTasks_Click(object sender, RoutedEventArgs e)
    {
        new TaskListWindow().Show();
    }

    private void BtnInitDB_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to initialize the data?", "Init DB", MessageBoxButton.YesNo);
        if(result == MessageBoxResult.Yes)
        {
            DalTest.Initialization.Do();
        }
    }

}
