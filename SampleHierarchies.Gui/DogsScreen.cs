using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class DogsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    public DogsScreen(IDataService dataService, ISettingsService settingsService)
    {
        _dataService = dataService;
        ScreenDefinitionJson = "DogsScreen.json";
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
                        DogsScreenChoices choice = (DogsScreenChoices)selectedLine - 1;
                        switch (choice)
                        {
                            case DogsScreenChoices.List:
                                ListDogs();
                                Console.ReadLine();
                                break;

                            case DogsScreenChoices.Create:
                                AddDog();
                                Console.ReadLine();
                                break;

                            case DogsScreenChoices.Delete:
                                DeleteDog();
                                Console.ReadLine();
                                break;

                            case DogsScreenChoices.Modify:
                                EditDogMain();
                                Console.ReadLine();
                                break;

                            case DogsScreenChoices.Exit:
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
    /// List all dogs.
    /// </summary>
    private void ListDogs()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Dogs is not null &&
            _dataService.Animals.Mammals.Dogs.Count > 0)
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 3);
            int i = 1;
            foreach (Dog dog in _dataService.Animals.Mammals.Dogs)
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 4);
                dog.Display();
                i++;
            }
        }
        else
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 5);
        }
    }

    /// <summary>
    /// Add a dog.
    /// </summary>
    private void AddDog()
    {
        try
        {
            Dog dog = AddEditDog();
            _dataService?.Animals?.Mammals?.Dogs?.Add(dog);
            ScreenDefinionService.Show(ScreenDefinitionJson, 6);
        }
        catch
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 7);
        }
    }

    /// <summary>
    /// Deletes a dog.
    /// </summary>
    private void DeleteDog()
    {
        try
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 8);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                _dataService?.Animals?.Mammals?.Dogs?.Remove(dog);
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
    /// Edits an existing dog after choice made.
    /// </summary>
    private void EditDogMain()
    {
        try
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 12);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                Dog dogEdited = AddEditDog();
                dog.Copy(dogEdited);
                ScreenDefinionService.Show(ScreenDefinitionJson, 13);
                dog.Display();
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
    /// Adds/edit specific dog.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Dog AddEditDog()
    {
        ScreenDefinionService.Show(ScreenDefinitionJson, 16);
        string? name = Console.ReadLine();
        ScreenDefinionService.Show(ScreenDefinitionJson, 17);
        string? ageAsString = Console.ReadLine();
        ScreenDefinionService.Show(ScreenDefinitionJson, 18);
        string? breed = Console.ReadLine();

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (breed is null)
        {
            throw new ArgumentNullException(nameof(breed));
        }
        int age = Int32.Parse(ageAsString);
        Dog dog = new Dog(name, age, breed);

        return dog;
    }

    #endregion // Private Methods
}
