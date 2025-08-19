using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VoltarParaOrigem : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private Vector3 posicaoOriginal;
    private Quaternion rotacaoOriginal;

    private void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        posicaoOriginal = transform.position;
        rotacaoOriginal = transform.rotation;

        grab.selectExited.AddListener(OnSoltar);
    }

    private void OnSoltar(SelectExitEventArgs args)
    {
        StartCoroutine(RetornarAposTempo(1.5f));
    }

    private System.Collections.IEnumerator RetornarAposTempo(float tempo)
    {
        yield return new WaitForSeconds(tempo);

        transform.position = posicaoOriginal;
        transform.rotation = rotacaoOriginal;

        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
