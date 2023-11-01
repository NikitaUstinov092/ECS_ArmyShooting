using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
    public struct UnitEntityInitialazerSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<UnitData> _unitData;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var poolUnitType = world.GetPool<UnitTypeComponent>();
            var poolHealth = world.GetPool<HealthComponent>();
            var poolTeamNumber = world.GetPool<TeamComponent>();
            var poolMove = world.GetPool<MoveComponent>();
            var poolDamage = world.GetPool<DamageComponent>();

            const int armyCount = 2; 
            var entitiesPerArmy = _unitData.Value.CountSpawnInRow * _unitData.Value.Row; 

            for (var i = 0; i < entitiesPerArmy * armyCount; i++)
            {
                var entity = world.NewEntity();
                
                poolUnitType.Add(entity);
                poolMove.Add(entity);
                poolDamage.Add(entity);
                poolHealth.Add(entity);
                poolTeamNumber.Add(entity);
            }
        }
    }