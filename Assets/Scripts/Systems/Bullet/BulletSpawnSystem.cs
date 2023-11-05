using Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Object = UnityEngine.Object;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

namespace Systems.Bullet
{
    public class BulletSpawnSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BulletSpawnComponent, ViewComponent>> _filterBullet;
        private readonly EcsPoolInject<BulletSpawnComponent> _poolBullet;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterBullet.Value)
            {
                ref var bulletFilter = ref _filterBullet.Pools.Inc1.Get(entity);
                
                ref var viewFilter = ref _filterBullet.Pools.Inc2.Get(entity);
                
                var bullet = Object.Instantiate(bulletFilter.Bullet, bulletFilter.SpawnTransform.position,UnityEngine.Quaternion.Euler(90,0,0) );
                
                viewFilter.View = bullet.gameObject;
                bullet.Init(systems.GetWorld());
                bullet.PackEntity(entity);
                _poolBullet.Value.Del(entity);
            }
        }
    }
}