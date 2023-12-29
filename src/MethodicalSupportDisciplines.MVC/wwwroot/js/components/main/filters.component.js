const filtersButton = document.getElementsByClassName('filters-button')[0];
const filtersPanel = document.getElementsByClassName('filters-sidebar')[0];
const sidebarCloseButton = document.getElementsByClassName('sidebar-close-button')[0];
if (filtersPanel !== undefined && filtersButton !== undefined && sidebarCloseButton !== undefined) {
    filtersButton.addEventListener('click', () => {
        filtersPanel.classList.toggle('show-filters');
    });
    window.addEventListener('click', (event) => {
        const isClickInsideSidebar = filtersPanel.contains(event.target);
        const isClickOnButton = filtersButton.contains(event.target);
        if (!isClickInsideSidebar && !isClickOnButton) {
            filtersPanel.classList.remove('show-filters');
        }
    });
    sidebarCloseButton.addEventListener('click', () => {
        filtersPanel.classList.remove('show-filters');
    });
}
