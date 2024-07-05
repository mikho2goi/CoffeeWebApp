//Lê Minh Thiên B2012144


// sự kiện nút enter
function handleKeyPress(event) {
    if (event.key === 'Enter') {
        window.location.href = "menutong.html";
    }
}

//chức năng tìm kiếm sản phẩm
let box = document.querySelector('.searching-result');
let searchInput = document.querySelector('.searchInput input')
searchInput.addEventListener('input', function (e) {
    let textSearch = e.target.value.trim().toLowerCase();
    let listProductDOM = document.querySelectorAll('.product');

    listProductDOM.forEach(item => {

        if (item.innerText.toLowerCase().includes(textSearch)) {
            item.classList.remove('hide');
            box.classList.remove('hide');
        } else {
            item.classList.add('hide');

        }
    })
})

function formatCurrency(amount) {
    // Sử dụng Number.toLocaleString() để định dạng tiền tệ
    return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(amount);
}
searchInput.addEventListener('click', function (e) {
    box.classList.remove('hide');
    e.preventDefault();
    $.ajax({
        url: '/Product/GetProducts',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            let html = '';
            console.log(data);
            data.forEach(product => {
                let formattedPrice = formatCurrency(product.price)
                html += `
                <a href"#" class="">
                    <div class="product d-flex ">
                        <img src="${product.urlImage}" alt="" class=" ">
                        <div class="d-flex flex-column justify-content-center align-items-center text-center w-100 text-wrap" >
                            <h5>${product.nameProduct }</h5>
                            <p>${formattedPrice}</p>
                        </div>
                    </div>
                </a>
                `;
            });
            $('#productResults').html(html);
        },
    });
});


// Bắt sự kiện click trên document để ẩn kết quả tìm kiếm khi click ra ngoài
document.addEventListener('click', function (event) {
    if (!event.target.closest('.searching-result') && !event.target.closest('.searchInput')) {
        box.classList.add('hide');
    }
});

