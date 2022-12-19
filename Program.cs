using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var f = args.Length < 1 ? @"..\..\1.txt" : args[1];
            var lines = File.ReadAllLines(f);

            var blocks = utils.markBlocks(lines.ToList());
            var nb = int.Parse(lines[0]);

            for (int i = 0; i < nb; i++)
            {
                block blo = blocks[i];
                List<candidate> cc = null;
                utils.calculate1(blo);

                while (null == (cc = utils.calculate2(blo, i + 1)))
                {
                    if (blo.msg.Length > 0) Console.WriteLine(blo.msg);
                }

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("candidate count = {0}", cc.Count());
                foreach (var c in cc)
                    Console.WriteLine("name = {0}, voices = {1}", c.name, c.nv);

            }
            Console.ReadKey();
        }
    }
}
