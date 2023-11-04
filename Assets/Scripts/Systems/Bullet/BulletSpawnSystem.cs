using Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Object = UnityEngine.Object;
using UnityEngine;

namespace Systems.Bullet
{
    public class BulletSpawnSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BulletComponent, ViewComponent>> _filterBullet;
        private readonly EcsPoolInject<BulletComponent> _poolBullet;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterBullet.Value)
            {
                ref var bulletFilter = ref _filterBullet.Pools.Inc1.Get(entity);
                
                if(bulletFilter.Spawned)
                    continue;
                
                ref var viewFilter = ref _filterBullet.Pools.Inc2.Get(entity);
                
                var bullet = Object.Instantiate(bulletFilter.Bullet, bulletFilter.SpawnTransform.position, Quaternion.identity);
               
               
                viewFilter.View = bullet.gameObject;
                bullet.Init(systems.GetWorld());
                bullet.PackEntity(entity);
                bulletFilter.Spawned = true;
            }
        }
    }
}