using System;
using System.Collections.Generic;

namespace TrieDataStructure
{
    class Node
    {
        //Fields
        public char value { get; set; }
        public List<Node> children { get; set; }
        public int numOfChildren { get; set; }

        //Constructor
        public Node(char value, int numOfChildren)
        {
            this.value = value;
            this.numOfChildren = numOfChildren;
            children = new List<Node>();
        }

        //Methods
        public void AddSentence(Node root, string[] testString)
        {
            foreach (string current_word in testString)
            {
                //Create a list of characters of our current word
                List<char> word = new List<char>();
                foreach (char a in current_word)
                    word.Add(a);

                AddWord(root, word);
            }
        }
        public void AddWord(Node node, List<char> word)
        {
            //if our node has no children, create a child and assign value to it.
            if (node.numOfChildren == 0)
            {
                node.children.Add(new Node(word[0], 0));
                node.numOfChildren++;
                word.RemoveAt(0);
                if (word.Count > 0)
                    AddWord(node.children[0], word);
            }
            else //node has children, search if value already exists.
            {
                int index = ValueExists(node, word[0]);

                if (index >= 0) //one of our children already holds the value.
                {
                    word.RemoveAt(0);
                    if (word.Count > 0)
                        AddWord(node.children[index], word);

                }
                else //none of our children hold the value - add a new child with value.
                {
                    node.children.Add(new Node(word[0], 0));
                    node.numOfChildren++;
                    word.RemoveAt(0);
                    if (word.Count > 0)
                        AddWord(node.children[node.numOfChildren - 1], word);
                }
            }
        }

        //Returns the index of the value if it exists, -1 othewise.
        public static int ValueExists(Node node, char val)
        {
            for (int i = 0; i < node.children.Count; i++)
            {
                if (node.children[i].value == val)
                    return i;
            }
            return -1;
        }
        public void PrintTree(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            Console.WriteLine(value);

            for (int i = 0; i < children.Count; i++)
                children[i].PrintTree(indent, i == children.Count - 1);
        }
    }
}
