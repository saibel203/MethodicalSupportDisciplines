using System.Linq.Expressions;

namespace MethodicalSupportDisciplines.BLL.Helpers;

public static class SearchHelper
{
    public static IReadOnlyList<T> ReadOnlySearch<T>(Expression<Func<T, bool>> predicate, IReadOnlyList<T> initialData)
        where T : class
    {
        return initialData.Where(predicate.Compile()).ToList();
    }
}