# Api
Подробная документация размещена в сваггере: `{localhost:8080}/swagger`
```
GET /api/survey/all - получить все анкеты
POST /api/survey/interview/new - создать новое интервью
GET /api/survey/question/{id} - получить текст вопроса и ответы к нему
POST /api/survey/result - сохранить ответ на вопрос
```
# Запуск
```bash
git clone
cd SurveyService
sudo docker compose pull
sudo docker compose build
sudo docker compose up -d
open http://localhost:8080/swagger
```
# Запуск тестов
```bash
sudo docker compose -f test.compose.yaml pull
sudo docker compose -f test.compose.yaml up -d
dotnet test (?)
```