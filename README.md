# Tea Round Picker 
This repository hosts the Back End and Front End code for a web application designed to fairly decide who makes the next round of tea in the office.

## Frontend [![Build & deploy Frontend](https://github.com/97saundersj/Tea-Round-Picker/actions/workflows/buildDeploy-frontend.yml/badge.svg)](https://github.com/97saundersj/Tea-Round-Picker/actions/workflows/buildDeploy-frontend.yml)

[React Web App](https://orange-dune-0b0970310.6.azurestaticapps.net/#/)

### Running the Frontend Project

**Open the Solution in VS Code:**
1. Launch VS Code.
2. Open the `Frontend` folder.

**Install Dependencies:**
   Make sure you have [Node.js](https://nodejs.org/) installed. Then run:
   ```
   npm install
   ```

**Start the Development Server:**
   ```
   npm start
   ```

### Running Tests

**Run Tests:**
   To execute tests, use:
   ```
   npm test
   ```

## Backend [![Build & deploy Backend](https://github.com/97saundersj/Tea-Round-Picker/actions/workflows/buildTestDeploy-backend.yml/badge.svg)](https://github.com/97saundersj/Tea-Round-Picker/actions/workflows/buildTestDeploy-backend.yml)

[Web API Swagger Page](https://tearoundpickerwebapi.azurewebsites.net/swagger)

### Running the Backend Project

**Open the Solution in Visual Studio:**
1. Launch Visual Studio.
2. Open the solution file located in the `BackEnd` directory.

**Set the Startup Project:**
- Right-click on the `TeaRoundPickerWebAPI` project in the Solution Explorer and select "Set as StartUp Project".

**Run the Project:**
- Press `F5` or click on the "Start" button to run the project. This will start the web API.

### Running Tests
To execute tests for the Back End:
1. Open the `TeaRoundPickerWebAPI.Tests` project in Solution Explorer.
2. Right-click on the project and select "Run Tests" or use the Test Explorer to run all tests.

### Architecture Decision Records

For detailed information about the technology stack used in this project, please refer to the [Technology Stack ADR](docs/ADRs/ADR-001-Technology-Stack.md).
