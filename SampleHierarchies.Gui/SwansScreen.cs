using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui
{
    /// <summary>
    /// Swan's screen.
    /// </summary>
    public sealed class SwansScreen : Screen
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
        public SwansScreen(IDataService dataService, ISettingsService settingsService)
        {
            ScreenDefinitionJson = "SwanScreen.json";
            _dataService = dataService;
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
                            SwanScreenChoices choice = (SwanScreenChoices)selectedLine - 1;
                            switch (choice)
                            {
                                case SwanScreenChoices.List:
                                    ListSwan();
                                    Console.ReadLine();
                                    break;

                                case SwanScreenChoices.Create:
                                    AddSwan();
                                    Console.ReadLine();
                                    break;

                                case SwanScreenChoices.Delete:
                                    DeleteSwan();
                                    Console.ReadLine();
                                    break;

                                case SwanScreenChoices.Modify:
                                    EditSwanMain();
                                    Console.ReadLine();
                                    break;

                                case SwanScreenChoices.Exit:
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
        /// List all swan's.
        /// </summary>
        private void ListSwan()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.Swans is not null &&
                _dataService.Animals.Mammals.Swans.Count > 0)
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 3);
                int i = 1;
                foreach (Swan swan in _dataService.Animals.Mammals.Swans)
                {
                    ScreenDefinionService.Show(ScreenDefinitionJson, 4);
                    swan.Display();
                    i++;
                }
            }
            else
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 5);
            }
        }

        /// <summary>
        /// Add a swan.
        /// </summary>
        private void AddSwan()
        {
            try
            {
                Swan swan = AddEditSwan();
                _dataService?.Animals?.Mammals?.Swans?.Add(swan);
                ScreenDefinionService.Show(ScreenDefinitionJson, 6);
            }
            catch
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 7);
            }
        }

        /// <summary>
        /// Deletes a swan.
        /// </summary>
        private void DeleteSwan()
        {
            try
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 8);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Swan? swan = (Swan?)(_dataService?.Animals?.Mammals?.Swans
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (swan is not null)
                {
                    _dataService?.Animals?.Mammals?.Swans?.Remove(swan);
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
        /// Edits an existing swan after choice made.
        /// </summary>
        private void EditSwanMain()
        {
            try
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 12);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Swan? swan = (Swan?)(_dataService?.Animals?.Mammals?.Swans?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (swan is not null)
                {
                    Swan swanEdited = AddEditSwan();
                    swan.Copy(swanEdited);
                    ScreenDefinionService.Show(ScreenDefinitionJson, 13);
                    swan.Display();
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
        /// Adds/edit specific swan.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        private Swan AddEditSwan()
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 16);
            string? name = Console.ReadLine();
            ScreenDefinionService.Show(ScreenDefinitionJson, 17);
            string? ageAsString = Console.ReadLine();
            ScreenDefinionService.Show(ScreenDefinitionJson, 18);
            string? color = Console.ReadLine();
            ScreenDefinionService.Show(ScreenDefinitionJson, 19);
            string? wingspanAsString = Console.ReadLine();
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
            if (color is null)
            {
                throw new ArgumentNullException(nameof(color));
            }
            if (wingspanAsString is null)
            {
                throw new ArgumentNullException(nameof(wingspanAsString));
            }
            if (habitat is null)
            {
                throw new ArgumentNullException(nameof(habitat));
            }
            int age = Int32.Parse(ageAsString);
            int wingspan = Int32.Parse(wingspanAsString);
            Swan swan = new Swan(name, age, color, wingspan, habitat);
            return swan;
        }

        #endregion // Private Methods
    }
}
