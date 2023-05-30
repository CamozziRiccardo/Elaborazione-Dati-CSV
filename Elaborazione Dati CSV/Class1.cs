using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Data.SqlTypes;
using System.Windows.Forms;

namespace Elaborazione_Dati_CSV
{
    internal class funzioni
    {
        public void RandeLog(string filename, string filename1, char delim)
        {
            Random r = new Random();
            StreamWriter sw = new StreamWriter(filename, append: false);
            StreamReader sr = new StreamReader(filename1);
            string s = sr.ReadLine();
            int i = 0;
            while (s != null)
            {
                if (i == 0)
                {
                    sw.WriteLine(s + delim + "MioValore" + delim + "Cancellazione logica");
                }
                else
                {
                    sw.WriteLine(s + delim + r.Next(10, 20) + delim + "true");
                }
                s = sr.ReadLine();
                i++;
            }
            sr.Close();
            sw.Close();
        }
        public int contaCampi(string filename, char delim)
        {
            StreamReader sr = new StreamReader(filename);
            string s = sr.ReadLine();
            sr.Close();
            return s.Split(delim).Length;
        }

        public string Long(string filename1, char delim)
        {
            string longest = "";
            StreamReader sr = new StreamReader(filename1);
            string s;
            int i = 0;
            string[] temp = new string[contaCampi(filename1, delim)];
            while ((s = sr.ReadLine()) != null)
            {
                temp = s.Split(delim);
                if (i != 0)
                {
                    for (int j = 0; j < temp.Length; j++)
                    {
                        if (longest.Length < temp[j].Length)
                        {
                            longest = temp[j];
                        }
                    }
                }
                i++;
            }
            sr.Close();

            return longest;
        }

        public void spaziatura(string filename, string filename1)
        {
            string s;
            StreamWriter sw = new StreamWriter(@"temporaneo.csv");
            StreamReader sr = new StreamReader(filename1);
            int i = 0;
            while ((s = sr.ReadLine()) != null)
            {
                if (i == 0)
                {
                    sw.WriteLine(s.PadRight(200));
                }
                else
                {
                    sw.WriteLine(s);
                }
                i++;
            }
            sw.Close();
            sr.Close();
            File.Replace(@"temporaneo.csv", filename, @"backup.csv");
            File.Delete(@"backup.csv");
        }

        public void TriRec(string filename, char delim, ListView list)
        {
            list.Clear();
            string s;
            StreamReader sr = new StreamReader(filename);
            int i = 0;
            while ((s = sr.ReadLine()) != null)
            {
                if (i != 0)
                {
                    string[] temp = s.Split(delim);
                    list.Items.Add("REGIONE: " + temp[2] + delim + "LONGITUDINE: " + temp[7] + delim + "ATITUDINE: " + temp[8]);
                }
                i++;
            }
            sr.Close();
        }

        public void NewRec(string filename, char delim, string[] temp)
        {
            StreamWriter sw = new StreamWriter(filename, append: true);
            for (int i = 0; i < temp.Length; i++)
            {
                if (i + 1 != temp.Length)
                {
                    sw.Write(temp[i] + delim);
                }
                else
                {
                    sw.WriteLine(temp[i]);
                }
            }
            sw.Close();
        }

        public void search(string filename, char delim, string name)
        {
            int pos;
            StreamReader sr = new StreamReader(filename);
            string s;
            name = name.ToUpper();
            int i = 0;
            int con = -1;
            while ((s = sr.ReadLine()) != null)
            {
                string[] split = s.Split(delim);
                if (i != 0)
                {
                    if (split[2] == name)
                    {
                        pos = i;
                        MessageBox.Show("Antenna trovata in posizione " + i);
                        con = 0;
                    }
                }
                i++;
            }
            if (con == -1)
            {
                MessageBox.Show("Nessuna antenna trovata nella regione inserita");
            }
            sr.Close();
        }

        public void mod(string filename, char delim, string oldcitt, string oldreg, string newcitt, string newreg)
        {
            StreamReader sr = new StreamReader(filename);
            StreamWriter sw = new StreamWriter(@"temp.csv");
            int i = 0;
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                if (i != 0)
                {
                    string[] split = s.Split(delim);
                    if (split[2] == oldcitt && split[1] == oldreg)
                    {
                        split[2] = newcitt;
                        split[3] = newreg;
                        for (int j = 0; j < split.Length; j++)
                        {
                            if (j == split.Length - 1) s = split[j];
                            else s = split[j] + delim;
                        }
                    }
                    sw.WriteLine(s);
                }
                i++;
            }
            sw.Close();
            sr.Close();
            File.Replace(@"temp", filename, @"backup.csv");
            File.Delete(@"backup.csv");
        }

        public void canc(string filename, char delim, string citt, string reg)
        {
            StreamReader sr = new StreamReader(filename);
            StreamWriter sw = new StreamWriter(@"temp.csv");
            int i = 0;
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                if (i != 0)
                {
                    string[] split = s.Split(delim);
                    if (split[2] == citt && split[1] == reg)
                    {
                        split[10] = "false";
                        for (int j = 0; j < split.Length; j++)
                        {
                            if (j == split.Length - 1) s = split[j];
                            else s = split[j] + delim;
                        }
                    }
                    sw.WriteLine(s);
                }
                i++;
            }
            sr.Close();
            sw.Close();
            File.Replace(@"temp", filename, @"backup.csv");
            File.Delete(@"backup.csv");
        }
    }
}