async function CheckUser(login, password) {
    const response = await fetch("/api/users", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            login: login,
            password: password
        })
    });
    console.log("запрос отправлен");
    if (response.ok == true) {
        const header = document.querySelector("h3");
        console.log("Данные успешно введены");
        header.textContent = "Данные успешно введены";
    }
    else {
        const header = document.querySelector("h3");
        console.log("Произошла ошибка");
        header.textContent = "Произошла ошибка";
    }
}
document.getElementById("formLogin").addEventListener("submit", function (event) {
    event.preventDefault();
});

document.addEventListener("DOMContentLoaded", () => {
    document.getElementById("logInBtn").addEventListener("click", async () => {
        const login = document.getElementById("first").value;
        const password = document.getElementById("password").value;
        await CheckUser(login, password);
    });
});