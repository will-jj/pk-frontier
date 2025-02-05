using System.Collections.Generic;

namespace PkFactory.Models;

public record Team()
{
    public required string OT;
    public string? Record;
    public string? RecordURL;
    /// <summary>
    /// Original site where it was posted
    /// </summary>
    public string? OriginalSource;
    public Tags Tags;
    public required List<Pokemon> Members;
}