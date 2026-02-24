// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Function to redirect to Index page
function redirectToIndex() {
    window.location.href = "/Students/Index";
}

//function to get ID input from user
function promptForId(action) {
    // User se ID mango
    var id = prompt("Please enter the Student ID to " + action + ":");
    // Agar user ne ID likhi hai aur Cancel nahi dabaya ye b make sure kro k cancel dabaya to form submit nai krna
    if (id !== null && id !== "") {
        // Redirect to the appropriate action with the ID
        window.location.href = "/Students/" + action + "/" + id;
    } else {
        alert("Please enter a valid Student ID.");
        redirectToIndex(); // Redirect to Index page if no valid ID is entered
    }
}
// Function to show alert if student is not found
function studentnotfound() {
    alert("Student not found.");
    redirectToIndex(); // Redirect to Index page if student is not found
}

function confirmDelete() {
    alert("Student Deleted Successfully.");
    redirectToIndex(); // Redirect to Index page after deletion
}

// <input type="checkbox" id="showPassword" onclick="togglePassword()"> Show Password
function togglePassword() {
    var passwordField = document.getElementById("Password");
    var confirmPasswordField = document.getElementById("ConfirmPassword");
    if (passwordField.type === "password") {
        passwordField.type = "text";
        confirmPasswordField.type = "text";
    } else {
        passwordField.type = "password";
        confirmPasswordField.type = "password";
    }
}
// Add a java function showing alert if simple user try to access delete,edit
function accessDenied() {
    alert("Access Denied. You do not have permission to perform this action.");
 }