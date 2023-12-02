using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SpeechLib;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_InputField messageinput;
    [SerializeField] private GameObject messagePrefab;
    [SerializeField] private GameObject answerPrefab;
    [SerializeField] private RectTransform chatPanel;

    private float yOffset = 0f;
    private float messageHeight = 100f;
    private MessageHandler messageHandler = new MessageHandler();

    IEnumerator SpeakText(string text)
    {
        var samson = GameObject.FindGameObjectWithTag("Player");
        var animka = samson.GetComponent<samson>().anim;
        animka.SetTrigger("talk");
        SpVoice voice = new SpVoice();
        Debug.Log(text);
        if (string.IsNullOrEmpty(text)) yield break; // Если текст пуст, не делаем ничего
        voice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        // Ждем, пока голос не закончит произношение текста
        yield return new WaitUntil(() => voice.Status.RunningState == SpeechRunState.SRSEDone);
    }

    void SetAnswer(string text)
    {
        StartCoroutine(SpeakText(text));

        GameObject answerInstance = Instantiate(answerPrefab, chatPanel);
        answerInstance.SetActive(true);
        Message answerComponent = answerInstance.GetComponent<Message>();
        if (answerComponent != null && answerComponent.textMessage != null)
        {
            answerComponent.textMessage.text = text; // Устанавливаем текст сообщения
        }
        else
        {
            Debug.LogError("Message component or textMessage is not set in the prefab.");
        }
        // Устанавливаем позицию облачка сообщения
        RectTransform rectTransform = answerInstance.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, yOffset);
        // Обновляем смещение для следующего сообщения
        yOffset -= messageHeight;
        messageinput.text = ""; // Очищаем поле ввода
    }

    void SetMessage(string text)
    {
        var textFormat = text;
        for (int i = 25; i < textFormat.Length; i += 26) { textFormat = textFormat.Insert(i, "\n"); }
        if (string.IsNullOrEmpty(text)) return; // Если текст пуст, не делаем ничего
        GameObject messageInstance = Instantiate(messagePrefab, chatPanel);
        messageInstance.SetActive(true);
        Message messageComponent = messageInstance.GetComponent<Message>();
        if (messageComponent != null && messageComponent.textMessage != null)
        {
            messageComponent.textMessage.text = textFormat; // Устанавливаем текст сообщения
        }
        else
        {
            Debug.LogError("Message component or textMessage is not set in the prefab.");
        }
        // Устанавливаем позицию облачка сообщения
        RectTransform rectTransform = messageInstance.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, yOffset);
        // Обновляем смещение для следующего сообщения
        yOffset -= messageHeight;
        messageinput.text = ""; // Очищаем поле ввода
        Debug.Log(text);
        string answer = messageHandler.ProcessQuestions(text);
        SetAnswer(answer);
    }

    void Start()
    {
        messageinput.onEndEdit.AddListener(SetMessage);
    }
}
