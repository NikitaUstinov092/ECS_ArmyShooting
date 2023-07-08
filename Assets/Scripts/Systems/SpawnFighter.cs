using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;

struct SpawnFighter : IEcsInitSystem
{
    private readonly EcsCustomInject<SharedData> _data;
    private readonly EcsFilterInject<Inc<UnitTypeComponent, ArmyComponent>> ecsFilter;
    public void Init(IEcsSystems systems)
    {
        var BlueTeam = InstatiateTeam("Blue Team", _data.Value.BlueFighter, _data.Value.BlueTeamStartPoint.position);
        var RedTeam = InstatiateTeam("Red Team", _data.Value.RedFighter, _data.Value.RedTeamStartPoint.position);

        foreach (var entityIndex in ecsFilter.Value)
        {
            ref var unitTypeComponent = ref ecsFilter.Pools.Inc1.Get(entityIndex);
            ref var armyComponent = ref ecsFilter.Pools.Inc2.Get(entityIndex);

            if (armyComponent.TeamNumber == 0)                 
                unitTypeComponent.View = GetFighter(BlueTeam);
            
            else if (armyComponent.TeamNumber == 1)      
                unitTypeComponent.View = GetFighter(RedTeam);            
        }
    }

    private List<GameObject> InstatiateTeam(string teamName, GameObject prefab, Vector3 spawnPosition)
    {
        var massTeam = new List<GameObject>();
        var parent = new GameObject(teamName);

        for (int row = 0; row < _data.Value.Row; row++)
        {
            // Цикл для создания элементов в каждом столбце
            for (int element = 0; element < _data.Value.CountSpawnInRow; element++)
            {
                // Вычисляем позицию спавна объекта в зависимости от столбца и элемента
                Vector3 offset = new Vector3(row * 2, 0, element * 2);

                // Создаем объект на заданной позиции
                GameObject newFighter = Object.Instantiate(prefab, spawnPosition + offset, Quaternion.identity);
                newFighter.transform.parent = parent.transform;
                
                newFighter.tag = teamName;

                massTeam.Add(newFighter);
            }
        }

        return massTeam;
    }
    private GameObject GetFighter(List<GameObject> list)
    {
        if (list.Count <= 0)
            return null;

        var fighter = list[0];
        list.RemoveAt(0);
        return fighter;
    }
}
