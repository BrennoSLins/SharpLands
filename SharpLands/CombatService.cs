using System;
using System.Collections.Generic;
using System.Text;

namespace SharpLands
{
    public class CombatService
    {
        public Task <int> PlayerAttack(Player player, Enemies enemy, int atkDice, int defDice)
        {
            
            int damageToEnemy = player.BaseAttack + atkDice - enemy.BaseDefense + defDice;
            if (damageToEnemy < 0) damageToEnemy = 0;

            enemy.Health -= damageToEnemy;

            return Task.FromResult(damageToEnemy);

        }
    }
}
