using System.Collections;
using UnityEngine;

public class WaterOnFireWarning : MonoBehaviour
{
    private CapsuleCollider capsuleCollider;

    [Header("Message")]
    [SerializeField] private GameObject message;

    private void Start()
    {
        message.SetActive(false);
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            message.SetActive(true);
            StartCoroutine(DeactivateAfterDelay());
        }
    }

    IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(5); // Wait for the specified time
        if (message != null)
        {
            message.SetActive(false); // Deactivate the GameObject
        }
    }
}
