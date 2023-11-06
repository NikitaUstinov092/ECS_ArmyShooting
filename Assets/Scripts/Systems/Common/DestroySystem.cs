using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

    internal struct DestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ViewComponent, HealthComponent>> _filterHealth;
        private readonly EcsPoolInject<ViewComponent> _poolView;
        private readonly EcsPoolInject<HealthComponent> _poolHealth;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterHealth.Value)
            {
                ref var viewComp = ref _poolView.Value.Get(entity);
                var healthComp = _poolHealth.Value.Get(entity);

                if (healthComp.Health > 0)
                    continue;
                
                Object.DestroyImmediate(viewComp.View);
                _world.Value.DelEntity(entity);
            }
        }
    }
