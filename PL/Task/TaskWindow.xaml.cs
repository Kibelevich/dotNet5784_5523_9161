using System;
using System.Windows;
using System.Windows.Controls;

namespace PL.Task;

/// <summary>
/// Interaction logic for TaskWindow.xaml
/// </summary>
public partial class TaskWindow : Window
{
    // Logic entity
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <summary>
    ///  A logical entity of Task
    /// </summary>
    public BO.Task Task
    {
        get { return (BO.Task)GetValue(TaskProperty); }
        set { SetValue(TaskProperty, value); }
    }

    /// <summary>
    /// Dependency property of the Task 
    /// </summary>
    public static readonly DependencyProperty TaskProperty =
        DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

    /// <summary>
    /// Loading the Task
    /// </summary>
    public TaskWindow(int ID = 0)
    {
        InitializeComponent();
        Task = (ID == 0) ?
            new BO.Task()
            {
                ID = 0,
                description = "",
                alias = "",
                dependList = null,////////////////////////////////////////////////////////////////////
                milestone = null,
                requiredEffortTime = TimeSpan.Zero,
                status = null,
                createdAt = DateTime.MinValue,
                baselineStart = null,
                start = null,
                forecastEndDate = null,
                deadline = DateTime.MinValue,
                complete = null,
                deliverable = null,
                remarks = null,
                engineer = null,
                complexityLevel = 0
            } :
            s_bl.Task.Read(ID)!;
    }

    /// <summary>
    /// Adds or updates the selection Task
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if ((sender as Button)?.Content.ToString() == "ADD")
            {
                s_bl.Task.Create(Task);
                MessageBox.Show("The Task was successfully added");
            }
            else
            {
                s_bl.Task.Update(Task);
                MessageBox.Show("The Task has been updated successfully");
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