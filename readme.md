Certainly! Here's a template for a README file based on the context you provided:

---

# CabTap

CabTap is a taxi reservation system built with ASP.NET Core.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Folder Structure](#folder-structure)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [License](#license)

## Overview

CabTap is a web application that provides users with a reliable and efficient taxi reservation system, designed to be user-friendly and easy to use.

## Features

- **User Roles:**
  - Guests
  - Clients
  - Admins

- **Reservation System:**
  - Clients are able to place reservations, provided all requirements are met.
  - Clients are able to make edits to certain parts of their reservation

- **Taxi Management:**
  - Admins can add, edit, and remove taxi information.
  - Passengers can view available taxis.

- **User Authentication:**
  - Secure registration and login role-based system.

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

### Installation

1. Clone the repository:

```bash
git clone https://github.com/VeselinTodorovv/DiplomenProekt.git
```

2. Navigate to the project directory:

```bash
cd CabTap
```

3. Restore dependencies and build the project:

```bash
dotnet build
```

4. Run the Api:

```bash
cd CabTap.Api
dotnet run
```

5. Run the Web App:

```bash
  cd ..
  cd CabTap.Web
  dotnet run
```

## Usage

- Visit the application in your web browser.
- Log in or register an account.
- Explore the features available for your role (client or admin).

## Folder Structure

```plaintext
CabTap/
|-- src/
|   |-- CabTap.Web/        # ASP.NET Core Web Application
|   |-- CabTap.Core/       # Core business logic and entities
|   |-- CabTap.Data/       # Data access layer
|   |-- CabTap.Services/   # Application services
|   |-- CabTap.Contracts/  # Shared interfaces and models
|   |-- CabTap.Tests/      # Unit tests
|-- .gitignore
|-- LICENSE
|-- README.md
```

## Technologies Used

#### Back end:
- C#
- ASP.NET Core MVC
- SQL Server

#### Front end:
- HTML, CSS
- Razor, Bootstrap
- JavaScript, LeafletJS

## License
This project is licensed under the [MIT License](LICENSE).