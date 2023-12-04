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
        Destroy(transform.parent.gameObject);
    }

    public void DestroyGoodTarget()
    {
        RandomSpawner.goodTargets--;
    }

    public void DestroyBadTarget()
    {
        RandomSpawner.badTargets--;
    }
}
