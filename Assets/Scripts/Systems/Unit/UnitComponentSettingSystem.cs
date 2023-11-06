using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems.Unit
{
    public struct UnitComponentSettingSystem: IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc< ViewComponent, HealthComponent, TeamComponent, MoveComponent, ShootComponent, DamageComponent>> _unitFilter;
        private readonly EcsCustomInject<UnitData> _unitData;
        
        public void Init(IEcsSystems systems)
        {
            var unitData = _unitData.Value;
            
            foreach (var entityIndex in _unitFilter.Value)
            {
                ref var unitTypeComp = ref _unitFilter.Pools.Inc1.Get(entityIndex);
                ref var healthComp = ref _unitFilter.Pools.Inc2.Get(entityIndex);
                ref var unitTeamComp = ref _unitFilter.Pools.Inc3.Get(entityIndex);
                ref var moveComp = ref _unitFilter.Pools.Inc4.Get(entityIndex);
                ref var damageComp = ref _unitFilter.Pools.Inc6.Get(entityIndex);
                
                SetView(ref unitTypeComp, ref unitTeamComp, ref unitData);
                SetMoveVector(ref unitTeamComp, ref moveComp, ref unitData);
                
                healthComp.Health = unitData.StartHealth;
                damageComp.Damage = unitData.Damage;
            }
        }
        
        private void SetView(ref ViewComponent view, ref TeamComponent team, ref UnitData data)
        {
            view.View = team.TeamType switch
            {
                1 => data.RedUnit.gameObject,
                2 => data.BlueUnit.gameObject,
                _ => view.View
            };
        }

        private void SetMoveVector(ref TeamComponent team, ref MoveComponent move, ref UnitData data)
        {
            move.Speed = data.Speed;
            move.Direction = team.TeamType switch
            {
                1 => -Vector3.forward,
                2 => Vector3.forward,
                _ => move.Direction
            };
        }
    }
}