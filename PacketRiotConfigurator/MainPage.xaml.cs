﻿namespace PacketRiotConfigurator;

public partial class MainPage : ContentPage
{
	private readonly MainViewModel _viewModel;

	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
        BindingContext = _viewModel;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await Task.Delay(600);
		await _viewModel.Initialise();
	}
}
