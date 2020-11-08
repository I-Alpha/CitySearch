using CitySearch.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CitySearch.Trie
{

    public class CityFinderTrie : ICityFinder
    {
        static ICityResult cityresults;
        private static ICollection<string> dataset = new List<string>();
        private static Trie trie;
        public static ICollection<string> Dataset
        {
            get { return dataset; }
            //converts items in collection to uppercase and store
            set
            {
                dataset = value.Select(v => v).ToList();
                trie = new Trie(dataset);
            }
        }


        public ICityResult Search(string searchString = null)
        {
            //create  Cityresult  object
            cityresults = new CityResult();
            SetThisResult(trie.GetSuggestions(searchString));             
            return cityresults;
        }
        private void SetThisResult(object[] result) {
            cityresults.NextCities = (ICollection<string>)result.First();
            if (cityresults.NextCities.Count > 1)
                cityresults.NextLetters = (ICollection<string>)result.ElementAt(1);
        }


        private class Trie
        {   //This implementation of Trie search algorith taken was taken and modified from: https://github.com/TomGullen/C-Sharp-Trie/tree/master
            //MIT License : https://github.com/TomGullen/C-Sharp-Trie/blob/master/LICENSE
            private class Node
            {

                public bool End { get; set; }
                public Dictionary<char, Node> Nodes { get; private set; }
                public Node ParentNode { get; private set; }
                public char C { get; private set; }

                /// <summary>
                /// String word represented by this node
                /// </summary>
                public string Word
                {
                    get
                    {
                        var b = new StringBuilder();
                        b.Insert(0, C.ToString(CultureInfo.InvariantCulture));
                        var selectedNode = ParentNode;
                        while (selectedNode != null)
                        {
                            b.Insert(0, selectedNode.C.ToString(CultureInfo.InvariantCulture));
                            selectedNode = selectedNode.ParentNode;
                        }
                        return b.ToString();
                    }
                }


                public Node(Node parent, char c)
                {
                    C = c;
                    ParentNode = parent;
                    End = false;
                    Nodes = new Dictionary<char, Node>();
                }

                /// <summary>
                /// Return list of end nodes under this node
                /// </summary>
                public IEnumerable<Node> EndNodes(char? ignoreChar = null)
                {
                    var r = new List<Node>();
                    if (End) r.Add(this);
                    foreach (var node in Nodes.Values)
                    {
                        if (ignoreChar != null && node.C == ignoreChar) continue;
                        r = r.Concat(node.EndNodes()).ToList();
                    }
                    return r;
                }
            }

            private Node TopNode_ { get; set; }
            private Node TopNode
            {
                get
                {
                    if (TopNode_ == null) TopNode_ = new Node(null, ' ');
                    return TopNode_;
                }
            }
            private bool CaseSensitive { get; set; }

            /// <summary>
            /// Get list of all words in trie that start with
            /// </summary>
            public object[] GetSuggestions(string wordStart, int fetchMax = 100)
            {
                if (fetchMax <= 0) throw new Exception("Fetch max must be positive integer.");

                wordStart = NormaliseWord(wordStart);

                var r = new HashSet<string>();

                var selectedNode = TopNode;
                foreach (var c in wordStart)
                {
                    // Nothing starting with this word
                    if (!selectedNode.Nodes.ContainsKey(c)) return new[] { r.ToList(), new List<string>() { } };
                    selectedNode = selectedNode.Nodes[c];
                }

                // Get end nodes for this node
                {
                    var endNodes = selectedNode.EndNodes().Take(fetchMax);
                    foreach (var node in endNodes)
                    {
                        r.Add(node.Word);
                    }
                }

                // Go up a node if not found enough suggestions
                if (r.Count < fetchMax)
                {
                    var parentNode = selectedNode.ParentNode;
                    if (parentNode != null)
                    {
                        var leftToFetch = fetchMax - r.Count;
                        var endNodes = parentNode.EndNodes(selectedNode.C).Take(leftToFetch);
                        foreach (var node in endNodes)
                        {
                            r.Add(node.Word);
                        }
                    }
                }
                //get subsequent char of each matched superstring
                var Endletters = r.Select(x => x[wordStart.Length].ToString()).ToList();

                return new[] { r.ToList(), Endletters };
            }

            /// <summary>
            /// Initialise instance of trie with set of words
            /// </summary>
            public Trie(IEnumerable<string> words, bool caseSensitive = false)
            {
                CaseSensitive = caseSensitive;
                foreach (var word in words)
                {
                    AddWord(word);
                }
            }

            /// <summary>
            /// Add a single word to the trie
            /// </summary>
            public void AddWord(string word)
            {
                word = NormaliseWord(word);
                var selectedNode = TopNode;

                for (var i = 0; i < word.Length; i++)
                {
                    var c = word[i];
                    if (!selectedNode.Nodes.ContainsKey(c))
                    {
                        selectedNode.Nodes.Add(c, new Node(selectedNode, c));
                    }
                    selectedNode = selectedNode.Nodes[c];
                }
                selectedNode.End = true;
            }

            /// <summary>
            /// Normalise word for trie
            /// </summary>
            private string NormaliseWord(string word)
            {
                if (String.IsNullOrWhiteSpace(word)) word = String.Empty;
                word = word.Trim();
                if (!CaseSensitive) word = word.Trim();

                return word;
            }

            /// <summary>
            /// Does this word exist in this trie?
            /// </summary>
            public bool IsWordInTrie(string word)
            {
                word = NormaliseWord(word);
                if (String.IsNullOrWhiteSpace(word)) return false;
                var selectedNode = TopNode;
                foreach (var c in word)
                {
                    if (!selectedNode.Nodes.ContainsKey(c)) return false;
                    selectedNode = selectedNode.Nodes[c];
                }
                return selectedNode.End;
            }
        }
    }
}

