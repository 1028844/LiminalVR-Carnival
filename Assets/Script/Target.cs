using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Target : MonoBehaviour, ITarget
{
    public float Time;
    [SerializeField] UnityEvent hitEvent;
    public int scoreReward = 1;

    public void Start()
    {
        Time = RandomSpawner.TimeSet;
        StartCoroutine(Decay());
    }

    public void Hit()
    {
        hitEvent.Invoke();
        if(RandomSpawner.TimeSet > 3f)
        {
             RandomSpawner.TimeSet -= 0.33f;
        }   
    }

    public void DestroyTarget()
    {
        Destroy(transform.parent.gameObject);
    }

    public void DestroyGoodTarget()
    {
        RandomSpawner.goodTargets--;
        FindObjectOfType<AudioManager>().Play("Yay");
    }

    public void DestroyBadTarget()
    {
        RandomSpawner.badTargets--;
        FindObjectOfType<AudioManager>().Play("Negative");
    }

private IEnumerator Decay()
    {
        yield return new WaitForSeconds(Time);
        Destroy(gameObject);
    }
}