using UnityEngine;

namespace Assets.Scripts.General
{
    public static class Calculator
    {
        public static bool IsLastRollCritical;
        private static int RollD20()
        {
            return Random.Range(1, 21);
        }

        public static int CalculateDamage(int min, int max)
        {
            int atkRoll = RollD20();
            
            int damage = Random.Range(min, max);

            IsLastRollCritical = atkRoll == 20;

            //Critical damage
            if (IsLastRollCritical)
            {
                damage += Random.Range(min, max);
            }

            return damage;
        }

        public static int CalculateRestoreHealth(int min, int max)
        {
            return Random.Range(min, max);
        }
    }
}
