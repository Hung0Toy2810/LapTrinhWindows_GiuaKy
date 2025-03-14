namespace LapTrinhWindows.Respositories.UserRespositories
{
    public interface IAdminRepository
    {
        // create emlpoyee
        Task<Employee> CreateEmployeeAsync(Employee employee);
        // update employee
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        // delete employee
        Task<Employee> DeleteEmployeeAsync(Employee employee);
        // get all employees
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        // get employee by id
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        // get employee by email
        Task<Employee> GetEmployeeByEmailAsync(string email);
        // get employee by username
        Task<Employee> GetEmployeeByUserNameAsync(string userName);
        // check if emloyee exists by username
        Task<bool> CheckIfEmployeeExistsByUserNameAsync(string userName);
        // check if employee exists by email
        Task<bool> CheckIfEmployeeExistsByEmailAsync(string email);
        
    }
}