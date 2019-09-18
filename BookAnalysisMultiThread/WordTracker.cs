using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BookAnalysisMultiThread {
    public class WordTracker
    {


        private List<WordTrack> wordTr = new List<WordTrack>();
        private List<WordTrack> shortestWords = new List<WordTrack>();
        private List<WordTrack> longestWords = new List<WordTrack>();
        private List<WordTrack> commonWords = new List<WordTrack>();


        char[] separators = new char[] { ' ', ',', '.', '"', '/', '(', ')', '-', '_', ':', ';' };

        private long wordCount = 0;

        Mutex mut = new Mutex();

        public WordTracker()
        {

        }

        public void processArray(string[] lines)
        {

            string[] firstHalf = lines.Take(lines.Length / 2).ToArray();
            string[] secondHalf = lines.Skip(lines.Length / 2).ToArray();

            Thread th1 = new Thread( () => processThread (firstHalf));
            Thread th2 = new Thread(() => processThread(secondHalf));

            th1.Start();
            th2.Start();

            th1.Join();
            th2.Join();
        }

        public void processThread(string[] lines)
        {
            WordTracker temp = new WordTracker(); 
            for (int i = 0; i < lines.Length; i++)
            {
                int j = 0;
                string[] words = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                while (j < words.Length)
                {
                    temp.addWordTrack(words[j].ToLower());
                    j++;
                }
            }
            mut.WaitOne();
            combineWordTracker(temp);
            mut.ReleaseMutex();
        }

        public void combineWordTracker(WordTracker WT1)
        {
            foreach (WordTrack word in WT1.getWordTr()) {
                addWordTrack(word);
            }

            wordCount = wordCount + WT1.getWordCount();

        }

        public void addWordTrack(string word) {

            wordCount++;

            foreach (WordTrack wordT in wordTr)
            {
                if (word == wordT.getWord())
                {
                    wordT.addNum();
                    return;
                }

            }

            wordTr.Add(new WordTrack(word, word.Length));

        }
        public void addWordTrack(WordTrack word) {
            foreach (WordTrack wordT in wordTr)
            {
                if (word.getWord() == wordT.getWord())
                {
                    wordT.addNum(word.getNum());
                    return;
                }
            }
            wordTr.Add(word);
        }

        public void calculateWords()
        {
            foreach (WordTrack word in wordTr)
            {
                findLongest(word);
                findShortest(word);
                findCommon(word);
            }
        }

        public void printWordTracker()
        {
            Console.Write(wordCount);
            Console.Write("\nLongest Words:\n");
            foreach(WordTrack word in longestWords)
            {
                word.printWordTrack();
            }
            Console.Write("\nShortest Words:\n");
            foreach (WordTrack word in shortestWords)
            {
                word.printWordTrack();
            }
            Console.Write("\nMost Common Words:\n");
            foreach (WordTrack word in commonWords)
            {
                word.printWordTrack();
            }
        }

        public long getWordCount()
        {
            return wordCount;
        }

        public List<WordTrack> getWordTr()
        {
            return wordTr;
        }

        public void findLongest(WordTrack word)
        {
            if(longestWords.FirstOrDefault() != null)
            {
                int l = word.getLength();
                if(l > longestWords.First().getLength())
                {
                    longestWords.Clear();
                    longestWords.Add(word);
                }
                else if (l == longestWords.First().getLength())
                {
                    longestWords.Add(word);
                }
                return;
            }
            longestWords.Add(word);
        }

        public void findShortest(WordTrack word)
        {
            if (shortestWords.FirstOrDefault() != null)
            {
                int s = word.getLength();
                if (s < shortestWords.First().getLength())
                {
                    shortestWords.Clear();
                    shortestWords.Add(word);
                }
                else if (s == shortestWords.First().getLength())
                {
                    shortestWords.Add(word);
                }
                return;
            }
            shortestWords.Add(word);
        }

        public void findCommon(WordTrack word)
        {
            if (commonWords.FirstOrDefault() != null)
            {
                int n = word.getNum();
                if(n > commonWords.First().getNum())
                {
                    commonWords.Clear();
                    commonWords.Add(word);
                }
                else if (n == commonWords.First().getNum())
                {
                    commonWords.Add(word);
                }
                return;
            }
            commonWords.Add(word);
        }

    }
}