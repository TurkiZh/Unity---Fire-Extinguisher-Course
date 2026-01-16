using UnityEngine;

public class HandData : MonoBehaviour
{

    public enum HandModelType {left, right }
    public HandModelType handType;
    public Transform root;
    public Animator animator;
    public Transform[] fingerBones;
}
