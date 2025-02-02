using System.Collections.Generic;

namespace PkFactory.Constants.Sets.Gen4;

public static class Sets4
{
    public const string CresseliaItoI6 =
        """
        Cresselia @ Leftovers
        Ability: Levitate
        Level: 50
        EVs: 248 HP / 252 Def / 4 SpD / 4 Spe
        Bold Nature
        IVs: 30 HP / 0 Atk / 30 SpA
        - Psychic
        - Thunder Wave
        - Flash
        - Moonlight
        """;

    public const string ArticunoItol6 =
        """
        Articuno @ Bright Powder
        Ability: Pressure
        Level: 50
        EVs: 252 HP / 4 SpD / 252 Spe
        Timid Nature
        IVs: 0 Atk / 30 Def / 30 SpA
        - Sheer Cold
        - Mind Reader
        - Substitute
        - Roost
        """;

    public const string GarchompItol6 =
        """
        Garchomp (M) @ Choice Scarf
        Ability: Sand Veil
        Level: 50
        EVs: 4 HP / 252 Atk / 252 Spe
        Jolly Nature
        IVs: 12 SpA
        - Outrage
        - Earthquake
        - Swords Dance
        - Fire Fang
        """;

    public const string GarchompTRE =
        """
        Garchomp
        Ability: Sand Veil
        Nature: Jolly
        EVs: 6 HP / 252 Atk / 252 Spd
        -Earthquake
        -Outrage
        -Swords Dance
        -Stone Edge
        """;

    public const string BlisseyTRE =
        """
        Blissey
        Ability: Natural Cure
        Nature: Bold
        EVs: 252 HP / 252 Def / 6 SpD
        -Seismic Toss
        -Counter
        -Aromatherapy
        -Soft-Boiled
        """;

    public const string GengarTRE =
        """
        Gengar
        Ability: Levitate
        Nature: Timid
        EVs: 6 HP / 252 SpA / 252 Spd
        -Thunderbolt
        -Counter
        -Shadow Ball
        -Destiny Bond
        """;

    public const string StarmieSquilliams =
        """
        Starmie
        Ability: Natural Cure
        Level: 50
        EVs: 252 SpA / 4 SpD / 252 Spe
        Timid Nature
        IVs: 0 Atk
        - Surf
        - Psychic
        - Thunderbolt
        - Ice Beam
        """;

    public const string ScizorSquilliams =
        """
        Scizor (M)
        Ability: Technician
        Level: 50
        EVs: 92 HP / 252 Atk / 164 Spe
        Adamant Nature
        - Bullet Punch
        - Bug Bite
        - Swords Dance
        - Roost
        """;

    public const string GarchompSquilliams =
        """
        Garchomp (F)
        Ability: Sand Veil
        Level: 50
        EVs: 12 HP / 252 Atk / 244 Spe
        Jolly Nature
        - Earthquake
        - Substitute
        - Outrage
        - Swords Dance 
        """;

    public const string UxieMagpie =
        """
        Uxie  
        Ability: Levitate  
        Level: 90  
        EVs: 252 HP / 4 Def / 20 SpD / 228 Spe  
        Timid Nature  
        IVs: 0 Atk  
        - Thunder Wave  
        - Memento  
        - Yawn  
        - Skill Swap
        """;

    public const string DrapionMagpie =
        """
        Drapion (F)  
        Ability: Battle Armor  
        Level: 50  
        EVs: 220 HP / 4 Atk / 36 Def / 244 SpD / 4 Spe  
        Careful Nature  
        - Substitute  
        - Crunch  
        - Acupressure  
        - Rest
        """;

    public const string SalamenceMagpie =
        """
        Salamence (M)  
        Ability: Intimidate  
        Level: 93  
        EVs: 212 HP / 252 Atk / 12 Def / 28 SpD / 4 Spe  
        Adamant Nature  
        - Dragon Dance  
        - Substitute  
        - Roost  
        - Dragon Claw
        """;
    
    // TODO: come up with a nice way of storing & organising this data
    public static readonly string[] Itol6Team = [CresseliaItoI6, ArticunoItol6, GarchompItol6];
    public static readonly string[] TREArcardeSingles = [GarchompTRE, BlisseyTRE, GengarTRE];
    public static readonly string[] SquilliamsArcadeSingles = [StarmieSquilliams, ScizorSquilliams, GarchompSquilliams];
    public static readonly string[] MagpieArcadeSingles220 = [UxieMagpie, DrapionMagpie, SalamenceMagpie];

    public static readonly List<string[]> AllSets =
    [
        Itol6Team,
        TREArcardeSingles,
        SquilliamsArcadeSingles,
        MagpieArcadeSingles220
    ];
}