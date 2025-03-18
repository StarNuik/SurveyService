# day 1
## Сущности
### Связи
- Анкета
  - 1:М вопросов
- Вопрос из анкеты
  - 1:М ответов
- Вариант ответа на вопрос
- Сессия анкеты
  - не вписывается в Апи
- Ответ пользователя
    - 1:1 к варианту

### Апи
- Получить вопрос и варианты ответа
  - in
    - айди вопроса
  - out
    - текст вопроса
    - массив вариантов ответа
      - айди ответа
      - текст ответа
- Сохранить ответ на вопрос
  - in
    - айди ответа
    - айди пользователя
  - out
    - айди след вопроса

тут не хватает:
  - айди опроса
  - как стартануть опрос

### Структура
- Анкета
  - айди
  - Название, описание (не используется)
- Вопрос анкеты
  - айди
  - реф Анкета.айди
  - Текст вопроса
- Вариант ответа
  - айди
  - реф Вопрос.айди
  - Текст
- Сессия
  - ???
- Ответ пользователя
  - айди
  - айди пользователя
  - ~~реф Анкета.айди~~
  - ~~реф Вопрос.айди~~
  - реф Ответ.айди

### возможные юзкейсы
#### как ответили пользователи на вопрос
```sql
select Question.Y, Answer.Y from Question
where Question.Id = {}
join Result where Result.QuestionId = Question.Id
join Answer where Result.AnswerId = Answer.Id
(group by ???, count() ???)
```
#### как ответил пользователь на все вопросы анкеты
```sql
select Question.Y, Answer.Y from Result
where Result.SurveyId = {} and Result.UserId = {}
join Question where Result.QuestionId = Question.Id
join Answer where Result.AnswerId = Answer.Id
```

## Семантика Api
### Получить вопрос
```
GET .../question/{questionId}
```
### Сохранить ответ
```
POST .../question/result
or
PUT  .../session/{sessionId}/question/{questionId}
```

## Вопросы
кто управляет сессией - фронт или бек

# day 2

предполагаю такой флоу:
```
userId, surveyId -> `POST /new-interview`
`POST /new-interview` -> interviewId, first QuestionResponse
do
    show QuestionResponse
    interviewId, answerId -> `POST /result`
    nextQuestionId -> `GET /question`
    `GET /question` -> next QuestionResponse
done
```

# day 3
я добавил ручку `/new-interview`, чтобы стартовать сессию
можно добавить ручку на получение опросников, чтобы только из Апи-шки можно было все проверить

я использую нумерацию по индексам вместо линкед листа для хранения вопросов в бд
тк иначе возникала circular dependency

  
