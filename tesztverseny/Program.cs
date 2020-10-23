using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztverseny
{
    class Program
    {
        static string Alap;
        static List<Adatok> adatok = new List<Adatok>();
        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader("valaszok.txt"))
            {
                Alap = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string row = sr.ReadLine();
                    string[] r = row.Split(' ');
                    string azon = r[0];
                    string megold = r[1];


                    adatok.Add(new Adatok { Azon = azon, Megold = megold });
                }
            }

            //1.feladat
            Console.WriteLine("1.Feladat: Az adatok beolvasása");

            //2.feladat
            Console.Write("2.Feladat");
            Console.WriteLine("A vetélkedőn {0} versenyző indult.", adatok.Count);

            //3.feladat
            Console.Write("3.Feladat: A versenyző azonostója = ");
            string beker = Console.ReadLine();

            Adatok bekert = new Adatok();

            for (int i = 0; i < adatok.Count; i++)
            {
                if (adatok[i].Azon == beker)
                {
                    bekert = adatok[i];
                    Console.WriteLine("{0} \t (a versenyző válasza)", adatok[i].Megold);
                }
            }

            //4.feladat
            Console.WriteLine("4.Feladat");
            Console.WriteLine("{0} \t(a helyes megoldás)", Alap);

            for (int i = 0; i < bekert.Megold.Length; i++)
            {
                if (Alap[i] == bekert.Megold[i])
                {
                    Console.Write("+");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine("\ta versenyző helyes válaszai");

            //5.feladat
            Console.Write("5.feladat: A feladat sorszáma = ");

            int besor = Convert.ToInt32(Console.ReadLine());
            int db = 0;
            char valasz = Alap[besor - 1];
            for (int i = 0; i < adatok.Count; i++)
            {
                if (valasz == adatok[i].Megold[besor - 1])
                {
                    db++;
                }
            }
            Console.WriteLine("A feladatra {0} fő, a versenyzők {1:#.##}%-a adott helyes választ.", db, ((double)db / adatok.Count) * 100);

            //6.feladat
            Console.WriteLine("6.Feladat: A versenyzők pontszámának meghatározása");
            using (StreamWriter sw = new StreamWriter("pontok.txt"))
            {

                for (int i = 0; i < adatok.Count; i++)
                {
                    int pontok = 0;
                    for (int g = 0; g < adatok[i].Megold.Length; g++)
                    {
                        if (Alap[g] == adatok[i].Megold[g])
                        {
                            if (g >= 0 && g <= 4)
                            {
                                pontok += 3;
                            }
                            if (g >= 5 && g <= 9)
                            {
                                pontok += 4;
                            }
                            if (g >= 10 && g <= 12)
                            {
                                pontok += 5;
                            }
                            if (g == 13)
                            {
                                pontok += 6;
                            }
                        }
                    }
                    adatok[i].Pontok = pontok;
                    sw.WriteLine(adatok[i].Azon + " " + pontok);
                }
                sw.Flush();
            }

            //7.feladat
            Console.WriteLine("7.Feladat");

            List<Adatok>[] sorrend = new List<Adatok>[57];

            for (int i = 0; i < sorrend.Length; i++)
            {
                sorrend[i] = new List<Adatok>();
            }

            foreach (var i in adatok)
            {
                sorrend[i.Pontok].Add(i);
            }

            int count = 0;
            int temp = 0;
            while (count < 3)
            {
                if (sorrend[sorrend.Length - 1 - temp].Count == 0)
                {
                    temp++;
                }
                else
                {
                    for (int j = 0; j < sorrend[sorrend.Length - 1 - temp].Count; j++)
                    {
                        Console.WriteLine("{0}. helyezett ({1} pont): {2}", count + 1, sorrend[sorrend.Length - 1 - temp][j].Pontok, sorrend[sorrend.Length - 1 - temp][j].Azon);
                    }
                    temp++;
                    count++;
                }
            }


            Console.ReadKey();
        }
    }
}
