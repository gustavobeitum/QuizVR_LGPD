using UnityEngine;

public class BotaoResposta : MonoBehaviour
{
    public string letraResposta; // "A", "B", etc.
    public QuizManager quizManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") || other.CompareTag("Martelo"))
        {
            quizManager.Responder(letraResposta);
        }
    }
}
