# Lab03-WordGuess
**Author**: Benjamin Taylor  
**Version**: 1.0.0

## Overview
A console-based game where the user is tasked with guessing a random word selected from a text file one letter at a time. The text file containing the game's words can be viewed and modified from within the application by using the admin menu. 

## Getting Started
1. Create a fork of this repository, and clone your fork to your device.  
2. Open the solution file (WordGuess.sln) in Visual Studio.
3. To run the app, click the green arrow button labeled "Start".
4. For testing, navigate to the BankTest project using the Solution Explorer.
5. To run the tests, press CTRL+R or navigate to Tests > Run > All Tests in the top-level menu.

## Using The Application
![screenshot](https://github.com/btaylor93/Lab03-WordGuess/raw/master/assets/screenshot.jpg)
1. Upon starting the application, the game's main menu should appear in a console window as shown above. From here, the user can choose to start the game, enter the admin menu, or quit the game.
2. In the game, you will see a series of lines indicating blank (unguessed) letters. You will be prompted to enter a single letter that you think may be in the word. If the letter is in the word, the letter will then become visible. The game continues until all of the letters in the word have been guessed. Try to guess the word in as few tries as possible!
3. In the admin menu, you will be able to view the words in the game's data file, add words to the data file, delete words from the data file, reset the data file, and return to the main menu. Words added to the game cannot contain spaces, symbols, or numbers, and must be between 3 and 16 characters in length.

## Architecture
**Languages Used**: C# 7.0 (.NET Core 2.1)

Written with Visual Studio Community 2017.

## Change Log
06-16-2018 2:37pm - Initial version.
