using MauiApp2.Models;

namespace MauiApp2.Views.Pages;

public partial class EditDevicePage : ContentPage
{
	public EditDevicePage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Метод вызываемый при перерисовке окна
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    /// <summary>
    /// Сохранение данных приборов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveData(object sender, EventArgs e)
    {
        try
        {
            //var device = (DeviceBase)BindingContext;
            var device = new DeviceBase { Id = entryNameDevice.Text };
            if (!String.IsNullOrEmpty(device.Id))
            {
                App.DeviceBase.SaveItem(device);
            }
            this.Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "Ok");
        }
    }

    private void Cancel(object sender, EventArgs e)
    {
        try
        {
            this.Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "Ok");
        }
    }
}