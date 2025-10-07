using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextFragmentationMVC
{
    class TextModel
    {
        public string InputFile { get; } = "input.txt";
        public string OutputFile { get; } = "output.txt";
        public List<string> FragmentedFiles { get; private set; } = new List<string>();

        //Create or overwrite input.txt
        public void CreateInputFile(string content)
        {
            try
            {
                File.WriteAllText(InputFile, content);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating input file: " + ex.Message);
            }
        }
        //Overloaded CreateInputFile to preserve multiple lines
        public void CreateInputFile(string[] lines)
        {
            try
            {
                File.WriteAllLines(InputFile, lines); // writes each element as a new line
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating input file: " + ex.Message);
            }
        }
    }
}

