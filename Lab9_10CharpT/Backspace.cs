using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_10CharpT
{
    internal class Backspace : ArrayList, IEnumerable, ICloneable
    {
        public void Process(string input)
        {
            Clear();
            foreach (char c in input)
            {
                if (c == '#')
                {
                    if (Count > 0)
                        RemoveAt(Count - 1);
                }
                else
                {
                    Add(c);
                }
            }
        }

        public string GetResult()
        {
            char[] result = new char[Count];
            CopyTo(result);
            return new string(result);
        }

        public object Clone()
        {
            Backspace clone = new Backspace();
            foreach (var item in this)
                clone.Add(item);
            return clone;
        }
    }
}
