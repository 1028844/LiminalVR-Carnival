using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour, ITarget
{
    [SerializeField] UnityEvent hitEvent;
    [SerializeField] GameObject hitSoundObj;
    public int scoreReward = 1;

    [SerializeField] float movingTargetBounds;
    bool _moving = false;
    int _direction = 1;
    float _speed = 1;
    float _minMoveX, _maxMoveX;

    void Start()
    {
        _minMoveX = transform.position.x - movingTargetBounds;
        _maxMoveX = transform.position.x + movingTargetBounds;

        _direction = Random.Range(-1, 2);

        RandomizeMovingTarget();
    }

    void Update()
    {
        if (_moving)
        {
            if (transform.position.x > _maxMoveX) _direction = -1;
            if (transform.position.x < _minMoveX) _direction = 1;

            transform.position = new Vector3(transform.position.x + (_direction * _speed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }

    public void Hit()
    {
        hitEvent.Invoke();
        CreateHitSound();
    }

    public void DestroyTarget()
    {
        Destroy(transform.parent.gameObject);
    }
    public void DestroyStartTarget()
    {
        Destroy(transform.gameObject);
    }

    public void DestroyGoodTarget()
    {
        RandomSpawner.goodTargets--;
        DestroyTarget();
    }

    public void DestroyBadTarget()
    {
        RandomSpawner.badTargets--;
        DestroyTarget();
    }

    void RandomizeMovingTarget()
    {
        int isMoving = Random.Range(0, 2);
        if (isMoving == 1) _moving = true;
    }

    void CreateHitSound()
    {
        GameObject newObj = Instantiate(hitSoundObj, transform.position, Quaternion.identity);
        newObj.transform.parent = null;
    }
}
