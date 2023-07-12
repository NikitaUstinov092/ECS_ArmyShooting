using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using System.Threading.Tasks;

namespace Client {
  sealed class ShootCountDownSystem: IEcsRunSystem 
    {    
        private readonly EcsFilterInject<Inc<ShootCountDownComponent>> shootCountDownPool;
        public void Run(IEcsSystems systems)
        {
            for (var entityIndex = 0; entityIndex < shootCountDownPool.Value.GetEntitiesCount(); entityIndex++)
            {
                if (shootCountDownPool.Pools.Inc1.Has(entityIndex))
                {
                    ref var shootCountDownPoolComp = ref shootCountDownPool.Pools.Inc1.Get(entityIndex);
                    //Task.Run(async () =>
                    //{
                    //    await DeletComponentc(entityIndex);
                    //});
                }
              
              
            }
          
        }

        private async Task DeletComponentc(int entityIndex)
        {
            await Task.Delay(3000); // Асинхронная задержка в 3 секунды

            shootCountDownPool.Pools.Inc1.Del(entityIndex);
        }

    }
}