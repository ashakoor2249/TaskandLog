using TaskandLog.ViewModel;
using System;


namespace TaskandLog.View;

public partial class TaskAssignment : ContentPage
{

    public TaskAssignment(TaskAssignmentViewModel taskAssignmentViewModel)
	{
     
        InitializeComponent();
        BindingContext = taskAssignmentViewModel;
    }

  
}