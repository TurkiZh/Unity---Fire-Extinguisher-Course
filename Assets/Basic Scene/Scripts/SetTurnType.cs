using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SetTurnType : MonoBehaviour
{
    public ControllerInputActionManager rightHandTurn;

    public void SetTypeFromIndex(int index)
    {
        if(index == 0)
        {
            rightHandTurn.smoothTurnEnabled = true;
        }        
        else if(index == 1)
        {
            rightHandTurn.smoothTurnEnabled = false;
        }
    }
}
