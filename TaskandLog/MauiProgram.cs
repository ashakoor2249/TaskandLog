﻿using TaskandLog.View;
using TaskandLog.ViewModel;

namespace TaskandLog;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("ArialBlack.ttf", "ArialBlack");
				fonts.AddFont("TimesNewRoman.otf", "Times");
			});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddSingleton<DatabasePage>();
		builder.Services.AddSingleton<DatabasePageViewModel>();
		builder.Services.AddSingleton<TaskAssignment>();
		builder.Services.AddSingleton<TaskAssignmentViewModel>();
		return builder.Build();
	}
}
