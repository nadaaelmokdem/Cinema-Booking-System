<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - CineBook</title>
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
</head>
<body>
    <header class="site-header">
        <nav class="navbar">
            <a class="brand" asp-controller="Home" asp-action="Index">🎬 CineBook</a>
            <button class="nav-toggle" id="navToggle" aria-label="Toggle navigation">☰</button>
            <div class="nav-links" id="navLinks">
                <a asp-controller="Movies" asp-action="Index">Movies</a>
                <a asp-controller="Cinemas" asp-action="Index">Cinemas</a>
                @if (User.Identity?.IsAuthenticated == true)
                {
                    <a asp-controller="Bookings" asp-action="MyBookings">My Bookings</a>
                }
                @if (User.IsInRole("Admin"))
                {
                    <a asp-area="Admin" asp-controller="Movies" asp-action="Index">Admin Panel</a>
                }
                <span class="nav-spacer"></span>
                @if (User.Identity?.IsAuthenticated == true)
                {
                    <span class="nav-user">Hi, @User.Identity!.Name</span>
                    <a asp-controller="Account" asp-action="ChangePassword">Change Password</a>
                    <form asp-controller="Account" asp-action="Logout" method="post" class="inline-form">
                        <button type="submit" class="link-button">Logout</button>
                    </form>
                }
                else
                {
                    <a asp-controller="Account" asp-action="Login">Login</a>
                    <a asp-controller="Account" asp-action="Register">Register</a>
                }
            </div>
        </nav>
    </header>

    <main class="container">
        @if (TempData["SuccessMessage"] is not null)
        {
            <div class="alert alert-success">@TempData["SuccessMessage"]</div>
        }
        @if (TempData["ErrorMessage"] is not null)
        {
            <div class="alert alert-error">@TempData["ErrorMessage"]</div>
        }

        @RenderBody()
    </main>

    <footer class="site-footer">
        <p>&copy; @DateTime.Now.Year CineBook - Cinema Booking System</p>
    </footer>

    <script src="~/js/site.js" asp-append-version="true"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
