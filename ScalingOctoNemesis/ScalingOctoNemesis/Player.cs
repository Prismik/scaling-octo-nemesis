using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScalingOctoNemesis
{
    class Player
    {
        public string Name  { get; set; }
        public Color Color  { get; set; }
        public string Civ   { get; set; }
        public int Team     { get; set; }
        public Player(string n, Color c, string civ)
        {
            Name = n;
            Color = c;
            Civ = civ;
        }
    }
}
