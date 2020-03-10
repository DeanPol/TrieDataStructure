using System;
using System.IO;

namespace TrieDataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            //Define data we are working with.
            string inputFilePath = @"C:\Users\Dean\dev\TrieDataStructure\data\input_small.txt";
            string searchFilePath = @"C:\Users\Dean\dev\TrieDataStructure\data\search_small.txt";

            if ((!File.Exists(inputFilePath)) || (!File.Exists(searchFilePath)))
            {
                Console.WriteLine("One or both of the files could not be found");
                return;
            }

            //Create the first node of the tree
            Node root = new Node('\0', 0);

            string[] inputData = System.IO.File.ReadAllLines(inputFilePath);
            string[] searchData = System.IO.File.ReadAllText(searchFilePath).Split();

            //Our populate tree method.
            foreach(string sentence in inputData)
                root.AddSentence(root, sentence.Split());
            
            root.PrintTree("", true);

            //Our search method.
            Console.WriteLine("Words found: {0}", root.SearchSentence(root, searchData));
        }
    }
}
