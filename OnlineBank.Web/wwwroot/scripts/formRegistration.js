async function CreateUser(numberCard, dateEnd, cvv, login, password) {
    DisabledButton("regBtn");
    try {
        const response = await fetch("/api/createUser", {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                numberCard: numberCard,
                dateEnd: dateEnd,
                cvv: cvv,
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
        UndisabledButton("regBtn");
    }
    catch (error) {
        console.log(error);
        UndisabledButton("regBtn");
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
    button.innerHTML = "Оформить";
}

document.getElementById("regBtn").addEventListener("click", async (event) => {
    event.preventDefault();
    const numberCard = document.getElementById("numberCard").value;
    const dateEnd = document.getElementById("dateEnd").value;
    const cvv = document.getElementById("cvv").value;
    const login = document.getElementById("loginUser").value;
    const password = document.getElementById("passwordUser").value;
    await CreateUser(numberCard, dateEnd, cvv, login, password);
});


