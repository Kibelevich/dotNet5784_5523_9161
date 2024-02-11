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


    private void btnEngineers_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }

    private void btnTask_Click(object sender, RoutedEventArgs e)
    {
        new TaskListWindow().Show();
    }

    private void btnInitDB_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to initialize the data?", "Init DB", MessageBoxButton.YesNo);
        if(result == MessageBoxResult.Yes)
        {
            DalTest.Initialization.Do();
        }
    }

}
