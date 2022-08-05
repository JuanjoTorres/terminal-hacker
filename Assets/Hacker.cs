using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Game configuration data
    const string menuHint = "You may type 'menu' at any time.";
    string[] Level1Password = { "books", "aisle", "shelf", "password", "font", "borrow" };
    string[] Level2Password = { "prisions", "handcuffs", "holster", "uniform", "arrest" };
    string[] Level3Password = { "asteroid", "astronaut", "atmosphere", "constellation", "nightfall" };

    // Game state
    int level;
    string password;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

    // Use this for initialization
    void Start ()
    {
        ShowMainMenu();
    }

    void ShowMainMenu ()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();

        Terminal.WriteLine("What would you like to hack into?\n");
        Terminal.WriteLine("\tPress 1 for the local library.");
        Terminal.WriteLine("\tPress 2 for the police station.");
        Terminal.WriteLine("\tPress 3 for the NASA.");
        Terminal.WriteLine("\nEnter your selection:");
    }

    void OnUserInput (string input)
    {
        if (input == "menu") // we can always go direct to main menu
        {
            ShowMainMenu();
        }
        else if (input == "exit" || input == "quit" || input == "close")
        {
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu (string input)
    {
        bool isValidLevelNumber = ( input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("Please, select a level Mr Bond!");
        }
        else
        {
            Terminal.WriteLine ("Please chosen a valid level.");
        }
    }

    void AskForPassword ()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine(menuHint);
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = Level1Password[Random.Range(0, Level1Password.Length)];
                break;
            case 2:
                password = Level2Password[Random.Range(0, Level2Password.Length)];
                break;
            case 3:
                password = Level3Password[Random.Range(0, Level3Password.Length)];
                break;
            default:
                Debug.LogError("Invalid level number.");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    ________
   /       //
  /       //
 /_______//
(_______(/
"               );
                break;
            case 2:
                Terminal.WriteLine("You got the prison key!");
                Terminal.WriteLine("Play again for a greater challenge.");
                Terminal.WriteLine(@"
 ___
/0  \________
\___/-='  = '
");
                break;
            case 3:
                Terminal.WriteLine("Welcome to the NASA Network...");
                Terminal.WriteLine(@" 
  ___ _ __   __ _  ___ ___ 
 / __| '_ \ / _` |/ __/ _ \
 \__ \ |_) | (_| | (_|  __/
 |___/ .__/ \__,_|\___\___|
     |_|                    
");
                break;
            default:
                Debug.LogError("Invalid level reached.");
                break;
        }
    }
}
