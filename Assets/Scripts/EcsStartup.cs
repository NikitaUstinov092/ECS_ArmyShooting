using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Systems.Bullet;
using Systems.Unit;
using UnityEngine;
using UnityEngine.Serialization;
using Views;

public sealed class EcsStartup : MonoBehaviour    
   {
        [FormerlySerializedAs("_data")] [SerializeField]
        private UnitData _unitData;
        
        [SerializeField]
        private BulletData _bulletData;
        
        private EcsWorld _world;
        private IEcsSystems _systems;

        private void Start () 
        {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _systems
                 .Add (new UnitEntityInitialazerSystem())
                 .Add (new UnitTeamSelectorSystem())
                 .Add (new UnitComponentSettingSystem())
                 .Add (new UnitTeamSpawnSystem())
                 .Add (new UnitShootComponentSettingSystem())
                 .Add (new MoveSystem())
                 .Add (new UnitCheckShootDistanceSystem())
                 .Add (new BulletEntityFactorySystem())
                 .Add (new BulletComponentSettingSystem())
                 .Add (new BulletSpawnSystem())
                 .Add (new UnitShootCoolDownSystem())
            
                
                
                 // .Add(new ShootInitSystem())
                 //.Add(new ShootRunSystem())
                //.Add(new ShootCountDownSystem())            
               //  .Add(new DamageSystem())
              //   .Add(new DestroySystem())

                // .Add (new TestSystem2 ())

                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif          
                .Inject()
                .Inject(_unitData)
                .Inject(_bulletData)
                .Init ();
        }

        private void Update () 
        {
            // process systems here.
            _systems?.Run ();
        }

        private void OnDestroy () 
        {
            if (_systems != null) 
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world == null) 
                return;
            _world.Destroy ();
            _world = null;
        }
    }



