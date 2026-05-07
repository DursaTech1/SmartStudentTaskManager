# Requirements Document

## Introduction

This spec covers a comprehensive "make it perfect" polish pass on the SmartStudentTaskManager — a VB.NET WinForms desktop application (net10.0-windows) that helps students manage academic tasks with a MySQL backend. The improvements span six areas: security hardening, UX polish, missing/incomplete features, code quality, data integrity, and user-facing settings. The goal is to elevate the app from a functional prototype to a genuinely excellent, production-quality student tool.

---

## Glossary

- **Application**: The SmartStudentTaskManager WinForms desktop application.
- **Dashboard**: The `frmDashboard` main shell form containing sidebar navigation, metric cards, task grids, calendar, and all task management controls.
- **DatabaseHelper**: The static helper class that owns the MySQL connection string and all query execution methods.
- **frmAddEditTask**: The dialog form used to create or edit a single task.
- **frmLogin**: The borderless login card form.
- **frmRegister**: The borderless registration card form.
- **frmSettings**: A new settings/profile dialog form to be created as part of this spec.
- **frmTaskDetails**: The read-only task detail form with the dynamically-built subtask checklist panel.
- **SecurityHelper**: The class responsible for password hashing and verification.
- **Task**: The domain model class representing a student task record.
- **TaskReminderService**: The background service that polls for upcoming tasks and shows system-tray balloon tips.
- **ThemeManager**: The module that owns the design-system palette, fonts, and all styling helpers.
- **User**: The domain model class representing a logged-in student account.
- **Config_File**: An `appsettings.json` file stored in the application directory that holds the database connection string.
- **Empty_State_Panel**: A styled panel with an icon and message shown inside a DataGridView's parent container when the grid has zero rows.
- **Shortcut_Legend**: A modal or tooltip overlay that lists all keyboard shortcuts available in the Dashboard.
- **Snooze**: The action of deferring a task reminder by a configurable number of minutes so it fires again later.
- **Dark_Mode**: An alternative colour palette applied application-wide that uses dark backgrounds and light foreground text.
- **Progress_Bar**: The `pbCompletion` WinForms ProgressBar control on the Dashboard view.
- **Character_Counter**: A label adjacent to a text input that displays the current character count and the maximum allowed.
- **Tag**: A short free-text label (up to 30 characters) attached to a task for flexible categorisation beyond the fixed Category list.
- **Analytics_View**: A new panel within the Dashboard sidebar that displays task completion trends and priority distribution using GDI+ drawn charts.
- **Recurring_Task**: A task with `IsRecurring = TRUE` that the Application automatically re-creates after it is marked Completed.
- **Subtask**: A child checklist item belonging to a parent Task, stored in the `Subtasks` table.
- **Export**: The action of writing the current task grid contents to a file (CSV or JSON).
- **Print_Report**: The action of rendering the current task grid to a `PrintDocument` for preview and printing.

---

## Requirements

### Requirement 1: Externalise Database Connection String

**User Story:** As a student deploying the app on a new machine, I want the database connection string stored outside the compiled binary, so that I can change the server, database name, or credentials without recompiling.

#### Acceptance Criteria

1. THE Application SHALL read the MySQL connection string from a `Config_File` named `appsettings.json` located in the application's base directory at startup.
2. WHEN `appsettings.json` is absent or the `ConnectionString` key is missing, THE Application SHALL display a clear error message instructing the user to create the file, then exit gracefully.
3. THE DatabaseHelper SHALL expose a `LoadConnectionString` shared method that reads `appsettings.json` and sets the internal connection string field before any query is executed.
4. THE Application SHALL call `DatabaseHelper.LoadConnectionString` as the very first operation in `ApplicationEvents.Startup` before any form is shown.
5. IF `appsettings.json` contains a malformed JSON value for `ConnectionString`, THEN THE Application SHALL display a descriptive parse-error message and exit gracefully.

---

### Requirement 2: Upgrade Password Hashing to BCrypt

**User Story:** As a student, I want my password stored with a strong, salted hash, so that my account is protected even if the database is compromised.

#### Acceptance Criteria

1. THE SecurityHelper SHALL hash new passwords using BCrypt with a work factor of at least 12.
2. THE SecurityHelper SHALL verify passwords by comparing the input against the stored BCrypt hash using a constant-time comparison.
3. WHEN a user registers, THE frmRegister SHALL store the BCrypt hash produced by `SecurityHelper.HashPassword` in the `Users.PasswordHash` column.
4. WHEN a user logs in, THE frmLogin SHALL call `SecurityHelper.VerifyPassword` which uses BCrypt verification, not a plain SHA-256 comparison.
5. THE Application SHALL include a one-time migration path: WHEN an existing user logs in and their stored hash is detected as a legacy SHA-256 hash (64 hex characters, no `$2` prefix), THE frmLogin SHALL re-hash the supplied password with BCrypt and update the database record transparently.

---

### Requirement 3: Input Validation and Length Caps

**User Story:** As a student, I want the app to reject oversized or malformed input before it reaches the database, so that data stays clean and the UI never breaks.

#### Acceptance Criteria

1. THE frmAddEditTask SHALL enforce a maximum title length of 200 characters and display a Character_Counter label (e.g. "47/200") that updates on every keystroke.
2. THE frmAddEditTask SHALL enforce a maximum description length of 1000 characters and display a Character_Counter label that updates on every keystroke.
3. THE frmAddEditTask SHALL enforce a maximum notes length of 2000 characters and display a Character_Counter label that updates on every keystroke.
4. WHEN a user types beyond the maximum length in any field, THE frmAddEditTask SHALL prevent further input and colour the Character_Counter label red.
5. THE frmRegister SHALL enforce a maximum username length of 50 characters and a maximum email length of 254 characters.
6. THE frmRegister SHALL validate that the email field matches a basic RFC-5321 pattern (contains exactly one `@`, at least one `.` after `@`, no spaces) before allowing registration.

---

### Requirement 4: Empty-State Messaging in Grids

**User Story:** As a student, I want to see a friendly message when a task grid is empty, so that I understand there are no tasks rather than thinking the app has a bug.

#### Acceptance Criteria

1. WHEN `dgvTasks` contains zero rows after a load or filter operation, THE Dashboard SHALL display an Empty_State_Panel inside the Manage Tasks view with the text "No tasks yet — click ＋ Add Task to get started."
2. WHEN `dgvRecentTasks` contains zero rows, THE Dashboard SHALL display an Empty_State_Panel inside the Dashboard view with the text "You have no upcoming tasks."
3. WHEN `dgvCalendarTasks` contains zero rows for the selected date, THE Dashboard SHALL display an Empty_State_Panel inside the Calendar view with the text "No tasks due on this day."
4. WHEN rows are loaded into a grid, THE Dashboard SHALL hide the corresponding Empty_State_Panel.
5. THE Empty_State_Panel SHALL use `ThemeManager.MutedTextColor` for text and a centred layout within its parent container.

---

### Requirement 5: Dark Mode Toggle

**User Story:** As a student who studies at night, I want a dark mode, so that the app is comfortable to use in low-light environments.

#### Acceptance Criteria

1. THE Dashboard SHALL include a `btnDarkMode` button in the sidebar that toggles between light and dark palettes.
2. WHEN dark mode is activated, THE ThemeManager SHALL apply a dark palette (background `#0F172A`, surface `#1E293B`, text `#F1F5F9`, sidebar `#0F172A`) to all visible controls on the Dashboard, frmAddEditTask, frmTaskDetails, and frmSettings.
3. WHEN dark mode is deactivated, THE ThemeManager SHALL restore the standard light palette to all controls.
4. THE Application SHALL persist the dark-mode preference to `appsettings.json` so that the chosen mode is restored on next launch.
5. WHILE dark mode is active, THE ThemeManager SHALL ensure all DataGridView grids use dark surface and light foreground colours, including alternating row colours.
6. WHILE dark mode is active, THE Dashboard progress bar SHALL use a visible accent colour (`#6366F1`) against the dark background.

---

### Requirement 6: Keyboard Shortcut Legend

**User Story:** As a student, I want to see a list of available keyboard shortcuts, so that I can work faster without guessing what keys do what.

#### Acceptance Criteria

1. THE Dashboard SHALL include a "⌨ Shortcuts" button (or menu item) that opens a Shortcut_Legend dialog.
2. THE Shortcut_Legend SHALL list all active shortcuts in a two-column table: key combination in the left column, action description in the right column.
3. THE Shortcut_Legend SHALL include at minimum: Ctrl+N (Add Task), Ctrl+E (Edit Task), Delete (Delete selected task), Enter (View Details), F5 (Refresh), Ctrl+D (Duplicate), Ctrl+P (Print), Ctrl+S (Export CSV).
4. THE Dashboard SHALL add Ctrl+D as a keyboard shortcut for Duplicate Task and Ctrl+S for Export CSV in the `Dashboard_KeyDown` handler.
5. THE Shortcut_Legend dialog SHALL be dismissible with Escape or a Close button.

---

### Requirement 7: Reminder Snooze and Dismiss

**User Story:** As a student, I want to snooze or dismiss a task reminder from the system tray, so that I can acknowledge it without being interrupted repeatedly.

#### Acceptance Criteria

1. WHEN a balloon tip reminder is shown, THE TaskReminderService SHALL attach a `BalloonTipClicked` handler that opens the Dashboard and navigates to the relevant task's details.
2. THE TaskReminderService SHALL expose a `SnoozeTask(taskId As Integer, minutes As Integer)` method that removes the task from `_notifiedTaskIds` and schedules it to re-notify after the specified number of minutes.
3. WHEN the user right-clicks the system-tray NotifyIcon, THE TaskReminderService SHALL show a context menu with options: "Open App", "Snooze 15 min", "Snooze 1 hour", "Dismiss All".
4. WHEN "Dismiss All" is selected, THE TaskReminderService SHALL clear `_notifiedTaskIds` so no further reminders fire for the current session's already-notified tasks.
5. WHEN "Open App" is selected, THE TaskReminderService SHALL bring the Dashboard window to the foreground.

---

### Requirement 8: Recurring Task Auto-Regeneration

**User Story:** As a student with weekly recurring assignments, I want the app to automatically create the next occurrence of a recurring task when I complete it, so that I never have to manually re-add it.

#### Acceptance Criteria

1. WHEN a task with `IsRecurring = TRUE` is toggled to `Status = 'Completed'`, THE Dashboard SHALL automatically insert a new task row with the same Title, Description, Priority, Category, Notes, and `IsRecurring = TRUE`, with `DueDate` set to the original `DueDate` plus 7 days and `Status = 'Pending'`.
2. THE frmAddEditTask SHALL display a read-only label "Recurs weekly" next to the `chkIsRecurring` checkbox when the task is in edit mode and `IsRecurring` is true.
3. WHEN a recurring task is duplicated via `btnDuplicateTask`, THE Dashboard SHALL preserve `IsRecurring = TRUE` on the duplicate.
4. IF the auto-generated next occurrence insert fails, THEN THE Dashboard SHALL log the error to `System.Diagnostics.Debug` and show a non-blocking toast notification rather than a blocking MessageBox.

---

### Requirement 9: Tag Field on Tasks

**User Story:** As a student, I want to attach a short free-text tag to a task (e.g. "exam", "group project"), so that I can organise tasks beyond the fixed category list.

#### Acceptance Criteria

1. THE DatabaseHelper SHALL add a `Tag` column (`VARCHAR(30) NULL`) to the `Tasks` table via `RunMigrations` if it does not already exist.
2. THE Task class SHALL include a `Tag As String` property.
3. THE frmAddEditTask SHALL include a `txtTag` TextBox with a Character_Counter capped at 30 characters and a placeholder text of "e.g. exam, group project".
4. WHEN saving a task, THE frmAddEditTask SHALL persist the `Tag` value (or `NULL` if blank) to the database.
5. THE Dashboard task grids (`dgvTasks`, `dgvRecentTasks`) SHALL include a "Tag" column in their SELECT queries and display it in the grid.
6. THE Dashboard advanced filter panel SHALL include a `txtFilterTag` TextBox so users can filter tasks by tag using a LIKE search.

---

### Requirement 10: Analytics View

**User Story:** As a student, I want to see a visual summary of my task completion trends and priority distribution, so that I can understand my workload at a glance.

#### Acceptance Criteria

1. THE Dashboard sidebar SHALL include a "📊 Analytics" navigation button (`btnNavAnalytics`) that shows a new `pnlAnalyticsView` panel.
2. THE Analytics_View SHALL display a GDI+-drawn horizontal bar chart showing the count of tasks per Priority (High, Medium, Low) using the existing `ThemeManager` priority colours.
3. THE Analytics_View SHALL display a GDI+-drawn donut or pie chart showing the ratio of Completed to Pending to Overdue tasks using `ThemeManager.SuccessColor`, `ThemeManager.WarningColor`, and `ThemeManager.DangerColor`.
4. THE Analytics_View SHALL display a "Completion Rate This Week" percentage label calculated as tasks completed in the last 7 days divided by tasks due in the last 7 days.
5. WHEN the user navigates to the Analytics_View, THE Dashboard SHALL query the database for the latest counts and redraw all charts.
6. THE Analytics_View charts SHALL be drawn on a `Panel` using the `Paint` event and GDI+ — no third-party charting library is required.

---

### Requirement 11: Settings / Profile Page

**User Story:** As a student, I want a settings page where I can change my password and update my email, so that I can keep my account secure and up to date.

#### Acceptance Criteria

1. THE Dashboard sidebar SHALL include a "⚙ Settings" navigation button (`btnNavSettings`) that shows a new `pnlSettingsView` panel (or opens `frmSettings` as a dialog).
2. THE frmSettings SHALL allow the user to change their password by entering their current password, a new password, and a confirmation of the new password.
3. WHEN the user submits a password change, THE frmSettings SHALL verify the current password using `SecurityHelper.VerifyPassword` before updating the hash.
4. WHEN the current password is incorrect, THE frmSettings SHALL display an inline error label "Current password is incorrect" without closing the form.
5. THE frmSettings SHALL allow the user to update their email address, subject to the same RFC-5321 validation as frmRegister.
6. WHEN the email update is saved, THE frmSettings SHALL update the `Users.Email` column and refresh `GlobalVariables.CurrentUser.Email`.
7. THE frmSettings SHALL display the current username (read-only) and account creation date.

---

### Requirement 12: Complete and Robust Export (CSV and JSON)

**User Story:** As a student, I want to export my tasks to CSV or JSON reliably, so that I can use the data in spreadsheets or other tools.

#### Acceptance Criteria

1. THE ExportToCSV method SHALL include a UTF-8 BOM so that Excel opens the file with correct encoding without manual import steps.
2. THE ExportToCSV method SHALL escape field values that contain commas, double-quotes, or newlines by wrapping them in double-quotes and doubling any internal double-quotes per RFC 4180.
3. THE ExportToJSON method SHALL produce valid JSON: string values SHALL have `\n`, `\r`, and `\t` characters escaped in addition to `\` and `"`.
4. THE ExportToJSON method SHALL omit the `TaskID` and `DaysLeft` computed columns from the exported output.
5. WHEN the export file is written successfully, THE Dashboard SHALL open the containing folder in Windows Explorer so the user can immediately find the file.
6. WHEN the DataGridView has zero rows, THE Dashboard SHALL show a MessageBox with the text "There are no tasks to export." before opening the SaveFileDialog.

---

### Requirement 13: Subtask Panel Stability in frmTaskDetails

**User Story:** As a student, I want the subtask checklist in the task details window to resize correctly when I resize the window, so that controls are never clipped or overlapping.

#### Acceptance Criteria

1. THE frmTaskDetails subtask GroupBox SHALL use `Anchor = Top | Bottom | Left | Right` so it resizes with the form.
2. THE frmTaskDetails `dgvSubtasks` SHALL use `Anchor = Top | Bottom | Left | Right` within the GroupBox so it fills available space.
3. THE frmTaskDetails action buttons (`btnDeleteSubtask`, `btnMarkAllDone`) SHALL use `Anchor = Bottom | Left` so they remain at the bottom of the GroupBox when the form is resized.
4. THE frmTaskDetails `lblSubtaskProgress` SHALL use `Anchor = Bottom | Right` so it remains at the bottom-right of the GroupBox.
5. THE frmTaskDetails SHALL set a `MinimumSize` of 1020×600 to prevent controls from collapsing below usable dimensions.
6. WHEN a subtask title exceeds 255 characters, THE frmTaskDetails SHALL truncate the input to 255 characters before inserting into the database.

---

### Requirement 14: "Remember Me" on Login

**User Story:** As a student who uses the app daily, I want a "Remember Me" option on the login screen, so that I don't have to type my username every time.

#### Acceptance Criteria

1. THE frmLogin SHALL include a `chkRememberMe` CheckBox labelled "Remember me".
2. WHEN the user logs in successfully with "Remember Me" checked, THE frmLogin SHALL persist the username (not the password) to `appsettings.json` under the key `RememberedUsername`.
3. WHEN frmLogin loads and `RememberedUsername` is present in `appsettings.json`, THE frmLogin SHALL pre-populate `txtUsername` and check `chkRememberMe`.
4. WHEN the user logs in with "Remember Me" unchecked, THE frmLogin SHALL remove `RememberedUsername` from `appsettings.json` if it exists.
5. THE Application SHALL never persist the user's password or password hash to any local file.

---

### Requirement 15: Dashboard UX Polish

**User Story:** As a student, I want the dashboard to feel polished and responsive, so that using it is a pleasure rather than a chore.

#### Acceptance Criteria

1. THE Dashboard Progress_Bar SHALL display a smooth animated fill using `ProgressBarStyle.Continuous` and a custom `Paint` handler that draws the filled portion in `ThemeManager.SuccessColor` with rounded ends.
2. WHEN the Dashboard loads or refreshes, THE metric card count labels SHALL animate from 0 to their target value over 400 ms using a `System.Windows.Forms.Timer` with 20 ms ticks.
3. THE Dashboard `dgvTasks` SHALL show a "DaysLeft" column with colour-coded text: green for ≥ 3 days, amber for 1–2 days, red for 0 or overdue.
4. THE Dashboard `btnNavDashboard`, `btnNavManageTasks`, `btnNavCalendar`, `btnNavAnalytics`, and `btnNavSettings` sidebar buttons SHALL each display a Unicode emoji icon prefix (📋, ✅, 📅, 📊, ⚙) aligned left with consistent padding.
5. THE Dashboard title bar SHALL display the current user's username in `lblWelcome` using the format "Welcome, {Username}!" and update it immediately after a settings change.
6. WHEN a task grid row is right-clicked, THE Dashboard context menu SHALL be positioned at the mouse cursor location and SHALL include all seven actions: Add, Edit, Delete, View Details, Toggle Status, Duplicate, and Export Row.
7. THE Dashboard auto-refresh timer SHALL reload metrics and tasks silently (no MessageBox on error during auto-refresh); errors SHALL be written to `System.Diagnostics.Debug` only.

---

### Requirement 16: Print Report Polish

**User Story:** As a student, I want the printed task report to look professional, so that I can hand it in or pin it up as a study planner.

#### Acceptance Criteria

1. THE Print_Report SHALL include a page header with the application name, the current user's username, and the print date/time on every page.
2. THE Print_Report SHALL include a page footer with the page number (e.g. "Page 1 of N") centred at the bottom of each page.
3. THE Print_Report SHALL alternate row background colours (white and light grey `#F9FAFB`) to improve readability.
4. THE Print_Report SHALL truncate cell values that exceed the column width with an ellipsis character rather than overflowing into adjacent columns.
5. WHEN the task list is empty, THE Print_Report SHALL print a single page with the header and the message "No tasks to display."

---

### Requirement 17: Code Quality and Defensive Programming

**User Story:** As a developer maintaining this codebase, I want consistent error handling and defensive null checks throughout, so that the app never crashes silently or shows raw exception messages to students.

#### Acceptance Criteria

1. THE DatabaseHelper `ExecuteReader` method SHALL wrap the connection and command in a `Using` block and return a disconnected `DataTable` instead of a live `MySqlDataReader`, eliminating the risk of connection leaks.
2. THE frmDashboard `btnClose_Click` handler SHALL call `Application.Exit()` only after disposing `reminderService` and `refreshTimer`; the existing `OnFormClosed` override already handles disposal and SHALL be relied upon instead of duplicating disposal logic.
3. WHEN any database operation in `LoadDashboardMetrics` or `LoadTasks` throws an exception during the auto-refresh timer tick, THE Dashboard SHALL suppress the MessageBox and log to `System.Diagnostics.Debug` instead.
4. THE frmTaskDetails `EnsureSubtaskUi` method SHALL guard against being called more than once using the existing `If dgvSubtasks IsNot Nothing Then Return` check, which SHALL remain in place.
5. THE Task class SHALL include a read-only computed property `IsOverdue As Boolean` that returns `True` when `Status = "Pending"` and `DueDate < DateTime.Now`.
6. THE User class SHALL include a read-only computed property `DisplayName As String` that returns `Username` if non-empty, otherwise `Email`.

---

### Requirement 18: Dashboard Search Box

**User Story:** As a student, I want a fast, well-styled search box on the Dashboard that searches across all relevant task fields and gives me instant, readable results, so that I can find any task without scrolling through the full list.

#### Acceptance Criteria

1. WHEN the user types in `txtSearch`, THE Dashboard SHALL wait 300 ms after the last keystroke before executing the database query (debounce), so that no query fires while the user is still typing.
2. WHEN the debounce timer fires, THE Dashboard SHALL execute a single SQL query that matches tasks using `LIKE @Search` with `OR` conditions across the following columns: `Title`, `Description`, `Category`, `Tag`, and `Priority`.
3. THE Dashboard search query SHALL use MySQL's default case-insensitive `LIKE` matching so that a search for "math" matches "Math", "MATH", and "mathematics".
4. WHEN the search query returns one or more rows, THE Dashboard SHALL update `lblTaskCount` to display "Found: N tasks" where N is the row count.
5. WHEN the search query returns zero rows, THE Dashboard SHALL update `lblTaskCount` to display "No tasks found".
6. WHEN `txtSearch` is empty or contains only whitespace, THE Dashboard SHALL cancel any pending debounce timer and call `LoadTasks()` to restore the full task list.
7. THE Dashboard SHALL display a clear button (`btnSearchClear`, labelled "✕") positioned inside or immediately adjacent to `txtSearch` that is visible only when `txtSearch.Text` is non-empty.
8. WHEN `btnSearchClear` is clicked, THE Dashboard SHALL clear `txtSearch`, hide `btnSearchClear`, cancel any pending debounce timer, and call `LoadTasks()` to restore the full task list.
9. THE Dashboard search box SHALL cap input at 200 characters; characters beyond 200 SHALL be silently discarded.
10. WHEN `txtSearch` receives focus, THE Dashboard SHALL apply a focus border to the control using `ThemeManager.BorderFocusColor`; WHEN `txtSearch` loses focus, THE Dashboard SHALL restore the default border colour.
11. THE `btnSearchClear` button SHALL be styled using `ThemeManager` colours (background `ThemeManager.SurfaceColor`, foreground `ThemeManager.MutedTextColor`) so that it is visually consistent with the rest of the design system.
