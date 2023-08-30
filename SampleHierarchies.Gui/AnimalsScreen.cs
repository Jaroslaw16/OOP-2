using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Animals main screen.
/// </summary>
public sealed class AnimalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;

    /// <summary>
    /// Animals screen.
    /// </summary>
    private MammalsScreen _mammalsScreen;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    public AnimalsScreen(
        IDataService dataService,
        MammalsScreen mammalsScreen,
        ISettingsService settingsService)
    {
        _dataService = dataService;
        _mammalsScreen = mammalsScreen;
        ScreenDefinitionJson = "AnimalsScreen.json";
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        try
        {
            while (true)
            {
                Console.Clear();
                ScreenDefinionService.Show(ScreenDefinitionJson, 0);
                Console.SetCursorPosition(0, selectedLine);
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    default:
                        break;
                    case ConsoleKey.UpArrow:
                        if (selectedLine > 1)
                        {
                            Console.SetCursorPosition(1, selectedLine);
                            selectedLine--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedLine < 4)
                        {
                            Console.SetCursorPosition(1, selectedLine);
                            selectedLine++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        AnimalsScreenChoices choice = (AnimalsScreenChoices)selectedLine-1;
                        switch (choice)
                        {
                            case AnimalsScreenChoices.Mammals:
                                _mammalsScreen.Show();
                                break;

                            case AnimalsScreenChoices.Read:
                                ReadFromFile();
                                Console.ReadLine();
                                break;

                            case AnimalsScreenChoices.Save:
                                SaveToFile();
                                Console.ReadLine();
                                break;

                            case AnimalsScreenChoices.Exit:
                                ScreenDefinionService.Show(ScreenDefinitionJson, 1);
                                return;
                        }
                        break;
                }
            }
        }
        catch
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 2);
        }
    }

    #endregion // Public Methods

    #region Private Methods

    /// <summary>
    /// Save to file.
    /// </summary>
    private void SaveToFile()
    {
        try
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 3);
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            ScreenDefinionService.Show(ScreenDefinitionJson, 4);
        }
        catch
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 5);
        }
    }

    /// <summary>
    /// Read data from file.
    /// </summary>
    private void ReadFromFile()
    {
        try
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 6);
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            ScreenDefinionService.Show(ScreenDefinitionJson, 7);
        }
        catch
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 8);
        }
    }

    #endregion // Private Methods
}
