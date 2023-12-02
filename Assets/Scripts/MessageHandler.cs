using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.Collections.Generic;

public class MessageHandler
{
    // ������� ���� � ����� ����������
    private string basePath = @"C:\Users\denis\hakatonlguchat\Assets\Scripts\";

    // ������� �������� � �������
    private Dictionary<string, string> questAnswers = new Dictionary<string, string>()
    {
        {"��� ������� ��������?", "�������� ��� ������� � 1710 ���� ������ I ��� ������������� ���������� ����������."},
        {"��� ��� � ���������?", "����� ��������� ������ � ������� ������ I, ���������� ��������� ����� ������ ����������."},
        {"��� �������� ������ ������� � ����?", "��������� �� ��� ���� ��������� � ��������� � ��������� ���������. ������ ��� ��� ��� �������� ������ ������� � ����."},
        {"�� ��� ���������� � ���������?", "� ������ ������� ������ ������ ������� �������� �������� \"������� ������\", ����� ���������� ����� ������� ������, ������, ���� � ���"},
        {"��� ��?", "�� ��������� ����������� ������� � ��������� \"������, ����������� ����� ����\". ������ �� �����, ����� ������ ���..."},
        {"���������� ����", "�� ������������ ���� �������� � ��������� ������������ ������ 900 ������ ����!"},
        {"������", "������! � ������ ������, � ��� ��� ������ ���� ���. �� - ���� ���� �� ���������!" }
    };

    public string ProcessQuestions(string question)
    {
        // ���������, ���� �� ������ � �������
        if (questAnswers.ContainsKey(question))
        {
            return FormatAnswer(questAnswers[question]);
        }

        // ������ ���� � �����
        string questionPath = Path.Combine(basePath, "question.txt");

        // ���������� ������ � ����
        File.WriteAllText(questionPath, question);

        Thread.Sleep(10000);
        // ������ ���� � ����� ������
        string answerPath = Path.Combine(basePath, "answer.txt");

        // ������ ����� �� �����
        string answer = File.ReadAllText(answerPath);
        File.Delete(answerPath);

        // ����������� �����
        answer = FormatAnswer(answer);

        return answer;
    }

    private string FormatAnswer(string answer)
    {
        StringBuilder formattedAnswer = new StringBuilder();
        int count = 0;

        for (int i = 0; i < answer.Length; i++)
        {
            if (count == 150)
            {
                break;
            }

            formattedAnswer.Append(answer[i]);
            count++;

            if (count % 40 == 0)
            {
                formattedAnswer.AppendLine();
            }
        }

        return formattedAnswer.ToString();
    }
}