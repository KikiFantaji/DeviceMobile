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
    /// Метод вызываемый при перерисовке окна
    /// </summary>
    protected override void OnAppearing()
    {
        try
        {
            // Установление источника данных для списка счётчика
            DeviceDataList.ItemsSource =
                App.DeviceData.GetItems().Where(d => d.DeviceId == ((DeviceBase)BindingContext).Id);
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "Ok");
        }

        base.OnAppearing();
    }

    /// <summary>
    /// Обработка нажатия кнопки добавления
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
            await DisplayAlert("Ошибка", ex.Message, "Ok");
        }
    }

    /// <summary>
    /// Выгрузка данныз уборудования в базу данных
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UploadData(object sender, EventArgs e)
    {
        try
        {
            Data.MysqlCon.Close();
            Data.MysqlCon.Open();
            DisplayAlert("", $"Успешно выгружено данных - {UploadDataDevice()}", "Ok");
        }
        catch
        {
            DisplayAlert("Ошибка", $"Не удалось выгрузить данные", "Ok");
        }
        finally
        {
            Data.MysqlCon.Close();
        }
    }

    /// <summary>
    /// Выгрузка данных приборов в таблицу 
    /// </summary>
    /// <returns>Количество выгруженных записей</returns>
    private int UploadDataDevice()
    {
        try
        {
            // Установление источника данных для списка счётчика
            var deviceData =
                App.DeviceData.GetItems().Where(d => d.DeviceId == ((DeviceBase)BindingContext).Id);
            var countAddData = 0; // Счёткчик элементов в перечислении

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
            DisplayAlert("Ошибка", ex.Message, "Ok");
        }

        return 0;
    }
}