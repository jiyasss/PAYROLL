using System;
using System.Collections.Generic;
using System.Linq;
using Validation;
namespace PayrollEmp
{
    class Program
    {
        static List<Employee> employees = new List<Employee>();
        static List<PayrollEntry> payrollEntries = new List<PayrollEntry>();
        static void Main()
        {
            bool exitProgram = false;
            while (!exitProgram)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        EmployeesMenu();
                        break;
                    case "2":
                        PayrollMenu();
                        break;
                    case "3":
                        exitProgram = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void DisplayMainMenu()
        {
            Console.WriteLine("Welcome,");
            Console.WriteLine("Please choose a command:");
            Console.WriteLine("Press 1 for Employees Menu");
            Console.WriteLine("Press 2 for Payroll Menu");
            Console.WriteLine("Press 3 to Exit Program");
        }
        static void EmployeesMenu()
        {
            bool returnToMainMenu = false;

            while (!returnToMainMenu)
            {
                Console.WriteLine("Menu 1: Employees");
                Console.WriteLine("Press 1 to list all employees");
                Console.WriteLine("Press 2 to add a new employee");
                Console.WriteLine("Press 3 to update an employee");
                Console.WriteLine("Press 4 to delete an employee");
                Console.WriteLine("Press 5 to return to main menu");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        ListEmployees();
                        break;
                    case "2":
                        AddEmployee();
                        break;
                    case "3":
                        UpdateEmployee();
                        break;
                    case "4":
                        DeleteEmployee();
                        break;
                    case "5":
                        returnToMainMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void PayrollMenu()
        {
            bool returnToMainMenu = false;

            while (!returnToMainMenu)
            {
                Console.WriteLine("Menu 2: Payroll");
                Console.WriteLine("Press 1 to insert new payroll entry");
                Console.WriteLine("Press 2 to view payroll history for an employee");
                Console.WriteLine("Press 3 to return to main menu");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        InsertPayrollEntry();
                        break;
                    case "2":
                        ViewPayrollHistory();
                        break;
                    case "3":
                        returnToMainMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void ListEmployees()
        {
            Console.WriteLine("List of Employees:");
            foreach (var employee in employees)
            {
                Console.WriteLine($"Employee Id: {employee.Id}, Name: {employee.Name}, Department: {employee.Department}");
                Console.WriteLine($"Address: {employee.Address}, Email: {employee.Email}, Phone: {employee.Phone}");
                Console.WriteLine();
            }
        }
        static void AddEmployee()
        {
            Console.WriteLine("Enter employee details:");
            int id;
            bool isIdValid = false;
            do
            {
                Console.Write("ID: ");
                id = NumberValidator.number(Console.ReadLine());
                if (employees.Any(emp => emp.Id == id))
                {
                    Console.WriteLine("Employee with this ID already exists. Please enter a unique ID.");
                }
                else
                {
                    isIdValid = true;
                }
            } while (!isIdValid);
            Console.Write("Name: ");
            string name = StringValidator.ValidateString(Console.ReadLine().Trim());
            Console.Write("Address: ");
            string address = Console.ReadLine().Trim();
            Console.Write("Email: ");
            string email = EmailValidator.Email(Console.ReadLine().Trim());
            Console.Write("Phone: ");
            string phone = Phone.Number(Console.ReadLine().Trim());
            Console.Write("Department: ");
            string department = StringValidator.ValidateString(Console.ReadLine().Trim());
            employees.Add(new Employee(id, name, address, email, phone, department));
            Console.WriteLine("Employee added successfully.\n");
        }
        static void UpdateEmployee()
        {
            Console.Write("Enter employee ID to update: ");
            int id = NumberValidator.number(Console.ReadLine().Trim());
            Employee employee = employees.Find(e => e.Id == id);
            if (employee != null)
            {
                Console.WriteLine($"Current details:");
                Console.WriteLine($"Name: {employee.Name}, Address: {employee.Address}, Email: {employee.Email}, Phone: {employee.Phone}, Department: {employee.Department}");
                Console.Write("Enter new Name: ");
                string newName = StringValidator.ValidateString(Console.ReadLine().Trim());
                Console.Write("Enter new Address: ");
                string address = Console.ReadLine().Trim();
                Console.Write("Enter new Email: ");
                string email = EmailValidator.Email(Console.ReadLine().Trim());
                Console.Write("Enter new Phone: ");
                string phone = Phone.Number(Console.ReadLine().Trim());
                Console.Write("Enter new Department: ");
                string newDepartment = StringValidator.ValidateString(Console.ReadLine().Trim());
                employee.Name = newName;
                employee.Address = address;
                employee.Email = email;
                employee.Phone = phone;
                employee.Department = newDepartment;
                Console.WriteLine("Employee details updated.\n");
            }
            else
            {
                Console.WriteLine("Employee not found.\n");
            }
        }
        static void DeleteEmployee()
        {
            Console.Write("Enter employee ID to delete: ");
            int id = NumberValidator.number(Console.ReadLine().Trim());
            Employee employee = employees.Find(e => e.Id == id);
            if (employee != null)
            {
                employees.Remove(employee);
                Console.WriteLine("Employee deleted.\n");
            }
            else
            {
                Console.WriteLine("Employee not found.\n");
            }
        }
        static void InsertPayrollEntry()
        {
            Console.Write("Enter employee ID: ");
            int id = NumberValidator.number(Console.ReadLine().Trim());
            Employee employee = employees.Find(e => e.Id == id);
            if (employee != null)
            {
                Console.Write("Enter Payroll ID: ");
                int pid = NumberValidator.number(Console.ReadLine().Trim());
                Console.Write("Enter per Hour Rate: ");
                double amount = CurrencyValidator.Currency(Console.ReadLine().Trim());
                Console.Write("Enter Total Hour Worked: ");
                int totalHour = NumberValidator.number(Console.ReadLine().Trim());
                Console.Write("Enter Date for Payroll (MM/dd/yyyy): ");
                DateTime dateTime = DateParser.ParseDate(Console.ReadLine().Trim());
                bool foundExisting = false;
                foreach (PayrollEntry entry in payrollEntries)
                {
                    if (entry.Employee.Id == id && entry.Date.Month == dateTime.Month && entry.Date.Year == dateTime.Year)
                    {
                        entry.Id = pid;
                        entry.Amount = amount;
                        entry.TotalHours = totalHour;
                        entry.Date = dateTime;
                        foundExisting = true;
                        Console.WriteLine("Payroll entry updated successfully.\n");
                        break;
                    }
                }
                if (!foundExisting)
                {
                    payrollEntries.Add(new PayrollEntry(pid, employee, totalHour, amount, dateTime));
                    Console.WriteLine("Payroll entry added successfully.\n");
                }
            }
            else
            {
                Console.WriteLine("Employee not found.\n");
            }
        }
        static void ViewPayrollHistory()
        {
            Console.Write("Enter employee ID to view payroll history: ");
            int id = NumberValidator.number(Console.ReadLine().Trim());
            Employee employee = employees.Find(e => e.Id == id);
            if (employee != null)
            {
                Console.WriteLine($"Payroll history for {employee.Name}:");
                foreach (var entry in payrollEntries)
                {
                    if (entry.Employee.Id == id)
                    {
                        Console.WriteLine($"Date: {entry.Date.ToShortDateString()}, Payroll Id: {entry.Id}, Total Hour Worked: {entry.TotalHours}, Per Hour Amount: {entry.Amount}");
                    }
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Employee not found.\n");
            }
        }
    }
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public Employee(int id, string name, string address, string email, string phone, string department)
        {
            Id = id;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
            Department = department;
        }
    }
    class PayrollEntry
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int TotalHours { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public PayrollEntry(int id, Employee employee, int totalHours, double amount, DateTime date)
        {
            Id = id;
            Employee = employee;
            TotalHours = totalHours;
            Amount = amount;
            Date = date;
        }
    }
}