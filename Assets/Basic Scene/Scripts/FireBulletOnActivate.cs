using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class FireBulletOnActive : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;

    void Start()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabbable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    public void FireBullet(ActivateEventArgs args)
    {
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().linearVelocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnedBullet, 5);
    }
}
