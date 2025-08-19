using UnityEngine;
using System.Collections;

public class BotaoResetFisico : MonoBehaviour
{
    public QuizManager quizManager;
    public AudioClip somPressionar;

    private AudioSource audioSource;
    private bool podePressionar = true;
    public float tempoCooldown = 1f; // Tempo entre pressões

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (podePressionar && (other.CompareTag("Hand") || other.CompareTag("Martelo")))
        {
            StartCoroutine(AcionarBotao());
        }
    }

    private IEnumerator AcionarBotao()
    {
        podePressionar = false;

        if (audioSource != null && somPressionar != null)
        {
            audioSource.PlayOneShot(somPressionar);
        }

        quizManager.ReiniciarQuiz();

        yield return new WaitForSeconds(tempoCooldown);
        podePressionar = true;
    }
}
