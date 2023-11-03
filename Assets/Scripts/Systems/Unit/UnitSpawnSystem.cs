using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

struct UnitSpawnSystem : IEcsInitSystem
{
    private readonly EcsCustomInject<UnitData> _data;
    private readonly EcsFilterInject<Inc<ViewComponent, TeamComponent>> _ecsFilter;
    private EcsWorld _world;

    public void Init(IEcsSystems systems) // не должен распределять должен спавнить
    {
        _world = systems.GetWorld();

        foreach (var entity in _ecsFilter.Value)
        {
            var redTeam = InstantiateTeam("Red Team", _data.Value.RedFighter, _data.Value.RedTeamStartPoint.position,
                180);
            var blueTeam = InstantiateTeam("Blue Team", _data.Value.BlueFighter,
                _data.Value.BlueTeamStartPoint.position, 0);

            foreach (var entityIndex in _ecsFilter.Value)
            {
                ref var viewComponent = ref _ecsFilter.Pools.Inc1.Get(entityIndex);
                ref var armyComponent = ref _ecsFilter.Pools.Inc2.Get(entityIndex);

                if (armyComponent.TeamType == 1)
                {
                    ConnectEntityObject(blueTeam, entityIndex, viewComponent);
                }
                else if (armyComponent.TeamType == 1)
                {
                    ConnectEntityObject(redTeam, entityIndex, viewComponent);
                }
            }
        }
    }
    private void ConnectEntityObject(List<ECSMonoObject> team, int entity,  ViewComponent view)
    {
        var unit = CreateUnit(team);
        unit.Init(_world);
        unit.PackEntity(entity);
        view.View = _data.Value.RedFighter.gameObject;
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
        private ECSMonoObject CreateUnit(List<ECSMonoObject> list)
        {
            if (list.Count <= 0)
                return null;

            var unit = list[0];
            list.RemoveAt(0);
            return unit;
        }
}

