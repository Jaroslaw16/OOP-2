using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Wolf's screen.
/// </summary>
public sealed class WolfScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private readonly IDataService _dataService;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    public WolfScreen(IDataService dataService)
    {
        _dataService = dataService;
        ScreenDefinitionJson = "WolfScreen.json";

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
                        WolfScreenChoices choice = (WolfScreenChoices)selectedLine - 1;
                        switch (choice)
                        {
                            case WolfScreenChoices.List:
                                ListWolf();
                                Console.ReadLine();
                                break;

                            case WolfScreenChoices.Create:
                                AddWolf();
                                Console.ReadLine();
                                break;

                            case WolfScreenChoices.Delete:
                                DeleteWolf();
                                Console.ReadLine();
                                break;

                            case WolfScreenChoices.Modify:
                                EditWolfMain();
                                Console.ReadLine();
                                break;

                            case WolfScreenChoices.Exit:
                                ScreenDefinionService.Show(ScreenDefinitionJson, 1);
                                Console.ReadLine();
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
    /// List all wolf's.
    /// </summary>
    private void ListWolf()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Wolfs is not null &&
            _dataService.Animals.Mammals.Wolfs.Count > 0)
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 3);
            int i = 1;
            foreach (Wolf wolf in _dataService.Animals.Mammals.Wolfs)
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 4);
                wolf.Display();
                i++;
            }
        }
        else
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 5);
        }
    }

    /// <summary>
    /// Add a wolf.
    /// </summary>
    private void AddWolf()
    {
        try
        {
            Wolf wolf = AddEditWolf();
            _dataService?.Animals?.Mammals?.Wolfs?.Add(wolf);
            ScreenDefinionService.Show(ScreenDefinitionJson, 6);
        }
        catch
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 7);
        }
    }

    /// <summary>
    /// Deletes a wolf.
    /// </summary>
    private void DeleteWolf()
    {
        try
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 8);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Wolf? wolf = (Wolf?)(_dataService?.Animals?.Mammals?.Wolfs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (wolf is not null)
            {
                _dataService?.Animals?.Mammals?.Wolfs?.Remove(wolf);
                ScreenDefinionService.Show(ScreenDefinitionJson, 9);
            }
            else
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 10);
            }
        }
        catch
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 11);
        }
    }

    /// <summary>
    /// Edits an existing wolf after choice made.
    /// </summary>
    private void EditWolfMain()
    {
        try
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 12);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Wolf? wolf = (Wolf?)(_dataService?.Animals?.Mammals?.Wolfs?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (wolf is not null)
            {
                Wolf wolfEdited = AddEditWolf();
                wolf.Copy(wolfEdited);
                ScreenDefinionService.Show(ScreenDefinitionJson, 13);
                wolf.Display();
            }
            else
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 14);
            }
        }
        catch
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 15);
        }
    }

    /// <summary>
    /// Adds/edit specific wolf.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Wolf AddEditWolf()
    {
        ScreenDefinionService.Show(ScreenDefinitionJson, 16);
        string? name = Console.ReadLine();
        ScreenDefinionService.Show(ScreenDefinitionJson, 17);
        string? ageAsString = Console.ReadLine();
        ScreenDefinionService.Show(ScreenDefinitionJson, 18);
        string? furColor = Console.ReadLine();
        ScreenDefinionService.Show(ScreenDefinitionJson, 19);
        string? kilogramsAsString = Console.ReadLine();
        ScreenDefinionService.Show(ScreenDefinitionJson, 20);
        string? habitat = Console.ReadLine();

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (furColor is null)
        {
            throw new ArgumentNullException(nameof(furColor));
        }
        if (kilogramsAsString is null)
        {
            throw new ArgumentNullException(nameof(kilogramsAsString));
        }
        if (habitat is null)
        {
            throw new ArgumentNullException(nameof(habitat));
        }
        int age = Int32.Parse(ageAsString);
        int kilgrams = Int32.Parse(kilogramsAsString);
        Wolf wolf = new (name, age, furColor, kilgrams, habitat);
        return wolf;
    }

    #endregion // Private Methods
}
