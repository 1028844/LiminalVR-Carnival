using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour, ITarget
{
    [SerializeField] UnityEvent hitEvent;
    [SerializeField] GameObject hitSoundObj;
    public int scoreReward = 1;

    public void Hit()
    {
        hitEvent.Invoke();
        CreateHitSound();
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

    void CreateHitSound()
    {
        GameObject newObj = Instantiate(hitSoundObj, transform.position, Quaternion.identity);
        newObj.transform.parent = null;
    }
}
