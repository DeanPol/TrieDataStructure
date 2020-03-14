using System;
using System.IO;

namespace TrieDataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            //Target data we are working with.
            string inputFilePath = @"<filepath-to-input>";
            string searchFilePath = @"<filepath-to-input>";

            if ((!File.Exists(inputFilePath)) || (!File.Exists(searchFilePath)))
            {
                Console.WriteLine("Input and/or search files could not be found.");
                return;
            }

            //Create the first node of the tree
            Node root = new Node('\0', 0);

            string[] inputData = System.IO.File.ReadAllLines(inputFilePath);
            string[] searchData = System.IO.File.ReadAllText(searchFilePath).Split();

            //Populate tree.
            foreach(string sentence in inputData)
                root.AddSentence(root, sentence.Split());
            
            root.PrintTree("", true);

            //Print out search results.
            Console.WriteLine("Words found: {0}", root.SearchSentence(root, searchData));
        }
    }
}
