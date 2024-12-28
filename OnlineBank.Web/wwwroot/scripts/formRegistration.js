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

}