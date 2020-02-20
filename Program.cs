﻿using System;
using System.Collections.Generic;

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
        private Dictionary<string, User> userDictionary = null;
        private Dictionary<string, CarWorkshop> carWorkshops = null;
        #endregion

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Car Workshops!\n");

            var main = new MainClass();
            main.userDictionary = new Dictionary<string, User>();
            main.carWorkshops = new Dictionary<string, CarWorkshop>();

            while (true)
            {
                var input = main.SelectOption();
                if (input == 0)
                    break;

                switch (input)
                {
                    case 1:
                        main.FillDictionary<User>(new User());
                        break;
                    case 2:
                        main.ShowUsers();
                        break;
                    case 3:
                        main.DeleteUserByUserName();
                        break;
                    case 4:
                        main.FillDictionary<CarWorkshop>(new CarWorkshop());
                        break;
                    default:
                        Console.Write("Please select an option from the list.\n");
                        break;
                }
            }
        }

        private int SelectOption()
        {
            Console.WriteLine("====Please select an option from the list.====\n");
            Console.WriteLine("Create a user: press 1");
            Console.WriteLine("Show users: press 2");
            Console.WriteLine("Delete a user: press 3");
            Console.WriteLine("Create a car workshop: press 4");
            Console.WriteLine("Show a car workshop: press 5");
            Console.WriteLine("Delete a car workshop: press 6\n");
            Console.WriteLine("===============================================");

            Console.Write("\nYou press: ");
            var input = 0;
            int.TryParse(Console.ReadLine(), out input);
            Console.WriteLine();

            return input;
        }

        private Dictionary<string, User> DeleteUserByUserName()
        {
            Console.WriteLine("***********************************************");
            Console.Write("Username: ");
            var userName = Console.ReadLine();

            if (userDictionary.ContainsKey(userName))
                userDictionary.Remove(userName);
            else
                Console.WriteLine("User does not exist in the dictionary.");

            return userDictionary;
        }

        private void ShowUsers()
        {
            if (userDictionary.Count == 0)
                Console.WriteLine("There are no users in the dictionary.\n");
            else
            {
                Console.WriteLine("-----------------------------------------------");
                foreach (KeyValuePair<string, User> item in userDictionary)
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

        private Dictionary<string, T> Create<T>(T entity)
        {
            if (entity is User)
            {

            }

            /*
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrEmpty(user.Email) || string.IsNullOrWhiteSpace(user.Email))
            {
                Console.WriteLine("You need to fill username or email information.\n");
                return null;
            }

            if (userDictionary.Count == 0)
                userDictionary.Add(user.Username, user);
            else
            {
                if (userDictionary.ContainsKey(user.Username))
                {
                    Console.WriteLine($"The user with key - {user.Username} - already exists in the dictionary.\n");
                    return userDictionary;
                }
                else
                {
                    foreach (KeyValuePair<string, User> keyValuePair in userDictionary)
                    {
                        if (keyValuePair.Value.Email.Equals(user.Email))
                        {
                            Console.WriteLine($"The user with email - {user.Email} - already exists in the dictionary.\n");
                            return userDictionary;
                        }
                    }
                    userDictionary.Add(user.Username, user);
                }
            }

            return userDictionary;
            */

            return new Dictionary<string, T>();
        }

        private void FillDictionary<T>(T entity)
        {
            var dictionary = new Dictionary<string, T>();

            if (entity is User)
            {
                _user = entity as User;
                Create<T>((T)(object)FillInformation<User>(_user));
            }

            //if (entity is CarWorkshop)
            //{
            //    _carWorkshop = entity as CarWorkshop;
            //    var result = FillInformation<CarWorkshop>(_carWorkshop);
            //    dictionary.Add(_carWorkshop.CompanyName, (T)(object)result);
            //}
        }

        private List<T> FillInformation<T>(T entity)
        {
            var list = new List<T>();

            if (entity is User)
            {
                FillUserInformation(list);
            }

            if (entity is CarWorkshop)
            {
                FillCarWorkshopInformation(list);
            }

            return list;
        }

        private void FillCarWorkshopInformation<T>(List<T> list)
        {
            Console.WriteLine("***********************************************");
            Console.Write("Company Name: ");
            _carWorkshop.CompanyName = Console.ReadLine();

            Console.Write("Car Trademarks: ");
            _carWorkshop.CarTrademarks = Console.ReadLine();

            var location = FillLocation();

            _carWorkshop.City = location[0];
            _carWorkshop.PostalCode = Convert.ToInt32(location[1]);
            _carWorkshop.Country = location[2];

            list.Add((T)(object)_carWorkshop);
        }

        private void FillUserInformation<T>(List<T> list)
        {
            Console.WriteLine("***********************************************");
            Console.Write("Username: ");
            _user.Username = Console.ReadLine();

            Console.Write("Email: ");
            _user.Email = Console.ReadLine();

            var location = FillLocation();

            _user.City = location[0];
            _user.PostalCode = Convert.ToInt32(location[1]);
            _user.Country = location[2];

            list.Add((T)(object)_user);
        }

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
    }
}