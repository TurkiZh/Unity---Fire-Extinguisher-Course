using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ReleaseFoam : MonoBehaviour
{
    [Header("Foam")]
    [SerializeField] private GameObject foamPrefab;
    [SerializeField] private Transform foamSpawnPoint;
    [SerializeField] private float fireSpeed = 4f;
    [SerializeField] private float foamInterval = 0.1f; // How fast the foam shoots while holding

    [Header("Pin")]
    [SerializeField] private GameObject pin;
    private Rigidbody pinRgdBody;
    private bool pinPulled = false;

    private XRGrabInteractable grabbable;
    private bool isFiring = false;

    void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        pinRgdBody = pin.GetComponent<Rigidbody>();
        grabbable.activated.AddListener(StartFiring);
        grabbable.deactivated.AddListener(StopFiring);
    }

    public void StartFiring(ActivateEventArgs args)
    {
        if (!pinPulled) return;

        isFiring = true;
        InvokeRepeating(nameof(ReleaseFoamProjectile), 0f, foamInterval);
    }

    public void StopFiring(DeactivateEventArgs args)
    {
        isFiring = false;
        CancelInvoke(nameof(ReleaseFoamProjectile));
    }

    void ReleaseFoamProjectile()
    {
        GameObject foam = Instantiate(foamPrefab, foamSpawnPoint.position, foamSpawnPoint.rotation);

        if (foam.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = foamSpawnPoint.forward * fireSpeed;
        }

        Destroy(foam, 3f);
    }

    public void OnPinPulled()
    {
        pinPulled = true;
        //pinRgdBody.useGravity = true;
        //pinRgdBody.isKinematic = false;
    }
}
