using UnityEngine;

public class SweepManager : MonoBehaviour
{
    [Header("Sweep")]
    [SerializeField] private float maxTimeBetweenZones = 1.5f;
    [SerializeField] private int requiredSweeps = 3;
    private string lastZone = "";
    private float lastTime = 0f;
    private int sweepCount = 0;

    [Header("Fire")]
    [SerializeField] private GameObject fireToExtinguish;

    public void RegisterSweep(string currentZone)
    {
        float currentTime = Time.time;

        if (lastZone != "" && currentZone != lastZone && (currentTime - lastTime <= maxTimeBetweenZones))
        {
            sweepCount++;
            Debug.Log("Valid Sweep! Count: " + sweepCount);

            if (sweepCount >= requiredSweeps)
            {
                ExtinguishFire();
            }
        }

        lastZone = currentZone;
        lastTime = currentTime;
    }

    void ExtinguishFire()
    {
        fireToExtinguish.SetActive(false); // or play fade/scale/fire down animation
        Debug.Log("Fire extinguished!");
    }
}
