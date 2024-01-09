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
    public BO.Engineer Engineer
    {
        get { return (BO.Engineer)GetValue(EngineerProperty); }
        set { SetValue(EngineerProperty, value); }
    }

    public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

    public EngineerWindow(int ID = 0)
    {
        InitializeComponent();
        Engineer = (ID == 0) ?
            new BO.Engineer() { ID = 0, name = "", email = "", level = 0, cost = 0, currentTask = null }:
            s_bl.Engineer.Read(ID)!;
    }

    public void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if ((sender as Button)?.Content.ToString() == "ADD")
            {
                s_bl.Engineer.Create(Engineer);
                MessageBox.Show("The engineer was successfully added");
            }
            else
            {
                s_bl.Engineer.Update(Engineer);
                MessageBox.Show("The engineer has been updated successfully");
            }
            Close();
        }
        catch (BO.BlAlreadyExistException ex)
        { MessageBox.Show(ex.Message); }
        catch (BO.BlIllegalPropertyException ex)
        { MessageBox.Show(ex.Message); }
        catch (BO.BlDoesNotExistException ex)
        { MessageBox.Show(ex.Message); }
        catch (Exception ex)
        { MessageBox.Show(ex.Message); }
    }
}
