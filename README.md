# Frontier Sports --- Jersey Store (C#.NET Windows Forms)

Welcome to **Frontier Sports**, a fully functional jersey store built as
part of my **Event Driven Programming** course project.\
This project was a lot of fun to create --- and I hope you enjoy
exploring, modifying, and building on it as well!

## 🚀 Overview

Frontier Sports is a Windows Forms application built using the **C#.NET
Framework**. The app simulates a fully interactive online sports jersey
store with:

-   Login, account creation, and user session tracking
-   Shopping by **brand** or **sport**
-   A cart system that remembers items per user
-   Order history and payment display pages
-   A database-backed inventory and user-order system
-   Image-rich interface with attractive UI design

# ✅ Features

### 🖼️ Attractive UI & Homepage

-   Clean, modern front page
-   Login system with session handling
-   Option to create a new user account

### 🛍️ Shop by Brand

Use the menu strip to browse jerseys by: - **Nike** - **Adidas** -
**Puma** - **New Balance**

### ⚽ Shop by Sport

Explore collections from: - **Formula 1 (F1)** - **Cricket** -
**Football** - **Basketball**

### 🛒 Cart System (Session-Based & Persistent)

-   Add items to your cart
-   Add **multiple quantities** of the same jersey
-   Cart is tied to each individual user
-   Cart persists across sessions

### 💳 Payments Page (View Only)

-   Shows a mock payment summary
-   Does **NOT** process actual payments
-   Used for order review only

### 📦 Order History

-   A dedicated page showing user's previous and placed orders
-   All order history is fetched from the SQL database

### 🖼️ Full Image Support

-   Every jersey in the store is displayed with its image
-   All images included within the project assets

# 👨‍💻 For Developers --- How to Run & Modify the Project

## ✅ 1. Clone the Repository

    git clone https://github.com/Shreeyans2305/JerseyStore.git

## ✅ 2. Create Your Own Database

Create a new SQL database and add these **three tables**:

### dbo.Table3 --- Accounts

``` sql
CREATE TABLE [dbo].[Table3] (
    [username] VARCHAR (50) NOT NULL,
    [password] VARCHAR (50) NOT NULL,
    [email]    VARCHAR (50) NOT NULL
);
```

### dbo.Table4 --- Cart

``` sql
CREATE TABLE [dbo].[Table4] (
    [username] VARCHAR (50) NOT NULL,
    [product]  VARCHAR (50) NOT NULL,
    [price]    DECIMAL (18) NOT NULL,
    [qty]      INT          NOT NULL
);
```

### dbo.Table5 --- Orders

``` sql
CREATE TABLE [dbo].[Table5] (
    [username] VARCHAR (50) NOT NULL,
    [product]  VARCHAR (50) NOT NULL,
    [qty]      INT          NOT NULL,
    [price]    INT          NOT NULL,
    [status]   VARCHAR (50) NOT NULL
);
```

## ✅ 3. Update the Connection String

Replace the connection string with your own:

``` csharp
string connectionString = @"Data Source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE;Integrated Security=True;";
```

## ✅ 4. Run the Project

-   Build the application
-   Log in or create a new account
-   Explore the UI and shop around
-   Test cart, orders, and navigation

# 🙌 Final Note

This project was created for my **Event Driven Programming** course, and
I genuinely enjoyed building it.\
I hope you have just as much fun experimenting with the code, extending
it, or turning it into your own custom store.
