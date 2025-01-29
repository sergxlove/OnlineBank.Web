async function CheckUser(login, password) {
    DisabledButton("logInBtn");
    try {
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
            case 500:
                response.text().then(text => {
                    if (text != "") {
                        const toastLiveExample = document.getElementById('liveToast')
                        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample);
                        const textBody = document.getElementById('toastBody');
                        textBody.textContent = text;
                        toastBootstrap.show();
                    }
                })
                break;
            default:
                break;
        }
        UndisabledButton("logInBtn");
    }
    catch (error) {
        console.log(error);
        UndisabledButton("logInBtn");
    }
}

async function DisabledButton(nameBtn) {
    var button = document.getElementById(nameBtn);
    button.innerHTML = '<div class="spinner-border" role="status"></div>';
    button.disabled = true;
}

async function UndisabledButton(nameBtn) {
    var button = document.getElementById(nameBtn);
    button.disabled = false;
    button.innerHTML = "Войти";
}

document.getElementById("logInBtn").addEventListener("click", async (event) => {
    event.preventDefault();
    const login = document.getElementById("loginUser").value;
    const password = document.getElementById("passwordUser").value;
    await CheckUser(login, password);
});


