using System;
using System.Collections.Generic;

namespace TextFragmentationMVC
{
    class TextView
    {
        public string GetInputText()
        {
            Console.WriteLine("Enter text to store in input.txt (press ENTER on an empty line to finish):");

            var lines = new List<string>();
            while (true)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    break;//empty line → finish input
                lines.Add(line);
            }

            //join lines with Environment.NewLine to preserve new lines in file
            return string.Join(Environment.NewLine, lines);
        }
        public string[] GetInputTextLines()
        {
            Console.WriteLine("Enter text to store in input.txt (press ENTER on an empty line to finish):");

            var lines = new List<string>();//empty list of strings,we keep adding lines one by one
            while (true)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) break;//finish input
                lines.Add(line);
            }
            return lines.ToArray();//return as array of lines
        }
        public void ShowMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public int AskWordsPerFile()
        {
            int n;
            while (true)
            {
                Console.Write("Enter number of words per fragmented file: ");
                if (int.TryParse(Console.ReadLine(), out n) && n > 0)//it reads what user types and convert that into integer ,
                                                                     //if its valid number it stores it in n and returns true

                    return n;
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
        }
    }
}