using System.Collections;
using UnityEngine;

public class BotaoInterativo : MonoBehaviour
{
    private Animator animator;
    private bool pressionado = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pressionado && (other.CompareTag("Hand") || other.CompareTag("Martelo")))
        {
            pressionado = true;
            animator.SetTrigger("Pressionar");
            StartCoroutine(VoltarAposTempo());
        }
    }

    private IEnumerator VoltarAposTempo()
    {
        yield return new WaitForSeconds(1f);
        Resetar();
    }

    public void Resetar()
    {
        pressionado = false;
    }
}
