using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems.Unit
{
    public struct UnitTeamSelectorSystem: IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<TeamComponent>> _ecsFilter;
        private const int RedTeamId = 1;
        private const int BlueTeamId = 2;
       
        public void Init(IEcsSystems systems)
        {
            foreach (var entityIndex in _ecsFilter.Value)
            {
                ref var teamComponent = ref _ecsFilter.Pools.Inc1.Get(entityIndex);
                teamComponent.TeamType = entityIndex % 2 == 0 ? RedTeamId : BlueTeamId;
            }
        }
    }
}