function updateDateTime() {
    const now = new Date();
    const options = { year: 'numeric', month: 'long', day: 'numeric', hour12: false };
    const dateTimeString = now.toLocaleString('ru-RU', options);
    
    document.getElementById('datetime').innerText = dateTimeString;
}

updateDateTime();