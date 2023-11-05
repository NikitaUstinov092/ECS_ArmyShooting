using System;

namespace Views
{
    [Serializable]
    public class BulletData
    {
        public ECSMonoObject BulletBlue;
        public ECSMonoObject BulletRed;
       
        public float Speed; 
        public int Health;
        public float Damage;
        public float DestroyDelay;
    }
}