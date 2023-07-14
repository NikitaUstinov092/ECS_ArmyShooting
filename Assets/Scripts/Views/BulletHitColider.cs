using UnityEngine;

public class BulletHitColider : ECSMonoObject
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<ECSMonoObject>(out var colliderFighter))
        {
            OnTriggerAction(this, colliderFighter);
        }
    }
}
