using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    struct MoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ArmyComponent, MoveComponent, UnitTypeComponent>> ecsFilter;
        private readonly EcsCustomInject<SharedData> _data;

        private const float minSpeed = 1;
        private const float maxSpeed = 3;

        public void Init(IEcsSystems systems)
        {
            foreach (var entityIndex in ecsFilter.Value)
            {
                ref var armyComponent = ref ecsFilter.Pools.Inc1.Get(entityIndex);
                ref var moveComponent = ref ecsFilter.Pools.Inc2.Get(entityIndex);

                if (armyComponent.TeamNumber == 0)          
                    moveComponent.TargteTranform = _data.Value.RedTeamStartPoint;
     
                else if (armyComponent.TeamNumber == 1)
                    moveComponent.TargteTranform = _data.Value.BlueTeamStartPoint;
               
                moveComponent.Speed = GetRandomSpeed();
            }
        }

        public void Run (IEcsSystems systems) {

            foreach (var entityIndex in ecsFilter.Value)
            {
                ref var moveComponent = ref ecsFilter.Pools.Inc2.Get(entityIndex);
                ref var unitComponent = ref ecsFilter.Pools.Inc3.Get(entityIndex);

                unitComponent.View.transform.position = Vector3.MoveTowards(unitComponent.View.transform.position,
                  new Vector3(unitComponent.View.transform.position.x, unitComponent.View.transform.position.y, moveComponent.TargteTranform.position.z), Time.deltaTime);

            }
        }


        private float GetRandomSpeed()
        {
            return Random.Range(minSpeed, maxSpeed);
        }
    }
}