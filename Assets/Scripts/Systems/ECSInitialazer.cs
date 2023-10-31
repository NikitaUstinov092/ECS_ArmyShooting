using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
    public struct ECSInitialazer : IEcsInitSystem
    {
        private readonly EcsCustomInject<SharedData> _data;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var poolBlock = world.GetPool<UnitTypeComponent>();
            var poolHealth = world.GetPool<HealthComponent>();
            var poolArmy = world.GetPool<ArmyComponent>();
            var poolMove = world.GetPool<MoveComponent>();
            var poolShoot = world.GetPool<ShootComponent>();
         

            const int armyCount = 2; 
            var entitiesPerArmy = _data.Value.CountSpawnInRow * _data.Value.Row; 

            for (var i = 0; i < entitiesPerArmy * armyCount; i++)
            {
                var entity = world.NewEntity();
                poolBlock.Add(entity);
                poolMove.Add(entity);
                poolHealth.Add(entity).Health = _data.Value.StartHealth;
                
                var armyId = i / entitiesPerArmy;
                poolArmy.Add(entity).TeamNumber = armyId;
                poolShoot.Add(entity);
            }
        }
    }
