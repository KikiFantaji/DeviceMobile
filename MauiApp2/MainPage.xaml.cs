using MauiApp2.Models;
using MauiApp2.Query;
using MauiApp2.Views.Pages;
using MySqlConnector;
using ZXing.Net.Maui;

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
            barcodeReader.IsDetecting = true;
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "Ok");
        }
    }

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        try
        {
            Dispatcher.Dispatch(async () =>
            {
                barcodeReader.IsDetecting = false;
                //barcodeResult.Text = $"{e.Results[0].Value} {e.Results[0].Format}";
                var editDeviceDataPage = new EditDeviceDataPage();
                var deviceDesc = $"{e.Results[0].Value} {e.Results[0].Format}";
                var idDevice = deviceDesc.Split(';');
                editDeviceDataPage.BindingContext = idDevice[0].Substring(3);
                await Navigation.PushAsync(editDeviceDataPage);
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "Ok");
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
                        Id = Convert.ToString(reader.GetValue(0))
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
            UploadAllDevice();
            //DisplayAlert("", $"Успешно выгружено счётчиков - {UploadAllDevice()}", "Ok");
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
    /// Выгрузка счётчиков в таблицу 
    /// </summary>
    /// <returns>Количество выгруженных записей</returns>
    private int UploadAllDevice()
    {
        try
        {
            var devicesMysql = new List<DeviceBase>();
            using (var cmd = new MySqlCommand(SelectQuery.SelectAllDevice, Data.MysqlCon))
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    devicesMysql.Add(new DeviceBase
                    {
                        Id = Convert.ToString(reader.GetValue(0))
                    });
                }
            }

            Data.MysqlCon.Close();
            Data.MysqlCon.Open();

            var devicesSqlite = App.DeviceBase.GetItems();
            var countAddDevice = 0; // Счётчик затронутых записей

            foreach (var device in devicesSqlite)
            {
                if (devicesMysql.FirstOrDefault(d => d.Id == device.Id) == null)
                {
                    using (var cmd = new MySqlCommand(InsertQuery.InsertDevice(device), Data.MysqlCon))
                    {
                        cmd.ExecuteNonQuery();
                        countAddDevice++;
                    }
                }
            }

            return countAddDevice;
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "Ok");
        }

        return 0;
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
                using (var cmd = 
                    new MySqlCommand(InsertQuery.InsertDeviceData(device), Data.MysqlCon))
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