using MauiApp2.Models;

namespace MauiApp2.Views.Pages;

public partial class EditServerDataPage : ContentPage
{
	public EditServerDataPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// ����� ���������� ��� ����������� ����
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //try
        //{
        //    // TODO: �������� ������� ������
        var serverData = App.DataServer.GetItems().FirstOrDefault();
        if (serverData != null)
        {
            entryServer.Text = serverData.Server;
            entryDatabase.Text = serverData.DataBase;
            entryPassword.Text = serverData.Password;
            entryPort.Text = serverData.Port;
            entryUser.Text = serverData.User;
        }
        //}
        //catch (Exception ex)
        //{ 
        //    DisplayAlert("������", ex.Message, "Ok");
        //}
    }

    /// <summary>
    /// ���������� ������ �������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveData(object sender, EventArgs e)
    {
        try
        {
            var serverData = new DataServer()
            {
                DataBase = entryDatabase.Text,
                Password = entryPassword.Text,
                Port = entryPort.Text,
                Server = entryServer.Text,
                User = entryUser.Text
            };
            App.DataServer.DeleteItems();
            App.DataServer.SaveItem(serverData);
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