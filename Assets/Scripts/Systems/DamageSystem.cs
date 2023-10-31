using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

internal struct DamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent>> _filterHits;
        private readonly EcsPoolInject<HitComponent> _poolHits;
        private readonly EcsPoolInject<HealthComponent> _poolHealth;
        private readonly EcsWorldInject _world;
        private const int Damage = 1; 
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterHits.Value)
            {
              
                ref HitComponent hitC = ref _poolHits.Value.Get(entity);

                if (hitC.FirstCollider == null)
                   continue;

                if (hitC.SecondCollider == null)
                   continue;

                if (hitC.FirstCollider.CompareTag(hitC.SecondCollider.tag))
                   continue;

                if (hitC.FirstCollider.GetComponent<BulletHitColider>()!=null 
                   && hitC.SecondCollider.GetComponent<BulletHitColider>() != null)
                   continue;

                int fighterEntity = PackerEntityUtils.UnpackEntities(_world.Value, hitC.SecondCollider.ecsPacked);
            
                ref HealthComponent healthC = ref _poolHealth.Value.Get(fighterEntity);

                healthC.Health -= Damage;

                Object.DestroyImmediate(hitC.FirstCollider.gameObject);
                _world.Value.DelEntity(entity);
               
            }
        }
    }
