using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounterStrike.Models.Maps
{
    public class Map : IMap
    {
        public Map()
        {
        }

        public string Start(ICollection<IPlayer> players)
        {
            ICollection<IPlayer> terrorists = players
                .Where(p => p.GetType().Name == "Terrorist").ToArray();
            ICollection<IPlayer> counterTerrorists = players
                .Where(p => p.GetType().Name == "CounterTerrorist").ToArray();
            IGun currentWeapon;

            while (terrorists.Any(x => x.IsAlive == true) && counterTerrorists.Any(x => x.IsAlive == true))
            {
                foreach (IPlayer terrorist in terrorists.Where(p => p.IsAlive && p.Gun.BulletsCount > 0))
                {
                    currentWeapon = terrorist.Gun;

                    foreach (IPlayer counterTerrorist in counterTerrorists.Where(p => p.IsAlive))
                    {
                        if (currentWeapon.BulletsCount > 0)
                        {
                            counterTerrorist.TakeDamage(currentWeapon.Fire());
                        }
                    }
                }

                foreach (IPlayer counterTerrorist in counterTerrorists.Where(p => p.IsAlive && p.Gun.BulletsCount > 0))
                {
                    currentWeapon = counterTerrorist.Gun;

                    foreach (IPlayer terrorist in terrorists.Where(p => p.IsAlive))
                    {
                        if (currentWeapon.BulletsCount > 0)
                        {
                            terrorist.TakeDamage(currentWeapon.Fire());
                        }
                    }
                }
            }
            if (counterTerrorists.All(ct => ct.IsAlive == false))
            {
                return "Terrorist wins!";
            }
            else
            {
                return "Counter Terrorist wins!";
            }
        }
    }
}
