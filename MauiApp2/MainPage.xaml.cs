using MauiApp2.Models;
using MauiApp2.Query;
using MauiApp2.Views.Pages;
using MySqlConnector;

namespace MauiApp2;

public partial class MainPage : ContentPage
{
    public MainPage()
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
            // Установление источника данных для списка счётчиков
            DeviceList.ItemsSource = App.DeviceBase.GetItems();
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "Ok");
        }
    }

    /// <summary>
    /// Переход на страницу добавления данных
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void PushPageEditDataDevice(object sender, EventArgs e)
    {
        try
        {
            var editdeviceDataPage = new EditDeviceDataPage();
            await Navigation.PushAsync(editdeviceDataPage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "Ok");
        }
    }

    /// <summary>
    /// Переход на страницу добавления счётчика
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void AddDevicePage(object sender, EventArgs e)
    {
        try
        {
            var devicePage = new EditDevicePage();
            await Navigation.PushAsync(devicePage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "Ok");
        }
    }

    /// <summary>
    /// Добавление счётчиков в локальную БД
    /// </summary>
    /// <returns>Количество добавленных записей</returns>
    private int AddDeviceToLocalDb()
    {
        try
        {
            var countAddData = 0;
            using (var cmd = new MySqlCommand(SelectQuery.SelectAllDevice, Data.MysqlCon))
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    App.DeviceBase.SaveItem(new DeviceBase
                    {
                        Id = Convert.ToInt32(reader.GetValue(0)),
                        Name = Convert.ToString(reader.GetValue(1))
                    });
                    countAddData++;
                }
            }
            return countAddData;
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "Ok");
        }
        return 0;
    }

    /// <summary>
    /// Синхронизация счётчиков
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SyncDeviceList(object sender, EventArgs e)
    {
        try
        {
            Data.MysqlCon.Close();
            Data.MysqlCon.Open();
            App.DeviceBase.DeleteItems();
            DisplayAlert("", $"Успешно синхронизированно записей - {AddDeviceToLocalDb()}", "Ok");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "Ok");
        }
        finally
        {
            Data.MysqlCon.Close();
        }
    }

    /// <summary>
    /// Обработка нажатия элемента в списке
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var selectedDevice = (DeviceBase)e.SelectedItem;
            var deviceDataPage = new DeviceDataPage();
            deviceDataPage.BindingContext = selectedDevice;
            await Navigation.PushAsync(deviceDataPage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "Ok");
        }
    }

    /// <summary>
    /// Выгрузка данных счётчиков в базу данных
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UploadAllData(object sender, EventArgs e)
    {
        try
        {
            Data.MysqlCon.Close();
            Data.MysqlCon.Open();
            DisplayAlert("", $"Успешно выгружено данных - {UploadAllDataDevice()}", "Ok");
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
    /// Выгрузка данных счётчиков в таблицу 
    /// </summary>
    /// <returns>Количество выгруженных записей</returns>
    private int UploadAllDataDevice()
    {
        try
        {
            var deviceData = App.DeviceData.GetItems(); // Перечисление данных со счётчиков
            var countAddData = 0; // Счётчик затронутых записей

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