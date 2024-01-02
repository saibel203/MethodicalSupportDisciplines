export interface INotificationService {
    showErrorMessage(message: string): void;
    showSuccessMessage(message: string): void;
    showConfirmMessage(message: string): void;
}