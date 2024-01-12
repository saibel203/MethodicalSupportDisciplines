const pathFields = document.getElementsByClassName('material-path-field') as HTMLCollectionOf<HTMLInputElement>;
const selectField = document.getElementsByClassName('material-type-id') as HTMLCollectionOf<HTMLSelectElement>;
const filePathInput = document.getElementsByClassName('file-path-text') as HTMLCollectionOf<HTMLInputElement>;
const fileFileInput = document.getElementsByClassName('file-path-file') as HTMLCollectionOf<HTMLInputElement>;

const hrefValue: string = '1';
const bookValue: string = '2';
const fileValue: string = '3';

for (let i: number = 0; i < pathFields.length; i++) {
    filePathInput[i].style.display = 'block';
    fileFileInput[i].style.display = 'none';
    
    selectField[i].addEventListener('change', (): void => {
        const selectValue = selectField[i].value;

        if (selectValue === hrefValue) {
            filePathInput[i].setAttribute('style', 'display: block');
            fileFileInput[i].setAttribute('style', 'display: none');
        } else if (selectValue === bookValue || selectValue === fileValue) {
            fileFileInput[i].setAttribute('style', 'display: block');
            filePathInput[i].setAttribute('style', 'display: none');
        } else {
            console.error('Select material type error');
        }
    });
}