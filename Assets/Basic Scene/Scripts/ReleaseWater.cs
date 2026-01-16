using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ReleaseWater : MonoBehaviour
{
    [Header("Water")]
    [SerializeField] private GameObject waterParticles; // GameObject holding the particle system
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private Transform waterSpawnPoint;

    [Header("Pin")]
    [SerializeField] private GameObject pin;
    private Rigidbody pinRgdBody;
    private bool pinPulled = false;

    private XRGrabInteractable grabbable;

    void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        pinRgdBody = pin.GetComponent<Rigidbody>();

        // Event bindings
        grabbable.activated.AddListener(StartFiring);
        grabbable.deactivated.AddListener(StopFiring);

        // Ensure it's off at start
        waterParticles.SetActive(false);
    }

    public void StartFiring(ActivateEventArgs args)
    {
        if (!pinPulled) return;
        waterParticles.SetActive(true);
        InvokeRepeating(nameof(ReleaseWaterProjectile), 0f, 0.4f);
    }

    public void StopFiring(DeactivateEventArgs args)
    {
        waterParticles.SetActive(false);
        CancelInvoke(nameof(ReleaseWaterProjectile));
    }

    void ReleaseWaterProjectile()
    {
        GameObject water = Instantiate(waterPrefab, waterSpawnPoint.position, waterSpawnPoint.rotation);

        if (water.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = waterSpawnPoint.forward * 2;
        }

        Destroy(water, 3f);
    }

    public void OnPinPulled()
    {
        pinPulled = true;
    }
}
