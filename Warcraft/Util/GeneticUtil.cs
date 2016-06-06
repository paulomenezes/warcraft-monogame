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
        static Random random = new Random();

        public static string[] Encode(Unit enemy)
        {
            string damage = FloatToBinary(enemy.information.Damage);
            string armor = FloatToBinary(enemy.information.Armor);
            string hitPoints = FloatToBinary(enemy.information.HitPointsTotal);
            string sight = FloatToBinary(enemy.information.Sight);
            string spawn = FloatToBinary(enemy.information.Spawn);
            string precision = FloatToBinary(enemy.information.Precision);
            string type = FloatToBinary((int)enemy.information.Type);
            
            return new string[7] { damage, armor, hitPoints, sight, spawn, precision, type };
        }

        public static InformationUnit Decode(string[] data)
        {
            string damage = data[0];
            string armor = data[1];
            string hitPoints = data[2];
            string sight = data[3];
            string spawn = data[4];
            string precision = data[5];
            string type = data[6];

            float _damage = BinaryToFloat(damage);
            float _armor = BinaryToFloat(armor);
            float _hitPoints = BinaryToFloat(hitPoints);
            float _sight = BinaryToFloat(sight);
            float _precision = BinaryToFloat(precision);

            int _spawn = (int)BinaryToFloat(spawn);
            int _type = (int)BinaryToFloat(type);

            InformationUnit info = null;
            if (_type < 3)
                info = new InformationUnit("Grunt", Race.ORC, Faction.HORDE, _hitPoints, _armor, _sight, 10, 600, 1, Buildings.NONE, 60, _damage, _precision, 1, _spawn, Units.GRUNT);
            else if (_type >= 3)
                info = new InformationUnit("Troll Axethrower", Race.ORC, Faction.HORDE, _hitPoints, _armor, _sight, 10, 600, 1, Buildings.NONE, 60, _damage, _precision, 1, _spawn, Units.TROLL_AXETHROWER);

            return info;
        }

        public static float BinaryToFloat(string value)
        {
            char[] valueChar = value.ToCharArray();

            for (int j = 0; j < valueChar.Length; j++)
            {
                if (random.Next(0, 100) < 5)
                {
                    valueChar[j] = valueChar[j] == '1' ? '0' : '1';
                }
            }

            value = new string(valueChar);

            int i = Convert.ToInt32(value, 2);
            byte[] b = BitConverter.GetBytes(i);
            return BitConverter.ToSingle(b, 0);
        }

        public static string IntToBinary(int value)
        {
            return Convert.ToString(value, 2);
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
