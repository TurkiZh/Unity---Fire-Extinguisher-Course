using UnityEngine;

public class SweepDetect : MonoBehaviour
{
    public enum ExtinguisherType
    {
        Foam,
        Water,
        CO2
    }

    [SerializeField] private ExtinguisherType requiredExtinguisher;
    [SerializeField] private string zoneID; // "A" or "B"
    [SerializeField] private SweepManager sweepManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredExtinguisher.ToString()))
        {
            sweepManager.RegisterSweep(zoneID);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(requiredExtinguisher.ToString()))
        {
            sweepManager.RegisterSweep(zoneID);
        }
    }
}
