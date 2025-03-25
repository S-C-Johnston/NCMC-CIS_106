// See https://aka.ms/new-console-template for more information

using BookManagement.Services;

BookManagerService bms = new();

bms.Dispatch();
// User options:
// Add new book to inventory with validation of:
// - title
// - author
// - genre
// - unique ID
// List all books
// Display book by ID
// Remove book by ID
// Exit

// The majority of this program should take place within a BookManagerService
// class. Program.cs should simply instantiate the BookManagerService and start
// the process.
