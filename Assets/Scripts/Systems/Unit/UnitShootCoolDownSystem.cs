﻿using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems.Unit
{
    public struct UnitShootCoolDownSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ShootCountDownComponent>> _filterShootCountDown;
        private readonly  EcsPoolInject<ShootCountDownComponent> _poolShootCountDown;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterShootCountDown.Value)
            {
                ref var shootCountDown = ref _filterShootCountDown.Pools.Inc1.Get(entity).CoolDawnDelay;
                shootCountDown -= Time.deltaTime;

                if (shootCountDown <= 0)
                {
                    _poolShootCountDown.Value.Del(entity);
                }
            }
        }
    }
}