using Entities.Models;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Repository.Utility;

namespace Repository.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint minAge, uint maxAge) =>
        employees.Where(e => (e.Age >= minAge && e.Age <= maxAge));

    public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return employees;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return employees.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderyByQueryString)
    {
        // Check if the orderByQueryString is empty.
        if (string.IsNullOrWhiteSpace(orderyByQueryString))
            return employees.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderyByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return employees.OrderBy(e => e.Name);

        return employees.OrderBy(orderQuery);
    }
}
