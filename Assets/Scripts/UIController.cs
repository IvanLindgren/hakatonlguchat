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
        if (string.IsNullOrEmpty(text)) yield break; // ���� ����� ����, �� ������ ������
        voice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        // ����, ���� ����� �� �������� ������������ ������
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
            answerComponent.textMessage.text = text; // ������������� ����� ���������
        }
        else
        {
            Debug.LogError("Message component or textMessage is not set in the prefab.");
        }
        // ������������� ������� ������� ���������
        RectTransform rectTransform = answerInstance.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, yOffset);
        // ��������� �������� ��� ���������� ���������
        yOffset -= messageHeight;
        messageinput.text = ""; // ������� ���� �����
    }

    void SetMessage(string text)
    {
        var textFormat = text;
        for (int i = 25; i < textFormat.Length; i += 26) { textFormat = textFormat.Insert(i, "\n"); }
        if (string.IsNullOrEmpty(text)) return; // ���� ����� ����, �� ������ ������
        GameObject messageInstance = Instantiate(messagePrefab, chatPanel);
        messageInstance.SetActive(true);
        Message messageComponent = messageInstance.GetComponent<Message>();
        if (messageComponent != null && messageComponent.textMessage != null)
        {
            messageComponent.textMessage.text = textFormat; // ������������� ����� ���������
        }
        else
        {
            Debug.LogError("Message component or textMessage is not set in the prefab.");
        }
        // ������������� ������� ������� ���������
        RectTransform rectTransform = messageInstance.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, yOffset);
        // ��������� �������� ��� ���������� ���������
        yOffset -= messageHeight;
        messageinput.text = ""; // ������� ���� �����
        Debug.Log(text);
        string answer = messageHandler.ProcessQuestions(text);
        SetAnswer(answer);
    }

    void Start()
    {
        messageinput.onEndEdit.AddListener(SetMessage);
    }
}
