namespace MethodicalSupportDisciplines.MVC.Helpers;

public static class ViewHelper
{
    public static string GetUserFullName(string firstName, string lastName, string patronymic)
    {
        string resultString = $"{lastName} {firstName} {patronymic}";
        return resultString;
    }

    public static string ConvertPhoneNumber(string phoneNumber)
    {
        if (phoneNumber.Length == 10)
        {
            string resultString = 
                $"+38-({phoneNumber.Substring(0, 3)})-{phoneNumber.Substring(3, 3)}-" +
                $"{phoneNumber.Substring(6, 2)}-{phoneNumber.Substring(8, 2)}";

            return resultString;
        }

        return phoneNumber;
    }
}