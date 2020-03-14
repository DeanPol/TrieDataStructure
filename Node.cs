using System;
using System.Collections.Generic;

namespace TrieDataStructure
{
    sealed class Node : Node_Core
    {
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
        private void AddWord(Node node, List<char> word)
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

                if (SearchWord(node, word, current_word) == true)
                {
                    wordsFound++;
                    Console.WriteLine("Found : {0}", current_word);
                }
            }
            return wordsFound;
        } 

        private bool SearchWord(Node node, List<char> word, string current_word) //recursive boolean method, these are fun!...
        {
            if (word.Count == 0 && node.isFinal == true)
                return true;

            if (word.Count == 0 && node.isFinal == false) //Search term is too short, autocomplete candidate.
            {
                AutoCompleteWord(node, current_word);
                return false;
            }

            int index = ValueExists(node, word[0]);
            if (index >= 0)
            {
                word.RemoveAt(0);
                 return (SearchWord(node.children[index], word, current_word));
            }
            else
                return false;
        }

        //This method will find existing words that contain as a prefix our current search word (assuming criterias are met).
        private void AutoCompleteWord(Node node, string current_word)
        {
            List<string> suggestions = new List<string>();
            List<char> characters = new List<char>();
            PopulateSuggestions(node, suggestions, characters);
            
            //filter through suggestions, only show words that aren't too different
            for (int i = 0; i < suggestions.Count; i++)
            {
                if ((float)current_word.Length / (float)suggestions[i].Length < 0.7) //if our search word is no more than 30% different in size.
                    suggestions.RemoveAt(i);
            }

            if (suggestions.Count > 0)
            {
                Console.Write("Instead of '{0}', did you mean '{1}' ?", current_word, current_word + suggestions[0]);
                for (int i = 1; i < suggestions.Count; i++)
                    Console.Write(" or '{0}' ?", current_word + suggestions[i]);
                Console.WriteLine();
            }
            //just to be safe...
            suggestions.Clear();
            characters.Clear();
        }

        //Depth first search. Populate list only with complete suffixes.
        private void PopulateSuggestions(Node node, List<string> suggestions, List<char> characters)
        {
            while(node.numOfChildren > 0)
            {
                for(int i = 0; i < node.numOfChildren; i++)
                {
                    characters.Add(node.children[i].value);
                    PopulateSuggestions(node.children[i], suggestions, characters);
                    characters.RemoveAt(characters.Count - 1);
                }
                return;
            }
            string suggestion = "";
            foreach (char a in characters)
                suggestion += a;
            suggestions.Add(suggestion);
        }

        //Returns the index of the child node that holds the value, -1 if value doesn't exist.
        private int ValueExists(Node node, char val) //Time complexity O(n), need to improve!
        {
            for (int i = 0; i < node.numOfChildren; i++)
            {
                if (node.children[i].value == val)
                    return i;
            }
            return -1;
        }
        //Prints our tree in a neat fashion. (Linear)
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
