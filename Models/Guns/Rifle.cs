using System;
using System.Collections.Generic;
using System.Text;

namespace CounterStrike.Models.Guns
{
    public class Rifle : Gun
    {
        public const int MAX_BULLETS_PER_SHOT = 10;

        public Rifle(string name, int bulletsCount) 
            : base(name, bulletsCount)
        {
        }

        public override int Fire()
        {
            int bulletsFired = 0;

            if (this.BulletsCount >= MAX_BULLETS_PER_SHOT)
            {
                bulletsFired += MAX_BULLETS_PER_SHOT;
                this.BulletsCount -= bulletsFired;
            }
            

            return bulletsFired;
        }
    }
}
