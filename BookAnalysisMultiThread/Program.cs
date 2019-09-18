using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;



namespace BookAnalysisMultiThread
{
    class Program
    {
        /*
         *This program reads through a .txt file and tracks the number of appearances of each word, and the length of each word. 
         *Before the program ends it outputs the longest, shortest, and most common words, allowing for ties, by outputting all of the tying words.
         *
         *
         * Next Step, streamline code, and make derivitave program which utilizes multithreading to speed the process up.
         */



        static void Main(string[] args)
        {
            
            WordTracker WT = new WordTracker();
            string[] lines = System.IO.File.ReadAllLines( @"..\..\Sample.txt");

            WT.processArray(lines);


            WT.calculateWords();
            WT.printWordTracker();
          
            Console.Read();
        }

        
    }


}
