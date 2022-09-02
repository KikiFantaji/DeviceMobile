namespace MauiApp2.Views.Pages;

public partial class DeviceListPage : ContentPage
{
	public DeviceListPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		try
		{
			DeviceList.ItemsSource = App.DeviceBase.GetItems();
		}
		catch (Exception ex)
		{
			DisplayAlert("������", ex.Message, "Ok");
		}
	}
}