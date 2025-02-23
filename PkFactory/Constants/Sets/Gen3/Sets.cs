using PkFactory.Models;

namespace PkFactory.Constants.Sets.Gen3;

public static class Sets3
{
    // Adedede's team
    // 
    public static readonly Pokemon SkarmoryAdedede = new()
    {
        Showdown = 
            """
            Skarmory (M) @ Chesto Berry  
            Ability: Sturdy  
            Level: 50  
            Shiny: Yes  
            EVs: 252 HP / 140 Def / 116 SpD  
            Bold Nature  
            IVs: 0 Atk  
            - Protect  
            - Rest  
            - Whirlwind  
            - Torment  
            """
    };

    public static readonly Pokemon BlisseyAdedede = new()
    {
        Showdown = 
            """
            Blissey @ Leftovers  
            Ability: Natural Cure  
            Level: 50  
            Shiny: Yes  
            EVs: 52 HP / 252 Def / 28 SpD / 172 Spe  
            Timid Nature  
            IVs: 0 Atk  
            - Protect  
            - Substitute  
            - Soft-Boiled  
            - Growl  
            """
    };


    public static readonly Pokemon LatiosAdedede = new()
    {
        Showdown = 
            """
            Latios @ Lum Berry  
            Ability: Levitate  
            Level: 50  
            Shiny: Yes  
            EVs: 172 HP / 108 Def / 4 SpA / 4 SpD / 220 Spe  
            Timid Nature  
            IVs: 0 Atk  
            - Substitute  
            - Calm Mind  
            - Recover  
            - Dragon Claw  
            """
    };

    public static readonly Team AdededeTowerSingles50 = new()
    {
        OT = "Adedede",
        Name = "Adedede",
        Tags = Tags.Tower | Tags.Singles,
        Members = [SkarmoryAdedede, BlisseyAdedede, LatiosAdedede],
        OriginalSource = "https://www.smogon.com/forums/threads/gen-iii-battle-frontier-discussion-and-records.3648697/"
    };
    
}