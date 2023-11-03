using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems.Unit
{
    public struct UnitComponentSettingSystem: IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc< ViewComponent, HealthComponent, TeamComponent, MoveComponent, ShootComponent, DamageComponent>> _ecsFilter;
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
                
                SetView(ref unitType, ref unitTeam, ref unitData);
                SetMoveVector(ref unitTeam, ref move, ref unitData);
                
                health.Health = unitData.StartHealth;
                damage.Damage = unitData.Damage;
            }
        }
        
        private void SetView(ref ViewComponent view,ref TeamComponent team,ref UnitData data)
        {
            view.View = team.TeamType switch
            {
                1 => data.RedFighter.gameObject,
                2 => data.BlueFighter.gameObject,
                _ => view.View
            };
        }

        private void SetMoveVector(ref TeamComponent team,ref MoveComponent move, ref UnitData data)
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