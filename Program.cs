using System;
using System.Collections.Generic;

namespace Musketeers
{
    class MainClass
    {
        private Dictionary<string, User> userDictionary = null;

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Car Workshops!\n");

            var main = new MainClass();
            main.userDictionary = new Dictionary<string, User>();

            while (true)
            {
                var input = main.SelectOption();
                if (input == 0)
                    break;

                switch (input)
                {
                    case 1:
                        main.CreateUser();
                        break;
                    case 2:
                        main.ShowUsers();
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
            Console.WriteLine("Delete a car workshop: press 5\n");
            Console.WriteLine("===============================================");

            Console.Write("\nYou press: ");
            var input = 0;
            int.TryParse(Console.ReadLine(), out input);
            Console.WriteLine();

            return input;
        }

        private Dictionary<string, User> CreateUser()
        {
            var user = new User();
            FillUserInformation(user);

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
                if (userDictionary.ContainsKey(user.Username) || userDictionary.Values.Equals(user.Email))
                {
                    Console.WriteLine($"The user with key - {user.Username} - or emai - {user.Email} - already exists in the dictionary.");
                }
                else
                    userDictionary.Add(user.Username, user);

            }

            return userDictionary;
        }

        private void FillUserInformation(User user)
        {
            Console.WriteLine("***********************************************");
            Console.Write("Username: ");
            user.Username = Console.ReadLine();

            Console.Write("Email: ");
            user.Email = Console.ReadLine();

            Console.Write("City: ");
            user.City = Console.ReadLine();

            Console.Write("Postal Code: ");
            var postalCode = 0;
            int.TryParse(Console.ReadLine(), out postalCode);
            user.PostalCode = postalCode;

            Console.Write("Country: ");
            user.Country = Console.ReadLine();
            Console.WriteLine("***********************************************\n");
        }

        private void ShowUsers()
        {
            if (userDictionary.Count == 0)
                Console.WriteLine("There are no users to show.\n");
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
    }
}