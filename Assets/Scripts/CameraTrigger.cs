using Unity.Cinemachine;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineConfiner3D confine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            confine.BoundingVolume = this.GetComponent<BoxCollider>();
            Debug.Log("HEY!");
        }

    }
}
