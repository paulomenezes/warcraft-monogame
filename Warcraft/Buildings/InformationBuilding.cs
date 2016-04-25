﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Util;

namespace Warcraft.Buildings
{
    class InformationBuilding : Information
    {
        public Util.Units Creates;

        public InformationBuilding(string name, int hitPoints, int costGold, int costFood, Util.Units creates, int buildTime)
        {
            Name = name;

            HitPoints = hitPoints;

            CostGold = costGold;
            CostFood = costFood;
            Creates = creates;
            BuildTime = buildTime;
        }
    }
}