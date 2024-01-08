using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public ObservableCollection<BO.Engineer> Engineer
    {
        get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerProperty); }
        set { SetValue(EngineerProperty, value); }
    }

    public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

    public EngineerWindow( int ID=0)
    {
        InitializeComponent();
        BO.Engineer? engineer;
        if (ID == 0)
        {
           engineer = new BO.Engineer() { ID = 0, name = "", email = "", level = 0, cost = 0, currentTask = null };
        }
        else
        {
            engineer = s_bl.Engineer.Read(ID);
        }
    }

    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {

    }


}
