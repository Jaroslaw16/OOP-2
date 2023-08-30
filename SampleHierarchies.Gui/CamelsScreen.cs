using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui
{
    /// <summary>
    /// Camel's screen.
    /// </summary>
    public sealed class CamelsScreen : Screen
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
        public CamelsScreen(IDataService dataService)
        {
            _dataService = dataService;
            ScreenDefinitionJson = "CamelScreen.json";
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
                            CamelScreenChoices choice = (CamelScreenChoices)selectedLine - 1;
                            switch (choice)
                            {
                                case CamelScreenChoices.List:
                                    ListCamel();
                                    Console.ReadLine();
                                    break;

                                case CamelScreenChoices.Create:
                                    AddCamel();
                                    Console.ReadLine();
                                    break;

                                case CamelScreenChoices.Delete:
                                    DeleteCamel();
                                    Console.ReadLine();
                                    break;

                                case CamelScreenChoices.Modify:
                                    EditCamelMain();
                                    Console.ReadLine();
                                    break;

                                case CamelScreenChoices.Exit:
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
        /// List all camel's.
        /// </summary>
        private void ListCamel()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.Camels is not null &&
                _dataService.Animals.Mammals.Camels.Count > 0)
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 3);
                int i = 1;
                foreach (Camel camel in _dataService.Animals.Mammals.Camels)
                {
                    ScreenDefinionService.Show(ScreenDefinitionJson, 4);
                    camel.Display();
                    i++;
                }
            }
            else
            {
                 ScreenDefinionService.Show(ScreenDefinitionJson, 5);
            }
        }

        /// <summary>
        /// Add a camel.
        /// </summary>
        private void AddCamel()
        {
            try
            {
                Camel camel = AddEditCamel();
                _dataService?.Animals?.Mammals?.Camels?.Add(camel);
                ScreenDefinionService.Show(ScreenDefinitionJson, 6);
            }
            catch
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 7);
            }
        }

        /// <summary>
        /// Deletes a camel.
        /// </summary>
        private void DeleteCamel()
        {
            try
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 8);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Camel? camel = (Camel?)(_dataService?.Animals?.Mammals?.Camels
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (camel is not null)
                {
                    _dataService?.Animals?.Mammals?.Camels?.Remove(camel);
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
        /// Edits an existing camel after choice made.
        /// </summary>
        private void EditCamelMain()
        {
            try
            {
                ScreenDefinionService.Show(ScreenDefinitionJson, 12);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Camel? camel = (Camel?)(_dataService?.Animals?.Mammals?.Camels?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (camel is not null)
                {
                    Camel camelEdited = AddEditCamel();
                    camel.Copy(camelEdited);
                    ScreenDefinionService.Show(ScreenDefinitionJson, 13);
                    camel.Display();
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
        /// Adds/edit specific camel.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        private Camel AddEditCamel()
        {
            ScreenDefinionService.Show(ScreenDefinitionJson, 16);
            string? name = Console.ReadLine();
            ScreenDefinionService.Show(ScreenDefinitionJson, 17);
            string? ageAsString = Console.ReadLine();
            ScreenDefinionService.Show(ScreenDefinitionJson, 18);
            string? color = Console.ReadLine();
            ScreenDefinionService.Show(ScreenDefinitionJson, 19);
            string? speedAsString = Console.ReadLine();
            ScreenDefinionService.Show(ScreenDefinitionJson, 20);
            string? diet = Console.ReadLine();

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
            if (speedAsString is null)
            {
                throw new ArgumentNullException(nameof(speedAsString));
            }
            if (diet is null)
            {
                throw new ArgumentNullException(nameof(diet));
            }
            int age = Int32.Parse(ageAsString);
            int speed = Int32.Parse(speedAsString);
            Camel camel = new (name, age, color, speed, diet);
            return camel;
        }

        #endregion // Private Methods
    }
}
