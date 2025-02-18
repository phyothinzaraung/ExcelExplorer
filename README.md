# Excel Explorer App

## Overview
Excel Explorer App is an ASP.NET Core MVC application that allows users to upload Excel files, store data in a MySQL database, and manage items through CRUD operations.

## Features
- Upload Excel files and import data into a MySQL database
- View, edit, and delete items
- Uses Entity Framework Core with MySQL database
- Implements Repository Pattern and Dependency Injection

## Technologies Used
- ASP.NET Core MVC
- Entity Framework Core
- MySQL with Pomelo.EntityFrameworkCore.MySql
- EPPlus for Excel processing

## Prerequisites
- .NET 6.0 or later
- MySQL 8.0 or later
- Visual Studio Code or Visual Studio

## Setup Instructions

1. **Clone the Repository**
   ```sh
   git clone https://github.com/your-username/ExcelExplorerApp.git
   cd ExcelExplorerApp
   ```

2. **Update Database Connection**
   - Modify `appsettings.json` with your MySQL credentials:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=localhost;Database=ExcelDB;User=root;Password=yourpassword;"
       }
     }
     ```

3. **Run Database Migrations**
   ```sh
   dotnet ef database update
   ```

4. **Run the Application**
   ```sh
   dotnet run
   ```

5. **Open in Browser**
   - Navigate to `https://localhost:5001`

## Screenshots

### Upload Excel File
![Unknown-2](https://github.com/user-attachments/assets/c6a77b5e-67eb-4cde-a995-e19ab4666ca9)

### Item List
![Unknown](https://github.com/user-attachments/assets/165067a5-b7ed-4d76-8c37-8d26045cf09a)

### Edit Item
![Unknown-3](https://github.com/user-attachments/assets/a7def881-12d2-4e50-8e43-608bd0859094)

### Edit Item
![Unknown-4](https://github.com/user-attachments/assets/fb9c08af-3396-4e85-a887-9934db327607)

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
This project is licensed under the MIT License.

