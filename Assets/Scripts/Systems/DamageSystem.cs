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

            hitC.firstCollide.gameObject.TryGetComponent(out BulletHitColider firstBullet);
              hitC.secondCollide.gameObject.TryGetComponent(out BulletHitColider secondBullet);

            if (firstBullet != null && secondBullet != null)
                continue;          

            (int, int) entitiesCollide =
                       PackerEntityUtils.UnpackEntities(_world.Value, hitC.firstCollide.ecsPacked, hitC.secondCollide.ecsPacked); //“ы пытаешьс€ распаковать entity а она удалена

                ref HealthComponent healthC = ref _poolHealth.Value.Get(entitiesCollide.Item1);
               
                healthC.Health -= _damage;
            
            Object.Destroy(hitC.firstCollide.gameObject);
            _world.Value.DelEntity(entity);        

            }
        }
    }
