using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class IntroMessageManager : MonoBehaviour
{
    [SerializeField] private GameObject move; // XR Rig or movement root object
    [SerializeField] private GameObject teleportation; // XR Rig or movement root object
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button continueButton;

    private int messageStep = 0;

    private void Start()
    {
        // Lock player movement
        if (move != null)
            move.SetActive(false);
        if (teleportation != null)
            teleportation.SetActive(false);

        messagePanel.SetActive(true);
        messageText.text = "Welcome\nIn this scenario, you will learn how to Identify the cause of fire, choose the right fire extinguisher, and excute PASS method to estinguish the fire";
        continueButton.onClick.AddListener(HandleContinue);
    }

    private void HandleContinue()
    {
        messageStep++;

        if (messageStep == 1)
        {
            messageText.text = "The \"PASS\" method is a helpful acronym for remembering the steps to use a fire extinguisher. It stands for\n" +
                "Pull, Aim, Squeeze, and Sweep.\n" +
                "This method ensures efficient and safe operation of the extinguisher to combat small fires.";
            continueButton.GetComponentInChildren<TMP_Text>().text = "Continue";
        }
        else if (messageStep == 2)
        {
            messageText.text = "CAUTION: You have to use the right fire extinguisher\n" +
                "Water is primarily used for Class A fires (ordinary combustibles like wood, paper, and garbage).\n" +
                "Foam is effective on both Class A and Class B fires (flammable liquids like gasoline).\n" +
                "CO2 extinguishers are best for Class B fires and electrical fires.";
            continueButton.GetComponentInChildren<TMP_Text>().text = "Done";
        }
        else if (messageStep == 3)
        {
            messagePanel.SetActive(false);
            if (move != null)
                move.SetActive(true);
            if (teleportation != null)
                teleportation.SetActive(true);
        }
    }
}
