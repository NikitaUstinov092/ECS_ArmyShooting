using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public struct UnitEntityInitialazerSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<ViewComponent> _poolUnitType;
        private readonly EcsPoolInject<HealthComponent> _poolHealth;
        private readonly EcsPoolInject<TeamComponent> _poolTeamNumber;
        private readonly EcsPoolInject<MoveComponent> _poolMove;
        private readonly EcsPoolInject<DamageComponent> _poolDamage;
        private readonly EcsPoolInject<ShootComponent> _poolShoot;
        
        private readonly EcsCustomInject<UnitData> _unitData;
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
           
            const int armyCount = 2; 
            var unitsPerArmy = _unitData.Value.CountSpawnInRow * _unitData.Value.Row; 

            for (var i = 0; i < unitsPerArmy * armyCount; i++)
            {
                var entity = world.NewEntity();
                
                _poolUnitType.Value.Add(entity);
                _poolHealth.Value.Add(entity);
                _poolTeamNumber.Value.Add(entity);
                _poolMove.Value.Add(entity);
                _poolDamage.Value.Add(entity);
                _poolShoot.Value.Add(entity);
            }
        }
    }
