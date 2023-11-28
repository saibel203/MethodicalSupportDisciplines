const header: HTMLElement = document.querySelector('.header-main');

// During scrolling, the header decreases
window.addEventListener('scroll', (): void => {
    const scroll: number = window.scrollY || document.documentElement.scrollTop;

    if (scroll >= 10) {
        header.classList.remove('header-main');
        header.classList.add('scroll-on');
    } else {
        header.classList.remove('scroll-on');
        header.classList.add('header-main');
    }
});

const navItemsElements: HTMLCollectionOf<Element> = document.getElementsByClassName('nav-item');

// Handle mouse over/leave on .nav-item
for (const element of navItemsElements) {
    element.addEventListener('mouseover', (event: MouseEvent) => handleMouseOver(event));
    element.addEventListener('mouseleave', (event: MouseEvent) => handleMouseLeave(event));
}

function handleMouseOver(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    if (window.innerWidth > 750 && target.closest('.nav-item')) {
        const navItem: HTMLElement = target.closest('.nav-item');
        navItem.classList.add('show');

        setTimeout(() => {
            if (navItem.matches(':hover')) {
                navItem.classList.add('show');
            } else {
                navItem.classList.remove('show');
            }
        }, 1);
    }
}

function handleMouseLeave(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    const relatedTarget = event.relatedTarget as HTMLElement;

    if (window.innerWidth > 750 && target.closest('.nav-item')) {
        const navItem: HTMLElement = target.closest('.nav-item');

        if (!navItem.contains(relatedTarget)) {
            navItem.classList.remove('show');
        }
    }
}