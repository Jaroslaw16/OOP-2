namespace SampleHierarchies.Interfaces.Data;
using Enums;
/// <summary>
/// Settings interface.
/// </summary>
public interface ISettings
{
    #region Interface Members

    /// <summary>
    /// Version of settings.
    /// </summary>

    Dictionary<ScreensEnum, ConsoleColor> ScreensColor { get; set; }

    #endregion // Interface Members
}

