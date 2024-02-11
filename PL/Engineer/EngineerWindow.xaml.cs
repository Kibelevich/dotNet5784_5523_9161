using System;
using System.Windows;
using System.Windows.Controls;

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    // Logic entity
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <summary>
    ///  A logical entity of engineer
    /// </summary>
    public BO.Engineer Engineer
    {
        get { return (BO.Engineer)GetValue(EngineerProperty); }
        set { SetValue(EngineerProperty, value); }
    }

    /// <summary>
    /// Dependency property of the engineer 
    /// </summary>
    public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

    /// <summary>
    /// Loading the engineer
    /// </summary>
    public EngineerWindow(int ID = 0)
    {
        InitializeComponent();
        Engineer = (ID == 0) ?
            new BO.Engineer() { ID = 0, name = "", email = "", level = 0, cost = 0, currentTask = new BO.TaskInEngineer { ID = 0, alias = "" } } :
            s_bl.Engineer.Read(ID)!;
    }

    /// <summary>
    /// Adds or updates the selection engineer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
