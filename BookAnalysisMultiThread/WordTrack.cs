using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnalysisMultiThread
{
    public class WordTrack
    {
        private string word;
        private int num;
        private int length;
        

        public WordTrack(string word, int len)
        {
            this.word = word;
            num = 1;
            length = len;
        }

        public void printWordTrack()
        {
            Console.Write(word + ": " + num + "   Length: " + length + "\n");
        }

        public int getLength()
        {
            return length;
        }

        public string getWord()
        {
            return word;
        }

        public int getNum()
        {
            return num;
        }

        public void addNum()
        {
            num++;
        }

        public void addNum(int i)
        {
            num += i;
        }
    }
}
