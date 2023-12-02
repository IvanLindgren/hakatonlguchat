import g4f
import time
import os

def process_questions():
    
    print("Скрипт работает...")
    

    while True:
        # Проверяем, есть ли новый вопрос
        if os.path.exists('question.txt'):
            # Читаем вопрос из файла
            with open('question.txt', 'r') as file:
                question = file.read()
            question = "Представь, что ты экскурсовод по Петергофу по имени Самсон-фонтан. Пиши максимально серьезно, не используя смалйики. Твой лимит - 80 символов на ответ. Твой запрос от пользователя" + question + " , ответь на него."
            print("Вопрос был получен" + question)
            # Обрабатываем вопрос
            answer = g4f.ChatCompletion.create(
                model=g4f.models.gpt_4,
                messages=[{"role": "user", "content": question}]
            )

            print(answer)

            # Записываем ответ в файл
            with open('answer.txt', 'w', encoding='utf-8') as file:
                file.write(answer)
            os.remove('question.txt')

        # Ждем некоторое время перед следующей проверкой
        time.sleep(1)

if __name__ == "__main__":
    process_questions()
