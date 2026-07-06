:root {
    --primary: #e63946;
    --primary-dark: #c1121f;
    --dark: #1d1d29;
    --light-bg: #f7f7fb;
    --border: #e2e2ea;
    --success: #2a9d5c;
    --danger: #e63946;
    --muted: #6c6c7c;
}

* { box-sizing: border-box; }

body {
    font-family: 'Segoe UI', system-ui, -apple-system, sans-serif;
    margin: 0;
    background: var(--light-bg);
    color: #222;
}

a { color: var(--primary-dark); text-decoration: none; }
a:hover { text-decoration: underline; }

.container {
    max-width: 1100px;
    margin: 0 auto;
    padding: 1.5rem 1rem 3rem;
}

/* ---- Header / nav ---- */
.site-header {
    background: var(--dark);
    color: white;
}

.navbar {
    display: flex;
    align-items: center;
    max-width: 1100px;
    margin: 0 auto;
    padding: 0.75rem 1rem;
    flex-wrap: wrap;
}

.brand {
    color: white;
    font-size: 1.3rem;
    font-weight: bold;
    margin-right: auto;
}

.nav-toggle {
    display: none;
    background: none;
    border: none;
    color: white;
    font-size: 1.5rem;
    cursor: pointer;
}

.nav-links {
    display: flex;
    align-items: center;
    gap: 1rem;
    flex-wrap: wrap;
}

.nav-links a, .nav-links .link-button {
    color: #e6e6f0;
}

.nav-spacer { flex: 1 1 auto; }

.nav-user { color: #b7b7c9; }

.link-button {
    background: none;
    border: none;
    padding: 0;
    font: inherit;
    cursor: pointer;
    text-decoration: underline;
}

.inline-form { display: inline; }

/* ---- Buttons ---- */
.btn {
    display: inline-block;
    padding: 0.5rem 1rem;
    border-radius: 6px;
    border: none;
    cursor: pointer;
    font-size: 0.95rem;
    text-decoration: none;
}
.btn:hover { text-decoration: none; opacity: 0.9; }
.btn-primary { background: var(--primary); color: white; }
.btn-secondary { background: #e2e2ea; color: #222; }
.btn-danger { background: var(--danger); color: white; }
.btn-sm { padding: 0.3rem 0.7rem; font-size: 0.85rem; }

/* ---- Alerts ---- */
.alert {
    padding: 0.9rem 1.1rem;
    border-radius: 6px;
    margin-bottom: 1rem;
}
.alert-success { background: #d9f2e3; color: #1a6b3c; border: 1px solid #a8ddbc; }
.alert-error { background: #fbdada; color: #8f1f2b; border: 1px solid #f2a5ac; }

/* ---- Forms ---- */
.form { max-width: 700px; }
.form-narrow { max-width: 420px; }
.form-group { margin-bottom: 1rem; }
.form-group label { display: block; font-weight: 600; margin-bottom: 0.3rem; }
.form-control {
    width: 100%;
    padding: 0.55rem 0.7rem;
    border: 1px solid var(--border);
    border-radius: 6px;
    font-size: 1rem;
}
.form-check label { font-weight: normal; display: flex; align-items: center; gap: 0.5rem; }
.field-error { color: var(--danger); font-size: 0.85rem; }
.validation-summary { color: var(--danger); margin-bottom: 1rem; }
.validation-summary ul { margin: 0; padding-left: 1.2rem; }

/* ---- Tables ---- */
.table { width: 100%; border-collapse: collapse; margin-top: 1rem; }
.table th, .table td {
    padding: 0.7rem 0.6rem;
    border-bottom: 1px solid var(--border);
    text-align: left;
}
.table th { background: #efeff5; }

/* ---- Movie cards ---- */
.hero {
    text-align: center;
    padding: 2.5rem 1rem;
    background: linear-gradient(135deg, var(--dark), #33334a);
    color: white;
    border-radius: 10px;
    margin-bottom: 2rem;
}
.hero h1 { margin-top: 0; }

.movie-grid, .cinema-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
    gap: 1.2rem;
    margin-top: 1.2rem;
}

.movie-card, .cinema-card {
    background: white;
    border-radius: 8px;
    overflow: hidden;
    box-shadow: 0 1px 4px rgba(0,0,0,0.08);
}
.movie-card-body, .cinema-card { padding: 0.9rem; }
.movie-poster { width: 100%; height: 260px; object-fit: cover; display: block; }
.movie-poster-large { width: 220px; height: 320px; object-fit: cover; border-radius: 8px; }
.thumb { width: 50px; height: 70px; object-fit: cover; border-radius: 4px; }
.thumb-large { width: 140px; height: 200px; object-fit: cover; border-radius: 6px; margin-bottom: 1rem; }

.movie-details { display: flex; gap: 1.5rem; flex-wrap: wrap; margin-bottom: 2rem; }
.movie-details-body { flex: 1 1 300px; }

.filter-bar { display: flex; gap: 0.6rem; margin: 1rem 0; flex-wrap: wrap; }
.filter-bar input, .filter-bar select { padding: 0.5rem; border-radius: 6px; border: 1px solid var(--border); }

.booking-summary {
    background: white;
    padding: 1rem 1.2rem;
    border-radius: 8px;
    margin-bottom: 1.2rem;
    box-shadow: 0 1px 4px rgba(0,0,0,0.08);
}

.total-price-box {
    font-size: 1.3rem;
    font-weight: bold;
    margin: 1rem 0;
    padding: 0.8rem;
    background: #fff3ee;
    border-radius: 6px;
    color: var(--primary-dark);
}

.badge {
    display: inline-block;
    padding: 0.2rem 0.6rem;
    border-radius: 12px;
    font-size: 0.8rem;
}
.badge-success { background: #d9f2e3; color: #1a6b3c; }
.badge-muted { background: #e2e2ea; color: #555; }
.badge-info { background: #dbe8fb; color: #1c4e8f; }

.text-muted { color: var(--muted); }

.error-page { text-align: center; padding: 3rem 1rem; }

.admin-title { color: var(--muted); margin-bottom: 0.2rem; }

/* ---- Responsive ---- */
@media (max-width: 768px) {
    .nav-toggle { display: block; }
    .nav-links {
        display: none;
        flex-direction: column;
        align-items: flex-start;
        width: 100%;
        padding-top: 0.5rem;
    }
    .nav-links.open { display: flex; }
    .nav-spacer { display: none; }

    .table thead { display: none; }
    .table, .table tbody, .table tr, .table td { display: block; width: 100%; }
    .table tr {
        margin-bottom: 0.8rem;
        border: 1px solid var(--border);
        border-radius: 6px;
        padding: 0.4rem 0.6rem;
        background: white;
    }
    .table td {
        border: none;
        padding: 0.35rem 0;
        display: flex;
        justify-content: space-between;
        gap: 0.5rem;
    }
    .table td[data-label]:not([data-label=""])::before {
        content: attr(data-label);
        font-weight: 600;
        color: var(--muted);
    }

    .movie-details { flex-direction: column; }
    .movie-poster-large { width: 100%; max-width: 280px; height: auto; }
}
