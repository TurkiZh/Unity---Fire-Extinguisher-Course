using UnityEngine;

public class DestroyFire : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Destroy(other.gameObject); // Or trigger fire extinguishing effect
        }
    }

}
