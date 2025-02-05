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
    
    public uint? PID { get; set; }

    public Set()
    {
    }

    public Set(string showdownText, uint? pID = null)
    {
        ShowdownText = showdownText;
        IsNotValid = false;
        PID = pID;
    }
}