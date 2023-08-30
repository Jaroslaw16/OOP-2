using Newtonsoft.Json;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using Newtonsoft;
using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace SampleHierarchies.Services;

/// <summary>
/// Screen Definion Service.
/// </summary>
/// 
public static class ScreenDefinionService
{
    #region ScreenDefinionService Implementation 

    /// <inheritdoc/>

    /// Used to read json
    public static ScreenDefinition? Read(string jsonPath)
    {
        try
        {
            if (jsonPath is null)
            {
                throw new ArgumentNullException(nameof(jsonPath));
            }
            string jsonSource = File.ReadAllText(jsonPath);
            var jsonContent = System.Text.Json.JsonSerializer.Deserialize<ScreenDefinition>(jsonSource);
            if (jsonContent == null)
            {
                throw new ArgumentNullException(nameof(jsonContent));
            }
            return jsonContent;
        }
        catch
        {
            Console.WriteLine("Data reading was not successful.");
            return null;
        }
    }

    /// <inheritdoc/>
    
    /// Used to create json
    public static void Write(ScreenDefinition settings, string jsonPath)
    {
        try
        {
            if (jsonPath is null) throw new ArgumentNullException(nameof(jsonPath));
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(settings));
            Console.WriteLine("Data saving to: '{0}' was successful.", jsonPath);
        }
        catch
        {
            Console.WriteLine("Data saving was not successful.");
        }
    }
    // Used to Show text and load settings from json
    public static void Show(string jsonPath, int lineId)
    {
        try
        {
            if (Console.IsOutputRedirected != true) Console.Clear(); // if StringWriter is enabled in unit tests
            ScreenDefinition screenDefinition = Read(jsonPath);
            if (screenDefinition == null) { throw new ArgumentNullException(nameof(screenDefinition)); }
            if (lineId > screenDefinition.LineEntries.Count) { throw new OverflowException(nameof(lineId));}
            Console.BackgroundColor = screenDefinition.LineEntries[lineId].BackgroundColor;
            Console.ForegroundColor = screenDefinition.LineEntries[lineId].ForegroundColor;
            Console.WriteLine(screenDefinition.LineEntries[lineId].Text);
            Console.ResetColor();
        }
        catch
        {
            Console.ResetColor();
            Console.WriteLine("Data reading from json was not successful.");
            
        }
    }

    #endregion // ScreenDefinionService Implementation
}