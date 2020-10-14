using CounterStrike.Core.Contracts;
using CounterStrike.Models.Guns;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Maps;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Repositories;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounterStrike.Core
{
    public class Controller : IController
    {
        private GunRepository guns;
        private PlayerRepository players;
        private IMap map;

        public Controller()
        {
            this.guns = new GunRepository();
            this.players = new PlayerRepository();
            this.map = new Map();
        }

        public string AddGun(string type, string name, int bulletsCount)
        {
            if (type == "Pistol")
            {
                this.guns.Add(new Pistol(name, bulletsCount));
            }
            else if (type == "Rifle")
            {
                this.guns.Add(new Rifle(name, bulletsCount));
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunType);
            }

            return String.Format(OutputMessages.SuccessfullyAddedGun, name);
        }

        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {
            IGun gunToAdd = this.guns.FindByName(gunName);
            if (gunToAdd == null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }
            if (type == "Terrorist")
            {
                this.players.Add(new Terrorist(username, health, armor, gunToAdd));
            }
            else if (type == "CounterTerrorist")
            {
                this.players.Add(new CounterTerrorist(username, health, armor, gunToAdd));
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerType);
            }

            return String.Format(OutputMessages.SuccessfullyAddedPlayer, username);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var player in players.Models.OrderBy(x => x.GetType().Name).ThenByDescending(x => x.Health)
                .ThenBy(x => x.Username))
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string StartGame()
        {
            return map.Start((ICollection<IPlayer>)this.players.Models);
        }
    }
}
