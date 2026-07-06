@model List<Movie>
@{
    ViewData["Title"] = "Home";
}

<section class="hero">
    <h1>Now Showing</h1>
    <p>Browse movies, check showtimes, and book your seats in a couple of clicks.</p>
    <a class="btn btn-primary" asp-controller="Movies" asp-action="Index">See All Movies</a>
</section>

<div class="movie-grid">
    @foreach (var movie in Model)
    {
        <div class="movie-card">
            <a asp-controller="Movies" asp-action="Details" asp-route-id="@movie.Id">
                <img src="@(string.IsNullOrEmpty(movie.PosterPath) ? "/images/posters/placeholder.png" : movie.PosterPath)" alt="@movie.Title poster" class="movie-poster" />
            </a>
            <div class="movie-card-body">
                <h3>@movie.Title</h3>
                <p class="text-muted">@movie.Category?.Name &middot; @movie.DurationMinutes min</p>
                <a class="btn btn-secondary" asp-controller="Movies" asp-action="Details" asp-route-id="@movie.Id">View Showtimes</a>
            </div>
        </div>
    }
</div>

@if (!Model.Any())
{
    <p class="text-muted">No movies are currently showing. Please check back soon.</p>
}
