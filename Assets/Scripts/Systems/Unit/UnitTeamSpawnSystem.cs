using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

struct UnitTeamSpawnSystem : IEcsInitSystem
{
    private readonly EcsCustomInject<UnitData> _data;
    private readonly EcsFilterInject<Inc<ViewComponent, TeamComponent>> _ecsFilter;
    private EcsWorld _world;

    private const string RedTeamName = "Red Team";
    private const string BlueTeamName = "Blue Team";

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        var dataValue = _data.Value;
        
        var redTeam = InstantiateTeam(RedTeamName, dataValue.RedUnit, dataValue.RedTeamStartPoint.position,180);
        var blueTeam = InstantiateTeam(BlueTeamName, dataValue.BlueUnit, _data.Value.BlueTeamStartPoint.position, 0);
        
        foreach (var entity in _ecsFilter.Value)
        {
                ref var viewComponent = ref _ecsFilter.Pools.Inc1.Get(entity);
                ref var armyComponent = ref _ecsFilter.Pools.Inc2.Get(entity);

                switch (armyComponent.TeamType)
                {
                    case 1:
                        ConnectEntityObject(blueTeam, entity, ref viewComponent);
                        break;
                    case 2:
                        ConnectEntityObject(redTeam, entity, ref viewComponent);
                        break;
                }
        }
    }
    private List<ECSMonoObject> InstantiateTeam(string teamName,
        ECSMonoObject prefab, Vector3 spawnPosition, float rotation)
    {
        var massTeam = new List<ECSMonoObject>();
        var parent = new GameObject(teamName);
        var dataValue = _data.Value;

        for (var row = 0; row < dataValue.Row; row++)
        {
            for (var element = 0; element < dataValue.CountSpawnInRow; element++)
            {
                var offset = new Vector3(row * 2, 0, element * 2);
                var newUnit = Object.Instantiate(prefab, spawnPosition + offset, Quaternion.identity);
                newUnit.transform.parent = parent.transform;
                newUnit.transform.rotation = Quaternion.Euler(0, rotation, 0);
                massTeam.Add(newUnit);
            }
        }
        return massTeam;
    }
    private void ConnectEntityObject(List<ECSMonoObject> team, int entity, ref ViewComponent view)
    {
        var unit = GetUnitObject(team);
        unit.Init(_world);
        unit.PackEntity(entity);
        view.View = unit.gameObject;
    }
    private ECSMonoObject GetUnitObject(List<ECSMonoObject> list)
    {
        var unit = list[0];
        list.Remove(unit);
        return unit;
    }
}

