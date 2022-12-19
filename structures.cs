using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class structures
    {
    }

    public class candidate
    {
        public int id { get; set; }         // number in list
        public string name { get; set; }
        public int nv { get; set; } = 0;        // number of voices
        public int status { get; set; } = 0;    // 1 - deleted
        public string msg { get; set; }
    }

    public class block
    {
        public block()
        {
            candidates = new List<candidate>();
            blanks = new List<string>();
            papers = new List<string[]>();
        }

        public int ibeg { get; set; }         //start
        public int iend { get; set; }         //finish
        public int nc { get; set; }             // number of candidates
        public int nb { get; set; }             // number of blanks in this block
        public List<candidate> candidates { get; set; }
        public List<string> blanks { get; set; }
        public List<string[]> papers { get; set; }
        public List<int> voice { get; set; }

        public string msg { get; set; } = "";

        public void set_data(List<string> lines, int i)
        {
            var b = this;
            b.iend = i - 1;
            b.nb = b.iend - (b.ibeg + b.nc);
            b.candidates = (lines.GetRange(b.ibeg + 1, b.nc)).Select(e => new candidate() { name = e }).ToList();
            b.candidates.ForEach(e => e.id = b.candidates.IndexOf(e) + 1);
            b.blanks = lines.GetRange(b.ibeg + b.nc + 1, b.nb);
            b.blanks.ForEach(e => b.papers.Add(e.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)));
        }

    }

}
