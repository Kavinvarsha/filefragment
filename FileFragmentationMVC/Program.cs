using System;

namespace TextFragmentationMVC
{
    class Program
    {
        static void Main()
        {
            var model = new TextModel();
            var view = new TextView();
            var controller = new TextController(model, view);
            controller.Run();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
