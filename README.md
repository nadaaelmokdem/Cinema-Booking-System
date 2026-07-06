# 🎬 CineBook — Cinema Booking System

An ASP.NET Core MVC (.NET 10) application for browsing movies and showtimes,
and booking cinema tickets online — with a separate, role-protected Admin
area for managing the entire catalog (movies, cinemas, halls, showtimes) and
viewing all bookings.

![.NET 10](https://img.shields.io/badge/.NET-10-512BD4)
![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET%20Core-MVC-blue)
![EF Core](https://img.shields.io/badge/EF%20Core-SQLite-informational)
![License](https://img.shields.io/badge/license-MIT-green)

## Table of contents

- [Features](#-features)
- [Tech stack](#-tech-stack)
- [Project layout](#-project-layout)
- [Domain model](#-domain-model)
- [Getting started](#-getting-started)
- [Default admin account](#-default-admin-account)
- [Where uploaded posters are stored](#-where-uploaded-posters-are-stored)
- [Roles & authorization](#-roles--authorization)
- [Validation & UX](#-validation--ux)
- [Notes before you submit / deploy](#-notes-before-you-submit--deploy)

## ✨ Features

**Everyone (no account needed)**
- Browse movies currently showing, filter by category or search by title
- See full showtimes for a movie — cinema, hall, date/time, price, seats left
- Browse the list of cinemas and drill into each one's halls & schedule

**Registered customers**
- Register / log in / log out / change password (ASP.NET Core Identity)
- Book seats on any showtime that hasn't started yet, with a live-updating
  total price as you change the seat count — no page reload
- View upcoming & past bookings, and cancel any booking before its showtime starts

**Admins**
- Separate `/Admin` area, fully role-protected
- Manage movies (create/edit/delete, upload posters), categories, cinemas,
  halls, and showtimes
- See every booking made across the whole system

**Everywhere**
- Server-side + client-side validation with errors shown next to each field
- A clear success/error banner after every create, update, delete, booking,
  or cancellation
- Custom, friendly 404 and 500 pages
- Responsive layout — usable on both desktop and mobile

## 🧱 Tech stack

- ASP.NET Core MVC, .NET 10
- ASP.NET Core Identity (cookie auth, roles: `Admin`, `Customer`)
- Entity Framework Core with SQLite (zero-config; swap to SQL Server if you like)
- Plain CSS + a small amount of vanilla JS (mobile nav toggle, live price calculation)

## 📁 Project layout

```
CinemaBookingSystem.sln
src/CinemaBookingSystem/
  Controllers/        # Public + customer controllers (Home, Movies, Cinemas, Bookings, Account)
  Areas/Admin/         # Admin-only area (Movies, Categories, Cinemas, Halls, Showtimes, Bookings)
  Models/              # Domain entities
  ViewModels/          # Form/view models (registration, login, booking, movie form, etc.)
  Data/                # ApplicationDbContext + DbInitializer (seeds roles/admin on first run)
  Views/                # Razor views for the public/customer side
  Migrations/           # EF Core migrations (generate locally - see below)
  wwwroot/
    images/posters/     # Uploaded movie posters land here
    css/site.css
    js/site.js
```

## 🗂 Domain model

- **Category** — 1‑to‑many with Movie (e.g. Action, Drama, Comedy)
- **Movie** — title, description, duration, release date, poster path, category
- **Cinema** — name, address, city
- **Hall** — belongs to a Cinema, has a seat `Capacity`
- **Showtime** — a Movie playing in a Hall at a specific `StartTime`, with a `TicketPrice`
- **Booking** — a User booking N seats on a Showtime; stores `TotalPrice`, `IsCancelled`

Business rules enforced in the controllers (not just the UI):
- A booking cannot request more seats than `Hall.Capacity - already-booked seats`.
- A booking cannot be created for a showtime whose `StartTime` has already passed.
- A booking can only be cancelled while its showtime hasn't started yet.
- Two showtimes in the same hall cannot overlap in time (checked against movie duration).
- A Category/Cinema/Hall/Movie that is still referenced (movies, halls, showtimes,
  active bookings) cannot be deleted — the admin gets a clear error message instead.

## 🚀 Getting started

> This repository was written in a sandbox without internet/NuGet access, so the
> code was authored and reviewed by hand rather than compiled here. Follow the
> steps below on a machine with the .NET 10 SDK installed.

1. **Install the .NET 10 SDK** from https://dotnet.microsoft.com/download

2. **Restore & build**
   ```bash
   cd src/CinemaBookingSystem
   dotnet restore
   dotnet build
   ```

3. **Create the database migration** (the `Migrations/` folder is included as a
   placeholder — generate the real migration once on your machine so the schema
   matches the entity model exactly):
   ```bash
   dotnet tool install --global dotnet-ef   # if you don't already have it
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
   This creates `cinema.db` (SQLite) in the project folder and applies the schema.
   Commit the generated files under `Migrations/` to your repo as required by the
   submission rules.

   If you'd rather use SQL Server, change the connection string in
   `appsettings.json` and swap `UseSqlite` for `UseSqlServer` in `Program.cs`
   (the `Microsoft.EntityFrameworkCore.SqlServer` package is already referenced).

4. **Run the app**
   ```bash
   dotnet run
   ```
   The console will print the URL (typically `https://localhost:5001` or similar).
   On first run, the app automatically:
   - applies any pending migrations,
   - creates the `Admin` and `Customer` roles,
   - creates the default admin account (see below),
   - seeds a handful of starter categories.

## 🔑 Default admin account

| Field    | Value              |
|----------|--------------------|
| Email    | `admin@cinema.local` |
| Password | `Admin@12345`      |

These come from `appsettings.json` (`DefaultAdmin:Email` / `DefaultAdmin:Password`)
so you can change them before first run, or override them via environment
variables / user secrets in production. **Change the default password after your
first login in any real deployment.**

Log in at `/Account/Login`, then visit `/Admin/Movies` (also linked from the top
nav as "Admin Panel" once you're signed in as an admin) to manage movies,
categories, cinemas, halls, and showtimes.

## 🖼 Where uploaded posters are stored

Poster images uploaded through **Admin → Movies → Create/Edit** are saved to:

```
src/CinemaBookingSystem/wwwroot/images/posters/
```

Each file is renamed to a new GUID + its original extension to avoid collisions,
and the relative path (e.g. `/images/posters/<guid>.jpg`) is stored on the
`Movie.PosterPath` column so it can be served directly as a static file. A
placeholder image is shown on movie cards/pages when no poster has been
uploaded yet. Allowed formats: `.jpg`, `.jpeg`, `.png`, `.webp`, up to 5 MB.

## 🔐 Roles & authorization

- Public (no login): Home, Movies list/details, Cinemas list/details.
- `Customer` role (any logged-in user is auto-enrolled as Customer on
  registration): booking a showtime, viewing/cancelling their own bookings,
  changing their password.
- `Admin` role: everything under `/Admin/...`, protected with
  `[Authorize(Roles = "Admin")]` on every controller in the Admin area, so
  non-admins get redirected to an Access Denied page even if they guess the URL.
  Admin-only nav links are also hidden in the layout for non-admins.

## ✅ Validation & UX

- All form models use Data Annotations (`[Required]`, `[Range]`, `[StringLength]`,
  `[Compare]`, etc.) which drive both server-side `ModelState` validation and
  client-side jQuery-unobtrusive validation (via `_ValidationScriptsPartial`),
  with errors shown next to the offending field (`asp-validation-for`).
- Every create/update/delete/cancel/booking action sets a `TempData`
  success or error message, shown at the top of the page after redirect.
- The booking page recalculates the total price live in JavaScript as the
  number of seats changes, with no page reload; the server independently
  recalculates and re-validates the total and seat availability on submit.
- Custom, friendly `404` and `500` pages (`Views/Home/NotFound.cshtml` /
  `Error.cshtml`), wired up via `UseStatusCodePagesWithReExecute` and
  `UseExceptionHandler`.
- Layout is responsive: a collapsible nav on small screens and tables that
  reflow into stacked cards on mobile (see the `@media (max-width: 768px)`
  block in `wwwroot/css/site.css`).

## 📝 Notes before you submit / deploy

- Run `dotnet ef migrations add InitialCreate` locally as described above and
  commit the generated `Migrations/*.cs` files — the repo currently has an
  empty placeholder folder since migrations must be generated against a live
  SDK + the exact EF Core package versions you restore.
- Double-check the NuGet package versions in `CinemaBookingSystem.csproj`
  resolve for your installed .NET 10 SDK/EF Core release; adjust versions if
  a newer/older stable release is available when you restore.
- Consider moving `DefaultAdmin:Password` out of `appsettings.json` and into
  user secrets or environment variables before deploying anywhere public.
