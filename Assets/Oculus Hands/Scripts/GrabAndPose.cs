using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GrabAndPose : MonoBehaviour
{
    public float poseTransitionDuration = 0.2f;
    public HandData rightHandPose;
    public HandData leftHandPose;

    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;
    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;

    private Quaternion[] startingFingerRotation;
    private Quaternion[] finalFingerRotation;

    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnSetPose);

        rightHandPose.gameObject.SetActive(false);
        leftHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs e)
    {
        if (e.interactorObject is XRDirectInteractor)
        {
            HandData handData = e.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = false;

            if (handData.handType == HandData.HandModelType.right)
            {
                SetHandDataValues(handData, rightHandPose);
            }
            else
            {
                SetHandDataValues(handData, leftHandPose);
            }
            StartCoroutine(SetHandDataRoutine(handData, finalHandPosition, finalHandRotation, finalFingerRotation, startingHandPosition, startingHandRotation, startingFingerRotation));
        }
    }

    public void UnSetPose(BaseInteractionEventArgs e)
    {
        if (e.interactorObject is XRDirectInteractor)
        {
            HandData handData = e.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;

            StartCoroutine(SetHandDataRoutine(handData, startingHandPosition, startingHandRotation, startingFingerRotation, finalHandPosition, finalHandRotation, finalFingerRotation));
        }
    }

    public void SetHandDataValues(HandData h1, HandData h2)
    {
        startingHandPosition = h1.root.localPosition;
        finalHandPosition = h2.root.localPosition;

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotation = new Quaternion[h1.fingerBones.Length];
        finalFingerRotation = new Quaternion[h2.fingerBones.Length];

        for (int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRotation[i] = h1.fingerBones[i].localRotation;
            finalFingerRotation[i] = h2.fingerBones[i].localRotation;
        }
    }

    public void SetHandData(HandData h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
    {
        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;

        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            h.fingerBones[i].localRotation = newBonesRotation[i];
        }
    }

    public IEnumerator SetHandDataRoutine(HandData h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation,
        Vector3 startingPosition, Quaternion startingRotation, Quaternion[] startingBonesRotation)
    {
        float timer = 0;

        while (timer < poseTransitionDuration)
        {
            Vector3 p = Vector3.Lerp(startingPosition, newPosition, timer / poseTransitionDuration);
            Quaternion r = Quaternion.Lerp(startingRotation, newRotation, timer / poseTransitionDuration);
            h.root.localPosition = p;
            h.root.localRotation = r;

            for(int i =0; i < newBonesRotation.Length; i++)
            {
                h.fingerBones[i].localRotation = Quaternion.Lerp(startingBonesRotation[i], newBonesRotation[i], timer / poseTransitionDuration);
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }

#if UNITY_EDITOR

    [MenuItem("Tools/Mirror selected right grab pose")]
    public static void MirrorRightPose()
    {
        GrabAndPose handPose = Selection.activeGameObject.GetComponent<GrabAndPose>();
        handPose.MirrorPose(handPose.leftHandPose, handPose.rightHandPose);
    }

#endif

    public void MirrorPose(HandData poseToMirror, HandData poseUsedToMirror)
    {
        Vector3 mirroredPosition = poseUsedToMirror.root.localPosition;
        mirroredPosition.x *= -1;

        Quaternion mirroredQuaternion = poseUsedToMirror.root.localRotation;
        mirroredQuaternion.y *= -1;
        mirroredQuaternion.z *= -1;

        poseToMirror.root.localPosition = mirroredPosition;
        poseToMirror.root.localRotation = mirroredQuaternion;

        for( int i = 0; i < poseUsedToMirror.fingerBones.Length; i++)
        {
            poseToMirror.fingerBones[i].localRotation = poseUsedToMirror.fingerBones[i].localRotation;
        }
    }
}
