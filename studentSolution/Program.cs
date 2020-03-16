using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using StudentManagement.Core.DomainModel;
using StudentManagement.Core.UnitOfWork;
using StudentManagement.MyData.UnitOfWork;


namespace studentSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args != null && args.Length >= 2)
                {   
                    // validate inputs
                    string fileName = args[0];

                    if (!File.Exists(fileName))
                    {
                        Console.WriteLine($"ERROR: File '{fileName}' not Found.");
                        return;
                    }

                    string filter1 = args[1];
                    var field1 = filter1.Split('=');
                    if (field1.Length != 2)
                    {
                        Console.WriteLine($"ERROR: Filter '{filter1}' is not correct.");
                        return;
                    }

                    string[] field2 = null;
                    string filter2 = null;
                    if (args.Length == 3)
                    {
                        filter2 = args[2];
                        field2 = filter2.Split('=');
                        if (field2.Length != 2)
                        {
                            Console.WriteLine($"ERROR: Filter '{filter2}' is not correct.");
                            return;
                        }
                    }

                    // step 1 - map csv file to objects
                    var students = File.ReadLines(fileName).Select(line => CreateStudent(line)).ToList();
                    IUnitOfWork work = new UnitOfWork();
                    foreach (var student in students)
                    {
                        work.Students.SaveOrUpdate(student);
                    }

                    // step 2 - apply the search
                    IEnumerable<Student> resultList = null;
                    
                    switch (field1[0])
                    {
                        case "name":
                            resultList = work.Students.SearchByName(field1[1]);
                            break;
                        case "type":
                            
                            if (field2 != null)
                            {
                                if (field2[0] == "gender")
                                {
                                    resultList = work.Students.SearchByGenderAndType(field2[1].Substring(0, 1), field1[1]);
                                }
                                else
                                {
                                    Console.WriteLine($"Error: Filter '{filter2}' contains invalid information.");
                                    return;
                                }
                            }
                            else
                            {
                                resultList = work.Students.SearchByStudentType(field1[1]);
                            }
                            break;
                        default:
                            Console.WriteLine($"Error: Filter '{filter1}' contains invalid information.");
                            return;
                    }
                    
                    WriteLine(resultList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        private static void WriteLine(IEnumerable<Student> resultList)
        {
            if (!resultList.Any())
            {
                Console.WriteLine("No Result....");
                return;
            }

            foreach (var student in resultList)
            {
                Console.WriteLine($"{student.Type}, {student.Name}, {student.Gender}, {student.LastUpdate}");
            }
        }

        public static Student CreateStudent(string csvLine)
        {
            var values = csvLine.Split(',');
            if (values.Length == 4)
            {
                return new Student()
                {
                    Type = values[0],
                    Name = values[1],
                    Gender = values[2].Substring(0,1),
                    LastUpdate = CreateDate(values[3]),
                    Id = Guid.NewGuid()
                };
            }
            else
            {
                throw new FormatException("The cvs line contains invalid format");
            }
        }

        private static DateTime CreateDate(string stringDate)
        {
            try
            {
                int year = int.Parse(stringDate.Substring(0, 4));
                int month = int.Parse(stringDate.Substring(4, 2));
                int day = int.Parse(stringDate.Substring(6, 2));
                int hour = int.Parse(stringDate.Substring(8, 2));
                int min = int.Parse(stringDate.Substring(10, 2));
                int sec = int.Parse(stringDate.Substring(12, 2));

                return new DateTime(year, month, day, hour, min, sec);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error at moment to parse string to Date");
                throw e;
            }
           
        }
    }
}
