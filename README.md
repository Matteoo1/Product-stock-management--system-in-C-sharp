#  Product Stock Management System in C Sharp

## Image of the System
<img width="670" alt="image" src="https://github.com/mohamadd10/Product-stock-system-in-C-sharp/assets/119814738/5fbae3c9-d5b2-4702-91de-77c490b89a4f">

## Overview
I developed this C# project in my free time as a hobby project, it implements a stock management system using Windows Forms and a MySQL database. It features a form for adding, updating, searching, and deleting product entries in the stock database, including handling images for each product.

## Features
- **Product Management:** Add, update, delete, and search for products in the stock database.
- **Image Handling:** Support for adding and displaying product images.
- **Dynamic Data Grid:** Products are displayed in a DataGridView, where rows can be selected to view or modify product details.
- **Validation and Error Handling:** Includes basic validation and error handling for database operations.

## How to Run
1. Ensure that you have the .NET SDK and MySQL Server installed on your machine.
2. Open the project in Visual Studio or another compatible .NET development environment.
3. Update the database connection string in `Data_base_stock.cs` to match your MySQL server settings.
4. Build and run the project from the IDE or use the following commands in the command prompt:
   ```bash
   dotnet build
   dotnet run
   
## Requirements
.NET SDK
MySQL Server (any recent version should work)
Visual Studio 2019 or later (for development and debugging)

## Future Enhancements
- Adding a new features for addung/updating how many pieces left in the stock
