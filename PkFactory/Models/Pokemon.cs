namespace PkFactory.Models;

public record Pokemon()
{
    public required string Showdown { get; init; }
    public uint? PID { get; init; }
    public Tags? Tags { get; init; } 
    
    // TODO add other info as required
    // such as YT/Twitch/Records/Smogon Links
    // will just use these models to gen the static site
    // Some of this might also be better in teams
}