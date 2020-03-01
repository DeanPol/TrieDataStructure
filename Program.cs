using System;

namespace TrieDataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create the first node of the tree
            Node root = new Node('\0', 0);

            string[] inputString = { "bad", "sad", "sam", "bass", "bad", "crybaby"};
            string[] searchString = { "bass", "never", "random", "sad", "ba", "crybab", "saman", "bas", "sam", "sad" };

            root.AddSentence(root, inputString);
            root.PrintTree("", true);

            Console.WriteLine("Words found: {0}", root.SearchSentence(root, searchString));
            
        }
    }
}
