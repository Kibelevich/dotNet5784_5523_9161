using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerListWindow.xaml
/// </summary>
public partial class EngineerListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.EngineerExperiece Level { get; set; } = BO.EngineerExperiece.All;

    public ObservableCollection<BO.EngineerInList> EngineerList
    {
        get { return (ObservableCollection<BO.EngineerInList>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }

    public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.EngineerInList>), typeof(EngineerListWindow), new PropertyMetadata(null));

    public EngineerListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.EngineerInList.ReadAll();
        EngineerList = temp == null ? new() : new(temp!);
    }

    private void cbEngineerExperienceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var temp = Level == BO.EngineerExperiece.All ?
            s_bl?.EngineerInList.ReadAll() :
            s_bl?.EngineerInList.ReadAll(item => item.level == Level);
        EngineerList = temp == null ? new() : new(temp!);
    }

    private void btnAddEngineer_Click(object sender, RoutedEventArgs e)
    {
        new EngineerWindow().ShowDialog();
        var temp = s_bl?.EngineerInList.ReadAll();
        EngineerList = temp == null ? new() : new(temp!);
    }
    private void lvUpdateEngineer_DoubleClick(object sender, RoutedEventArgs e)
    {
        BO.EngineerInList? engineer = (sender as ListView)?.SelectedItem as BO.EngineerInList;
        if (engineer != null)
            new EngineerWindow(engineer.ID).ShowDialog();
        var temp = s_bl?.EngineerInList.ReadAll();
        EngineerList = temp == null ? new() : new(temp!);
    }
}
