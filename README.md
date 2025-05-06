# Library Management System

## Setup Guide

### Prerequisites

Ensure the following tools are installed:

- **.NET 8 SDK** – [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
- **SQL Server Developer Edition** – [microsoft.com/sql-server](https://www.microsoft.com/en-us/sql-server)
- **SQL Server Management Studio (SSMS)** – [aka.ms/ssmsfullsetup](https://aka.ms/ssmsfullsetup)

### Required NuGet Packages

Before running or building the project, ensure the following packages are installed:

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `Swashbuckle.AspNetCore`

Install them using the .NET CLI:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Swashbuckle.AspNetCore
```
## Step-by-Step Setup

### 1. Clone the Project

First, clone the repository to your local machine:

```bash
git clone https://github.com/XApple15/LibraryManagementSystem.git
cd LibraryManagementSystem
```

### 2. Configure SQL Server Connection

To configure the SQL Server connection, open the `appsettings.json` file in the project and update the connection string:

```json
"ConnectionStrings": {
  "LibraryDb": "Server=localhost;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```
Adjust server name and database as needed. No need to create the Database. It is created automatically if it doesn`t exist.

### 3. Run the Application

Once you have cloned the project and configured the SQL Server connection, you can run the application.

To start the application, use the following command:

```bash
dotnet run
```
Visit the API Swagger UI at: https://localhost:7232/swagger


## Sample API Endpoints

Below is a list of available API endpoints that you can interact with:

| Method | Endpoint                                         | Description                          |
|--------|--------------------------------------------------|--------------------------------------|
| GET    | `/api/book`                                      | List all books                       |
| POST   | `/api/book`                                      | Add a new book                       |
| PUT    | `/api/book/{id}`                                 | Update an existing book              |
| GET    | `/api/book/search?title=...&author=...&inStock=..` | Search books by title, author, or stock |
| POST   | `/api/book/lend/{id}`                            | Lend a book                          |
| POST   | `/api/book/return/{id}`                          | Return a book                        |
| GET    | `/api/book/{id}/recommendations`                 | View similar book recommendations    |



# Innovative Functionality: AI Book Similarity Recommendation

This system includes a lightweight recommendation engine that provides a list of similar books based on basic string similarity logic applied to titles and authors.

## Algorithm Description

Given a selected book identified by its `bookId`, the system performs the following steps:

1. Retrieve the target book from the database.
2. Fetch all books and exclude the target from the comparison set.
3. For each candidate book:
   - Normalize and tokenize the title and author fields.
   - Compare words in the title and author.
   - Compute a score based on the number of intersecting words and an exact author match.
4. Filter out books with a score of 0.
5. Sort the results in descending order of similarity.
6. Return the top 5 most similar books.

## API Endpoint Example

```http
GET /api/books/{bookId}/recommendations
```


## Normalization and Tokenization

For both the title and author fields of the target and candidate books, the following steps are taken:

- Convert the text to lowercase.
- Split the text into words using whitespace.

This process ensures that both the title and author are compared in a consistent and standardized way, eliminating any case or formatting discrepancies.

## Similarity Scoring Function

Each pair of books receives a similarity score computed based on the following rules:

- **+5 points** if the author names are an exact match.
- **+1 point per shared word** in the book titles.
- **+1 point per shared word** in the author names.

### C# Equivalent:

```csharp
if (a.Author == b.Author) score += 5;
score += titleWordsA.Intersect(titleWordsB).Count();
score += authorWordsA.Intersect(authorWordsB).Count();
```

## Output

The final output of the book similarity recommendation engine is a ranked list of up to 5 books. The books are sorted by similarity score in descending order, with the most similar books appearing first.

Only books with a score greater than 0 are included in the output. Books with a similarity score of 0 are filtered out.

This ensures that the returned list consists of only the most relevant and similar books based on the selected book's title and author.
