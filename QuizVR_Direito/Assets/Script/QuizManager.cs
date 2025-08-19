using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public GameObject[] perguntas;
    public string[] respostasCertas;

    public AudioClip somAcerto;
    public AudioClip somErro;
    public AudioClip somBotao;

    public Image imagemFundo;
    public GameObject telaFinal;
    public TextMeshProUGUI textoResultado;

    private AudioSource audioSource;
    private int perguntaAtual = 0;
    private int pontuacao = 0;
    private bool esperandoResposta = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        telaFinal.SetActive(false);
        MostrarPergunta(0);
    }

    public void Responder(string respostaSelecionada)
    {
        if (esperandoResposta) return;

        StartCoroutine(ExecutarFeedbackResposta(respostaSelecionada));
        audioSource.PlayOneShot(somBotao);
    }

    private IEnumerator ExecutarFeedbackResposta(string respostaSelecionada)
    {
        esperandoResposta = true;

        Color originalColor = imagemFundo.color;

        bool acertou = respostaSelecionada == respostasCertas[perguntaAtual];

        if (acertou)
        {
            imagemFundo.color = new Color(0f, 1f, 0f, originalColor.a); //Verde
            audioSource.PlayOneShot(somAcerto);
            pontuacao++;
        }
        else
        {
            imagemFundo.color = new Color(1f, 0f, 0f, originalColor.a); //Vermelho
            audioSource.PlayOneShot(somErro);
        }

        yield return new WaitForSeconds(1f);

        imagemFundo.color = new Color(0f, 0f, 0f, originalColor.a); //Preto
        perguntaAtual++;

        if (perguntaAtual < perguntas.Length)
        {
            MostrarPergunta(perguntaAtual);
        }
        else
        {
            MostrarTelaFinal();
        }

        esperandoResposta = false;
    }

    private void MostrarPergunta(int index)
    {
        for (int i = 0; i < perguntas.Length; i++)
        {
            perguntas[i].SetActive(i == index);
        }

        if (index < 0 || index >= perguntas.Length)
            return;

        Button[] botoes = perguntas[index].GetComponentsInChildren<Button>();

        foreach (Button botao in botoes)
        {
            botao.onClick.RemoveAllListeners();

            string textoAlternativa = botao.GetComponentInChildren<TextMeshProUGUI>().text;

            botao.onClick.AddListener(() => Responder(textoAlternativa));
        }
    }


    private void MostrarTelaFinal()
    {
        telaFinal.SetActive(true);
        float porcentagem = (float)pontuacao / perguntas.Length * 100f;
            textoResultado.text = $"Parabéns! Você acertou {pontuacao} de {perguntas.Length} perguntas.\n" +
                          $"Aproveitamento: {porcentagem:F0}%";

        // Desativa todas as perguntas
        foreach (var pergunta in perguntas)
        {
            pergunta.SetActive(false);
        }
    }

    public void ReiniciarQuiz()
    {
        pontuacao = 0;
        perguntaAtual = 0;
        telaFinal.SetActive(false);

        MostrarPergunta(-1);
        MostrarPergunta(0);
    }

}