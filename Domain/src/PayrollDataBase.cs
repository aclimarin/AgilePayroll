using System.Collections;

namespace Domain;

public class PayrollDataBase
{
    private static Hashtable _employees = new Hashtable();
    private static Hashtable _unionMember = new Hashtable();

    public static void AddEmployee(int id, Employee employee)
    {
        _employees[id] = employee;
    }

    public static Employee GetEmployee(int id)
    {
        return _employees[id] as Employee;
    }

    public static void DeleteEmployee(int id)
    {
        _employees.Remove(id);
    }

    public static void AddUnionMember(int memberId, Employee employee)
    {
        _unionMember[memberId] = employee.Id;
    }

    public static Employee GetUnionMember(int memberId)
    {
        if (!_unionMember.ContainsKey(memberId))
        {
            return null;
        }
        return GetEmployee((int)_unionMember[memberId]);
    }

    public static void RemoveUnionMember(int memberId)
    {
        _unionMember.Remove(memberId);
    }

    public static ICollection GetAllEmployeeIds()
    {
        return _employees.Keys;
    }
}