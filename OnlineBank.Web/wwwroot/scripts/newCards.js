async function CreateCard(firstName, secondName, lastName, dateBirth,
    passportData, numberPhone, email, loginUser, passwordUser) {
    DisabledButton("cardBtn");
    try {
        const response = await fetch("/api/createCard", {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                firstName: firstName,
                secondName: secondName, 
                lastName: lastName,
                dateBirth: dateBirth,
                passportData: passportData,
                numberPhone: numberPhone, 
                email: email,
                loginUser: loginUser,
                passwordUser: passwordUser
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
            default:
                break;
        }
        UndisabledButton("cardBtn");
    }
    catch (error) {
        console.log(error);
        UndisabledButton("cardBtn");
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
    button.innerHTML = "Отправить данные";
}

document.getElementById("cardBtn").addEventListener("click", async (event) => {
    event.preventDefault();
    const firstName = document.getElementById("firstName").value;
    const secondName = document.getElementById("secondName").value;
    const lastName = document.getElementById("lastName").value;
    const passportData = document.getElementById("passportData").value;
    const numberPhone = document.getElementById("numberPhone").value;
    const login = document.getElementById("loginUser").value;
    const password = document.getElementById("passwordUser").value;
    await CreateCard(firstName, secondName, lastName, dateBirth, passportData, numberPhone, email,
        login, password);
})