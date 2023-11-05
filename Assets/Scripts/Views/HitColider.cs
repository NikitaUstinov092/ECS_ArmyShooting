using UnityEngine;

public class HitColider : ECSMonoObject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ECSMonoObject>(out var collide))
            OnTriggerAction(this, collide);
    }
}
