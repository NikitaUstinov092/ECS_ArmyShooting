using Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.Bullet
{
    public class BulletSpawnSystem: IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BulletSpawnComponent, ViewComponent>> _filterBullet;
        private readonly EcsPoolInject<BulletSpawnComponent> _poolBullet;
        private GameObject _bulletParent;
        public void Init(IEcsSystems systems)
        {
            _bulletParent = new GameObject("Bullets");
        }
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterBullet.Value)
            {
                ref var bulletFilterComp = ref _filterBullet.Pools.Inc1.Get(entity);
                
                ref var viewFilterComp = ref _filterBullet.Pools.Inc2.Get(entity);
                
                var bullet = Object.Instantiate(bulletFilterComp.Bullet, bulletFilterComp.SpawnTransform.position,UnityEngine.Quaternion.Euler(90,0,0) );
              
                bullet.transform.parent = _bulletParent.transform;
                viewFilterComp.View = bullet.gameObject;
                bullet.Init(systems.GetWorld());
                bullet.PackEntity(entity);
                _poolBullet.Value.Del(entity);
            }
        }
    }
}