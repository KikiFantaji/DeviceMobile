using MauiApp2.Models;
using MauiApp2.Query;
using MySqlConnector;

namespace MauiApp2.Views.Pages;

public partial class DeviceDataPage : ContentPage
{
    public DeviceDataPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// ����� ���������� ��� ����������� ����
    /// </summary>
    protected override void OnAppearing()
    {
        try
        {
            // ������������ ��������� ������ ��� ������ ��������
            DeviceDataList.ItemsSource =
                App.DeviceData.GetItems().Where(d => d.DeviceId == ((DeviceBase)BindingContext).Id);
        }
        catch (Exception ex)
        {
            DisplayAlert("������", ex.Message, "Ok");
        }

        base.OnAppearing();
    }

    /// <summary>
    /// ��������� ������� ������ ����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void CreateData(object sender, EventArgs e)
    {
        try
        {
            var devicePage = new DeviceDataPage();
            devicePage.BindingContext = (DeviceBase)BindingContext;
            await Navigation.PushAsync(devicePage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("������", ex.Message, "Ok");
        }
    }

    /// <summary>
    /// �������� ������ ������������ � ���� ������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UploadData(object sender, EventArgs e)
    {
        try
        {
            Data.MysqlCon.Close();
            Data.MysqlCon.Open();
            DisplayAlert("", $"������� ��������� ������ - {UploadDataDevice()}", "Ok");
        }
        catch
        {
            DisplayAlert("������", $"�� ������� ��������� ������", "Ok");
        }
        finally
        {
            Data.MysqlCon.Close();
        }
    }

    /// <summary>
    /// �������� ������ �������� � ������� 
    /// </summary>
    /// <returns>���������� ����������� �������</returns>
    private int UploadDataDevice()
    {
        try
        {
            // ������������ ��������� ������ ��� ������ ��������
            var deviceData =
                App.DeviceData.GetItems().Where(d => d.DeviceId == ((DeviceBase)BindingContext).Id);
            var countAddData = 0; // �������� ��������� � ������������

            foreach (var device in deviceData)
            {
                using (var cmd = new MySqlCommand(InsertQuery.InsertDeviceData(device), Data.MysqlCon))
                {
                    cmd.ExecuteNonQuery();
                    countAddData++;
                }
            }

            App.DeviceData.DeleteItems();

            return countAddData;
        }
        catch (Exception ex)
        {
            DisplayAlert("������", ex.Message, "Ok");
        }

        return 0;
    }
}