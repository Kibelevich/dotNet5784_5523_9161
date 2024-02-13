using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace PL.Task;

/// <summary>
/// Interaction logic for taskListWindow.xaml
/// </summary>
public partial class TaskListWindow : Window
{
    // Logic entity
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    // Filtration Task level
    public BO.Status Status { get; set; } = BO.Status.None;

    /// <summary>
    ///  A logical list of tasks
    /// </summary>
    public ObservableCollection<BO.TaskInList> TaskList
    {
        get { return (ObservableCollection<BO.TaskInList>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }

    /// <summary>
    /// Dependency property of the logical task list
    /// </summary>
    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));

    /// <summary>
    /// Loading the list of tasks
    /// </summary>
    public TaskListWindow()
    {
        InitializeComponent();
        //try
        //{
            var temp = s_bl?.TaskInList.ReadAll();
            TaskList = temp == null ? new() : new(temp!);
        //}catch(BO.BlDeadlinePassedException ex)
        //{
        //    MessageBox.Show(ex.Message);
        //}
    }

    /// <summary>
    /// Filter the tasks by the selection level
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CbStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var temp = Status == BO.Status.None ?
            s_bl?.TaskInList.ReadAll() :
            s_bl?.TaskInList.ReadAll(item =>
            item.status == Status);
        TaskList = temp == null ? new() : new(temp!);
    }

    /// <summary>
    /// Add Task to the list of tasks
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnAddTask_Click(object sender, RoutedEventArgs e)
    {
        new TaskWindow().ShowDialog();
        var temp = s_bl?.TaskInList.ReadAll();
        TaskList = temp == null ? new() : new(temp!);
    }

    /// <summary>
    /// Updates the task
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LvUpdateTask_DoubleClick(object sender, RoutedEventArgs e)
    {
        BO.TaskInList? Task = (sender as ListView)?.SelectedItem as BO.TaskInList;
        if (Task != null)
            new TaskWindow(Task.ID).ShowDialog();
        var temp = s_bl?.TaskInList.ReadAll();
        TaskList = temp == null ? new() : new(temp!);
    }
}
