//using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class RandomCustom
    {
        public virtual int RollXdY(int x, int y)
        {
            int sum = 0;
            for (int i = 0; i < x; i++)
            {
                sum += Random.Range(0, y);
            }
            return sum;
        }
    }
}
