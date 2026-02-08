📝 To-Do List API
=================

📌 Description
--------------
A **To-Do List REST API** built using **ASP.NET Core Minimal API**.

The system allows users to manage their daily tasks in a **secure and organized** way.
Each user can create tasks, categorize them using tags, add progress notes, and control task status.
The API strictly enforces **data ownership**, ensuring that every user can only access and modify their own data 🔐.

This project focuses on backend best practices such as:
- Authentication & Authorization
- Clean business logic
- Secure data access
- Ownership-based validation

--------------------------------------------------

💡 Main Idea
------------
The core idea of this project is to build a **multi-user task management system** where:

- 👤 Each user owns their tasks, tags, and notes
- ⚙️ Business rules are enforced at the backend level
- 🚫 Unauthorized access to other users' data is completely prevented
- 🔄 Tasks follow a controlled lifecycle instead of being simple CRUD records

--------------------------------------------------

✨ Key Features
--------------
- 👤 User registration & login
- 🔐 JWT-based authentication
- 🛡️ Secure, user-specific data access
- ✅ Task management (Create, Read, Update, Delete)
- 🏷️ Task categorization using tags
- 📝 Task progress tracking using notes
- 🔄 Task status management with defined business rules
- 📘 Swagger API documentation

--------------------------------------------------

📋 Task Management
------------------
Each task contains:

- 📝 Title
- 📄 Description
- 🏷️ Tag
- 🔄 Status
- 📅 Creation date
- ⏳ Expected end date
- ✅ Completion date (if completed)

### Supported Task Statuses
- NotStarted
- InProgress
- TimeOut
- Completed

### Task Business Rules
- 🚫 A user cannot access or modify tasks owned by another user
- 🔒 Completed tasks cannot be updated or deleted
- ⛔ Tasks cannot be manually set to TimeOut
- ⏩ Task status can only move forward
- ⏱️ Task deadlines must be at least 30 minutes in the future

--------------------------------------------------

🏷️ Tags
--------
- 👤 Each user can create their own tags
- 🔐 Tags are user-specific
- 📌 Tags are used to organize tasks
- 🚫 A user cannot access or use another user's tags

--------------------------------------------------

📝 Notes
--------
- 🧩 Notes are attached to tasks
- 📈 Used to track task progress
- ➕ Each task can have multiple notes
- ✏️ Notes can be created, updated, and deleted

--------------------------------------------------

🔐 Authentication & Security
----------------------------
- 🔑 Authentication is implemented using JWT tokens
- 👥 ASP.NET Core Identity is used for user management
- 🛡️ All task, tag, and note endpoints require authentication
- 🚫 Ownership validation is applied on every operation to prevent IDOR attacks

--------------------------------------------------

🔗 MinimalApis.Extensions
-------------------------
Minimal APIs do **not** perform automatic model validation by default,
unlike MVC Controllers.

To avoid repetitive manual validation logic in every endpoint,
this project uses **MinimalApis.Extensions** to provide:

✔ Automatic DTO validation using DataAnnotations  
✔ Consistent `400 Bad Request` validation responses  
✔ Clean and thin endpoints  
✔ Centralized validation behavior  

This allows the API to stay minimal in syntax,
while still enforcing proper request validation.

--------------------------------------------------

🧱 Route Groups
---------------
Route Groups are used to:

- Group related endpoints
- Apply authorization once per group
- Improve API readability and structure
- Keep Program.cs clean and scalable

--------------------------------------------------

🌐 API Endpoints
----------------

👤 User
- POST   /api/register   → Register a new user
- POST   /api/login      → Login and receive JWT token

✅ Tasks
- GET    /api/tasks
- GET    /api/tasks/{id}
- POST   /api/tasks
- PUT    /api/tasks/{id}
- PATCH  /api/tasks/{id}
- PATCH  /api/tasks/{id}/extend-time
- DELETE /api/tasks/{id}

🏷️ Tags
- GET    /api/tags
- GET    /api/tags/{id}
- POST   /api/tags
- PUT    /api/tags/{id}
- DELETE /api/tags/{id}

📝 Notes
- GET    /api/notes/task/{taskId}
- GET    /api/notes/{id}
- POST   /api/notes/task/{taskId}
- PUT    /api/notes/{id}
- DELETE /api/notes/{id}

--------------------------------------------------

🛠️ Technologies Used
--------------------
- ASP.NET Core Minimal API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Authentication
- MinimalApis.Extensions
- Swagger (OpenAPI)

--------------------------------------------------

🎯 Project Purpose
------------------
This project was built to practice and demonstrate:

- 🧠 Backend API design
- 🔐 Secure authentication & authorization
- 🧱 Clean separation between API and business logic
- 🛡️ Ownership-based data access
- 🔄 Real-world task lifecycle handling

🚀 How to Run the Project
========================

This project has **two branches**:

🔹 `main`  
- Core To-Do functionality (Tasks, Tags, Notes)  
- No user authentication  

🔹 `auth-custom-endpoints`  
- Adds full user management & JWT authentication  
- Each user owns their data  

--------------------------------------------------

▶️ Quick Start
1️⃣ Clone the repository:
2️⃣ Switch to the branch you want:
3️⃣ Update `appsettings.json` (connection string + JWT if using `auth-custom-endpoints`)  
4️⃣ Apply migrations:
5️⃣ Run the app:

--------------------------------------------------

💡 Notes
- You can **swap branches anytime** to test the system with or without authentication  
- Both branches share the same core business logic
<img width="990" height="609" alt="image" src="https://github.com/user-attachments/assets/e6f5d567-c72b-4dd8-b1c0-93bceea1034b" />

