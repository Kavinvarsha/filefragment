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
        //Fragmentation method
        public void Fragment(int wordsPerFile)
        {
            try
            {
                if (!File.Exists(InputFile))//checks if input.txt present
                    throw new FileNotFoundException("Input file does not exist.");

                var allText = File.ReadAllText(InputFile);
                var words = allText.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);//tells it to split by space,tab,newline and remove empty spaces

                FragmentedFiles.Clear();//FragmentedFiles-list storing names of all created fragment files
                int fileCount = (int)Math.Ceiling((double)words.Length / wordsPerFile);//by dividing total no of words in input file to wordsperfile we are getting no of words per fragmented file

                int digits = fileCount.ToString().Length;//how many digits the filecount has .used to format filenames with leading 0

                for (int i = 0; i < fileCount; i++)
                {
                    var fileWords = words.Skip(i * wordsPerFile).Take(wordsPerFile);//skips words that belong to earlier files,,,takes the next wordsPerFile words for this file.
                    string filename = $"{(i + 1).ToString().PadLeft(digits, '0')}.txt";//file numbering from 1 and add 0 on left if needed
                    File.WriteAllText(filename, string.Join(" ", fileWords));
                    FragmentedFiles.Add(filename);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error during fragmentation: " + ex.Message);
            }
        }
        //Defragmentation method
        public void Defragment()
        {
            try
            {
                if (FragmentedFiles.Count == 0)
                    throw new Exception("No fragmented files found.");

                using (var writer = new StreamWriter(OutputFile))//allows to write text in output file
                {
                    foreach (var file in FragmentedFiles)
                    {
                        if (!File.Exists(file))
                            throw new FileNotFoundException($"Fragment file {file} not found.");

                        string content = File.ReadAllText(file);
                        writer.Write(content + " ");//writes the fragment text into output.txt file
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error during defragmentation: " + ex.Message);
            }
        }
        //Compare input and output
        public bool CompareFiles()
        {
            try
            {
                if (!File.Exists(InputFile) || !File.Exists(OutputFile))
                    throw new Exception("Input or Output file missing.");

                string inputText = File.ReadAllText(InputFile);
                string outputText = File.ReadAllText(OutputFile);

                return string.Equals(inputText.Replace("\r\n", "\n").Trim(),
                                     outputText.Replace("\r\n", "\n").Trim());//compares both
            }
            catch (Exception ex)
            {
                throw new Exception("Error during comparison: " + ex.Message);
            }
        }
    }
}

