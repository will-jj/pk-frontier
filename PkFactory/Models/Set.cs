using CommunityToolkit.Mvvm.ComponentModel;
using PkFactory.ViewModels;

namespace PkFactory.Models;

public partial class Set : ViewModelBase
{
    [ObservableProperty]
    public partial string ShowdownText { get; set; } = string.Empty;
    
    [ObservableProperty]
    public partial bool IsNotValid { get; set; }
    
    [ObservableProperty]
    public partial string Errors { get; set; } = string.Empty;
    
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