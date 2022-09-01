using MauiApp2.Models;

namespace MauiApp2.Views.Pages;

public partial class EditDeviceDataPage : ContentPage
{
	public EditDeviceDataPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Метод вызываемый при перерисовке окна
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            pickerDevice.ItemsSource = App.DeviceBase.GetItems().ToList();
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "Ok");
        }
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
            //var device = (DataDevice)BindingContext;
            var device = new DataDevice
            {
                DeviceId = ((DeviceBase)pickerDevice.SelectedItem).Id,
                Date = DateTime.Now,
                Value = int.Parse(entryValue.Text)
            };
            if (!String.IsNullOrEmpty(device.Value.ToString()) &&
                !String.IsNullOrEmpty(device.DeviceId.ToString()))
            {
                App.DeviceData.SaveItem(device);
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