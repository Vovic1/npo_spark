using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal static class utils
    {
        internal static List<block> markBlocks(List<string> lines)
        {
            var nl = lines.Count;
            if (nl < 3) return null;

            List<block> blocks = new List<block>();
            block b = null;

            for (int i = 2; i < nl; i++)
            {
                var s = lines[i];

                if (s.Equals(""))
                {
                    if (b == null) continue;
                    b.set_data(lines, i);
                    blocks.Add(b);
                    b = null;
                    continue;
                }

                if (b == null)
                {
                    b = new block();
                    b.ibeg = i;
                    b.nc = int.Parse(s);
                }
            }

            if (b != null)
            {
                b.set_data(lines, nl);
                blocks.Add(b);
            }

            return blocks;
        }


        //initial voice calculation
        internal static void calculate1(block b)
        {
            for (int j = 0; j < b.candidates.Count; j++)
            {
                if (b.candidates[j].status > 0) continue;
                var c = (j + 1).ToString().Trim();
                var n = b.papers.Count(e => e[0].Equals(c));
                b.candidates[j].nv = n;
            }
        }

        //voice recalculation
        internal static List<candidate> calculate2(block b, int bnum)
        {
            try
            {
                var cc = b.candidates.Where(e => e.status < 1).ToList();
                cc.Sort((e1, e2) => { if (e1.nv > e2.nv) return 1; else if (e1.nv < e2.nv) return -1; else return 0; });

                var c1 = cc[0];
                var c3 = cc[cc.Count - 1];

                if (c3.nv > b.nb / 2) { var res = new List<candidate>(); res.Add(c3); return res; }
                if (c1.nv == c3.nv) { var res = new List<candidate>(); res.AddRange(cc); return res; }

                int n1, n2;
                c1.status = 1;              // deleted

                for (int i = 0; i < b.nb; i++)
                {
                    n1 = int.Parse(b.papers[i][0]);
                    if (n1 != c1.id) continue;

                    n2 = int.Parse(b.papers[i][1]);
                    var c = b.candidates.Find(e => e.id == n2);
                    if (c.status < 1) c.nv++;
                }

                return null;
            }
            catch
            {
                b.msg = string.Format("data eror in block numberf {0}", bnum);
                return null;
                //throw new System.Exception(string.Format("data eror in block numberf {0}", bnum));
            }
        }

    }
}
