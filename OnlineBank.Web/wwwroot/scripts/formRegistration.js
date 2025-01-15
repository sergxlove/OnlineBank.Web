async function CreateUser(numberCard, dateEnd, cvv, login, password) {
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
    if (response.ok == true) {
        window.location.href = "/index.html";
    }
    else {
        response.text().then(text => {
            if (text != "") {
                const toastLiveExample = document.getElementById('liveToast')
                const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample);
                const textBody = document.getElementById('toastBody');
                textBody.textContent = text;
                toastBootstrap.show();
            }
        });
    }
}

document.getElementById("formReg").addEventListener("submit", function (event) {
    event.preventDefault();
});

document.addEventListener("DOMContentLoaded", () => {
    document.getElementById("regBtn").addEventListener("click", async () => {
        const numberCard = document.getElementById("numberCard").value;
        const dateEnd = document.getElementById("dateEnd").value;
        const cvv = document.getElementById("cvv").value;
        const login = document.getElementById("first").value;
        const password = document.getElementById("password").value;
        await CreateUser(numberCard, dateEnd, cvv, login, password);
    });
});