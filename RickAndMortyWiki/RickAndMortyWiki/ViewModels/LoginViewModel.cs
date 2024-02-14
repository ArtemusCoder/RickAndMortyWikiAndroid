using System;
using System.IO;
using System.Net.Http;
using System.Reactive;
using ReactiveUI;
using RickAndMortyWiki.Models;
using System.Text.Json;
using Avalonia.Platform;

namespace RickAndMortyWiki.ViewModels;

public class LoginViewModel : ReactiveObject, IScreen, IRoutableViewModel
{
    public IScreen? HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    public RoutingState Router { get; } = new RoutingState();
    public ReactiveCommand<Unit, Unit> RegisterCommand { get; }
    public ReactiveCommand<Unit, Unit> LoginCommand { get; }
    public SettingsModel? Settings { get; set; }

    private string username;
    private string password;

    public string Username
    {
        get => username;
        set => this.RaiseAndSetIfChanged(ref username, value);
    }

    public string Password
    {
        get => password;
        set => this.RaiseAndSetIfChanged(ref password, value);
    }


    [Obsolete("Obsolete")]
    public LoginViewModel(IScreen? screen)
    {
        HostScreen = screen;
        LoginCommand = ReactiveCommand.Create(Login);
        RegisterCommand = ReactiveCommand.Create(Register);
        // if (Settings.LoggedIn) ReactiveCommand.Create<string>(PerformAction);
    }

    [Obsolete("Obsolete")]
    private void Register()
    {
        var newUser = new User
        {
            username = Username,
            password = Password
        };
        
        Router.Navigate.Execute(new NavigationViewModel(this));
    }

    [Obsolete("Obsolete")]
    private void Login()
    {
        string url = $"https://10.0.2.2:8000/login?username={Username}&password={Password}";
        var httpClient = new HttpClient();
        var response = httpClient.GetStringAsync(url).Result;
        var apiResponse = JsonSerializer.Deserialize<UserResponse>(response);
        Console.WriteLine(apiResponse.message);
        if (apiResponse.message == "Login successful")
        {
            Router.Navigate.Execute(new NavigationViewModel(this));
        }
        else
        {
            
        }
        Router.Navigate.Execute(new NavigationViewModel(this));
    }
}