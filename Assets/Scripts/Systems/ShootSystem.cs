using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

struct ShootSystem : IEcsInitSystem, IEcsRunSystem
{
    private readonly EcsCustomInject<SharedData> _data;
    private readonly EcsFilterInject<Inc<ArmyComponent, UnitTypeComponent>> ecsFilterArmyUnitType;
    private readonly EcsFilterInject<Inc<ShootComponent>> ecsFilterShoot;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        EcsPool<ShootComponent> poolShoot = world.GetPool<ShootComponent>();

        for (int i = 0; i < ecsFilterArmyUnitType.Value.GetEntitiesCount(); i++)
        {        
            int entity = world.NewEntity();

            ref var armyComponent = ref ecsFilterArmyUnitType.Pools.Inc1.Get(i);
            ref var unitTypeComponent = ref ecsFilterArmyUnitType.Pools.Inc2.Get(i);

            if(armyComponent.TeamNumber == 0)   
                SetShootCompEntity(entity, poolShoot, unitTypeComponent, "Red Team", Color.blue);
     
            else if (armyComponent.TeamNumber == 1)      
                SetShootCompEntity(entity, poolShoot, unitTypeComponent, "Blue Team", Color.red);          
            
        }
    }


    private void SetShootCompEntity(int entity ,EcsPool<ShootComponent> poolShoot, UnitTypeComponent unit, string targetTag, Color bulletColor)
    {
        poolShoot.Add(entity).Spawn = unit.View.transform;
        poolShoot.Get(entity).TargetTag = targetTag;
        var bullet = poolShoot.Get(entity).Bullet = _data.Value.Bullet;
        bullet.GetComponent<MeshRenderer>().sharedMaterial.color = bulletColor;
    }


    public  void Run(IEcsSystems systems)
    {

        for (int i = 0; i < ecsFilterShoot.Value.GetEntitiesCount(); i++)
        {
            ref var shootComponent = ref ecsFilterShoot.Pools.Inc1.Get(i);
           // await Shoot(shootComponent);
        }
    }


    private IEnumerator ShootCoroutine(ShootComponent shootComponent)
    {
        yield return new WaitForSeconds(2f);
        GameObject bullet = Object.Instantiate(shootComponent.Bullet, shootComponent.Spawn.transform.position, Quaternion.identity);
      
    }

}
