using System;

namespace TextFragmentationMVC
{
    class TextController
    {
        private readonly TextModel _model;
        private readonly TextView _view;

        public TextController(TextModel model, TextView view)
        {
            _model = model;
            _view = view;
        }

        public void Run()
        {
            bool exitProgram = false;

            while (!exitProgram)
            {
                try
                {
                    //Ask to create empty input.txt
                    Console.Write("Do you want to create an empty input.txt? (Y/N, X to exit): ");
                    string createEmpty = Console.ReadLine()?.Trim().ToLowerInvariant();

                    if (createEmpty == "x" || createEmpty == "exit")
                    {
                        _view.ShowMessage("Exiting program.");
                        return;
                    }

                    if (createEmpty == "y" || createEmpty == "yes")
                    {
                        _model.CreateInputFile("");
                        _view.ShowMessage("Empty input.txt created successfully.");

                        // Enter text to store in input.txt(line by line)
                        string[] inputLines = _view.GetInputTextLines();//Get array of lines from user

                        if (inputLines.Length > 0)
                        {
                            _model.CreateInputFile(inputLines);//Write each line separately
                            _view.ShowMessage("Text stored in input.txt successfully.");
                        }

                    }
                    else
                    {
                        _view.ShowMessage("Exiting program because input.txt was not created.");
                        return;//Exit program
                    }

                    // Fragmentation
                    int wordsPerFile = _view.AskWordsPerFile();
                    _model.Fragment(wordsPerFile);
                    _view.ShowFiles(_model.FragmentedFiles);

                    //Check a fragmented file
                    string checkFile = _view.AskFileName();
                    try
                    {
                        var (content, charCount) = _model.CheckFile(checkFile);
                        _view.ShowMessage($"File Content:\n{content}\nCharacter Count: {charCount}");
                    }
                    catch (Exception ex)
                    {
                        _view.ShowError(ex.Message);
                    }

                    //Ask if user wants defragmentation
                    Console.Write("Do you want to defragment all fragmented files into output.txt? (Y/N): ");
                    string defragChoice = Console.ReadLine()?.Trim().ToLowerInvariant();
                    if (defragChoice == "y" || defragChoice == "yes")
                    {
                        _model.Defragment();
                        _view.ShowMessage("Defragmentation done. output.txt created.");

                        //Display defragmented text in console
                        try
                        {
                            string outputContent = System.IO.File.ReadAllText(_model.OutputFile);//stores all text from output.txt
                            _view.ShowMessage("Defragmented text (output.txt):\n" + outputContent);
                        }
                        catch (Exception ex)
                        {
                            _view.ShowError("Error reading output.txt: " + ex.Message);
                        }
                    }


                    //Ask if user wants to compare input.txt and output.txt
                    Console.Write("Do you want to compare input.txt and output.txt? (Y/N): ");
                    string compareChoice = Console.ReadLine()?.Trim().ToLowerInvariant();
                    if (compareChoice == "y" || compareChoice == "yes")
                    {
                        if (_model.CompareFiles())
                            _view.ShowMessage("SUCCESS: input.txt = output.txt");
                        else
                            _view.ShowError("ERROR: input.txt and output.txt differ!");
                    }

                    //Ask if user wants to delete all created files
                    Console.Write("Do you want to delete all created files? (Y/N): ");
                    string deleteChoice = Console.ReadLine()?.Trim().ToLowerInvariant();
                    if (deleteChoice == "y" || deleteChoice == "yes")
                    {
                        try
                        {
                            //Delete fragmented files
                            foreach (var file in _model.FragmentedFiles)
                            {
                                if (System.IO.File.Exists(file))
                                    System.IO.File.Delete(file);
                            }

                            //Delete input.txt and output.txt
                            if (System.IO.File.Exists(_model.InputFile))
                                System.IO.File.Delete(_model.InputFile);
                            if (System.IO.File.Exists(_model.OutputFile))
                                System.IO.File.Delete(_model.OutputFile);

                            _view.ShowMessage("All files deleted successfully.");
                        }
                        catch (Exception ex)
                        {
                            _view.ShowError("Error deleting files: " + ex.Message);
                        }
                    }

                    //Ask if user wants to exit or restart
                    Console.Write("Do you want to restart the process? (Y/N): ");
                    string restartChoice = Console.ReadLine()?.Trim().ToLowerInvariant();
                    if (restartChoice == "n" || restartChoice == "no")
                    {
                        _view.ShowMessage("Exiting program.");
                        exitProgram = true;
                    }

                }
                catch (Exception ex)
                {
                    _view.ShowError("Unexpected error: " + ex.Message);
                }
            }
        }

    }
}
