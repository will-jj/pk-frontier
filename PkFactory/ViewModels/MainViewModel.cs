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
    private string _greeting = "Create Emerald Factory Ready Save";

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

    [ObservableProperty]
    private bool _includeTeam;

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

        if (IncludeTeam)
        {
            SetTeam();
        }
        

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

    private void SetTeam()
    {
        if(_saveFile is null) return;

        int partyCount = 0;
        foreach (string member in Constants.Sets.AdededeTowerSingles50)
        {
            ShowdownSet set = new ShowdownSet(member);
            
            // can adapt to gen in future based on save
            PKM pkm = new PK3();
            pkm.ApplySetDetails(set);
            if (string.IsNullOrEmpty(set.Nickname))
            {
                pkm.Nickname = "TEST";
            }
            
            // Still show as traded but at least give the name
            pkm.OriginalTrainerName = Name;
            
            // TODO: If loading saves check where to put them properly
            _saveFile.SetBoxSlotAtIndex(pkm, 0, partyCount);
            _saveFile.SetPartySlotAtIndex(pkm, partyCount);
            partyCount++;
        }
    }
}