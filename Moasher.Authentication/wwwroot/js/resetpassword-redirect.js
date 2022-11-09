window.addEventListener("load", function () {
    let a = document.querySelector("a.PostResetPasswordRedirectUri");
    if (a) {
        window.location = a.href;
    }
});