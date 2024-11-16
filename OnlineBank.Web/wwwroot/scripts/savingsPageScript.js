function toggleDiv(index) {
    var o = document.getElementById('panel' + index);
    var h = (o.style.height) ? parseInt(o.style.height) : 50; // Получаем текущую высоту
    o.style.height = (h === 50) ? '200px' : '50px'; // Меняем высоту
    var vp = document.getElementById('vp' + index);
    if(vp.innerText == 'v') {
        vp.innerText = '^';
    }
    else {
        vp.innerText = 'v';
    }
}