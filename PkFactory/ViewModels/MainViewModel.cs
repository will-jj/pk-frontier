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
using PkFactory.Constants.Sets.Gen3;
using PkFactory.Services;
using PKHeX.Core;
using PkFactory.Models;
using PKHeX.Core.AutoMod;

namespace PkFactory.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveFileCommand))]
    private bool _canSaveFile;

    private string? _filename = "emerald-factory.sav";

    [ObservableProperty]
    private string _greeting = "Create Frontier Ready Save";

    [ObservableProperty]
    private string _message;

    private string _name = string.Empty;

    private SaveFile? _saveFile;

    [ObservableProperty]
    private string _selectedGender;

    [ObservableProperty]
    private string? _selectedGame;

    public MainViewModel()
    {
        SelectedGender = GenderSelects[0];
        APILegality.SetAllLegalRibbons = false;
        if(OperatingSystem.IsBrowser())
        {
            Message = "Auto legality corrections are currently not applied in the browser, all default Pokemon are however legal, any changes made may not be.";
        }
    }

    [Required]
    [MinLength(1)]
    [MaxLength(7)]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value, true);
    }


    public ObservableCollection<string> Games { get; set; } = ["Emerald", "Heart Gold"];
    public ObservableCollection<string> GenderSelects { get; set; } = ["Boy", "Girl"];
    public ObservableCollection<Set> Sets { get; set; } = new()
    {
        new(),
        new(String.Empty),
        new Set(String.Empty),
    };

    [ObservableProperty]
    private string _selectedSet;

    [ObservableProperty]
    private int _selectedSetIndex;

    [ObservableProperty]
    private bool _includeTeam;

    [RelayCommand]
    public async Task GetPreppedFile(string asset)
    {
        Stream datain = AssetLoader.Open(new Uri(asset));
        using MemoryStream memoryStream = new();
        await datain.CopyToAsync(memoryStream);
        byte[] byteArray = memoryStream.ToArray();
        _saveFile = SaveUtil.GetVariantSAV(byteArray);
        if (_saveFile != null) CanSaveFile = true;
    }

    public async Task LoadFileFromDisk(string path)
    {
        Stream datain = File.OpenRead(path);
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
                if (_saveFile.Gender == (byte)PKHeX.Core.Gender.Female)
                {
                    SelectedGender = "Girl";
                }
                else
                {
                    SelectedGender = "Boy";
                }
            }
        }

        if (IncludeTeam)
        {
            // TODO: fix this too
            OnIncludeTeamChanged(IncludeTeam);
            UpdateSets();
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

    private List<PKM> ValidateAndGenerateTeams()
    {
        List<PKM> monsToAdd = new();
        if (_saveFile is null) return monsToAdd;
        
        foreach (Set member in Sets)
        {
            ShowdownSet set = new(member.ShowdownText);
            if (set.InvalidLines.Count > 0 || set.Species == 0)
            {
                member.Errors = string.Join('\n', set.InvalidLines);
                member.IsNotValid = true;
                continue;
            }

            member.IsNotValid = false;

            // can adapt to gen in future based on save
            PKM pkm;
            switch (_saveFile.Generation)
            {
                case 3:
                    pkm = new PK3();
                    break;
                case 4:
                    //pkm = new PK4();
                    pkm = _saveFile.PartyData[0].Clone();
                    break;
                default:
                    return monsToAdd;
            }

            pkm.ApplySetDetails(set);
            if (string.IsNullOrEmpty(set.Nickname))
            {
                pkm.Nickname = SpeciesName.GetSpeciesNameGeneration(pkm.Species,
                    2, //_saveFile.Language,
                    _saveFile.Generation);
            }

            // Still show as traded but at least give the name
            pkm.OriginalTrainerName = Name;

            // TODO: why this isn't working            
            //#if !BROWSER
            if (!OperatingSystem.IsBrowser())
            {
                // Make them legal
                PKM pkmLegal =
                    _saveFile.GetLegalFromTemplate(pkm, set, out LegalizationResult result, out ITracebackHandler _);
                pkmLegal.RestoreIVs(pkm.IVs);
                pkm = pkmLegal;

            }

            if (member.PID is not null)
                pkm.PID = (uint)member.PID;

            LegalityAnalysis la = new(pkm);

            if (!la.Valid)
            {

                string report = la.Report();
                member.Errors = report;
                member.IsNotValid = true;
                // redo the analysis just for ease...
                // allow for now
                //continue;
            }
            monsToAdd.Add(pkm);
        }
        
        return monsToAdd;
        
    }

    private void UpdateSets()
    {
        // TODO: Fix code repetition
        if (_saveFile is null) return;
        _ = ValidateAndGenerateTeams();
    }


    private void SetTeam()
    {
        if (_saveFile is null) return;

        int partyCount = 0;

        int numSets = Sets.Count;


        int emptyBox = -1;
        for (int box = 0; box < _saveFile.BoxCount; box++)
        {
            PKM[] boxData = _saveFile.GetBoxData(box);

            if (boxData.All(slot => slot.Species == 0)) // Check if all slots in the box are empty
            {
                emptyBox = box;
                break;
            }
        }

        if (numSets > _saveFile.GetBoxData(0).Length || emptyBox == -1)
        {
            // Fail for now
            // TODO: fix this, spread across empty boxes, or position
            // in threes, or something else...
            return;
        }

        List<PKM> monsToAdd = ValidateAndGenerateTeams();
        foreach (PKM pkm in monsToAdd)
        {
            // TODO: If loading saves check where to put them properly
            _saveFile.SetBoxSlotAtIndex(pkm, emptyBox, partyCount);
            partyCount++;
        }
    }

    partial void OnSelectedSetChanged(string? oldValue, string newValue)
    {
        if (oldValue is null) return;

        // TODO: make this flexible for any set choice
        if (SelectedSetIndex == 1)
        {
            Sets.Clear();
            foreach (Pokemon mon in Sets3.AdededeTowerSingles50.Members)
            {
                Sets.Add(new(mon.Showdown));
            }
        }
        else
        {
            Sets.Clear();
            for (int ii = 0; ii < 3; ii++)
            {
                Sets.Add(new());
            }
        }

    }

    partial void OnIncludeTeamChanged(bool value)
    {
        if (value)
        {
            Sets.Clear();
            if (_saveFile is null) return;
            if (_saveFile.Generation == 4)
            {
                foreach (Team team in Constants.Sets.Gen4.Sets4.AllSets)
                {
                    foreach (Pokemon member in team.Members)
                    {
                        Sets.Add(new(member.Showdown, member.PID));
                    }
                }
            }

            if (_saveFile.Generation == 3)
            {
                // not do anything for now
            }
        }
        else
        {
            Sets.Clear();
        }

        UpdateSets();
    }

    async partial void OnSelectedGameChanged(string? value)
    {
        try
        {
            if (value is null) return;
            Sets.Clear();
            CanSaveFile = false;
            switch (value)
            {
                case "Emerald":
                    _filename = "pkfrontier-emerald.sav";
                    await GetPreppedFile("avares://PkFactory/Assets/pokeemerald.sav");
                    break;
                case "Heart Gold":
                    _filename = "pkfrontier-hg.sav";
                    await GetPreppedFile("avares://PkFactory/Assets/HeartGold.sav");
                    break;
            }

            // Hack hack hack 
            OnIncludeTeamChanged(IncludeTeam);
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
            // Or just fix this method...
        }
    }
}