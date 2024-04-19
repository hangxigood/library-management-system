# Library Management System

## Overview

The Library Management System (LMS) is designed to streamline the operations of managing a library's catalog and its members. It provides an intuitive web-based interface for library administrators, allowing for efficient management of books and member activities.

## Features

- **Manage Books**: Add, update, delete, and search for books in the library's collection.
- **Manage Members**: Add, update, and delete library members.
- **View Loans**: Oversee current loans, process returns.
- **Borrow and Return Books**: Check out and return books using a simple interface.
- **View Personal Loan History**: Administrators can view members' current and past loans.

## Problem Solved

This system addresses the need for a streamlined, accessible, and efficient way to manage library resources and member interactions. By digitizing the library's operations, LMS reduces manual work, minimizes errors, and improves the overall user experience. It facilitates better inventory management, easier access to books, and more effective communication of book availability and loan status.

## Technologies Used

- **.NET 8**: For backend services.
- **Blazor**: For creating interactive UI components on the web.
- **SQLite**: For database management, providing a lightweight and efficient database solution.
- **SQLite-net**: For object-relational mapping, simplifying data manipulation and database management.

## Getting Started

To get a local copy up and running follow these simple steps:

### Prerequisites
#### Development
- .NET 8.
- Visual Studio 2022 or later:
  - Windows: with Mobile development with .NET workload.
  - Mac: Visual Studio 2022 for Mac.
- Mobile SDKs:
  - Android: Android SDK via Android SDK Manager in Visual Studio.
  - iOS and macOS: Xcode on a Mac.

#### Running the App
- Android Emulator or Physical Android Device.
- iOS Simulator or Physical iOS Device (requires macOS).
- Windows or macOS machines, depending on target platforms.
- Modern web browser (for Blazor hybrid scenarios).


### Installation

1. Clone the repo
   ```git clone https://github.com/hangxigood/library-management-system.git```
   
2. Navigate to the project directory and restore the .NET packages
	```dotnet restore```
	
3. Start the application
   ```dotnet run```


   
