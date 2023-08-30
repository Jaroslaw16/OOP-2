using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class MammalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Animals screen.
    /// </summary>
    private DogsScreen _dogsScreen;
    private WolfScreen _wolfScreen;
    private SwansScreen _swanScreen;
    private CamelsScreen _camelsScreen;
    private ISettingsService _settingsService;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="dogsScreen">Dogs screen</param>
    public MammalsScreen(DogsScreen dogsScreen, WolfScreen wolfScreen, SwansScreen swanScreen, ISettingsService settingsService, CamelsScreen camelsScreen)
    {
        _settingsService = settingsService;
        _dogsScreen = dogsScreen;
        _wolfScreen = wolfScreen;
        _swanScreen = swanScreen;
        _camelsScreen = camelsScreen;
        ScreenDefinitionJson = "MammalsScreen.json";
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
                        if (selectedLine < 5)
                        {
                            Console.SetCursorPosition(1, selectedLine);
                            selectedLine++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        MammalsScreenChoices choice = (MammalsScreenChoices)selectedLine - 1;
                        switch (choice)
                        {
                            case MammalsScreenChoices.Dogs:
                                _dogsScreen.Show();
                                break;

                            case MammalsScreenChoices.Wolfs:
                                _wolfScreen.Show();
                                break;

                            case MammalsScreenChoices.Swans:
                                _swanScreen.Show();
                                break;

                            case MammalsScreenChoices.Camels:
                                _camelsScreen.Show();
                                break;

                            case MammalsScreenChoices.Exit:
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
