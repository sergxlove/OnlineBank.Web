﻿async function CheckUser(login, password) {
    DisabledButton("logInBtn");
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
    switch (response.status) {
        case 200:
            window.location.href = "/index.html";
            break;
        case 400:
            response.text().then(text => {
                if (text != "") {
                    const toastLiveExample = document.getElementById('liveToast')
                    const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample);
                    const textBody = document.getElementById('toastBody');
                    textBody.textContent = text;
                    toastBootstrap.show();
                }
            });
            break;
        case 401:
            break;
    }
    UndisabledButton("logInBtn")
}

async function DisabledButton(nameBtn) {
    var button = document.getElementById(nameBtn);
    button.innerHTML = "Загрузка";
    button.style.backgroundColor = "black";
    button.disabled = true;
}

async function UndisabledButton(nameBtn) {
    var button = document.getElementById(nameBtn);
    button.disabled = false;
    button.innerHTML = "Загрузка";
}

document.getElementById("formLogin").addEventListener("submit", function (event) {
    event.preventDefault();
});

document.addEventListener("DOMContentLoaded", () => {
    document.addEventListener("click", async () => {
        const login = document.getElementById("first").value;
        const password = document.getElementById("password").value;
        await CheckUser(login, password);
    });
});