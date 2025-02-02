using CommunityToolkit.Mvvm.ComponentModel;
using PkFactory.ViewModels;

namespace PkFactory.Models;

public partial class Set : ViewModelBase
{
    [ObservableProperty]
    string _showdownText = string.Empty;

    [ObservableProperty]
    private bool _isNotValid;
    
    [ObservableProperty]
    string _errors = string.Empty;

    public Set()
    {
    }

    public Set(string showdownText)
    {
        ShowdownText = showdownText;
        IsNotValid = false;
    }
}