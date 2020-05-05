using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Server
{
    class Card
    {
        public string cardName;             // Card's name.
        public int cost;                    // Card's cost.
        public string type;                 // Card's type.
        public int spellDamage;             // Card's spell damage (Effect damage).
        public int attack;                  // Card's attack.
        public int life;                    // Card's life.
        public List<string> scripts;        // Names list of scripts needed for card.
        public bool canAttack = false;

        public Card(string _cardName)
        {
            string _path =  System.IO.Path.GetFullPath(@"..\..\..\CardAssets\") + _cardName + ".asset";
            if (File.Exists(_path))
            {
                cardName = _cardName;
                using (StreamReader file = new StreamReader(_path))
                {
                    int counter = 0;
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        if (ln.Contains("cost"))
                        {
                            cost = int.Parse(GetLineString(ln));
                        } 
                        else if (ln.Contains("type") && !ln.Contains("m_Script"))
                        {
                            type = GetLineString(ln);
                        }
                        else if (ln.Contains("spellDamage"))
                        {
                            spellDamage = int.Parse(GetLineString(ln));
                        }
                        else if (ln.Contains("attack"))
                        {
                            attack = int.Parse(GetLineString(ln));
                        }
                        else if (ln.Contains("life"))
                        {
                            life = int.Parse(GetLineString(ln));
                        }
                        else if (ln.Contains("scripts"))
                        {
                            scripts = new List<string>();
                            while((ln = file.ReadLine()) != null)
                            {
                                scripts.Add(ln);
                            }
                        }
                        counter++;
                    }
                }
            }   
        }

        private static string GetLineString(string ln)
        {
            int found = ln.IndexOf(": ");
            string tmp = ln.Substring(found + 2);
            return tmp;
        }
    }
}
