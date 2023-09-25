using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public abstract class DropItem : MonoBehaviour
{
    [SerializeField, Min(0)]
    private float _lifeTime;
    [SerializeField, Min(0)]
    protected float _timeActionInSeconds;
    [SerializeField]
    private Vector3 _rotationAngle;
    [SerializeField]
    private float _timeRotation;

    private Tween _tween;

    private void Start()
    {
        _tween = transform.DORotate(_rotationAngle, _timeRotation, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        StartCoroutine(Countdown(_lifeTime));
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
    
    private IEnumerator Countdown(float timeInSec)
    {
        yield return new WaitForSeconds(timeInSec);

        _tween.Kill();
        Destroy(gameObject);
    }

}
