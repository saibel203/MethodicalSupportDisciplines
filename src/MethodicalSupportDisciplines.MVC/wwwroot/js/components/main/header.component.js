const header = document.querySelector('.header-main');
// During scrolling, the header decreases
window.addEventListener('scroll', () => {
    const scroll = window.scrollY || document.documentElement.scrollTop;
    if (scroll >= 10) {
        header.classList.remove('header-main');
        header.classList.add('scroll-on');
    }
    else {
        header.classList.remove('scroll-on');
        header.classList.add('header-main');
    }
});
const navItemsElements = document.getElementsByClassName('nav-item');
// Handle mouse over/leave on .nav-item
for (const element of navItemsElements) {
    element.addEventListener('mouseover', (event) => handleMouseOver(event));
    element.addEventListener('mouseleave', (event) => handleMouseLeave(event));
}
function handleMouseOver(event) {
    const target = event.target;
    if (window.innerWidth > 750 && target.closest('.nav-item')) {
        const navItem = target.closest('.nav-item');
        navItem.classList.add('show');
        setTimeout(() => {
            if (navItem.matches(':hover')) {
                navItem.classList.add('show');
            }
            else {
                navItem.classList.remove('show');
            }
        }, 1);
    }
}
function handleMouseLeave(event) {
    const target = event.target;
    const relatedTarget = event.relatedTarget;
    if (window.innerWidth > 750 && target.closest('.nav-item')) {
        const navItem = target.closest('.nav-item');
        if (!navItem.contains(relatedTarget)) {
            navItem.classList.remove('show');
        }
    }
}
