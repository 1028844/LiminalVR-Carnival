using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform _barrel;
    [SerializeField] LayerMask _attackableLayers;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        Physics.Raycast(_barrel.transform.position, _barrel.forward, out hit, Mathf.Infinity, _attackableLayers);

        if(hit.transform != null)
        {
            hit.transform.GetComponent<Target>().Hit();
        }
    }
}
