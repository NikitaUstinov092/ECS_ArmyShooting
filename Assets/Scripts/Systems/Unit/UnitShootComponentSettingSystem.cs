using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems.Unit
{
    public class UnitShootComponentSettingSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<ViewComponent, ShootComponent>> _ecsFilter;

        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _ecsFilter.Value)
            {
                ref var unitView = ref _ecsFilter.Pools.Inc1.Get(entity);
                ref var shoot = ref _ecsFilter.Pools.Inc2.Get(entity);

                shoot.BulletSpawnPoint = unitView.View.transform;
            }
        }
    }
}