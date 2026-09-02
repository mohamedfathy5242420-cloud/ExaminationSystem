# Examination System API Documentation

Base URL during local development:

```text
https://localhost:{port}
```

All protected endpoints require:

```http
Authorization: Bearer {accessToken}
Content-Type: application/json
```

## User Types

Use `userType` during registration:

```text
Student
Instructor
Admin
```

The JWT contains `user_type`, and the API uses it for policies:

```text
AdminOnly
StudentOnly
InstructorOnly
```

## Authentication

### Register User

```http
POST /api/Auth/register
```

Body:

```json
{
  "fullName": "Demo Student",
  "email": "student@example.com",
  "password": "Password1",
  "userType": "Student"
}
```

Notes:

- Creates `Student`, `Instructor`, or `Admin` entity depending on `userType`.
- Account remains `Pending`.
- Sends OTP email if SMTP settings are valid.

### Verify Account

```http
POST /api/Auth/verify-account
```

Body:

```json
{
  "email": "student@example.com",
  "otpCode": "123456"
}
```

Notes:

- Activates pending account.
- OTP must be valid, unused, and not expired.

### Login

```http
POST /api/Auth/login
```

Body:

```json
{
  "email": "student@example.com",
  "password": "Password1"
}
```

Returns:

```json
{
  "accessToken": "...",
  "accessTokenExpiresAt": "2026-09-02T10:30:00Z",
  "refreshToken": "...",
  "refreshTokenExpiresAt": "2026-09-09T10:00:00Z",
  "userId": "guid",
  "fullName": "Demo Student",
  "email": "student@example.com",
  "userType": "Student"
}
```

Notes:

- Login is blocked if account is not active.

### Refresh Token

```http
POST /api/Auth/refresh-token
```

Body:

```json
{
  "refreshToken": "..."
}
```

Notes:

- Rotates refresh token.
- Returns new access token and refresh token.

### Forgot Password

```http
POST /api/Auth/forgot-password
```

Body:

```json
{
  "email": "student@example.com"
}
```

Notes:

- Generates password reset OTP.
- Sends reset email if SMTP settings are valid.

### Reset Password

```http
POST /api/Auth/reset-password
```

Body:

```json
{
  "email": "student@example.com",
  "otpCode": "123456",
  "newPassword": "Password1"
}
```

## Admin Content Management

Admin endpoints require an Admin access token.

### Get Diplomas

```http
GET /api/admin/diplomas
```

### Create Diploma

```http
POST /api/admin/diplomas
```

Body:

```json
{
  "title": "Backend Development Diploma",
  "description": "Learn backend fundamentals.",
  "instructorId": "guid"
}
```

### Update Diploma

```http
PUT /api/admin/diplomas/{id}
```

Body:

```json
{
  "title": "Backend Development Diploma",
  "description": "Updated description.",
  "instructorId": "guid"
}
```

### Delete Diploma

```http
DELETE /api/admin/diplomas/{id}
```

Notes:

- Soft deletes the diploma.

### Get Quizzes By Diploma

```http
GET /api/admin/quizzes/by-diploma/{diplomaId}
```

### Create Quiz

```http
POST /api/admin/quizzes
```

Body:

```json
{
  "diplomaId": "guid",
  "title": "C# Basics Quiz",
  "duration": 20,
  "passScore": 2,
  "maxAttempts": 3,
  "instructions": "Choose the best answer."
}
```

### Update Quiz

```http
PUT /api/admin/quizzes/{id}
```

Body:

```json
{
  "title": "C# Basics Quiz",
  "duration": 25,
  "passScore": 2,
  "maxAttempts": 3,
  "instructions": "Updated instructions."
}
```

### Delete Quiz

```http
DELETE /api/admin/quizzes/{id}
```

### Publish Quiz

```http
POST /api/admin/quizzes/{id}/publish
```

Rules:

- Quiz must exist.
- Quiz must have at least one question.
- Each question must have at least two options.
- Each question must have exactly one correct option.

### Unpublish Quiz

```http
POST /api/admin/quizzes/{id}/unpublish
```

### Get Questions By Quiz

```http
GET /api/admin/questions/by-quiz/{quizId}
```

### Create Question

```http
POST /api/admin/questions
```

Body:

```json
{
  "quizId": "guid",
  "text": "Which keyword declares a class in C#?",
  "explanation": "The class keyword defines a reference type.",
  "order": 1,
  "score": 1,
  "options": [
    {
      "text": "class",
      "isCorrect": true
    },
    {
      "text": "define",
      "isCorrect": false
    }
  ]
}
```

### Update Question

```http
PUT /api/admin/questions/{id}
```

Body:

```json
{
  "text": "Updated question text",
  "explanation": "Updated explanation",
  "order": 1,
  "score": 1,
  "options": [
    {
      "text": "Correct answer",
      "isCorrect": true
    },
    {
      "text": "Wrong answer",
      "isCorrect": false
    }
  ]
}
```

Rules:

- Cannot update questions in a published quiz.
- Replaces the question options.
- Must contain exactly one correct option.

### Delete Question

```http
DELETE /api/admin/questions/{id}
```

Rules:

- Cannot delete questions from a published quiz.

## Student Learning Experience

Student endpoints require a Student access token.

### Student Dashboard

```http
GET /api/student/dashboard
```

Returns:

- Enrolled diplomas.
- Latest attempts.
- Basic performance stats.

### Browse Published Diplomas

```http
GET /api/student/diplomas
```

Returns:

- Published diplomas.
- Enrollment status.
- Progress.
- Published quizzes count.
- Completed attempts count.

### Enroll In Diploma

```http
POST /api/student/diplomas/enroll
```

Body:

```json
{
  "diplomaId": "guid"
}
```

### View Diploma Quizzes

```http
GET /api/student/diplomas/{diplomaId}/quizzes
```

Rules:

- Diploma must be published.
- Student must be enrolled.

## Student Quiz Engine

Student endpoints require a Student access token.

### Start Quiz

```http
POST /api/student/quizzes/start
```

Body:

```json
{
  "quizId": "guid"
}
```

Rules:

- Quiz must be published.
- Student cannot exceed `maxAttempts`.
- Student cannot have more than one open attempt for the same quiz.
- Questions and options are returned in random order.
- Correct answers are not exposed.

### Answer Question

```http
POST /api/student/quizzes/answer
```

Body:

```json
{
  "attemptId": "guid",
  "questionId": "guid",
  "selectedOptionId": "guid"
}
```

Rules:

- Attempt must belong to the current student.
- Attempt must be open.
- Selected option must belong to the selected question.
- Answer can be changed while the attempt is open.
- If time is expired, the attempt is closed as `Expired`.

### Submit Quiz

```http
POST /api/student/quizzes/submit
```

Body:

```json
{
  "attemptId": "guid"
}
```

Rules:

- Calculates score from correct answers.
- Sets `IsPassed`.
- Closes the attempt as `Submitted` or `Expired`.

### Get Quiz Timer

```http
GET /api/student/quizzes/attempts/{attemptId}/timer
```

Returns:

- Server time.
- Start time.
- End time.
- Remaining seconds.
- Attempt status.

Rules:

- Server controls the quiz timer.
- If the time is over, the API auto-closes the attempt as `Expired`.

### Get Quiz Result

```http
GET /api/student/quizzes/attempts/{attemptId}/result
```

Returns:

- Score.
- Pass or fail.
- Student answers.
- Correct answers.
- Explanation for each question.

Rules:

- Attempt must belong to the current student.
- Attempt must be closed.

### Quiz History

```http
GET /api/student/quizzes/history?quizId={quizId}&diplomaId={diplomaId}
```

Query params are optional.

## Admin Monitoring

Admin endpoints require an Admin access token.

### Monitor Student Attempts

```http
GET /api/admin/monitoring/attempts?studentId={studentId}&quizId={quizId}&status={status}
```

Query params are optional.

Supported statuses:

```text
InProgress
Submitted
Expired
```

### Get Attempt Details

```http
GET /api/admin/monitoring/attempts/{attemptId}
```

Returns:

- Student info.
- Quiz and diploma info.
- Score and status.
- Answers, correct answers, and explanations.

## Admin Analytics

Admin endpoints require an Admin access token.

### Performance Analytics

```http
GET /api/admin/analytics/performance
```

Returns:

- Total attempts.
- Completed attempts.
- Passed attempts.
- Overall pass rate.
- Pass rate by quiz.
- Average score by diploma.
- Attempts over time.
- Most failed questions.

## Instructor Flow

Instructor endpoints require an Instructor access token.

### Instructor Dashboard

```http
GET /api/instructor/dashboard
```

### Instructor Diplomas

```http
GET /api/instructor/diplomas
```

Returns only diplomas owned by the current instructor.

### Instructor Diploma Quizzes

```http
GET /api/instructor/diplomas/{diplomaId}/quizzes
```

Rules:

- Diploma must belong to the current instructor.

### Instructor Student Attempts

```http
GET /api/instructor/attempts?diplomaId={diplomaId}&quizId={quizId}&status={status}
```

Query params are optional.

Rules:

- Instructor sees attempts only for quizzes inside their own diplomas.

## Suggested Manual Test Order

1. Register Instructor.
2. Verify Instructor OTP.
3. Register Admin.
4. Verify Admin OTP.
5. Register Student.
6. Verify Student OTP.
7. Login Admin.
8. Admin creates Diploma using Instructor Id.
9. Admin creates Quiz under Diploma.
10. Admin creates Questions with Options.
11. Admin publishes Quiz.
12. Login Student.
13. Student browses Diplomas.
14. Student enrolls in Diploma.
15. Student views Diploma Quizzes.
16. Student starts Quiz.
17. Student answers Questions.
18. Student checks Timer.
19. Student submits Quiz.
20. Student views Result.
21. Student views History.
22. Login Instructor.
23. Instructor views Dashboard, Diplomas, Quizzes, Attempts.
24. Login Admin.
25. Admin views Monitoring and Analytics.

## Architecture Notes

- API receives Requests.
- Controller creates Command or Query.
- MediatR sends Command or Query.
- Handler delegates to Orchestrator when workflow touches multiple domains.
- Orchestrator uses UnitOfWork, GenericRepository, Identity, and Events.
- Responses use ViewModels.
- No custom Services layer is used.
