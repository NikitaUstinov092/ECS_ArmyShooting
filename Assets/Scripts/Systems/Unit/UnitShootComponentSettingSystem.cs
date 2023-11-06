using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems.Unit
{
    public class UnitShootComponentSettingSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<ViewComponent, ShootComponent>> _unitFilter;
        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _unitFilter.Value)
            {
                ref var unitView = ref _unitFilter.Pools.Inc1.Get(entity);
                ref var shoot = ref _unitFilter.Pools.Inc2.Get(entity);

                shoot.BulletSpawnPoint = unitView.View.transform;
            }
        }
    }
}