using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Threading.Tasks;

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

                    if (!shootCountDownPoolComp.IsWaiting)
                    {
                        shootCountDownPoolComp.IsWaiting = true;
                        DeletComponent(entityIndex, shootCountDownPoolComp);
                    }
                                  
                }
            }
        }

        private async Task DeletComponent(int entityIndex, ShootCountDownComponent comp)
        {
            await Task.Delay(3000);
            shootCountDownPool.Pools.Inc1.Del(entityIndex);
            comp.IsWaiting = false;
        }
    }


  
}