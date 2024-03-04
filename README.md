# Digital Library

### Required SDKs and Framewords

- .NET6
- Node.js v20+
- React v18+

### How to Install and Run the Project

1. Clone this repository
2. Open the solution in VS Code or Visual Studio
3. If on Visual Studio, set DigitalLibrary.WebApi as start-up project. If using VS Code, open terminal at DigitalLibrary.WebApi and run

   ```
       dotnet run
   ```

4. In DigitalLibrary.WebApi, there is clientApp folder. Open terminal at clientApp and run

   ```
       npm install
       npm audit fix
       npm start
   ```

### Architecture details

This project uses Clean Architecture. This is because right now there might not be much domain based logic but later if domain logic increases, it can easily be added. Clean architecture ensures, the domain logic can be added without affecting the application and infrastructure of the application. The project consists for Four layers:

1. Infrastructure layer: It provides functionality for accessing external systems. It includes repositories that talk to the file Manager.
2. Application layer: manages application level logic
3. Domain layer: manages domain level logic
4. Presentation Layer aka Web Api Layer: manages requests and response to server

### Issues with the project

1. Does not implement request logging
2. Does not implement RFC standards for error response status code
3. Does not implement proper error display on frontend for user to navigate their way around
4. Does not implement re-usable Modal for Add and Delete pop-up on frontend.
5. Does not use .env to store server URL. If the url changes, the user has to change the url through out the frontend
6. Uses a little Inline CSS which makes it difficult to make changes for future.

### Suggestions to overcome these issues and enhancements which would make the project better

1. Implement logging using serilog on server
2. Use ProblemDetails class to follow RFC standards for error responses
3. Use snackbars and other UI elements to prompt errors for user
4. Create a generic Modal component which would take function based on action required
5. Add a .env file and add server URL to it. Then refer to it when making calls to server
6. Create separate files for CSS
