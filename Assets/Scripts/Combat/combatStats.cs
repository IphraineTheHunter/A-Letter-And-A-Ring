using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    class CombatStats
    {
        //Stable stats
  		public static int finesse = 0; //{get; set;} = 0;
  		public int might = 0;
  		public int wit = 0;
  		public int perceptiveness = 0;
  		public int toughness = 0;
  		public int willpower = 0;
        public int focusLimit()
        {
            return 20;
        }

        //Volitile stats
        public int focus = 0;
        public int bleed = 0;
        public int pain = 0;
        public List<Injury> injuries = new List<Injury>();

		//private return new CombatStats();
    }
}
