using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Xml.Linq;

public class CharacterInfo
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            if (!string.IsNullOrEmpty(value) && !IsNumeric(value) && !string.IsNullOrWhiteSpace(value) && !ContainsSpecialCharacters(value))
            {
                if (value.Length >= 3 && value.Length <= 20)
                {
                    this._name = value;
                }
                else
                {
                    Console.WriteLine("Invalid name length. Must be between 3 and 20 characters.");
                }
            }
            else if (IsNumeric(value) || isOnlyInt(value))
            {
                Console.WriteLine("Invalid name. Must have a letter.");
            }
            else if (ContainsSpecialCharacters(value))
            {
                Console.WriteLine("Invalid name. Special characters are not allowed.");
            }
            else if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Please enter a non-empty name.");
            }
        }
    }

    public void DisplayWelcomeMessage()
    {
        Console.WriteLine($"Welcome, {Name}!");
    }

    public void DisplayName()
    {
        Console.WriteLine($"Name: {Name}");
    }
    private bool IsNumeric(string input)
    {
        return long.TryParse(input, out _);
    }
    private bool isOnlyInt(string input)
    {
        return int.TryParse(input, out _);
    }
    private bool ContainsSpecialCharacters(string input)
    {
        return Regex.IsMatch(input, "[^a-zA-Z0-9]");
    }
}

public class CharacterDesign
{
    private string _race;

    private string[] RaceChoices = { "human", "elf", "orc", "dwarf" };

    public string Race
    {
        get { return _race; }
        set
        {
            do
            {
                try
                {
                    Console.WriteLine("Choose your race:");
                    for (int i = 0; i < RaceChoices.Length; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {RaceChoices[i]}");
                    }

                    Console.Write("Your choice: ");
                    int userInput = Convert.ToInt32(Console.ReadLine());
                    if (userInput >= 1 && userInput <= RaceChoices.Length)
                    {
                        this._race = RaceChoices[userInput - 1];
                        break;
                    }
                    else if (userInput <= 0 || userInput > RaceChoices.Length)
                    {
                        throw new IndexOutOfRangeException("Invalid choice. Please choose a race by entering a number from 1 to 4.");
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice. Can't enter a letter. ");
                }
            } while (true);
        }
    }

    public void DisplayRace()
    {
        Console.WriteLine($"Selected race: {Race}");

    }
}

public class GamePlay
{
    private bool _hardmode;

    public bool Hardmode
    {
        get { return _hardmode; }
        set
        {
            int choice;
            do
            {
                Console.Write("Do you want to play in hard mode? \n[1] Yes\n[2] No");
                try
                {
                    Console.Write("\nYour Choice: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 1 || choice == 2)
                    {
                        break;
                    }
                    else if (choice <= 0 || choice >= 3)
                    {
                        throw new IndexOutOfRangeException("Invalid input! Please select from 1 or 2 only");
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice. Can't enter a letter.");
                }
            } while (true);
            _hardmode = choice == 1;
        }
    }

    public void DisplayAnswer()
    {
        Console.WriteLine($"Are you a Hardmode player?: {Hardmode}");
    }
}
public abstract class Color
{
    public abstract void DisplayColor();
}


public class Hat : Color
{
    private string _hat;
    private string _hatcolor;

    private string[] hatChoices = { "Mage's hat", "Swordsman's helmet", "Archer's hood" };


    public string SelectedHat
    {
        get { return _hat; }
        set
        {
            int choice;
            do
            {
                try
                {
                    Console.WriteLine("Choose a hat:");
                    for (int i = 0; i < hatChoices.Length; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {hatChoices[i]}");
                    }
                    Console.Write("Your Choice: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice >= 1 & choice <= hatChoices.Length)
                    {
                        break;
                    }
                    else if (choice <= 0 || choice > hatChoices.Length)
                    {
                        throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 3 only");
                    }
                    else
                    {
                        throw new FormatException();
                    }

                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice. Can't enter a letter. ");
                }
            } while (true);

            _hat = hatChoices[choice - 1];
        }
    }



    private string[] hatColorChoices = { "Red", "Blue", "Yellow", "Green", "Orange", "Violet", "White", "Black" };
    public override void DisplayColor()
    {
        int colorChoice;
        do
        {
            try
            {
                Console.WriteLine($"Choose a {_hat} color:");
                for (int i = 0; i < hatColorChoices.Length; i++)
                {
                    Console.WriteLine($"[{i + 1}] {hatColorChoices[i]}");
                }

                Console.Write("Your choice: ");
                colorChoice = Convert.ToInt32(Console.ReadLine());

                if (colorChoice >= 1 && colorChoice <= hatColorChoices.Length)
                {
                    break;
                }
                else if (colorChoice <= 0 || colorChoice >= hatColorChoices.Length)
                {
                    throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 8 only");
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid choice. Can't enter a letter. ");
            }
        } while (true);
        _hatcolor = hatColorChoices[colorChoice - 1];
    }

    public void DisplaySelectedHat()
    {
        Console.WriteLine($"Selected hat: {SelectedHat}");
        Console.WriteLine($"Hat color: {_hatcolor}");
    }

    public class Shirt : Color
    {
        public string _shirt;
        public string _shirtcolor;

        private string[] shirtChoices = { "Mage's robe", "Swordsman's armor", "Archer's vest" };

        public string SelectShirt
        {
            get { return _shirt; }
            set
            {
                int choice;
                do
                {
                    try
                    {
                        Console.WriteLine("Choose a shirt:");
                        for (int i = 0; i < shirtChoices.Length; i++)
                        {
                            Console.WriteLine($"[{i + 1}] {shirtChoices[i]}");
                        }
                        Console.Write("Your Choice: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        if (choice >= 1 & choice <= shirtChoices.Length)
                        {
                            break;
                        }
                        else if (choice <= 0 || choice > shirtChoices.Length)
                        {
                            throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 3 only");
                        }
                        else
                        {
                            throw new FormatException();
                        }

                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid choice. Can't enter a letter. ");
                    }
                } while (true);

                _shirt = shirtChoices[choice - 1];
            }
        }

        private string[] shirtColorChoices = { "Red", "Blue", "Yellow", "Green", "Orange", "Violet", "White", "Black" };

        public override void DisplayColor()
        {
            int colorChoice;
            do
            {
                try
                {
                    Console.WriteLine($"Choose a {_shirt} color:");
                    for (int i = 0; i < shirtColorChoices.Length; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {shirtColorChoices[i]}");
                    }

                    Console.Write("Your choice: ");
                    colorChoice = Convert.ToInt32(Console.ReadLine());

                    if (colorChoice >= 1 && colorChoice <= shirtColorChoices.Length)
                    {
                        break;
                    }
                    else if (colorChoice <= 0 || colorChoice >= shirtColorChoices.Length)
                    {
                        throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 8 only");
                    }
                    else
                    {
                        throw new FormatException("Invalid choice. Can't enter a letter.");
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Error" + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice. Can't enter a letter.");
                }
            } while (true);

            _shirtcolor = shirtColorChoices[colorChoice - 1];
        }

        public void DisplaySelectedShirt()
        {
            Console.WriteLine($"Selected hat: {SelectShirt}");
            Console.WriteLine($"Hat color: {_shirtcolor}");
        }
    }

    public interface Colors
    {
        public void DisplayColors();
    }

    public class Pants : Colors
    {
        public string _pants;
        public string _pantscolor;

        private string[] pantsChoices = { "Mage's pants", "Swordsman's pants", "Archer's pants" };

        public void SelectPants()
        {
            int choice;
            do
            {
                try
                {
                    Console.WriteLine("Choose a pants:");
                    for (int i = 0; i < pantsChoices.Length; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {pantsChoices[i]}");
                    }
                    Console.Write("Your Choice: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice >= 1 && choice <= pantsChoices.Length)
                    {
                        break;
                    }
                    else if (choice <= 0 || choice > pantsChoices.Length)
                    {
                        throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 3 only");
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice. Can't enter a letter. ");
                }
            } while (true);

            _pants = pantsChoices[choice - 1];
        }

        private string[] pantsColorChoices = { "Red", "Blue", "Yellow", "Green", "Orange", "Violet", "White", "Black" };

        public void DisplayColors()
        {
            int colorChoice;
            do
            {
                try
                {
                    Console.WriteLine($"Choose a {_pants} color:");
                    for (int i = 0; i < pantsColorChoices.Length; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {pantsColorChoices[i]}");
                    }

                    Console.Write("Your choice: ");
                    colorChoice = Convert.ToInt32(Console.ReadLine());

                    if (colorChoice >= 1 && colorChoice <= pantsColorChoices.Length)
                    {
                        break;
                    }
                    else if (colorChoice <= 0 || colorChoice >= pantsColorChoices.Length)
                    {
                        throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 8 only");
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Error" + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice. Can't enter a letter.");
                }
            } while (true);

            _pantscolor = pantsColorChoices[colorChoice - 1];
        }

        public void DisplaySelectedPants()
        {
            Console.WriteLine($"Selected hat: {_pants}");
            Console.WriteLine($"Hat color: {_pantscolor}");
        }
    }

    public class Boots : Colors
    {
        private string _boots;
        private string _bootscolor;

        private string[] bootsChoices = { "Mage's boots", "Swordsman's boots", "Archer's boots" };

        public void SelectBoots()
        {
            int choice;
            do
            {
                try
                {
                    Console.WriteLine("Choose a boots:");
                    for (int i = 0; i < bootsChoices.Length; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {bootsChoices[i]}");
                    }
                    Console.Write("Your Choice: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice >= 1 && choice <= bootsChoices.Length)
                    {
                        break;
                    }
                    else if (choice <= 0 || choice > bootsChoices.Length)
                    {
                        throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 3 only");
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice. Can't enter a letter. ");
                }
            } while (true);

            _boots = bootsChoices[choice - 1];
        }

        private string[] bootsColorChoices = { "Red", "Blue", "Yellow", "Green", "Orange", "Violet", "White", "Black" };

        public void DisplayColors()
        {
            int colorChoice;
            do
            {
                try
                {
                    Console.WriteLine($"Choose a {_boots} color:");
                    for (int i = 0; i < bootsColorChoices.Length; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {bootsColorChoices[i]}");
                    }

                    Console.Write("Your choice: ");
                    colorChoice = Convert.ToInt32(Console.ReadLine());

                    if (colorChoice >= 1 && colorChoice <= bootsColorChoices.Length)
                    {
                        break;
                    }
                    else if (colorChoice <= 0 || colorChoice >= bootsColorChoices.Length)
                    {
                        throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 8 only");
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice. Can't enter a letter.");
                }
            } while (true);

            _bootscolor = bootsColorChoices[colorChoice - 1];
        }

        public void DisplaySelectedBoots()
        {
            Console.WriteLine($"Selected hat: {_boots}");
            Console.WriteLine($"Hat color: {_bootscolor}");
        }

        public class EyeColor
        {
            public string _eyecolor;

            public EyeColor(string eyecolor)
            {
                this._eyecolor = eyecolor;
            }

            public string GetEyeColor()
            {
                return _eyecolor;
            }
        }

        public class Body
        {
            public string _skincolor;
            public string _height;
            public string _weight;

            public void CharacterBody(string _skincolor)
            {
                this._skincolor = _skincolor;
            }

            public void CharacterBody(String _height, string _weight)
            {
                this._height = _height;
                this._weight = _weight;
            }

            public string GetSkinColor()
            {
                return _skincolor;
            }

            public string GetHeight()
            {
                return _height;
            }

            public string GetWeight()
            {
                return _weight;
            }
        }

        public struct Sex
        {
            public string Gender { get; set; }

            public void DisplaySex()
            {
                Console.WriteLine($"Your Selected Sex: {Gender}");

            }
        }

        public class UserInputHandler
        {
            public static void AskUserSex(out Sex chosenSex)
            {

                chosenSex = new Sex();

                string[] userSex = { "Male", "Female" };

                while (true)
                {
                    try
                    {
                        Console.WriteLine("Select Your Sex");
                        for (int i = 0; i < userSex.Length; i++)
                        {
                            Console.WriteLine($"[{i + 1}]" + userSex[i]);
                        }
                        Console.Write("Your Choice: ");

                        if (!int.TryParse(Console.ReadLine(), out int Choices))
                        {
                            throw new FormatException("Invalid choice. Can't enter a letter.");
                        }
                        else if (Choices <= 0 || Choices > userSex.Length)
                        {
                            throw new IndexOutOfRangeException("Please Select From 1 to 2");
                        }

                        chosenSex.Gender = userSex[Choices - 1];
                        break;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        public class Pet
        {
            private string pet;
            private string petelement;

            private string[] petlist = { "Dog", "Cat", "Hawk", "Owl" };
            private string[] petelementlist = { "Anemo", "Cryo", "Dendro", "Electro", "Geo", "Hydro", "Pyro" };

            public string SelectPet
            {
                get { return pet; }
                set
                {
                    do
                    {
                        try
                        {
                            Console.WriteLine("Choose your Pet:");
                            for (int i = 0; i < petlist.Length; i++)
                            {
                                Console.WriteLine($"[{i + 1}] {petlist[i]}");
                            }

                            Console.Write("Your choice: ");
                            int userInput = Convert.ToInt32(Console.ReadLine());
                            if (userInput >= 1 && userInput <= petlist.Length)
                            {
                                this.pet = petlist[userInput - 1];
                                break;
                            }
                            else if (userInput <= 0 || userInput > petlist.Length)
                            {
                                throw new IndexOutOfRangeException("Invalid choice. Please choose a Pet by entering a number from 1 to 4.");
                            }
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid choice. Can't enter a letter.");
                        }
                    } while (true);
                }
            }

            public string SelectPetElement
            {
                get { return petelement; }
                set
                {
                    do
                    {
                        try
                        {
                            Console.WriteLine("Choose your Pet Element:");
                            for (int i = 0; i < petelementlist.Length; i++)
                            {
                                Console.WriteLine($"[{i + 1}] {petelementlist[i]}");
                            }

                            Console.Write("Your choice: ");
                            int userInput = Convert.ToInt32(Console.ReadLine());
                            if (userInput >= 1 && userInput <= petelementlist.Length)
                            {
                                this.petelement = petelementlist[userInput - 1];
                                break;
                            }
                            else if (userInput <= 0 || userInput > petelementlist.Length)
                            {
                                throw new IndexOutOfRangeException("Invalid choice. Please choose a Pet Element by entering a number from 1 to 7.");
                            }
                            else
                            {
                                throw new FormatException();
                            }
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid choice. Can't enter a letter.");
                        }
                    } while (true);
                }
            }

            public void DisplayPet()
            {
                Console.WriteLine($"Selected pet: {pet}");
            }
            public void DisplayPetElement()
            {
                Console.WriteLine($"Selected pet element: {petelement}");
            }
        }

        public class ClassCreation
        {
            private string primaryclass;
            private string secondaryclass;

            private string[] primary = { "Swordman", "Mage", "Archer" };
            private string[] secondary = { "Scout", "healer", "Summoner", "buffer" };

            public string PrimaryClass
            {
                get { return primaryclass; }
                set
                {
                    do
                    {
                        try
                        {
                            Console.WriteLine("Choose your Primary class:");
                            for (int i = 0; i < primary.Length; i++)
                            {
                                Console.WriteLine($"[{i + 1}] {primary[i]}");
                            }

                            Console.Write("Your choice: ");
                            int userInput = Convert.ToInt32(Console.ReadLine());
                            if (userInput >= 1 && userInput <= primary.Length)
                            {
                                this.primaryclass = primary[userInput - 1];
                                break;
                            }
                            else if (userInput <= 0 || userInput > primary.Length)
                            {
                                throw new IndexOutOfRangeException("Invalid choice. Please choose a Primary Class by entering a number from 1 to 3.");
                            }
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid choice. Can't enter a letter.");
                        }
                    } while (true);
                }
            }

            public string SecondaryClass
            {
                get { return secondaryclass; }
                set
                {
                    do
                    {
                        try
                        {
                            Console.WriteLine("Choose your Secondary class:");
                            for (int i = 0; i < secondary.Length; i++)
                            {
                                Console.WriteLine($"[{i + 1}] {secondary[i]}");
                            }

                            Console.Write("Your choice: ");
                            int userInput = Convert.ToInt32(Console.ReadLine());
                            if (userInput >= 1 && userInput <= secondary.Length)
                            {
                                this.secondaryclass = secondary[userInput - 1];
                                break;
                            }
                            else if (userInput <= 0 || userInput > secondary.Length)
                            {
                                throw new IndexOutOfRangeException("Invalid choice. Please choose a Secondary Class by entering a number from 1 to 4.");
                            }
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid choice. Can't enter a letter.");
                        }
                    } while (true);
                }
            }


            public void DisplayPrimaryClass()
            {
                Console.WriteLine($"Selected Primary class: {PrimaryClass}");

            }
            public void DisplaySecondaryClass()
            {
                try
                {
                    Console.WriteLine($"Selected Secondary class: {SecondaryClass}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public class PlayerStats
        {
            private int statsPoints = 20;
            private int health = 0;
            private int strength = 0;
            private int agility = 0;

            public int Health { get { return health; } }
            public int Strength { get { return strength; } }
            public int Agility { get { return agility; } }

            public void Stats()
            {
                statsPoints = 20;
                health = 0;
                strength = 0;
                agility = 0;

                while (true)
                {
                    try
                    {
                        Console.WriteLine($"Please Select Which Stats Do you want to Upgrade (Points remaining: {statsPoints}):");
                        Console.WriteLine($"[1] Health: {health}\n[2] Strength: {strength}\n[3] Agility: {agility}\n[4] End the Stat distribution.");
                        Console.Write("Your Choice: ");

                        if (!int.TryParse(Console.ReadLine(), out int upgrade))
                        {
                            throw new FormatException();
                        }
                        if (upgrade >= 1 && upgrade <= 3)
                        {
                            if (upgrade != 1 && upgrade != 2 && upgrade != 3)
                        {
                            Console.WriteLine("Invalid input, Please select From 1 to 3");
                            continue;
                        }

                        Console.Write("How many Points Do you want to Add?: ");
                        if (!int.TryParse(Console.ReadLine(), out int addPoints))
                        {
                            throw new FormatException();
                        }
                        if (addPoints < 0)
                        {
                            Console.WriteLine("Can't Enter negative");
                            continue;
                        }
                        if (addPoints >= 21)
                        {
                            Console.WriteLine("Your Points are insufficient");
                            continue;
                        }
                        if (addPoints > statsPoints)
                        {
                            Console.WriteLine("You don't have sufficient points");
                            continue;
                        }
                        
                            statsPoints -= addPoints;
                            switch (upgrade)
                            {
                                case 1:
                                    health += addPoints;
                                    Console.WriteLine("Your Health stats have been successfully upgraded");
                                    break;
                                case 2:
                                    strength += addPoints;
                                    Console.WriteLine("Your Strength stats have been successfully upgraded");
                                    break;
                                case 3:
                                    agility += addPoints;
                                    Console.WriteLine("Your Agility stats have been successfully upgraded");
                                    break;
                            }
                        }
                        else if (upgrade == 4)
                        {
                            Console.Write($"Are you sure you? You still have {statsPoints} left. \n[1] Yes [2] No: ");
                            int sure = Convert.ToInt32(Console.ReadLine());

                            if(sure == 1)
                            {
                                break;
                            }
                            else if(sure == 2)
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Please Only choose from 1 or 2, because of that you will return to the stat points distribution.");
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid Input, Invalid choice. Can't enter a letter.");
                        continue;
                    }
                }
            }

            public void DisplayStats()
            {
                Console.WriteLine("==============STATS=============");
                Console.WriteLine($"HEALTH: {health}");
                Console.WriteLine($"STRENGTH: {strength}");
                Console.WriteLine($"AGILITY: {agility}");
                Console.WriteLine("================================");
            }
        }

        public class Server
        {
            private string server;

            private string[] serverlist = { "Asia", "NA", "EU" };

            public string SelectServer
            {
                get { return server; }
                set
                {
                    do
                    {
                        try
                        {
                            Console.WriteLine("Choose your Server:");
                            for (int i = 0; i < serverlist.Length; i++)
                            {
                                Console.WriteLine($"[{i + 1}] {serverlist[i]}");
                            }

                            Console.Write("Your choice: ");
                            int userInput = Convert.ToInt32(Console.ReadLine());
                            if (userInput >= 1 && userInput <= serverlist.Length)
                            {
                                this.server = serverlist[userInput - 1];
                                break;
                            }
                            else if (userInput <= 0 || userInput > serverlist.Length)
                            {
                                throw new IndexOutOfRangeException("Invalid choice. Please choose a Server by entering a number from 1 to 3.");
                            }
                            else
                            {
                                throw new FormatException();
                            }
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Error: Invalid choice. Can't enter a letter.");
                        }
                    } while (true);
                }
            }

            public void DisplayServer()
            {
                Console.WriteLine($"Selected server: {server}");
            }

            public class Element
            {
                private string element;

                private string[] elementlist = { "Anemo", "Cryo", "Dendro", "Electro", "Geo", "Hydro", "Pyro" };

                public string SelectElement
                {
                    get { return element; }
                    set
                    {
                        do
                        {
                            try
                            {
                                Console.WriteLine("Choose your Element:");
                                for (int i = 0; i < elementlist.Length; i++)
                                {
                                    Console.WriteLine($"[{i + 1}] {elementlist[i]}");
                                }

                                Console.Write("Your choice: ");
                                int userInput = Convert.ToInt32(Console.ReadLine());
                                if (userInput >= 1 && userInput <= elementlist.Length)
                                {
                                    this.element = elementlist[userInput - 1];
                                    break;
                                }
                                else if (userInput <= 0 || userInput > elementlist.Length)
                                {
                                    throw new IndexOutOfRangeException("Invalid choice. Please choose a Element by entering a number from 1 to 7.");
                                }
                            }
                            catch (IndexOutOfRangeException ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid choice. Can't enter a letter.");
                            }
                        } while (true);
                    }
                }

                public void DisplayElement()
                {
                    Console.WriteLine($"Seleceted Element: {element}");
                }
            }


            public class MainMethod
            {
                public static void Main(string[] args)
                {
                    CharacterInfo user = new CharacterInfo();
                    CharacterDesign design = new CharacterDesign();
                    GamePlay game = new GamePlay();
                    Hat hat = new Hat();
                    Shirt shirt = new Shirt();
                    Pants pants = new Pants();
                    Boots boots = new Boots();
                    Sex chosenSex;
                    Pet pet = new Pet();
                    ClassCreation classcreation = new ClassCreation();
                    PlayerStats playerStats = new PlayerStats();
                    Server server = new Server();
                    Element element = new Element();
                    SqlConnection connect;
                    string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\USERS\\WACOM\\SOURCE\\REPOS\\DATABASE\\DATABASE\\DATABASE1.MDF;Integrated Security=True";
                    connect = new SqlConnection(connection);


                    do
                    {
                        try
                        {
                            Console.WriteLine("DATABASE");
                            Console.WriteLine("Select From the selection \n[1] Add Data \n[2] Show Data \n[3] Delete Data \n[4] Exit Program");
                            Console.Write("Your Choice:");

                            if (!int.TryParse(Console.ReadLine(), out int select))
                            {
                                throw new FormatException("Please Enter A Number Only");
                            }
                            if (select <= 0 || select >= 5)
                            {
                                throw new IndexOutOfRangeException("Please Select from 1 to 4");
                            }

                            switch (select)
                            {
                                case 1:
                                    connect.Open();
                                    Console.Write("Welcome to game! [Press Enter to continue]");
                                    string enter = Console.ReadLine();

                                    do
                                    {
                                        Console.Write("Enter your name: ");
                                        user.Name = Console.ReadLine();

                                        if (string.IsNullOrEmpty(user.Name) || string.IsNullOrWhiteSpace(user.Name))
                                        {
                                            
                                        }
                                        else
                                        {
                                            bool isNameAlreadyExist = IsNameAlreadyExist(connect, user.Name, ignoreCase: true);

                                            if (isNameAlreadyExist)
                                            {
                                                Console.WriteLine("Player name is already taken. Please choose a different name.");
                                            }
                                            else
                                            {

                                                break;
                                            }
                                        }
                                    } while (true);
                                    user.DisplayWelcomeMessage();
                                    //sex
                                    UserInputHandler.AskUserSex(out chosenSex);
                                    //server
                                    server.SelectServer = "";
                                    //race
                                    design.Race = "";
                                    //hat
                                    hat.SelectedHat = "";
                                    //hat color
                                    hat.DisplayColor();
                                    //shirt
                                    shirt.SelectShirt = "";
                                    //shirt color
                                    shirt.DisplayColor();
                                    //pants
                                    pants.SelectPants();
                                    //spants color
                                    pants.DisplayColors();
                                    //boots
                                    boots.SelectBoots();
                                    //boots color
                                    boots.DisplayColors();
                                    //eye color
                                    string[] colors = { "Red", "Blue", "Yellow", "Green", "Orange", "Violet", "White", "Black" };
                                    int choice;
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Choose an eye color");
                                            for (int i = 0; i < colors.Length; i++)
                                            {
                                                Console.WriteLine($"[{i + 1}] {colors[i]}");
                                            }
                                            Console.Write("Your choice: ");
                                            choice = Convert.ToInt32(Console.ReadLine());
                                            if (choice >= 1 && choice <= colors.Length)
                                            {
                                                break;
                                            }
                                            else if (choice <= 0 || choice > colors.Length)
                                            {
                                                throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 8 only");
                                            }
                                            else
                                            {
                                                throw new FormatException();
                                            }
                                        }
                                        catch (IndexOutOfRangeException ex)
                                        {
                                            Console.WriteLine("Error: " + ex.Message);
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Please Enter A number Only");
                                        }
                                    }

                                    EyeColor eyecolor = new EyeColor(colors[choice - 1]);



                                    string[] skincolor = { "Fair", "tan", "dark", "light" };
                                    string[] height = { "Short", "Avarage", "Tall" };
                                    string[] weight = { "Light", "Avarage", "Heavy" };
                                    int choice1, choice2, choice3;
                                    //skin color
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Choose an skin color");
                                            for (int i = 0; i < skincolor.Length; i++)
                                            {
                                                Console.WriteLine($"[{i + 1}] {skincolor[i]}");
                                            }
                                            Console.Write("Your choice: ");
                                            choice1 = Convert.ToInt32(Console.ReadLine());
                                            if (choice1 >= 1 && choice1 <= skincolor.Length)
                                            {
                                                break;
                                            }
                                            else if (choice1 <= 0 || choice1 > skincolor.Length)
                                            {
                                                throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 4 only");
                                            }
                                            else
                                            {
                                                throw new FormatException("Invalid choice. Can't enter a letter.");
                                            }
                                        }
                                        catch (IndexOutOfRangeException ex)
                                        {
                                            Console.WriteLine("Error: " + ex.Message);
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Invalid choice. Can't enter a letter.");
                                        }
                                    }
                                    //height
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Choose your Height");
                                            for (int i = 0; i < height.Length; i++)
                                            {
                                                Console.WriteLine($"[{i + 1}] {height[i]}");
                                            }
                                            Console.Write("Your choice: ");
                                            choice2 = Convert.ToInt32(Console.ReadLine());
                                            if (choice2 >= 1 && choice2 <= height.Length)
                                            {
                                                break;
                                            }
                                            else if (choice2 <= 0 || choice2 > height.Length)
                                            {
                                                throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 3 only");
                                            }
                                            else
                                            {
                                                throw new FormatException();
                                            }
                                        }
                                        catch (IndexOutOfRangeException ex)
                                        {
                                            Console.WriteLine("Error: " + ex.Message);
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Invalid choice. Can't enter a letter.");
                                        }
                                    }
                                    //weight
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Choose your Weight");
                                            for (int i = 0; i < weight.Length; i++)
                                            {
                                                Console.WriteLine($"[{i + 1}] {weight[i]}");
                                            }
                                            Console.Write("Your choice: ");
                                            choice3 = Convert.ToInt32(Console.ReadLine());
                                            if (choice3 >= 1 && choice3 <= weight.Length)
                                            {
                                                break;
                                            }
                                            else if (choice3 <= 0 || choice3 > weight.Length)
                                            {
                                                throw new IndexOutOfRangeException("Invalid input! Please select from 1 to 3 only");
                                            }
                                            else
                                            {
                                                throw new FormatException();
                                            }
                                        }
                                        catch (IndexOutOfRangeException ex)
                                        {
                                            Console.WriteLine("Error: " + ex.Message);
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Invalid choice. Can't enter a letter.");
                                        }
                                    }
                                    Body body = new Body();
                                    body.CharacterBody(skincolor[choice1 - 1]);
                                    body.CharacterBody(height[choice2 - 1], weight[choice3 - 1]);
                                    game.Hardmode = true;
                                    classcreation.PrimaryClass = "";
                                    classcreation.SecondaryClass = "";
                                    element.SelectElement = "";
                                    playerStats.Stats();
                                    pet.SelectPet = "";
                                    pet.SelectPetElement = "";

                                    Console.WriteLine("===================================================");
                                    user.DisplayName();
                                    chosenSex.DisplaySex();
                                    server.DisplayServer();
                                    design.DisplayRace();
                                    hat.DisplaySelectedHat();
                                    shirt.DisplaySelectedShirt();
                                    pants.DisplaySelectedPants();
                                    boots.DisplaySelectedBoots();
                                    Console.WriteLine($"Selected Eye Color: {eyecolor.GetEyeColor()}");
                                    Console.WriteLine($"Selected Skin Color: {body.GetSkinColor()}");
                                    Console.WriteLine($"Selected Height: {body.GetHeight()}");
                                    Console.WriteLine($"Selected Weight: {body.GetWeight()}");
                                    game.DisplayAnswer();
                                    classcreation.DisplayPrimaryClass();
                                    classcreation.DisplaySecondaryClass();
                                    element.DisplayElement();
                                    playerStats.DisplayStats();
                                    pet.DisplayPet();
                                    pet.DisplayPetElement();
                                    Console.WriteLine("===================================================");

                                    string insertToQuery = "INSERT INTO dbo.Information([Player_Name], [server], [Race], [Selected Hat], [Color Of Hat], [Selected Shirt], [Color Of Shirt], [Selected Pants], [Color Of Pants], [Selected Boots], [Color Of Boots], [Eye Color], [Skin Color], [Height], [Weight], [Hard Mode], [Primary Class], [Secondary Class], [Selected Element], [Pet], [Pet Element], [Health], [Strength], [Agility], [GENDER]) VALUES(@PlayerName, @Server, @Race, @SelectedHat, @ColorOfHat, @SelectedShirt, @ColorOfShirt, @SelectedPants, @ColorOfPants, @SelectedBoots, @ColorOfBoots, @EyeColor, @SkinColor, @Height, @Weight, @HardMode, @PrimaryClass, @SecondaryClass, @SelectedElement, @Pet, @PetElement, @Health, @Strength, @Agility, @Gender)";
                                    SqlCommand insert = new SqlCommand(insertToQuery, connect);
                                    insert.Parameters.AddWithValue("@PlayerName", user.Name);
                                    insert.Parameters.AddWithValue("@Server", server.SelectServer);
                                    insert.Parameters.AddWithValue("@Race", design.Race);
                                    insert.Parameters.AddWithValue("@SelectedHat", hat._hat);
                                    insert.Parameters.AddWithValue("@ColorOfHat", hat._hatcolor);
                                    insert.Parameters.AddWithValue("@SelectedShirt", shirt._shirt);
                                    insert.Parameters.AddWithValue("@ColorOfShirt", shirt._shirtcolor);
                                    insert.Parameters.AddWithValue("@SelectedPants", pants._pants);
                                    insert.Parameters.AddWithValue("@ColorOfPants", pants._pantscolor);
                                    insert.Parameters.AddWithValue("@SelectedBoots", boots._boots);
                                    insert.Parameters.AddWithValue("@ColorOfBoots", boots._bootscolor);
                                    insert.Parameters.AddWithValue("@EyeColor", eyecolor._eyecolor);
                                    insert.Parameters.AddWithValue("@SkinColor", body._skincolor);
                                    insert.Parameters.AddWithValue("@Height", body._height);
                                    insert.Parameters.AddWithValue("@Weight", body._weight);
                                    insert.Parameters.AddWithValue("@HardMode", game.Hardmode);
                                    insert.Parameters.AddWithValue("@PrimaryClass", classcreation.PrimaryClass);
                                    insert.Parameters.AddWithValue("@SecondaryClass", classcreation.SecondaryClass);
                                    insert.Parameters.AddWithValue("@SelectedElement", element.SelectElement);
                                    insert.Parameters.AddWithValue("@Pet", pet.SelectPet);
                                    insert.Parameters.AddWithValue("@PetElement", pet.SelectPetElement);
                                    insert.Parameters.AddWithValue("@Health", playerStats.Health);
                                    insert.Parameters.AddWithValue("@Strength", playerStats.Strength);
                                    insert.Parameters.AddWithValue("@Agility", playerStats.Agility);
                                    insert.Parameters.AddWithValue("@Gender", chosenSex.Gender);
                                    insert.ExecuteNonQuery();

                                    connect.Close();
                                    Console.WriteLine("Please press enter to go back to the Main Menu");
                                    string conti = Console.ReadLine();

                                    break;
                                case 2:
                                    connect.Open();
                                    string VIEWDATA = "SELECT PLAYER_ID, Player_Name, server, Race, [Selected Hat], [Color Of Hat], [Selected Shirt], [Color Of Shirt], [Selected Pants], [Color Of Pants], [Selected Boots], [Color Of Boots], [Eye Color], [Skin Color], Height, Weight, [Hard Mode], [Primary Class], [Secondary Class], [Selected Element], Pet, [Pet Element], Health, Strength, Agility, GENDER FROM dbo.Information";
                                    SqlCommand diplay = new SqlCommand(VIEWDATA, connect);
                                    SqlDataReader reader = diplay.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        int id = Convert.ToInt32(reader["PLAYER_ID"]);
                                        Console.WriteLine("===================================================");
                                        Console.WriteLine($"PLAYER ID       : {id}");
                                        Console.WriteLine($"Player Name     : {reader["Player_Name"]}");
                                        Console.WriteLine($"server          : {reader["server"]}");
                                        Console.WriteLine($"Race            : {reader["Race"]}");
                                        Console.WriteLine($"Selected Hat    : {reader["Selected Hat"]}");
                                        Console.WriteLine($"Color Of Hat    : {reader["Color Of Hat"]}");
                                        Console.WriteLine($"Selected Shirt  : {reader["Selected Shirt"]}");
                                        Console.WriteLine($"Color Of Shirt  : {reader["Color Of Shirt"]}");
                                        Console.WriteLine($"Selected Pants  : {reader["Selected Pants"]}");
                                        Console.WriteLine($"Color Of Pants  : {reader["Color Of Pants"]}");
                                        Console.WriteLine($"Selected Boots  : {reader["Selected Boots"]}");
                                        Console.WriteLine($"Color Of Boots  : {reader["Color Of Boots"]}");
                                        Console.WriteLine($"Eye Color       : {reader["Eye Color"]}");
                                        Console.WriteLine($"Skin Color      : {reader["Skin Color"]}");
                                        Console.WriteLine($"Height          : {reader["Height"]}");
                                        Console.WriteLine($"Weight          : {reader["Weight"]}");
                                        Console.WriteLine($"Hard Mode       : {reader["Hard Mode"]}");
                                        Console.WriteLine($"Primary Class   : {reader["Primary Class"]}");
                                        Console.WriteLine($"Secondary Class : {reader["Secondary Class"]}");
                                        Console.WriteLine($"Selected Element: {reader["Selected Element"]}");
                                        Console.WriteLine($"Pet             : {reader["Pet"]}");
                                        Console.WriteLine($"Pet Element     : {reader["Pet Element"]}");
                                        Console.WriteLine($"Health          : {reader["Health"]}");
                                        Console.WriteLine($"Strength        : {reader["Strength"]}");
                                        Console.WriteLine($"Agility         : {reader["Agility"]}");
                                        Console.WriteLine($"Gender          : {reader["GENDER"]}");
                                        Console.WriteLine("===================================================");
                                    }
                                    reader.Close();
                                    connect.Close();
                                    break;
                                case 3:
                                    connect.Open();
                                    string VIEW_DATA_TO_DELETE = "SELECT PLAYER_ID, Player_Name FROM dbo.Information";
                                    string DELETE_DATA = "DELETE FROM dbo.Information WHERE PLAYER_ID = @SELECT";

                                    SqlCommand displayCommand = new SqlCommand(VIEW_DATA_TO_DELETE, connect);
                                    SqlDataReader Reader = displayCommand.ExecuteReader();

                                    while (Reader.Read())
                                    {
                                        int id = Convert.ToInt32(Reader["PLAYER_ID"]);
                                        Console.WriteLine("===================================================");
                                        Console.WriteLine($"PLAYER ID       : {id}");
                                        Console.WriteLine($"Player Name     : {Reader["Player_Name"]}");
                                        Console.WriteLine("===================================================");
                                    }

                                    Console.WriteLine("===================================================");
                                    Console.WriteLine("[0] Exit delete option");
                                    Console.WriteLine("===================================================");

                                    Reader.Close();
                                    while (true)
                                    {
                                        try
                                        {
                                            
                                            Console.Write("Select Which Player ID Do You want to delete: ");
                                            if (!int.TryParse(Console.ReadLine(), out int Select))
                                            {
                                                throw new FormatException();
                                            }

                                            if (Select == 0)
                                            {
                                                break;
                                            }
                                            else if (Select < 0)
                                            {
                                                Console.WriteLine("There is no Character ID that is a negative number!");
                                                break;
                                            }
                                            else
                                            {
                                                Console.Write("Are you sure you? [1] Yes [2] No: ");
                                                int choose = Convert.ToInt32(Console.ReadLine());

                                                if (choose == 1)
                                                {
                                                    SqlCommand deleteCommand = new SqlCommand(DELETE_DATA, connect);
                                                    deleteCommand.Parameters.AddWithValue("@SELECT", Select);
                                                    int rowAffected = deleteCommand.ExecuteNonQuery();

                                                    if (rowAffected > 0)
                                                    {
                                                        Console.WriteLine("Data Has Been Deleted");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("No data found");
                                                    }

                                                    break;
                                                }
                                                else if (choose == 2)
                                                {
                                                    break;
                                                }
                                                else if (choose != 1 && choose != 2)
                                                {
                                                    Console.WriteLine("It seems like you didn't pick from 1 or 2, so it will not delete the character");
                                                    break;
                                                }
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Please enter a number only, You will be automatically go back to the Main Menu");
                                            break;
                                        }
                                    }

                                    connect.Close();
                                    break;
                            }
                            if (select == 4)
                            {
                                connect.Close();
                                break;
                            }
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Please Enter a Number Only");
                        }
                    } while (true);
                }
                static bool IsNameAlreadyExist(SqlConnection conn, string name, bool ignoreCase = false)
                {
                    string query = "SELECT COUNT(*) FROM dbo.Information WHERE " + (ignoreCase ? "LOWER(Player_Name)" : "Player_Name") + " = @Name";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Name", ignoreCase ? name.ToLower() : name);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
                private bool ContainsSpecialCharacters(string input)
                {
                    return !Regex.IsMatch(input, "^[a-zA-Z0-9]+$");
                }
            }
        }
    }
}