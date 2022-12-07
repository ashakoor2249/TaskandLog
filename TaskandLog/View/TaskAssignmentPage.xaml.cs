using TaskandLog.ViewModel;
using System;
using TaskandLog.SharedComponents;
using TaskandLog.TableRepositories;
using TaskandLog.Tables;

namespace TaskandLog.View;

public partial class TaskAssignment : ContentPage
{
    readonly Methods methods = new();
    public AssignedTaskRepository AssignedTaskRepo=new();
    public AssignedTask AssignedTask = new();

    public List<AssignedTask> AssignedTasks = new();

    public string UpdateAssingedTask { get; set; }
    public string UpdatePersonaleName { get; set; }
    public string UpdateTaskName { get; set; }

    public string UpdateDay { get; set; }

    public TaskAssignment(TaskAssignmentViewModel taskAssignmentViewModel)
	{
     
        InitializeComponent();
        BindingContext = taskAssignmentViewModel;
    }

    public async void UpdateAssignment_ButtonClicked(object sender, EventArgs e)
    {
        if (selectAssignedTask.SelectedItem !=null)
        {
            AssignedTask Tasks =(AssignedTask)selectAssignedTask.SelectedItem;
            if(Tasks.Assigned_task_id==1)
            {
                return;
            }

            else
            {
                UpdateAssingedTask = await DisplayPromptAsync("Prompt", "Update Assigned Task(Keep spaces in between)", initialValue: Tasks.Assigned_task_sunwatch_personale_name + " " +
                                                             Tasks.Assigned_task_name + " " + Tasks.Assigned_task_day);
            }

            if(UpdateAssingedTask!=null)
            {
                var split = UpdateAssingedTask.Split(' ', 3);
                UpdatePersonaleName = split[0];
                UpdateTaskName = split[1];
                UpdateDay = split[2];
                AssignedTaskRepo.UpdateAssignedTask(Tasks.Assigned_task_id, UpdatePersonaleName, UpdateTaskName, UpdateDay);
                AssignedTasks.Clear();
                AssignedTasks = AssignedTaskRepo.GetAssignedTasks();
                selectAssignedTask.ItemsSource = AssignedTasks;
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }
}