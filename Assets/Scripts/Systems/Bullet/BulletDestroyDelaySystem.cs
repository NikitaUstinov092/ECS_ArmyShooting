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
                ref var bulletDestroyDelayComp = ref _filterBullet.Pools.Inc1.Get(entity);
                ref var healthComp = ref _filterBullet.Pools.Inc2.Get(entity);

                bulletDestroyDelayComp.Delay -= Time.deltaTime;

                if (bulletDestroyDelayComp.Delay <= 0)
                    healthComp.Health = 0;
            }
           
        }
    }
}