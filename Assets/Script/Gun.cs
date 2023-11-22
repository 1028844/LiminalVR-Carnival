using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform _barrel;
    [SerializeField] LayerMask _attackableLayers;

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) transform.position = new Vector3(transform.position.x + (7.0f * Time.deltaTime), transform.position.y, transform.position.z);
        if (Input.GetKey(KeyCode.D)) transform.position = new Vector3(transform.position.x - (7.0f * Time.deltaTime), transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;

        Physics.Raycast(_barrel.transform.position, _barrel.forward, out hit, Mathf.Infinity, _attackableLayers);

        if(hit.transform != null)
        {
            hit.transform.GetComponent<Target>().Hit();
        }
    }

    /*
    bool triggerValue;
    if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
    {
        Debug.Log("Trigger button is pressed.");
    }
    */
}
