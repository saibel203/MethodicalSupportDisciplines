export class NotificationService {
    showErrorMessage(message) {
        // @ts-ignore
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: message,
            footer: '<a href="">Why do I have this issue?</a>'
        });
    }
    showSuccessMessage(message) {
        // @ts-ignore
        Swal.fire('Successfully!', message, 'success');
    }
    showConfirmMessage(message) {
    }
}
