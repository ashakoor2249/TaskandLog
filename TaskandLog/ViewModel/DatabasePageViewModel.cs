using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskandLog.TableRepositories;

namespace TaskandLog.ViewModel;

public partial class DatabasePageViewModel: ObservableObject
{
    public LogTypeRepository LogTypeRepo = new();
    [ObservableProperty] public string newType;
    [ObservableProperty] public string removeType;
    [ObservableProperty] public string newTypeStatusMessage;
    [ObservableProperty] public string removeTypeStatusMessage;

    public DatabasePageViewModel() 
    { 

    }

    [RelayCommand]
    async void AddNewType()
    {

    }

    [RelayCommand]
    async void DeleteType()
    {

    }

    [RelayCommand]
    async void GetAllTypes()
    {

    }
}

