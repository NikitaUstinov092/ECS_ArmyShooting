using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

    internal struct DestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitTypeComponent, HealthComponent>> _filterHealth;
        private readonly EcsPoolInject<UnitTypeComponent> _poolView;
        private readonly EcsPoolInject<HealthComponent> _poolHealth;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterHealth.Value)
            {
                ref var viewC = ref _poolView.Value.Get(entity);
                var healthC = _poolHealth.Value.Get(entity);

                if (healthC.Health <= 0)
                {
                    Object.DestroyImmediate(viewC.View);
                    _world.Value.DelEntity(entity);
                }
            }
        }
    }
