using System;
using System.Collections.Generic;
using System.Text;

namespace SharpLands
{
    public class Enemies
    {

        public string Name { get; set; }
        public int Health { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        

    }

    public class Slime : Enemies
    {
        public Slime()
        {
            Name = "Slime";
            Health = 30;
            BaseAttack = 5;
            BaseDefense = 2;
        }
    }
    
}
