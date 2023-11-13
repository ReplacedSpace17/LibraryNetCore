document.addEventListener("DOMContentLoaded", function () {
    fetchBooks();
    const searchInput = document.getElementById("searchInput");
    searchInput.addEventListener("input", searchBooks);
});

function fetchBooks() {
    fetch("https://localhost:7185/api/books")
        .then(response => response.json())
        .then(data => displayBooks(data.$values))
        .catch(error => console.error("Error al obtener libros:", error));
}

function displayBooks(books) {
    const tableBody = document.getElementById("booksTableBody");
    tableBody.innerHTML = "";

    books.forEach(book => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${book.id}</td>
            <td>${book.title}</td>
            <td>${book.chapters}</td>
            <td>${book.pages}</td>
            <td>${book.price}</td>
            <td>${book.authorId}</td>
        `;
        tableBody.appendChild(row);
    });
}

function searchBooks() {
    const searchInput = document.getElementById("searchInput");
    const searchTerm = searchInput.value.toLowerCase();

    fetch("https://localhost:7185/api/books")
        .then(response => response.json())
        .then(data => {
            const filteredBooks = data.$values.filter(book => book.title.toLowerCase().includes(searchTerm));
            displayBooks(filteredBooks);
        })
        .catch(error => console.error("Error al obtener libros:", error));
}
