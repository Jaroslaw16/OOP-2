using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Application main screen.
/// </summary>
public sealed class MainScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Services.
    /// </summary>
    private readonly SettingsScreen _settingsScreen;

    /// <summary>
    /// Animals screen.
    /// </summary>
    private AnimalsScreen _animalsScreen;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    public MainScreen(
        AnimalsScreen animalsScreen,
        SettingsScreen settingsScreen)
    {
        _settingsScreen = settingsScreen;
        _animalsScreen = animalsScreen;
        ScreenDefinitionJson = "MainScreen.json";
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
                        if (selectedLine < 2)
                        {
                            Console.SetCursorPosition(1, selectedLine);
                            selectedLine++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        MainScreenChoices choice = (MainScreenChoices)selectedLine - 1;
                        switch (choice)
                        {
                            case MainScreenChoices.Animals:
                                _animalsScreen.Show();
                                break;

                            case MainScreenChoices.Exit:
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
}