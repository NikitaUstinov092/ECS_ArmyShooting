using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems.Unit
{
    public struct UnitTeamSelectorSystem: IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<TeamComponent>> _unitFilter;
        
        private const int RedTeamId = 1;
        private const int BlueTeamId = 2;
        public void Init(IEcsSystems systems)
        {
            foreach (var entityIndex in _unitFilter.Value)
            {
                ref var teamComponent = ref _unitFilter.Pools.Inc1.Get(entityIndex);
                teamComponent.TeamType = entityIndex % 2 == 0 ? RedTeamId : BlueTeamId;
            }
        }
    }
}