using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour, ITarget
{
    [SerializeField] UnityEvent hitEvent;

    public void Hit()
    {
        hitEvent.Invoke();
    }

    public void DestroyTarget()
    {
        Destroy(gameObject);
    }
}
