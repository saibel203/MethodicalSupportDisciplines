const pathFields = document.getElementsByClassName('material-path-field');
const selectField = document.getElementsByClassName('material-type-id');
const filePathInput = document.getElementsByClassName('file-path-text');
const fileFileInput = document.getElementsByClassName('file-path-file');
const hrefValue = '1';
const bookValue = '2';
const fileValue = '3';
for (let i = 0; i < pathFields.length; i++) {
    filePathInput[i].style.display = 'block';
    fileFileInput[i].style.display = 'none';
    selectField[i].addEventListener('change', () => {
        const selectValue = selectField[i].value;
        if (selectValue === hrefValue) {
            filePathInput[i].setAttribute('style', 'display: block');
            fileFileInput[i].setAttribute('style', 'display: none');
        }
        else if (selectValue === bookValue || selectValue === fileValue) {
            fileFileInput[i].setAttribute('style', 'display: block');
            filePathInput[i].setAttribute('style', 'display: none');
        }
        else {
            console.error('Select material type error');
        }
    });
}
