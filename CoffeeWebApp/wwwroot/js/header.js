//Lê Minh Thiên B2012144

let lastScrollTop = 0;
const header = document.getElementById('site-header');

window.addEventListener('scroll', () => {
  let currentScroll = window.scrollY || document.documentElement.scrollTop;
  if (currentScroll > lastScrollTop) {
    header.style.top = `-${header.offsetHeight}px`;
    let box = document.querySelector('.searching-result');
    box.classList.add('hide');
  
  } else {
    header.style.top = '0';
  }
  lastScrollTop = currentScroll <= 0 ? 0 : currentScroll;
});


