using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.Collections.Generic;

public class MessageHandler
{
    // Базовый путь к вашей директории
    private string basePath = @"C:\Users\denis\hakatonlguchat\Assets\Scripts\";

    // Словарь вопросов и ответов
    private Dictionary<string, string> questAnswers = new Dictionary<string, string>()
    {
        {"Кто основал Петергоф?", "Петергоф был основан в 1710 году Петром I как императорская загородная резиденция."},
        {"Кто жил в Петергофе?", "После основания дворца и посёлков Петром I, резиденция послужила домом многим правителям."},
        {"Кто построил фонтан Самсона и Льва?", "Следующая за ним Анна Иоанновна и вложилась в улучшение Петергофа. Именно при ней был построен фонтан Самсона и льва."},
        {"На что посмотреть в Петергофе?", "В первую очередь каждый обязан увидеть ансамбль фонтанов \"Большой каскад\", самые знаменитые среди которых Самсон, Нептун, Адам и Ева"},
        {"Кто вы?", "Мы персонажи знаменитого фонтана в Петергофе \"Самсон, разрывающий пасть льву\". Сейчас мы здесь, чтобы помочь вам..."},
        {"Интересный факт", "На обслуживание всех фонтанов в Петергофе одновременно уходит 900 литров воды!"},
        {"Привет", "Привет! Я фонтан Самсон, а это мой верный друг лев. Мы - ваши гиды по Петергофу!" }
    };

    public string ProcessQuestions(string question)
    {
        // Проверяем, есть ли вопрос в словаре
        if (questAnswers.ContainsKey(question))
        {
            return FormatAnswer(questAnswers[question]);
        }

        // Полный путь к файлу
        string questionPath = Path.Combine(basePath, "question.txt");

        // Записываем вопрос в файл
        File.WriteAllText(questionPath, question);

        Thread.Sleep(10000);
        // Полный путь к файлу ответа
        string answerPath = Path.Combine(basePath, "answer.txt");

        // Читаем ответ из файла
        string answer = File.ReadAllText(answerPath);
        File.Delete(answerPath);

        // Форматируем ответ
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