const loginPasswordField = document.getElementById('login-password-field');
const loginPasswordEyeIcon = document.getElementById('login-password-eye');
if (loginPasswordField && loginPasswordEyeIcon) {
    loginPasswordEyeIcon.addEventListener('click', () => {
        if (loginPasswordField.type === 'password') {
            loginPasswordField.setAttribute('type', 'text');
            loginPasswordEyeIcon.setAttribute('class', 'fa fa-eye-slash');
        }
        else {
            loginPasswordField.setAttribute('type', 'password');
            loginPasswordEyeIcon.setAttribute('class', 'fa fa-eye');
        }
    });
}
const loginPasswordFields = document.getElementsByClassName('login-password-field');
const loginPasswordIcons = document.getElementsByClassName('login-password-eye');
for (let i = 0; i < loginPasswordIcons.length; i++) {
    loginPasswordIcons[i].addEventListener('click', () => {
        if (loginPasswordFields[i].type === 'password') {
            loginPasswordFields[i].setAttribute('type', 'text');
            loginPasswordIcons[i].setAttribute('class', 'fa fa-eye-slash login-password-eye');
        }
        else {
            loginPasswordFields[i].setAttribute('type', 'password');
            loginPasswordIcons[i].setAttribute('class', 'fa fa-eye login-password-eye');
        }
    });
}
