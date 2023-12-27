const filtersButton = 
    document.getElementsByClassName('filters-button')[0] as HTMLButtonElement;
const filtersPanel 
    = document.getElementsByClassName('filters-sidebar')[0] as HTMLDivElement;
const sidebarCloseButton 
    = document.getElementsByClassName('sidebar-close-button')[0] as HTMLButtonElement;

if (filtersPanel !== undefined && filtersButton !== undefined && sidebarCloseButton !== undefined) {
    filtersButton.addEventListener('click', (): void => {
        filtersPanel.classList.toggle('show-filters');
    });

    window.addEventListener('click', (event: MouseEvent): void => {
        const isClickInsideSidebar: boolean = filtersPanel.contains(event.target as Node);
        const isClickOnButton: boolean = filtersButton.contains(event.target as Node);

        if (!isClickInsideSidebar && !isClickOnButton) {
            filtersPanel.classList.remove('show-filters');
        }
    });

    sidebarCloseButton.addEventListener('click', (): void => {
        filtersPanel.classList.remove('show-filters');
    });
}