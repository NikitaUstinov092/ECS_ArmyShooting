using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems.Unit
{
    public struct UnitComponentSettingSystem: IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc< UnitTypeComponent, HealthComponent, TeamComponent, MoveComponent, ShootComponent, DamageComponent>> _ecsFilter;
        private readonly EcsCustomInject<UnitData> _unitData;
        
        public void Init(IEcsSystems systems)
        {
            var unitData = _unitData.Value;
            
            foreach (var entityIndex in _ecsFilter.Value)
            {
                ref var unitType = ref _ecsFilter.Pools.Inc1.Get(entityIndex);
                ref var health = ref _ecsFilter.Pools.Inc2.Get(entityIndex);
                ref var unitTeam = ref _ecsFilter.Pools.Inc3.Get(entityIndex);
                ref var damage = ref _ecsFilter.Pools.Inc6.Get(entityIndex);
                
                ref var move = ref _ecsFilter.Pools.Inc4.Get(entityIndex);
                ref var shoot = ref _ecsFilter.Pools.Inc5.Get(entityIndex);
                
                health.Health = unitData.StartHealth;
                damage.Damage = unitData.Damage;

                SetView(unitType, unitTeam, unitData);
                SetMoveVector(unitTeam, move, unitData);
            }
        }
        
        private void SetView(UnitTypeComponent unitType, TeamComponent team, UnitData data)
        {
            unitType.View = team.TeamType switch
            {
                1 => data.RedFighter,
                2 => data.BlueFighter,
                _ => unitType.View
            };
        }

        private void SetMoveVector(TeamComponent team, MoveComponent move, UnitData data)
        {
            move.Speed = data.Speed;
            move.Direction = team.TeamType switch
            {
                1 => Vector3.forward,
                2 => -Vector3.forward,
                _ => move.Direction
            };
        }
    }
}