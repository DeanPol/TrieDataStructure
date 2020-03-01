namespace TrieDataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create the first node of the tree
            Node root = new Node('\0', 0);

            string[] testString = { "bad", "sad", "sam", "bass", "bad", "crybaby"};

            root.AddSentence(root, testString);
            root.PrintTree("", true);
        }
    }
}
