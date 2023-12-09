namespace MethodicalSupportDisciplines.MVC.Helpers;

public static class ViewHelper
{
    public static string GetUserFullName(string firstName, string lastName, string patronymic)
    {
        string resultString = $"{lastName} {firstName} {patronymic}";
        return resultString;
    }
}