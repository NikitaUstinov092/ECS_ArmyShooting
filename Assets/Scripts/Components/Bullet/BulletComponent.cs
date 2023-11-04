using UnityEngine;

namespace Components
{
    public struct BulletComponent
    {
        public Transform SpawnTransform;
        public ECSMonoObject Bullet;
        public bool Spawned;
    }
}