using System;
using System.Collections.Generic;
using System.Text;

namespace CounterStrike.Models.Guns
{
    public class Pistol : Gun
    {
        private const int MAX_BULLETS_PER_SHOT = 1;
        public Pistol(string name, int bulletsCount) 
            : base(name, bulletsCount)
        {
        }

        public override int Fire()
        {
            int bulletsFired = 0;

            if (BulletsCount >=  MAX_BULLETS_PER_SHOT)
            {
                bulletsFired += MAX_BULLETS_PER_SHOT;
                this.BulletsCount-= MAX_BULLETS_PER_SHOT;
            }

            return bulletsFired;
        }
    }
}
