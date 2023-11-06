using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

struct MoveSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<ViewComponent, MoveComponent>> _filterMovableObjects;
    private readonly EcsPoolInject<ViewComponent> _poolView;
    private readonly EcsPoolInject<MoveComponent> _poolMove;
    private const float MoveMultiple = 1000f;

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filterMovableObjects.Value)
        {
            ref var viewComp = ref _poolView.Value.Get(entity);
            ref var moveComp = ref _poolMove.Value.Get(entity);
            
            if(moveComp.Direction == null || viewComp.View == null)
                return;

            var position = viewComp.View.transform.position;
            position =
                Vector3.MoveTowards(position, new Vector3(position.x, position.y, moveComp.Direction.z * MoveMultiple), 
                    moveComp.Speed * Time.deltaTime);
            viewComp.View.transform.position = position;
        }
    }
}
