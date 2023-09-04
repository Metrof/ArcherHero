using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public abstract class DropItem : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotationAngle;
    [SerializeField]
    private float _timeRotation;

    private Tween _tween;

    private void Start()
    {
        _tween = transform.DORotate(_rotationAngle, _timeRotation, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _tween.Kill();
            TakeItem(player);
        }
    }

    protected abstract void TakeItem(Player player);
    
}
