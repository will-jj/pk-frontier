using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PkFactory.Models;

public partial class ObservableTeam : ObservableObject
{
    public ObservableCollection<Set> Sets { get; set; }

    [ObservableProperty]
    public partial string TeamName {get; set;}

    public ObservableTeam(string teamName, IList<Set> sets)
    {
        TeamName = teamName;
        Sets = [];
        foreach (Set member in sets)
        {
            Sets.Add(member);
        }
    }
    

}