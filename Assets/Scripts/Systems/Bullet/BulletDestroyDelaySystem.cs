using Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems.Bullet
{
    public struct BulletDestroyDelaySystem: IEcsRunSystem
    {  
        private readonly EcsFilterInject<Inc<BulletDestroyDelayComponent, HealthComponent>> _filterBullet;
        private readonly EcsPoolInject<BulletDestroyDelayComponent> _poolBulletDestroyDelay;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterBullet.Value)
            {
                ref var bulletDestroyDelay = ref _filterBullet.Pools.Inc1.Get(entity);
                ref var health = ref _filterBullet.Pools.Inc2.Get(entity);

                bulletDestroyDelay.Delay -= Time.deltaTime;

                if (bulletDestroyDelay.Delay <= 0)
                    health.Health = 0;
            }
           
        }
    }
}