using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class ReleaseCO : MonoBehaviour
{
    [Header("CO")]
    [SerializeField] private GameObject coParticles; // GameObject holding the particle system
    [SerializeField] private GameObject coPrefab;
    [SerializeField] private Transform coSpawnPoint;

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
        coParticles.SetActive(false);
    }

    public void StartFiring(ActivateEventArgs args)
    {
        if (!pinPulled) return;
        coParticles.SetActive(true);
        InvokeRepeating(nameof(ReleaseCOProjectile), 0f, 0.4f);
    }

    public void StopFiring(DeactivateEventArgs args)
    {
        coParticles.SetActive(false);
        CancelInvoke(nameof(ReleaseCOProjectile));
    }

    void ReleaseCOProjectile()
    {
        GameObject co = Instantiate(coPrefab, coSpawnPoint.position, coSpawnPoint.rotation);

        if (co.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = coSpawnPoint.forward * 5;
        }

        Destroy(co, 2f);
    }

    public void OnPinPulled()
    {
        pinPulled = true;
    }
}
