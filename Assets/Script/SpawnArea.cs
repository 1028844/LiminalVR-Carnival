using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public float spawnAreaX = 0;
    public float spawnAreaZ = 0;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(transform.position.x - spawnAreaX, transform.position.y, transform.position.z), new Vector3(transform.position.x + spawnAreaX, transform.position.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z - spawnAreaZ), new Vector3(transform.position.x, transform.position.y, transform.position.z + spawnAreaZ));
    }
}