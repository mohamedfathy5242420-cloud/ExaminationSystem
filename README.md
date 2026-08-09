# Examination System

A backend-focused online examination platform built with **ASP.NET Core**, designed to manage diplomas, quizzes, questions, student attempts, authentication, and performance analytics.

## Features

### Student

* Register and verify account using OTP
* Login with JWT & Refresh Tokens
* Browse published diplomas and quizzes
* Start and resume quiz attempts
* Server-side timer enforcement
* Submit quizzes and auto-submit on timeout
* View detailed results and attempt history
* Track learning progress

### Admin

* Manage Diplomas, Quizzes, Questions & Options
* Publish / Unpublish quizzes
* Validate quiz readiness before publishing
* Monitor student attempts
* View dashboard statistics
* Access performance analytics

## Architecture

The project follows **Clean Architecture** and **CQRS** principles:

```text
API
 │
Application
 │
Domain
 │
Infrastructure
```

* **API** — Controllers, Middleware & Authentication
* **Application** — CQRS, MediatR, DTOs, Validation & Business Use Cases
* **Domain** — Entities & Core Business Rules
* **Infrastructure** — EF Core, SQL Server, Identity, Repositories & External Services

## Technology Stack

* C#
* ASP.NET Core
* Entity Framework Core
* SQL Server
* ASP.NET Core Identity
* JWT Authentication
* MediatR
* CQRS
* Clean Architecture
* FluentValidation
* Repository & Unit of Work
* Caching

## Core Entities

```text
User
 ├── Student
 └── Admin

Diploma
 └── Quiz
      └── Question
           └── Option

Student
 └── QuizAttempt
      └── AttemptAnswer
```

## Key Business Rules

* Students can access **published content only**.
* Students can access **only their own attempts and results**.
* One active attempt is allowed per quiz.
* Quiz attempts can have configurable maximum attempts.
* Questions and options are shuffled per attempt.
* Correct answers are hidden until submission.
* Quiz time is enforced **server-side**.
* Expired attempts are automatically submitted.
* Quiz score is calculated from correct answers.
* Content uses **soft deletion**.
* JWT-based role authorization separates Students and Admins.
* Authentication endpoints are rate-limited.

## Quiz Flow

```text
Start Quiz
    ↓
Create Attempt
    ↓
Answer Questions
    ↓
Server-side Timer
    ↓
Submit / Auto Submit
    ↓
Calculate Score
    ↓
View Results
```

## Project Goal

Build a secure, scalable, and maintainable examination platform while applying real-world backend engineering practices such as **Clean Architecture, CQRS, authentication, authorization, validation, caching, database optimization, and server-side business rules**.
