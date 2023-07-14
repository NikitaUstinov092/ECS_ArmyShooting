using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Threading.Tasks;
using UnityEngine;

    struct ShootCountDownSystem : IEcsRunSystem {
        private readonly EcsFilterInject<Inc<ShootCountDownComponent>> _shootCountDownPool;
        public void Run(IEcsSystems systems)
        {
            foreach (var entityIndex in _shootCountDownPool.Value)
            {
                if (_shootCountDownPool.Pools.Inc1.Has(entityIndex))
                {
                    ref var shootCountDownPoolComp = ref _shootCountDownPool.Pools.Inc1.Get(entityIndex);

                    if (!shootCountDownPoolComp.ShootWaiting)
                    {
                        shootCountDownPoolComp.ShootWaiting = true;
                        DeleteComponent(entityIndex, shootCountDownPoolComp);
                    }
                                  
                }
            }
        }

        private async Task DeleteComponent(int entityIndex, ShootCountDownComponent comp)
        {
            await Task.Delay(GetRandomTime());
            _shootCountDownPool.Pools.Inc1.Del(entityIndex);
            comp.ShootWaiting = false;
        }

        private int GetRandomTime()
        {
            return Random.Range(2000, 3000);
        }
    }
