using MauiApp2.Models;

namespace MauiApp2.Views.Pages;

public partial class EditDeviceDataPage : ContentPage
{
	public EditDeviceDataPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// ����� ���������� ��� ����������� ����
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            //pickerDevice.ItemsSource = App.DeviceBase.GetItems().ToList();
            labelDivice.Text = BindingContext.ToString();
        }
        catch (Exception ex)
        {
            DisplayAlert("������", ex.Message, "Ok");
        }
    }

    /// <summary>
    /// ���������� ������ ��������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveData(object sender, EventArgs e)
    {
        try
        {
            App.DeviceBase.SaveItem(new DeviceBase { Id = labelDivice.Text });
            //var device = (DataDevice)BindingContext;
            var device = new DataDevice
            {
                DeviceId = labelDivice.Text,
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
            DisplayAlert("������", ex.Message, "Ok");
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
            DisplayAlert("������", ex.Message, "Ok");
        }
    }
}