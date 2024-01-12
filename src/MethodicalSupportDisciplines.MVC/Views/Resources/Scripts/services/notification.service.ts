import {INotificationService} from "../interfaces/INotificationService";

export class NotificationService implements INotificationService {
    showErrorMessage(message: string): void {
        // @ts-ignore
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: message,
            footer: '<a href="">Why do I have this issue?</a>'
        })
    }

    showSuccessMessage(message: string): void {
        // @ts-ignore
        Swal.fire(
            'Successfully!',
            message,
            'success'
        );
    }

    showConfirmMessage(message: string): void {

    }
}