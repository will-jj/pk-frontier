using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PkFactory.Services;
using PKHeX.Core;

namespace PkFactory.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveFileCommand))]
    private bool _canSaveFile;

    private string? _filename = "emerald-factory.sav";

    [ObservableProperty]
    private string _greeting = "Create Factory Ready Save";

    private string _name = string.Empty;

    private SaveFile? _saveFile;

    [ObservableProperty]
    private string _selectedGender;

    public MainViewModel()
    {
        SelectedGender = GenderSelects[0];
    }

    [Required]
    [MinLength(1)]
    [MaxLength(7)]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value, true);
    }

    public ObservableCollection<string> GenderSelects { get; set; } = ["Boy", "Girl"];

    [RelayCommand]
    public async Task GetPreppedFile()
    {
        Stream datain = AssetLoader.Open(new Uri("avares://PkFactory/Assets/pokeemerald.sav"));
        using MemoryStream memoryStream = new();
        await datain.CopyToAsync(memoryStream);
        byte[] byteArray = memoryStream.ToArray();
        _saveFile = SaveUtil.GetVariantSAV(byteArray);
        if (_saveFile != null) CanSaveFile = true;
    }

    [RelayCommand]
    public async Task GetFile()
    {
        TopLevel? topLevel = DialogManager.GetTopLevelForContext(this);
        if (topLevel == null) return;

        IReadOnlyList<IStorageFile> openedFile =
            await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions());
        IStorageFile? firstFile = openedFile.FirstOrDefault();
        if (firstFile != null)
        {
            _filename = firstFile.Name;
            Stream datain = await firstFile.OpenReadAsync();
            using MemoryStream memoryStream = new();
            await datain.CopyToAsync(memoryStream);
            byte[] byteArray = memoryStream.ToArray();
            _saveFile = SaveUtil.GetVariantSAV(byteArray);
            if (_saveFile != null)
            {
                CanSaveFile = true;
                Name = _saveFile.OT;
            }
        }
    }

    [RelayCommand(CanExecute = nameof(CanSaveFile))]
    public async Task SaveFile()
    {
        ValidateAllProperties();
        if (HasErrors) return;
        _saveFile!.OT = Name;

        // Will require scene change to update
        // Brendan is option 0
        if (SelectedGender == GenderSelects[0])
            _saveFile.Gender = (byte)Gender.Male;
        else
            _saveFile.Gender = (byte)Gender.Female;

        TopLevel? topLevel = DialogManager.GetTopLevelForContext(this);
        if (topLevel == null) return;
        FilePickerSaveOptions options = new()
        {
            SuggestedFileName = _filename
        };
        IStorageFile? fileOut = await topLevel.StorageProvider.SaveFilePickerAsync(options);
        if (fileOut != null)
        {
            await using Stream stream = await fileOut.OpenWriteAsync();
            await stream.WriteAsync(_saveFile.Write());
        }
    }
}