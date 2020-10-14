using CounterStrike.Models.Guns;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterStrike.Models.Players
{
    public abstract class Player : IPlayer
    {
        private string username;
        private int health;
        private int armor;
        private Gun gun;

        public Player(string username, int health, int armor, IGun gun)
        {
            this.Username = username;
            this.Health = health;
            this.Armor = armor;
            this.Gun = gun;
        }

        public string Username
        {
            get => this.username;
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerName);
                }

                this.username = value;
            }
        }

        public int Health
        {
            get => this.health;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerHealth);
                }

                this.health = value;
            }
        }

        public int Armor
        {
            get => this.armor;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerArmor);
                }

                this.armor = value;
            }
        }

        public IGun Gun
        {
            get => this.gun;
            private set
            {
                if ((Gun)value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGun);
                }

                this.gun = (Gun)value;
            }
        }

        public bool IsAlive => this.Health > 0;

        public void TakeDamage(int points)
        {
            bool isCaryOver = false;

            int caryoverPoints = 0;

            if (this.Armor > 0)
            {
                if (this.Armor >= points)
                {
                    this.Armor -= points;
                }
                else
                {
                    caryoverPoints = points - this.Armor;
                    this.Armor = 0;
                    isCaryOver = true;
                }
                
            }
            else if (this.Armor == 0)
            {
                if (this.Health >= points)
                {
                    this.Health -= points;
                }
                else
                {
                    this.Health = 0;
                }
            }

            if (isCaryOver)
            {
                this.Health -= caryoverPoints;
                if (this.Health < 0)
                {
                    this.Health = 0;
                }
                isCaryOver = false;
                caryoverPoints = 0;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name}: {this.Username}")
                .AppendLine($"--Health: {this.Health}")
                .AppendLine($"--Armor: {this.Armor}")
                .AppendLine($"--Gun: {this.Gun.Name}")
                .Append(Environment.NewLine);

            return sb.ToString().TrimEnd();
        }
    }
}
