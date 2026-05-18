# Smart Student Task Manager

A desktop productivity application built with **Visual Basic .NET (Windows Forms)** and **MySQL**. Designed for students to manage tasks, track deadlines, and stay productive  with an AI-style recommendation engine, Pomodoro timer, OCR support, and analytics dashboard.

---

## Features

### Task Management
- Create, edit, and delete tasks with title, description, due date, priority, category, and status
- Subtask support — break tasks into smaller steps
- Recurring task flag
- Filter and view tasks by status (Pending / Completed)

### Smart Recommendations
- AI-style engine scores pending tasks by urgency and priority
- Surfaces the top tasks you should work on right now
- Workload warnings when too many tasks are due soon
- Weekly productivity tips based on your completion rate

### Pomodoro Timer
- Built-in focus timer to help manage study sessions

### OCR (Optical Character Recognition)
- Scan and extract text from images directly into tasks

### Analytics Dashboard
- Visual breakdown of task completion, categories, and productivity trends

### User Accounts & Profiles
- Register and login with hashed passwords (PBKDF2)
- Profile management — full name, student ID, profile picture
- Role-based user model (Student / other roles)
- Remember me / remembered username support

### Keyboard Shortcuts
- Dedicated shortcuts form for quick navigation

### Theme Support
- `ThemeManager` class for UI theming

### Data & Security
- MySQL backend with automatic schema migrations on startup
- `SecurityHelper` for password hashing
- `BackupHelper` for data backup

---

## Tech Stack

| Layer       | Technology                  |
|-------------|-----------------------------|
| Language    | Visual Basic .NET           |
| UI          | Windows Forms               |
| Database    | MySQL                       |
| ORM/Data    | MySql.Data (MySqlClient)    |
| Platform    | .NET (Windows)              |

---


