using MauiApp2.Controllers;

namespace MauiApp2;

public partial class App : Application
{
    public const string DATABASE_NAME = "Offlink.db";
    public static DeviceService databaseDevice;
    public static DeviceDataService databaseDeviceData;
    public static DataServerService dataServerService;

    public static DeviceService DeviceBase
    {
        get
        {
            if (databaseDevice == null)
            {
                databaseDevice = new DeviceService(
                    Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), 
                            DATABASE_NAME));
            }
            return databaseDevice;
        }
    }

    public static DeviceDataService DeviceData
    {
        get
        {
            if (databaseDeviceData == null)
            {
                databaseDeviceData = new DeviceDataService(
                    Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                            DATABASE_NAME));
            }
            return databaseDeviceData;
        }
    }

    public static DataServerService DataServer
    {
        get
        {
            if (dataServerService == null)
            {
                dataServerService = new DataServerService(
                    Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                            DATABASE_NAME));
            }
            return dataServerService;
        }
    }

    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
