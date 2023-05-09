// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const navAnim = () => {
    const burger = document.querySelector(".burger");
    const navJs = document.querySelector(".nav-js");
    const nav = document.querySelector(".nav-links");
    const navLinks = document.querySelectorAll(".nav-links li");
    const btnSection = document.querySelector(".btn-section");
    const closeX = document.querySelector(".close-X");

    burger.addEventListener("click", () => {
        navJs.classList.add("nav-active");
        nav.classList.add("nav-links-active");
        burger.style.display = "none";
        closeX.style.display = "flex";
    });
    closeX.addEventListener("click", () => {
        burger.style.display = "block";
        closeX.style.display = "none";
        navJs.classList.remove("nav-active");
        nav.classList.remove("nav-links-active");
    });
   
};
