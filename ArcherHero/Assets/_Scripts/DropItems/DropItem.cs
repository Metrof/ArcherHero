using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public abstract class DropItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            TakeItem(player);
        }
    }

    protected abstract void TakeItem(Player player);
    
}
