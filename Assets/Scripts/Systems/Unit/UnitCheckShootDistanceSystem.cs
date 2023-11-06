using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


public struct UnitCheckShootDistanceSystem: IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<ViewComponent, TeamComponent, ShootComponent>> _unitFilter;
    private readonly EcsPoolInject<ShootCountDownComponent> _poolShootCountDown;
    private readonly EcsCustomInject<UnitData> _unitData;
    private readonly EcsWorld _world;
    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _unitFilter.Value)
        {
            ref var viewComp = ref _unitFilter.Pools.Inc1.Get(entity);
            ref var teamComp = ref _unitFilter.Pools.Inc2.Get(entity);
            ref var shootComp = ref _unitFilter.Pools.Inc3.Get(entity);
            
            shootComp.InFieldOfView = CheckShootDistance(ref teamComp, ref viewComp);
        }
    }
    
    private bool CheckShootDistance(ref TeamComponent team, ref ViewComponent view)
    {
        var unitTeam = team.TeamType;
        var unitView = view.View;

        foreach (var entity in _unitFilter.Value)
        {
            ref var entityView = ref _unitFilter.Pools.Inc1.Get(entity);
            ref var entityTeam = ref _unitFilter.Pools.Inc2.Get(entity);
            
            if (entityTeam.TeamType == unitTeam) 
                continue;
            
            if ((unitView.transform.position - entityView.View.transform.position).sqrMagnitude < _unitData.Value.ShotDistance)
                return true;
        }
        return false;
    }
}

