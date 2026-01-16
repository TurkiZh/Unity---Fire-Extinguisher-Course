using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HapticInterractaable : MonoBehaviour
{
    [Range(0,1)]
    public float intensity;
    public float duration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.activated.AddListener(TriggerHaptic);
    }

    public void TriggerHaptic(BaseInteractionEventArgs e)
    {
        if (e.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor controllerInterractor)
            {
            TriggerHaptic(controllerInterractor.xrController);
            }
    }

    public void TriggerHaptic(XRBaseController controller)
    {
        if (intensity > 0)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
    }
}
