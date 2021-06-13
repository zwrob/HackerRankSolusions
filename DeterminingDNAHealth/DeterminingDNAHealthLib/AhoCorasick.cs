using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DeterminingDNAHealthLib.AhoCorasick
{
    //https://www.geeksforgeeks.org/aho-corasick-algorithm-pattern-searching/
    // na podstawie https://github.com/pdonald/aho-corasick/blob/master/AhoCorasick.cs
    public class Trie
    {
        private readonly Node root = new Node();

        public void Add(string value)
        {
            Add(value, value);
        }
        public void Add(IEnumerable<char> word, string value)
        {
            // start at the root
            var node = root;

            // build a branch for the word, one letter at a time
            // if a letter node doesn't exist, add it
            foreach (char c in word)
            {
                string str = new string(new char[] { c });
                var child = node[str];

                if (child == null)
                {
                    child = node[str] = new Node(str, node);
                }

                node = child;
            }

            // mark the end of the branch
            // by adding a value that will be returned when this word is found in a text
            node.Values.Add(value);
        }

        public void Build()
        {
            var queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                // visit children
                foreach (var child in node)
                    queue.Enqueue(child);

                // fail link of root is root
                if (node == root)
                {
                    root.Fail = root;
                    continue;
                }

                var fail = node.Parent.Fail;

                while (fail[node.Word] == null && fail != root)
                    fail = fail.Fail;

                node.Fail = fail[node.Word] ?? root;
                if (node.Fail == node)
                    node.Fail = root;
            }
        }

        public IEnumerable<string> Find(IEnumerable<char> text)
        {
            var node = root;

            foreach (char c in text)
            {
                string str = new string(new char[] { c });
                while (node[str] == null && node != root)
                    node = node.Fail;

                node = node[str] ?? root;

                for (var t = node; t != root; t = t.Fail)
                {
                    foreach (string value in t.Values)
                        yield return value;
                }
            }
        }
    }

    public class Node : IEnumerable<Node>
    {
        private readonly string word;
        private readonly Node  parent;
        private readonly Dictionary<string, Node> children = new Dictionary<string, Node>();
        private readonly List<string> values = new List<string>();

        public string Word
        {
            get { return word; }
        }

        public Node Parent
        {
            get { return parent; }
        }


        public Node Fail
        {
            get;
            set;
        }

        /// <summary>
        /// Children for this node.
        /// </summary>
        /// <param name="c">Child word.</param>
        /// <returns>Child node.</returns>
        public Node this[string c]
        {
            get { return children.ContainsKey(c) ? children[c] : null; }
            set { children[c] = value; }
        }


        public Node()
        {
        }

        public Node(string word, Node parent)
        {
            this.word = word;
            this.parent = parent;
        }

        public List<string> Values
        {
            get { return values; }
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return children.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return Word.ToString();
        }
    }


    //AhoCorasick


    // poszukac Algorytm Aho-Corasick
    // https://github.com/pdonald/aho-corasick
}
