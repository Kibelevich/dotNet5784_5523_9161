using System;
using System.Windows;
using System.Windows.Controls;

namespace PL.Task;

/// <summary>
/// Interaction logic for taskWindow.xaml
/// </summary>
public partial class TaskWindow : Window
{
    // Logic entity
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <summary>
    ///  A logical entity of task
    /// </summary>
    public BO.Task Task
    {
        get { return (BO.Task)GetValue(TaskProperty); }
        set { SetValue(TaskProperty, value); }
    }

    /// <summary>
    /// Dependency property of the task 
    /// </summary>
    public static readonly DependencyProperty TaskProperty =
        DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

    /// <summary>
    /// Loading the task
    /// </summary>
    public TaskWindow(int ID = 0)
    {
        InitializeComponent();
        Task = (ID == 0) ?
            new BO.Task()
            {
                ID = 0,
                Description = "",
                Alias = "",
                DependList = null,
                Milestone = null,
                RequiredEffortTime = null,
                Status = null,
                CreatedAt = DateTime.Now,
                BaselineStart = null,
                Start = null,
                ForecastEndDate = null,
                Deadline = DateTime.Now,
                Complete = null,
                Deliverable = null,
                Remarks = null,
                Engineer = null,
                ComplexityLevel = 0
            } :
            s_bl.Task.Read(ID)!;
    }

    /// <summary>
    /// Adds or updates the selection Task
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (Task.Engineer != null && (Task.Engineer.ID < 200000000 || Task.Engineer.ID > 400000000))
                MessageBox.Show("Illegal property");
            else
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