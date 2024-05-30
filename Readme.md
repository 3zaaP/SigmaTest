# Job Candidate Hub API

This project is a web application with an API for storing and managing job candidate information. The application is built using .NET 6 and uses SQLite as the database.

## Features

- REST API for adding or updating candidate information.
- Retrieve a candidate by email.
- Retrieve all candidates.
- Delete a candidate by email.
- Unit tests for the repository and controller.

## Requirements

- .NET 6 SDK
- Visual Studio 2022

## Getting Started

### Clone the Repository

1. **Clone the Repository:**
   ```bash
   git clone <repository-url>
   cd JobCandidateHub
   ```

### Running the Application

1. **Open the Solution:**
   - Open Visual Studio 2022.
   - Open the solution file `JobCandidateHub.sln`.

2. **Restore Packages:**
   - Visual Studio will automatically restore the necessary packages.

3. **Apply Migrations and Update the Database:**
   - Open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console).
   - Run the following commands:
     ```powershell
     Add-Migration InitialCreate
     Update-Database
     ```

4. **Run the Application:**
   - Press `F5` or click the "Start" button in Visual Studio.

   The application will start and listen on `http://localhost:5000` and `https://localhost:5001`.

### API Endpoints

- **Add or Update Candidate:**
  ```http
  POST http://localhost:5000/api/candidates
  Content-Type: application/json

  {
    "firstName": "John",
    "lastName": "Doe",
    "phoneNumber": "1234567890",
    "email": "john.doe@example.com",
    "callTimeInterval": "9 AM - 5 PM",
    "linkedInProfileUrl": "https://linkedin.com/in/johndoe",
    "gitHubProfileUrl": "https://github.com/johndoe",
    "comment": "This is a comment"
  }
  ```

- **Get Candidate by Email:**
  ```http
  GET http://localhost:5000/api/candidates/john.doe@example.com
  ```

- **List All Candidates:**
  ```http
  GET http://localhost:5000/api/candidates
  ```

- **Delete Candidate by Email:**
  ```http
  DELETE http://localhost:5000/api/candidates/john.doe@example.com
  ```

### Running the Tests

1. **Open the Solution:**
   - Open Visual Studio 2022.
   - Open the solution file `JobCandidateHub.sln`.

2. **Run the Tests:**
   - Open the Test Explorer (Test > Test Explorer).
   - Click "Run All" to execute all tests.

## Notes

- Ensure you have trusted the development HTTPS certificate for .NET Core:
  ```bash
  dotnet dev-certs https --trust
  ```

- If you encounter any issues with HTTPS, you can disable HTTPS redirection by commenting out `app.UseHttpsRedirection();` in `Program.cs`.

## Project Structure

- **JobCandidateHub**: The main project containing the API.
- **JobCandidateHub.Tests**: The test project containing unit tests for the repository and controller.

## Contact

For any questions or issues, please contact [max3zaap@gmail.com].