## Api
Подробная документация размещена в сваггере: `{localhost:8080}/swagger`
```
GET /api/survey/all - получить все анкеты
POST /api/survey/interview/new - создать новое интервью
GET /api/survey/question/{id} - получить текст вопроса и ответы к нему
POST /api/survey/result - сохранить ответ на вопрос
```
## App Flow
```
'GET /survey/all' -> выбрать surveyId
surveyId, userId -> 'POST /survey/interview/new' -> сохранить interviewId, questionIds
foreach questionId in questionIds:
    questionId -> 'GET /survey/question/{id} -> показать вопрос и ответы
    interviewId, answerId -> 'POST /survey/result'
```
## Запуск
```bash
git clone git@github.com:StarNuik/SurveyService.git
cd SurveyService
sudo docker compose pull
sudo docker compose build
sudo docker compose up -d
open http://localhost:8080/swagger
```
## Запуск тестов
```bash
sudo docker compose -f test.compose.yaml pull
sudo docker compose -f test.compose.yaml up -d
dotnet run --project SurveyService.Migrations
dotnet test
```