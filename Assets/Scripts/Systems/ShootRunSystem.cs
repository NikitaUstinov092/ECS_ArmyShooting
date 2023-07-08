using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    struct ShootRunSystem : IEcsRunSystem {

        private readonly EcsFilterInject<Inc<ShootComponent>> ecsFilterShoot; // Добавленный фильтр

        private float spawnDelay; // Задержка в секундах между спавнами.
        private float spawnTimer; // Таймер для отслеживания времени.
        public void Run(IEcsSystems systems)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= GetRandomTime())
            {
                spawnTimer = 0f;

                for (int i = ecsFilterShoot.Value.GetEntitiesCount(); i < ecsFilterShoot.Value.GetEntitiesCount() * 2; i++)
                {
                    if (ecsFilterShoot.Pools.Inc1.Has(i))
                    {
                        ref var shootComponent = ref ecsFilterShoot.Pools.Inc1.Get(i);
                        GameObject newFighter = Object.Instantiate(shootComponent.Bullet, shootComponent.Spawn.position, shootComponent.Bullet.transform.rotation);
                        Rigidbody rb = newFighter.GetComponent<Rigidbody>();
                        rb.AddForce(-shootComponent.Spawn.forward * 300);

                    }
                }
            }
        }


        private float GetRandomTime()
        {
            return Random.Range(1, 5);
        }
    }
}