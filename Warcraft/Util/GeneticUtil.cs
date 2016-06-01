using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Units;

namespace Warcraft.Util
{
    class GeneticUtil
    {
        public static string Encode(Unit enemy)
        {
            string damage = FloatToBinary(enemy.information.Damage);
            string armor = FloatToBinary(enemy.information.Armor);
            string hitPoints = FloatToBinary(enemy.information.HitPoints);
            string sight = FloatToBinary(enemy.information.Sight);
            string spawn = FloatToBinary(enemy.information.Spawn);
            string precision = FloatToBinary(enemy.information.Precision);
            string type = FloatToBinary((int)enemy.information.Type);
            
            return damage + armor + hitPoints + sight + spawn + precision + type;
        }

        public static InformationUnit Decode(string data)
        {
            string damage = data.Substring(0, 32);
            string armor = data.Substring(32, 64);
            string hitPoints = data.Substring(64, 96);
            string sight = data.Substring(96, 128);
            string spawn = data.Substring(128, 160);
            string precision = data.Substring(160, 192);
            string type = data.Substring(192, 224);

            float _damage = BinaryToFloat(damage);
            float _armor = BinaryToFloat(armor);
            float _hitPoints = BinaryToFloat(hitPoints);
            float _sight = BinaryToFloat(sight);
            float _precision = BinaryToFloat(precision);

            int _spawn = (int)BinaryToFloat(spawn);
            int _type = (int)BinaryToFloat(type);

            InformationUnit info = null;
            if (_type == 0)
                info = new InformationUnit("Grunt", Race.ORC, Faction.HORDE, _hitPoints, _armor, _sight, 10, 600, 1, Buildings.NONE, 60, _damage, _precision, 1, _spawn, Units.GRUNT);
            else if (_type == 1)
                info = new InformationUnit("Troll Axethrower", Race.ORC, Faction.HORDE, _hitPoints, _armor, _sight, 10, 600, 1, Buildings.NONE, 60, _damage, _precision, 1, _spawn, Units.TROLL_AXETHROWER);

            return info;
        }

        public static float BinaryToFloat(string value)
        {
            int i = Convert.ToInt32(value, 2);
            byte[] b = BitConverter.GetBytes(i);
            return BitConverter.ToSingle(b, 0);
        }

        public static string FloatToBinary(float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            int i = BitConverter.ToInt32(b, 0);
            return Convert.ToString(i, 2);

            //int bitCount = sizeof(float) * 8; // never rely on your knowledge of the size
            //char[] result = new char[bitCount]; // better not use string, to avoid ineffective string concatenation repeated in a loop

            //// now, most important thing: (int)value would be "semantic" cast of the same
            //// mathematical value (with possible rounding), something we don't want; so:
            //int intValue = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);

            //for (int bit = 0; bit < bitCount; ++bit)
            //{
            //    int maskedValue = intValue & (1 << bit); // this is how shift and mask is done.
            //    if (maskedValue > 0)
            //        maskedValue = 1;

            //    // at this point, masked value is either int 0 or 1
            //    result[bitCount - bit - 1] = maskedValue.ToString()[0]; // bits go right-to-left in usual Western Arabic-based notation
            //}

            //return new string(result); // string from character array
        }

        public static int[] RouletteWheelSelection(List<UnitEnemy> enemies)
        {
            enemies = enemies.OrderBy(e => e.information.Fitness).ToList();

            float[] fitness = new float[enemies.Count];
            for (int i = 0; i < fitness.Length; i++)
            {
                if (i == 0)
                    fitness[i] = enemies[i].information.Fitness;
                else
                    fitness[i] = fitness[i - 1] + enemies[i].information.Fitness;
            }

            Random random = new Random();
            double value01 = random.NextDouble() * fitness[fitness.Length - 1];
            double value02 = random.NextDouble() * fitness[fitness.Length - 1];

            int[] index = new int[2];
            index[0] = -1;
            index[1] = -1;

            for (int i = 0; i < fitness.Length; i++)
            {
                if (index[0] == -1 && fitness[i] > value01)
                    index[0] = i;

                if (index[1] == -1 && fitness[i] > value02)
                    index[1] = i;

                if (index[0] != -1 && index[1] != -1)
                    break;
            }

            return index;
        }
    }
}
