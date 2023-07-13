using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Threading.Tasks;
using UnityEngine;

namespace Client {
    struct ShootCountDownSystem : IEcsRunSystem {
        private readonly EcsFilterInject<Inc<ShootCountDownComponent>> shootCountDownPool;
        public void Run(IEcsSystems systems)
        {
            foreach (var entityIndex in shootCountDownPool.Value)
            {
                if (shootCountDownPool.Pools.Inc1.Has(entityIndex))
                {
                    ref var shootCountDownPoolComp = ref shootCountDownPool.Pools.Inc1.Get(entityIndex);

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
            shootCountDownPool.Pools.Inc1.Del(entityIndex);
            comp.ShootWaiting = false;
        }

        private int GetRandomTime()
        {
            return Random.Range(2000, 3000);
        }
    }


  
}