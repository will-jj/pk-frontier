using System.Collections.Generic;
using PkFactory.Models;

namespace PkFactory.Constants.Sets.Gen4;

public static class Sets4
{
public static readonly Pokemon CresseliaItoI6 = new()
{
    Showdown = 
        """
        Cresselia @ Leftovers
        Ability: Levitate
        Level: 50
        EVs: 248 HP / 252 Def / 4 SpD / 4 Spe
        Bold Nature
        IVs: 30 HP / 1 Atk / 30 SpA
        - Psychic
        - Thunder Wave
        - Flash
        - Moonlight
        """,
    PID = 0x110D3881
};

public static readonly Pokemon ArticunoItol6 = new()
{
    Showdown = 
        """
        Articuno @ Bright Powder
        Ability: Pressure
        Level: 50
        EVs: 252 HP / 4 SpD / 252 Spe
        Timid Nature
        IVs: 2 Atk / 30 SpA
        - Sheer Cold
        - Mind Reader
        - Substitute
        - Roost
        """,
    PID = 0x4F0ACF77
};

public static readonly Pokemon GarchompItol6 = new()
{
    Showdown = 
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
        """
};

public static readonly Pokemon GarchompTRE = new()
{
    Showdown = 
        """
        Garchomp
        Ability: Sand Veil
        Nature: Jolly
        EVs: 6 HP / 252 Atk / 252 Spd
        - Earthquake
        - Outrage
        - Swords Dance
        - Stone Edge
        """
};

public static readonly Pokemon BlisseyTRE = new()
{
    Showdown = 
        """
        Blissey
        Ability: Natural Cure
        Nature: Bold
        EVs: 252 HP / 252 Def / 6 SpD
        - Seismic Toss
        - Counter
        - Aromatherapy
        - Soft-Boiled
        """
};

public static readonly Pokemon GengarTRE = new()
{
    Showdown = 
        """
        Gengar
        Ability: Levitate
        Nature: Timid
        EVs: 6 HP / 252 SpA / 252 Spd
        - Thunderbolt
        - Counter
        - Shadow Ball
        - Destiny Bond
        """
};

public static readonly Pokemon StarmieSquilliams = new()
{
    Showdown = 
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
        """
};

public static readonly Pokemon ScizorSquilliams = new()
{
    Showdown = 
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
        """
};

public static readonly Pokemon GarchompSquilliams = new()
{
    Showdown = 
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
        """
};

public static readonly Pokemon UxieMagpie = new()
{
    Showdown = 
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
        """,
    PID = 0x8CBCA403
};

public static readonly Pokemon DrapionMagpie = new()
{
    Showdown = 
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
        """
};

public static readonly Pokemon SalamenceMagpie = new()
{
    Showdown = 
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
        """
};
public static readonly Team Itol6Team = new Team()
{
    OT = "Itol6",
    Tags = Tags.Arcade,
    Members = [CresseliaItoI6, ArticunoItol6, GarchompItol6],
    OriginalSource = null
};

public static readonly Team TREArcardeSingles = new Team()
{
    OT = "TRE",
    Tags = Tags.Arcade | Tags.Singles,
    Members = [GarchompTRE, BlisseyTRE, GengarTRE],
    OriginalSource = null
};

public static readonly Team SquilliamsArcadeSingles = new Team()
{
    OT = "Squilliams",
    Tags = Tags.Arcade | Tags.Singles,
    Members = [StarmieSquilliams, ScizorSquilliams, GarchompSquilliams],
    OriginalSource = null
};

public static readonly Team MagpieArcadeSingles220 = new Team()
{
    OT = "Magpie",
    Tags = Tags.Arcade | Tags.Singles,
    Members = [UxieMagpie, DrapionMagpie, SalamenceMagpie],
    OriginalSource = null
};

public static readonly List<Team> AllSets =
[
    Itol6Team,
    TREArcardeSingles,
    SquilliamsArcadeSingles,
    MagpieArcadeSingles220
];
}