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

        public bool isFinal { get; set; }

        public List<string> wordsMatched = new List<string>();

        //Constructor
        public Node(char value, int numOfChildren)
        {
            this.value = value;
            this.numOfChildren = numOfChildren;
            children = new List<Node>();
            isFinal = false;
        }

        //==========Methods==========
        public void AddSentence(Node node, string[] sentence)
        {
            foreach (string current_word in sentence)
            {
                //Create a list of characters of our current word
                List<char> word = new List<char>();
                foreach (char a in current_word)
                    word.Add(a);

                AddWord(node, word);
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
                else
                    node.children[0].isFinal = true;
            }
            else //node has children, search if value already exists.
            {
                int index = ValueExists(node, word[0]);

                if (index >= 0) //one of our children already holds the value.
                {
                    word.RemoveAt(0);
                    if (word.Count > 0)
                        AddWord(node.children[index], word);
                    else
                        node.children[index].isFinal = true;
                }
                else //none of our children hold the value - add a new child with value.
                {
                    node.children.Add(new Node(word[0], 0));
                    node.numOfChildren++;
                    word.RemoveAt(0);
                    if (word.Count > 0)
                        AddWord(node.children[node.numOfChildren - 1], word);
                    else
                        node.children[node.numOfChildren - 1].isFinal = true;
                }
            }
        }

        public  int SearchSentence(Node node, string[] sentence) //very similar to AddSentence
        {
            int wordsFound = 0;
            foreach (string current_word in sentence)
            {
                List<char> word = new List<char>();
                foreach (char a in current_word)
                    word.Add(a);
                if (SearchWord(node, word) == true)
                {
                    wordsFound++;
                    wordsMatched.Add(current_word);
                }
            }

            return wordsFound;
        } 

        public bool SearchWord(Node node, List<char> word)
        {
            if (word.Count == 0 && node.isFinal == true)
                return true;

            if (word.Count == 0 && node.isFinal == false)
                return false;

            int index = ValueExists(node, word[0]);
            if (index >= 0)
            {
                word.RemoveAt(0);
                 return (SearchWord(node.children[index], word));
            }
            else
                return false;
        } ////recursive boolean method, these are fun!...

        //Returns the index of the child node that holds the value, -1 if value doesn't exist.
        public int ValueExists(Node node, char val)
        {
            for (int i = 0; i < node.numOfChildren; i++)
            {
                if (node.children[i].value == val)
                    return i;
            }
            return -1;
        }
        //Prints our tree in a neat fashion.
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
