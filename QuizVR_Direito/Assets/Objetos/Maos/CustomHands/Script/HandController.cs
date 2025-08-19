using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] InputActionReference gripInputAction;
    [SerializeField] InputActionReference triggerInputAction;

    private Animator handAnimator;

    private const string trigger = "Trigger";
    private const string grip = "Grip";

    private void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        gripInputAction.action.Enable();
        triggerInputAction.action.Enable();
    }

    private void OnDisable()
    {
        gripInputAction.action.Disable();
        triggerInputAction.action.Disable();
    }

    private void Update()
    {
        float gripValue = gripInputAction.action.ReadValue<float>();
        float triggerValue = triggerInputAction.action.ReadValue<float>();

        handAnimator.SetFloat(grip, gripValue);
        handAnimator.SetFloat(trigger, triggerValue);
    }
}