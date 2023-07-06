using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;

struct SpawnFighter : IEcsInitSystem
{
    private readonly EcsCustomInject<SharedData> _data;
    private readonly EcsFilterInject<Inc<UnitTypeComponent>> ecsUnitFilter;
    public void Init(IEcsSystems systems)
    {
        var Team = InstatiateTeam("Blue Team", _data.Value.BlueFighter, _data.Value.BlueTeamStartpoint.position);
      
        for (var i = 0; i < Team.Count; i++)
            ecsUnitFilter.Pools.Inc1.Get(i).View = Team[i];
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
                massTeam.Add(newFighter);
            }
        }

        return massTeam;
    }
  
}
