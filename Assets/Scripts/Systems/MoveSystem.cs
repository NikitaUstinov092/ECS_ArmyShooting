using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

    struct MoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TeamComponent, MoveComponent, UnitTypeComponent>> _ecsFilter;
        private readonly EcsCustomInject<UnitData> _data;

        private const float _minSpeed = 1;
        private const float _maxSpeed = 3;

        public void Init(IEcsSystems systems)
        {
            foreach (var entityIndex in _ecsFilter.Value)
            {
                ref var moveComponent = ref _ecsFilter.Pools.Inc2.Get(entityIndex);
               
                moveComponent.Speed = GetRandomSpeed();
            }
        }
        public void Run (IEcsSystems systems) {

            foreach (var entityIndex in _ecsFilter.Value)
            {
                ref var armyComponent = ref _ecsFilter.Pools.Inc1.Get(entityIndex);
                ref var moveComponent = ref _ecsFilter.Pools.Inc2.Get(entityIndex);
                ref var unitComponent = ref _ecsFilter.Pools.Inc3.Get(entityIndex);

                var position = unitComponent.View.transform.position;

                var targetVector = Vector3.forward;
               
                /*if (armyComponent.TeamType == 1)
                    targetVector =-targetVector;*/
                
                position = Vector3.MoveTowards(position,
                    position - targetVector, Time.deltaTime * moveComponent.Speed);
                
                unitComponent.View.transform.position = position;
            }
        }

        private float GetRandomSpeed()
        {
            return Random.Range(_minSpeed, _maxSpeed);
        }
    }
