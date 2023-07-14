using Leopotam.EcsLite;
using UnityEngine;

public abstract class ECSMonoObject : MonoBehaviour
{
    public EcsPackedEntity ecsPacked { get; private set; }
    protected EcsWorld _world;
    public void Init(EcsWorld world) => _world = world;
    public void PackEntity(int entity) => ecsPacked = _world.PackEntity(entity);

    protected virtual void OnTriggerAction(ECSMonoObject firstCollide, ECSMonoObject secondCollide)
    {
        if (_world != null)
        {
            Debug.Log("OnTriggerAction");
            int entity = _world.NewEntity();
            var poolHitC = _world.GetPool<HitComponent>();
            ref HitComponent hitC = ref poolHitC.Add(entity);
            hitC.firstCollide = firstCollide;
            hitC.secondCollide = secondCollide;
        }
    }
}