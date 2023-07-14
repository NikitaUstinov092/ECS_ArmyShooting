using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;

struct SpawnFighter : IEcsInitSystem
{
    private readonly EcsCustomInject<SharedData> _data;
    private readonly EcsFilterInject<Inc<UnitTypeComponent, ArmyComponent>> _ecsFilter;
    private EcsWorld _world;
    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        var BlueTeam = InstatiateTeam("Blue Team", _data.Value.BlueFighter, _data.Value.BlueTeamStartPoint.position, 0);
        var RedTeam = InstatiateTeam("Red Team", _data.Value.RedFighter, _data.Value.RedTeamStartPoint.position, 180);

        foreach (var entityIndex in _ecsFilter.Value)
        {
            ref var unitTypeComponent = ref _ecsFilter.Pools.Inc1.Get(entityIndex);
            ref var armyComponent = ref _ecsFilter.Pools.Inc2.Get(entityIndex);

            if (armyComponent.TeamNumber == 0)
            {
                var fighter = GetFighter(BlueTeam);
                unitTypeComponent.View = fighter.gameObject;
                fighter.PackEntity(entityIndex);
            }                         
            
            else if (armyComponent.TeamNumber == 1)
            {
                var fighter = GetFighter(RedTeam);
                unitTypeComponent.View = fighter.gameObject;
                fighter.PackEntity(entityIndex);
            }                      
        }
    }

    private List<ECSMonoObject> InstatiateTeam(string teamName, ECSMonoObject prefab, Vector3 spawnPosition, float rotation) 
    {
        var massTeam = new List<ECSMonoObject>();
        var parent = new GameObject(teamName);

        for (int row = 0; row < _data.Value.Row; row++)
        {
            // Цикл для создания элементов в каждом столбце
            for (int element = 0; element < _data.Value.CountSpawnInRow; element++)
            {
                // Вычисляем позицию спавна объекта в зависимости от столбца и элемента
                Vector3 offset = new Vector3(row * 2, 0, element * 2);

                // Создаем объект на заданной позиции
                ECSMonoObject newFighter = Object.Instantiate(prefab, spawnPosition + offset, Quaternion.identity); 
                newFighter.transform.parent = parent.transform;

                newFighter.transform.rotation = Quaternion.Euler(0, rotation, 0);
                
                newFighter.tag = teamName;

                newFighter.Init(_world);
                massTeam.Add(newFighter);
            }
        }

        return massTeam;
    }
    private ECSMonoObject GetFighter(List<ECSMonoObject> list)
    {
        if (list.Count <= 0)
            return null;

        var fighter = list[0];
        list.RemoveAt(0);
        return fighter;
    }
}
