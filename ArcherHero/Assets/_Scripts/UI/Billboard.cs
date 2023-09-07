
using UnityEngine;
using Zenject;

public class Billboard : MonoBehaviour
{
    private Camera _cam;
    
    [Inject]
    private void Construct(Camera camera)
    {
        _cam = camera;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + _cam.transform.forward);     
    }
}
