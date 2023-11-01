using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

struct SpawnUnit : IEcsInitSystem
{
    private readonly EcsCustomInject<UnitData> _data;
    private readonly EcsFilterInject<Inc<UnitTypeComponent, TeamComponent>> _ecsFilter;
    private EcsWorld _world;
    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        var blueTeam = InstatiateTeam("Blue Team", _data.Value.BlueFighter, _data.Value.BlueTeamStartPoint.position, 0);
        var redTeam = InstatiateTeam("Red Team", _data.Value.RedFighter, _data.Value.RedTeamStartPoint.position, 180);

        foreach (var entityIndex in _ecsFilter.Value)
        {
            ref var unitTypeComponent = ref _ecsFilter.Pools.Inc1.Get(entityIndex);
            ref var armyComponent = ref _ecsFilter.Pools.Inc2.Get(entityIndex);

            switch (armyComponent.TeamType)
            {
                case 0:
                {
                    var fighter = GetFighter(blueTeam);
                    unitTypeComponent.View = fighter.gameObject;
                    fighter.PackEntity(entityIndex);
                    break;
                }
                case 1:
                {
                    var fighter = GetFighter(redTeam);
                    unitTypeComponent.View = fighter.gameObject;
                    fighter.PackEntity(entityIndex);
                    break;
                }
            }                      
        }
    }

    private List<ECSMonoObject> InstatiateTeam(string teamName, ECSMonoObject prefab, Vector3 spawnPosition, float rotation) 
    {
        var massTeam = new List<ECSMonoObject>();
        var parent = new GameObject(teamName);

        for (int row = 0; row < _data.Value.Row; row++)
        {
            for (int element = 0; element < _data.Value.CountSpawnInRow; element++)
            {
                Vector3 offset = new Vector3(row * 2, 0, element * 2);
                
                ECSMonoObject newFighter = Object.Instantiate(prefab, spawnPosition + offset, Quaternion.identity); 
                newFighter.transform.parent = parent.transform;

                newFighter.transform.rotation = Quaternion.Euler(0, rotation, 0);              

                newFighter.Init(_world);
                massTeam.Add(newFighter);
            }
        }
        return massTeam;
    }
    private ECSMonoObject GetFighter([NotNull] List<ECSMonoObject> list)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (list.Count <= 0)
            return null;

        var fighter = list[0];
        list.RemoveAt(0);
        return fighter;
    }
}
