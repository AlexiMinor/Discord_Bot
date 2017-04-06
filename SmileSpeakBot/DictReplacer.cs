using System;
using System.Collections.Generic;
using System.Text;

namespace SmileSpeakBot
{
    class DictReplacer
    {
        public Dictionary<char, string> RepDict;

        public DictReplacer(Dictionary<char, string> dictoinary)
        {
            RepDict = dictoinary;
        }

        public string Replace(string inputString)
        {
            string output = "";
            inputString = inputString.ToLower();
            foreach (char c in inputString)
            {
                output += RepDict.ContainsKey(c) ? RepDict[c] : new string(c, 1);
            }
            return output;
        }
    }
}
