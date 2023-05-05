using BethanysPieShopHRM.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BethanysPieShopHRM
{
    internal class Utilities
    {
        private static string directory = @"E:\c_sharp_before_SoftUni\PluralsightCS\BethanysPieShopHRM\Utilitiess";
        private static string fileName = "employees.txt";

        internal static void RegisterEmployee(List<Employee> employees)
        {
            Console.WriteLine("Registering an employee");

            Console.WriteLine("What type of employee do you want to register?");
            Console.WriteLine("1. Employee \n2. Manager \n3. Store Manager \n4. Researcher\n5. Junior Researcher");
            Console.WriteLine("Your selection: ");
            string employeeType = Console.ReadLine();

            if (employeeType != "1" && employeeType != "2" && employeeType != "3" && employeeType != "4" && employeeType != "5")
            {
                Console.WriteLine("Invalid Selection");
                return;
            }

            Console.Write("Enter the first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter the last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter the email address: ");
            string email = Console.ReadLine();

            Console.Write("Enter the date of birth: ");
            DateTime birthday = DateTime.Parse(Console.ReadLine()); //ex. 2/16/2002

            Console.Write("Enter the hourly rate: ");
            string hourlyRate = Console.ReadLine();
            double rate = double.Parse(hourlyRate); //we will assume the input is in the correct format

            Employee employee = null;

            switch(employeeType)
            {
                case "1":
                    employee = new Employee(firstName, lastName, email, birthday, rate);
                    break;
                case "2":
                    employee = new Manager(firstName, lastName, email, birthday, rate);
                    break;
                case "3":
                    employee = new StoreManager(firstName, lastName, email, birthday, rate);
                    break;
                case "4":
                    employee = new Researcher(firstName, lastName, email, birthday, rate);
                    break;
                case "5":
                    employee = new JuniorResearcher(firstName, lastName, email, birthday, rate);
                    break;
            }

            employees.Add(employee);

            Console.WriteLine("Employee Created!\n\n");
        }
        internal static void CheckForExistingEmployeeFile()
        {
            string path = $"{directory}{fileName}";
            bool existingFileFound = File.Exists(path);

            if (existingFileFound)
            {
                Console.WriteLine("An existing file with Employee data is found.");
            }
            else
            {
                if(!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Directory is ready for saving files.");
                    Console.ResetColor();
                }
            }
        }
        internal static void ViewAllEmployees(List<Employee> employees)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                employees[i].DisplayEmployeeDetails();
            }
        }
        internal static void LoadEmployees(List<Employee> employees)
        {
            string path = $"{directory}{fileName}";
            try
            {
                if (File.Exists(path))
                {
                    employees.Clear();

                    //now read the file
                    string[] employeesAsString = File.ReadAllLines(path);
                    for (int i = 0; i < employeesAsString.Length; i++)
                    {
                        string[] employeeSplits = employeesAsString[i].Split(';');
                        string firstName = employeeSplits[0].Substring(employeeSplits[0].IndexOf(':') + 1);
                        string lastName = employeeSplits[1].Substring(employeeSplits[1].IndexOf(':') + 1);
                        string email = employeeSplits[2].Substring(employeeSplits[2].IndexOf(':') + 1);
                        DateTime birthday = DateTime.Parse(employeeSplits[3].Substring(employeeSplits[3].IndexOf(':') + 1));
                        double hourlyRate = double.Parse(employeeSplits[4].Substring(employeeSplits[4].IndexOf(':') + 1));
                        string employeeType = employeeSplits[5].Substring(employeeSplits[5].IndexOf(':') + 1);

                        Employee employee = null;

                        switch (employeeType)
                        {
                            case "1":
                                employee = new Employee(firstName, lastName, email, birthday, hourlyRate);
                                break;
                            case "2":
                                employee = new Manager(firstName, lastName, email, birthday, hourlyRate);
                                break;
                            case "3":
                                employee = new StoreManager(firstName, lastName, email, birthday, hourlyRate);
                                break;
                            case "4":
                                employee = new Researcher(firstName, lastName, email, birthday, hourlyRate);
                                break;
                            case "5":
                                employee = new JuniorResearcher(firstName, lastName, email, birthday, hourlyRate);
                                break;
                        }

                        employees.Add(employee);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Loaded {employees.Count} employees!\n\n");
                    Console.ResetColor();
                } 
            }
            catch (IndexOutOfRangeException iex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while parsing the file. Please check the data!\n\n");
                Console.WriteLine(iex.Message);
                //Console.ResetColor();
            }
            catch (FileNotFoundException fnfex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file could not be found");
                Console.WriteLine(fnfex.Message);
                Console.WriteLine(fnfex.StackTrace);
                //Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while loading the file!\n\n");
                Console.WriteLine(ex.Message);
                //Console.ResetColor();
            }
            finally
            {
                Console.ResetColor();
            }
        }
        internal static void SaveEmployees(List<Employee> employees)
        {
            string path = $"{directory}{fileName}";
            StringBuilder sb = new StringBuilder();
            foreach (Employee employee in employees)
            {
                string type = GetEmployeeType(employee);
                sb.Append($"firstName:{employee.FirstName};");
                sb.Append($"lastName:{employee.LastName};");
                sb.Append($"email:{employee.Email};");
                sb.Append($"birthday:{employee.BirthDay.ToShortDateString()};");
                sb.Append($"hourlyRate:{employee.HourlyRate};");
                sb.Append($"type:{type};");
                sb.Append(Environment.NewLine);
            }
            File.WriteAllText(path, sb.ToString());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saved employee successfully!");
            Console.ResetColor();
        }
        private static string GetEmployeeType(Employee employee)
        {
            if (employee is Manager)
            {
                return "2";
            }
            else if (employee is StoreManager)
            {
                return "3";
            }
            else if (employee is JuniorResearcher)
            {
                return "5";
            }
            else if (employee is Researcher)
            {
                return "4";
            }
            else if (employee is Employee)
            {
                return "1";
            }
            return "0";
        }
        internal static void LoadEmployeeById(List<Employee> employees)
        {
            try
            {
                Console.WriteLine("Enter the Employee ID you want to visualize");

                int selectedID = int.Parse(Console.ReadLine());
                Employee selectedEmployee = employees[selectedID];
                selectedEmployee.DisplayEmployeeDetails();
            }
            catch (FormatException fex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That's not the correct format to enter an ID\n\n");
                Console.ResetColor();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That ID is out of range my boi!\n\n");
                Console.ResetColor();
            }
        }
    }
}
