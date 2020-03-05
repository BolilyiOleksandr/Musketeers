using System;
using System.Collections.Generic;
using System.Linq;

namespace Musketeers
{
    class MainClass
    {
        #region Variables
        private string _city = string.Empty;
        private int _postalCode = 0;
        private string _country = string.Empty;

        private User _user = null;
        private CarWorkshop _carWorkshop = null;
        private Appointment _appointment = null;
        private Dictionary<string, User> _userDictionary = null;
        private Dictionary<string, CarWorkshop> _carWorkshopsDictionary = null;
        #endregion

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Car Workshops!\n");

            var main = new MainClass()
            {
                _userDictionary = new Dictionary<string, User>(),
                _carWorkshopsDictionary = new Dictionary<string, CarWorkshop>(),
                _appointment = new Appointment()
            };

            while (true)
            {
                var input = main.SelectOption();
                if (input == 0)
                    break;
                SwitchSelectedOptionFromList(main, input);
            }
        }

        /// <summary>
        /// The method that switches the selected option from the list.
        /// </summary>
        /// <param name="main">Main class instance.</param>
        /// <param name="input">Input value.</param>
        private static void SwitchSelectedOptionFromList(MainClass main, int input)
        {
            switch (input)
            {
                case 1:
                    main.Create<User>(new User());
                    break;
                case 2:
                    main.Show<User>();
                    break;
                case 3:
                    main.Delete<User>();
                    break;
                case 4:
                    main.Create<CarWorkshop>(new CarWorkshop());
                    break;
                case 5:
                    main.Show<CarWorkshop>();
                    break;
                case 6:
                    main.Delete<CarWorkshop>();
                    break;
                case 7:
                    main.Search<CarWorkshop>();
                    break;
                case 8:
                    main.CreateAppoitment();
                    break;
                case 9:
                    main.ShowAppointment();
                    break;
                case 10:
                    main.UpdateAppointmentDateTime();
                    break;
                case 11:
                    main.DeleteAppointment();
                    break;
                default:
                    Console.Write("Please select an option from the list.\n");
                    break;
            }
        }

        /// <summary>
        /// The method that proposes to select an option from the list.
        /// </summary>
        /// <returns></returns>
        private int SelectOption()
        {
            Console.WriteLine("====Please select an option from the list.====\n");
            Console.WriteLine("Create a user: press 1");
            Console.WriteLine("Show users: press 2");
            Console.WriteLine("Delete a user: press 3");
            Console.WriteLine("Create a car workshop: press 4");
            Console.WriteLine("Show a car workshop: press 5");
            Console.WriteLine("Delete a car workshop: press 6");
            Console.WriteLine("Search for all car workshops in a specific city: press 7");
            Console.WriteLine("Create an appointment: press 8");
            Console.WriteLine("Show an appointment: press 9");
            Console.WriteLine("Update an appointment date and time: press 10");
            Console.WriteLine("Delete an appointment: press 11");
            Console.WriteLine("Exit: press 0\n");
            Console.WriteLine("===============================================");

            Console.Write("\nYou press: ");
            var input = 0;
            int.TryParse(Console.ReadLine(), out input);
            Console.WriteLine();

            return input;
        }

        /// <summary>
        /// The method that creates an instance of the entity.
        /// </summary>
        /// <typeparam name="T">T type.</typeparam>
        /// <param name="entity">Entity instance</param>
        /// <returns></returns>
        private Dictionary<string, T> Create<T>(T entity)
        {
            if (entity is User)
            {
                return CreateUser(entity);
            }

            if (entity is CarWorkshop)
            {
                return CreateCarWorkshop(entity);
            }

            return new Dictionary<string, T>();
        }

        /// <summary>
        /// The method that creates the dictionary of the User class.
        /// </summary>
        /// <typeparam name="T">T type.</typeparam>
        /// <param name="entity">Entity instance.</param>
        /// <returns></returns>
        private Dictionary<string, T> CreateUser<T>(T entity)
        {
            FillUserInformation<T>(entity);

            if (!CheckUniqueFieldsAreFilled<T>(_user.Username, _user.Email, entity))
                return (Dictionary<string, T>)(object)_userDictionary;

            if (_userDictionary.Count == 0)
                _userDictionary.Add(_user.Username, _user);
            else
            {
                if (_userDictionary.ContainsKey(_user.Username))
                {
                    Console.WriteLine($"The user with key - {_user.Username} - already exists in the dictionary.\n");
                    return (Dictionary<string, T>)(object)_userDictionary;
                }
                else
                {
                    foreach (KeyValuePair<string, User> keyValuePair in _userDictionary)
                    {
                        if (keyValuePair.Value.Email.Equals(_user.Email))
                        {
                            Console.WriteLine($"The user with email - {_user.Email} - already exists in the dictionary.\n");
                            return (Dictionary<string, T>)(object)_userDictionary;
                        }
                    }
                    _userDictionary.Add(_user.Username, _user);
                }
            }
            return (Dictionary<string, T>)(object)_userDictionary;
        }

        /// <summary>
        /// The method that creates the dictionary of the CarWorkshop class.
        /// </summary>
        /// <typeparam name="T">T type.</typeparam>
        /// <param name="entity">Entity instance.</param>
        /// <returns></returns>
        private Dictionary<string, T> CreateCarWorkshop<T>(T entity)
        {
            FillCarWorkshopInformation<T>(entity);

            if (!CheckUniqueFieldsAreFilled<T>(_carWorkshop.CompanyName, string.Empty, entity))
                return (Dictionary<string, T>)(object)_carWorkshopsDictionary;

            if (_carWorkshopsDictionary.Count == 0)
                _carWorkshopsDictionary.Add(_carWorkshop.CompanyName, _carWorkshop);
            else
            {
                if (_carWorkshopsDictionary.ContainsKey(_carWorkshop.CompanyName))
                {
                    Console.WriteLine($"The car workshop with key - {_carWorkshop.CompanyName} - already exists in the dictionary.\n");
                    return (Dictionary<string, T>)(object)_carWorkshopsDictionary;
                }
                else
                {
                    _carWorkshopsDictionary.Add(_carWorkshop.CompanyName, _carWorkshop);
                }
            }
            return (Dictionary<string, T>)(object)_carWorkshopsDictionary;
        }

        /// <summary>
        /// The method that checks if the unique fields are filled.
        /// </summary>
        /// <typeparam name="T">T type.</typeparam>
        /// <param name="firstField">First field value.</param>
        /// <param name="secondField">Second field value.</param>
        /// <param name="entity">Entity instance.</param>
        /// <returns></returns>
        private bool CheckUniqueFieldsAreFilled<T>(string firstField, string secondField, T entity)
        {
            if (!string.IsNullOrEmpty(firstField) && !string.IsNullOrWhiteSpace(firstField))
            {
                if (!string.IsNullOrEmpty(secondField) && !string.IsNullOrWhiteSpace(secondField) &&
                    entity is User)
                {
                    CheckUserUniqueFieldsAreFilled(firstField, secondField);
                    return true;
                }
                if (entity is CarWorkshop)
                {
                    CheckCarWorkshopUniqueFieldsAreFilled(firstField);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The method that checks that the user's unique fields are filled.
        /// </summary>
        /// <param name="firstField">First field value.</param>
        /// <param name="secondField">Second field value.</param>
        private void CheckUserUniqueFieldsAreFilled(string firstField, string secondField)
        {
            if (string.IsNullOrEmpty(firstField) || string.IsNullOrWhiteSpace(firstField))
                Console.WriteLine("You need to fill a username information.");
            if (string.IsNullOrEmpty(secondField) || string.IsNullOrWhiteSpace(secondField))
                Console.WriteLine("You need to fill an email information.");

            Console.WriteLine();
        }

        /// <summary>
        /// The method that checks that the car workshop's unique fields are filled.
        /// </summary>
        /// <param name="firstField">First field value.</param>
        private void CheckCarWorkshopUniqueFieldsAreFilled(string firstField)
        {
            if (string.IsNullOrEmpty(firstField) || string.IsNullOrWhiteSpace(firstField))
                Console.WriteLine("You need to fill a company name information.");

            Console.WriteLine();
        }

        /// <summary>
        /// The method that fills the car workshop information.
        /// </summary>
        /// <typeparam name="T">T type.</typeparam>
        /// <param name="entity">Entity instance.</param>
        private void FillCarWorkshopInformation<T>(T entity)
        {
            _carWorkshop = entity as CarWorkshop;

            Console.WriteLine("***********************************************");
            Console.Write("Company Name: ");
            _carWorkshop.CompanyName = Console.ReadLine();

            Console.Write("Car Trademarks: ");
            _carWorkshop.CarTrademarks = Console.ReadLine();

            var location = FillLocation();

            _carWorkshop.City = location[0];
            _carWorkshop.PostalCode = Convert.ToInt32(location[1]);
            _carWorkshop.Country = location[2];
        }

        /// <summary>
        /// The method that fills the user information.
        /// </summary>
        /// <typeparam name="T">T type.</typeparam>
        /// <param name="entity">Entity instance.</param>
        private void FillUserInformation<T>(T entity)
        {
            _user = entity as User;

            Console.WriteLine("***********************************************");
            Console.Write("Username: ");
            _user.Username = Console.ReadLine();

            Console.Write("Email: ");
            _user.Email = Console.ReadLine();

            var location = FillLocation();

            _user.City = location[0];
            _user.PostalCode = Convert.ToInt32(location[1]);
            _user.Country = location[2];
        }

        /// <summary>
        /// The method that fills the location.
        /// </summary>
        /// <returns></returns>
        private List<string> FillLocation()
        {
            Console.Write("City: ");
            _city = Console.ReadLine();

            Console.Write("Postal Code: ");
            var postalCode = 0;
            int.TryParse(Console.ReadLine(), out postalCode);
            _postalCode = postalCode;

            Console.Write("Country: ");
            _country = Console.ReadLine();
            Console.WriteLine("***********************************************\n");

            var location = new List<string>()
            {
                _city,
                _postalCode.ToString(),
                _country
            };

            return location;
        }

        /// <summary>
        /// The method that deletes the type instance.
        /// </summary>
        /// <typeparam name="T">T type.</typeparam>
        /// <returns></returns>
        private Dictionary<string, T> Delete<T>()
        {
            if (typeof(T) == typeof(User))
            {
                return DeleteUser<T>();
            }
            if (typeof(T) == typeof(CarWorkshop))
            {
                return DeleteCarWorkshop<T>();
            }

            return new Dictionary<string, T>();
        }

        /// <summary>
        /// The method that deletes the user instance.
        /// </summary>
        /// <typeparam name="T">T type.</typeparam>
        /// <returns></returns>
        private Dictionary<string, T> DeleteUser<T>()
        {
            Console.WriteLine("***********************************************");
            Console.Write("Username: ");
            var userName = Console.ReadLine();

            if (_userDictionary.ContainsKey(userName))
                _userDictionary.Remove(userName);
            else
                Console.WriteLine("User does not exist in the dictionary.");

            Console.WriteLine("***********************************************\n");
            return (Dictionary<string, T>)(object)_userDictionary;
        }

        /// <summary>
        /// The method that deletes the car workshop instance.
        /// </summary>
        /// <typeparam name="T">T type.</typeparam>
        /// <returns></returns>
        private Dictionary<string, T> DeleteCarWorkshop<T>()
        {
            Console.WriteLine("***********************************************");
            Console.Write("Company Name: ");
            var companyName = Console.ReadLine();

            if (_carWorkshopsDictionary.ContainsKey(companyName))
                _carWorkshopsDictionary.Remove(companyName);
            else
                Console.WriteLine("Car Workshop does not exist in the dictionary.");

            Console.WriteLine("***********************************************\n");
            return (Dictionary<string, T>)(object)_carWorkshopsDictionary;
        }

        private void Show<T>()
        {
            if (typeof(T) == typeof(User))
            {
                ShowUsers();
            }
            if (typeof(T) == typeof(CarWorkshop))
            {
                ShowCarWorkshops();
            }
        }

        private void ShowUsers()
        {
            if (_userDictionary.Count == 0)
                Console.WriteLine("There are no users in the dictionary.\n");
            else
            {
                Console.WriteLine("-----------------------------------------------");
                foreach (KeyValuePair<string, User> item in _userDictionary)
                {
                    Console.WriteLine($"Username: {item.Value.Username}");
                    Console.WriteLine($"Email: {item.Value.Email}");
                    Console.WriteLine($"City: {item.Value.City}");
                    Console.WriteLine($"Postal Code: {item.Value.PostalCode}");
                    Console.WriteLine($"Country: {item.Value.Country}");
                    Console.WriteLine();
                }
                Console.WriteLine("-----------------------------------------------\n");
            }
        }

        private void ShowCarWorkshops()
        {
            if (_carWorkshopsDictionary.Count == 0)
                Console.WriteLine("There are no car workshops in the dictionary.\n");
            else
            {
                Console.WriteLine("-----------------------------------------------");
                foreach (KeyValuePair<string, CarWorkshop> item in _carWorkshopsDictionary)
                {
                    Console.WriteLine($"Company Name: {item.Value.CompanyName}");
                    Console.WriteLine($"Car Trademarks: {item.Value.CarTrademarks}");
                    Console.WriteLine($"City: {item.Value.City}");
                    Console.WriteLine($"Postal Code: {item.Value.PostalCode}");
                    Console.WriteLine($"Country: {item.Value.Country}");
                    Console.WriteLine();
                }
                Console.WriteLine("-----------------------------------------------\n");
            }
        }

        private void ShowCarWorkshops(string city)
        {
            if (_carWorkshopsDictionary.Where(i => i.Value.City == city).Count() == 0)
                Console.WriteLine($"There are no car workshops in the {city}.\n");
            else
            {
                Console.WriteLine("-----------------------------------------------");
                foreach (KeyValuePair<string, CarWorkshop> item in _carWorkshopsDictionary.Where(i => i.Value.City == city))
                {
                    Console.WriteLine($"Company Name: {item.Value.CompanyName}");
                    Console.WriteLine($"Car Trademarks: {item.Value.CarTrademarks}");
                    Console.WriteLine($"City: {item.Value.City}");
                    Console.WriteLine($"Postal Code: {item.Value.PostalCode}");
                    Console.WriteLine($"Country: {item.Value.Country}");
                    Console.WriteLine();
                }
                Console.WriteLine("-----------------------------------------------\n");
            }
        }

        private void Search<T>()
        {
            if (typeof(T) == typeof(CarWorkshop))
            {
                string city = GetCity();
                ShowCarWorkshops(city);
            }
        }

        private string GetCity()
        {
            Console.WriteLine("***********************************************");
            Console.Write("City: ");
            var city = Console.ReadLine();
            Console.WriteLine();
            return city;
        }

        private Dictionary<string, Tuple<string, string, DateTime>> CreateAppoitment()
        {
            Console.WriteLine("***********************************************");

            Console.Write("Username: ");
            var userName = Console.ReadLine();

            if (_userDictionary.Select(i => i.Key == userName).Count() == 0)
            {
                Console.WriteLine("There are no users in the dictionary.\n");
                return new Dictionary<string, Tuple<string, string, DateTime>>();
            }

            Console.Write("Company name: ");
            var companyName = Console.ReadLine();

            if (_carWorkshopsDictionary.Select(i => i.Key == companyName).Count() == 0)
            {
                Console.WriteLine("There is no company name in the dictionary.\n");
                return new Dictionary<string, Tuple<string, string, DateTime>>();
            }

            Console.Write("Car trademark: ");
            var carTrademark = Console.ReadLine();

            if (_carWorkshopsDictionary.Select(i => i.Value.CarTrademarks == carTrademark).Count() == 0)
            {
                Console.WriteLine("There is no car trademark in the dictionary.\n");
                return new Dictionary<string, Tuple<string, string, DateTime>>();
            }

            Console.Write("Date and Time: ");
            var dateTime = Convert.ToDateTime(Console.ReadLine());

            _appointment.Appointments = new Dictionary<string, Tuple<string, string, DateTime>>()
            {
                [userName] = Tuple.Create(companyName, carTrademark, dateTime)
            };

            Console.WriteLine("***********************************************\n");
            return _appointment.Appointments;
        }

        private void ShowAppointment()
        {
            Console.Write("Username: ");
            var userName = Console.ReadLine();

            if (_appointment.Appointments is null ||
                _appointment.Appointments.Where(i => i.Key == userName).Count() == 0)
                Console.WriteLine($"There are no appointments for {userName}.\n");
            else
            {
                Console.WriteLine("-----------------------------------------------");
                foreach (KeyValuePair<string, Tuple<string, string, DateTime>> item in _appointment.Appointments.Where(i => i.Key == userName))
                {
                    Console.WriteLine($"Username: {item.Key}");
                    Console.WriteLine($"Company name: {item.Value.Item1}");
                    Console.WriteLine($"Car trademark: {item.Value.Item2}");
                    Console.WriteLine($"Date and time: {item.Value.Item3}");
                    Console.WriteLine();
                }
                Console.WriteLine("-----------------------------------------------\n");
            }
        }

        private void UpdateAppointmentDateTime()
        {
            Console.Write("Username: ");
            var userName = Console.ReadLine();

            Console.Write("Company name: ");
            var companyName = Console.ReadLine();

            Console.Write("Car trademark: ");
            var carTrademark = Console.ReadLine();

            Console.Write("Date and Time: ");
            var dateTime = Convert.ToDateTime(Console.ReadLine());

            if (_appointment.Appointments is null ||
                _appointment.Appointments.Where(i => i.Key == userName).Count() == 0)
                Console.WriteLine($"There are no appointments for {userName}.\n");
            else
            {
                foreach (KeyValuePair<string, Tuple<string, string, DateTime>> keyValuePair in _appointment.Appointments)
                {
                    if (keyValuePair.Key.Equals(userName) &&
                        keyValuePair.Value.Item1.Equals(companyName) &&
                        keyValuePair.Value.Item2.Equals(carTrademark))
                    {
                        _appointment.Appointments = new Dictionary<string, Tuple<string, string, DateTime>>()
                        {
                            [userName] = Tuple.Create(companyName, carTrademark, dateTime)
                        };
                        break;
                    }
                }
            }
        }

        private void DeleteAppointment()
        {
            Console.Write("Username: ");
            var userName = Console.ReadLine();

            Console.Write("Company name: ");
            var companyName = Console.ReadLine();

            Console.Write("Car trademark: ");
            var carTrademark = Console.ReadLine();

            Console.Write("Date and Time: ");
            var dateTime = Convert.ToDateTime(Console.ReadLine());

            if (_appointment.Appointments is null ||
                _appointment.Appointments.Where(i => i.Key == userName).Count() == 0)
                Console.WriteLine($"There are no appointments for {userName}.\n");
            else
            {
                foreach (KeyValuePair<string, Tuple<string, string, DateTime>> keyValuePair in _appointment.Appointments)
                {
                    if (keyValuePair.Key.Equals(userName) &&
                        keyValuePair.Value.Item1.Equals(companyName) &&
                        keyValuePair.Value.Item2.Equals(carTrademark))
                    {
                        _appointment.Appointments.Remove(userName);
                        break;
                    }
                }
            }
        }

    }
}