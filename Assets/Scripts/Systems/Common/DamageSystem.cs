using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

internal struct DamageSystem : IEcsRunSystem
    {
            private readonly EcsFilterInject<Inc<HitComponent>> _filterHits;
            private readonly EcsPoolInject<HitComponent> _poolHits;
            private readonly EcsPoolInject<DamageComponent> _poolDamage;
            private readonly EcsPoolInject<TeamComponent> _poolTeam;
            private readonly EcsPoolInject<HealthComponent> _poolHealth;
            private readonly EcsWorldInject _world;
            public void Run(IEcsSystems systems)
            {
                foreach(var entity in _filterHits.Value)
                {
                    ref var hitComp = ref _poolHits.Value.Get(entity);
                    
                    var entitiesCollide =
                        PackerEntityUtils.UnpackEntities(_world.Value, hitComp.FirstCollider.EcsPacked, hitComp.SecondCollider.EcsPacked);
                    
                    ref var firstCollideTeam = ref _poolTeam.Value.Get(entitiesCollide.Item1);
                    ref var secondCollideTeam = ref _poolTeam.Value.Get(entitiesCollide.Item2);
                    
                    ref var healthComp = ref _poolHealth.Value.Get(entitiesCollide.Item1);
                    ref var damageComp = ref _poolDamage.Value.Get(entitiesCollide.Item2);

                    if (firstCollideTeam.TeamType != secondCollideTeam.TeamType)
                    {
                        healthComp.Health -= damageComp.Damage;
                    }
                    _world.Value.DelEntity(entity);
                }
            }
    }
