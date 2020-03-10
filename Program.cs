using System;
using System.IO;

namespace TrieDataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create the first node of the tree
            Node root = new Node('\0', 0);
            string inputFilePath = @"C:\Users\Dean\dev\TrieDataStructure\data\input_small.txt";
            string searchFilePath = @"C:\Users\Dean\dev\TrieDataStructure\data\search_small.txt";

            if ((!File.Exists(inputFilePath)) || (!File.Exists(searchFilePath)))
            {
                Console.WriteLine("One or both of the files could not be found");
                return;
            }

            string[] inputData = System.IO.File.ReadAllLines(inputFilePath);
            string[] searchData = System.IO.File.ReadAllText(searchFilePath).Split();

            foreach(string sentence in inputData)
                root.AddSentence(root, sentence.Split());
            
            root.PrintTree("", true);

            Console.WriteLine("Words found: {0}", root.SearchSentence(root, searchData));
            foreach (string word in root.wordsMatched)
                Console.WriteLine($"{word}");
        }
    }
}
