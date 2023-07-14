using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

internal struct DamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent>> _filterHits;
        private readonly EcsPoolInject<HitComponent> _poolHits;
        private readonly EcsPoolInject<HealthComponent> _poolHealth;
        private readonly EcsWorldInject _world;
        private const int _damage = 1; 
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterHits.Value)
            {
              
            ref HitComponent hitC = ref _poolHits.Value.Get(entity);

            if (hitC.firstCollide == null)
                continue;

            if (hitC.secondCollide == null)
                continue;

            if (hitC.firstCollide.tag == hitC.secondCollide.tag)
                continue;

            if (hitC.firstCollide.GetComponent<BulletHitColider>()!=null 
                && hitC.secondCollide.GetComponent<BulletHitColider>() != null)
                continue;

            int fighterEntity = PackerEntityUtils.UnpackEntities(_world.Value, hitC.secondCollide.ecsPacked);
            
            ref HealthComponent healthC = ref _poolHealth.Value.Get(fighterEntity);

            healthC.Health -= _damage;

                Object.DestroyImmediate(hitC.firstCollide.gameObject);
                _world.Value.DelEntity(entity);
               
            }
        }
    }
