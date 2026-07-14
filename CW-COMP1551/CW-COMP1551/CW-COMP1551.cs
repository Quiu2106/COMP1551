using System;
using System.Collections.Generic;
using System.Linq;

namespace CW_COMP1551
{
    // ============================================================
    // BASE CLASS: PERSON
    // Represents a general individual stored in the system.
    // Shared attributes and behaviours are centralised here to
    // avoid duplication in Teacher, Student, and AdminStaff.
    // ============================================================
    public abstract class Person
    {
        // Common attributes stored for every person
        public int PersonId { get; set; }
        public string FullName { get; set; }
        public int Telephone { get; set; }
        public string Email { get; set; }

        // Role is read-only for safety (set only by subclasses)
        public string Role { get; protected set; }

        // Constructor initialising shared fields
        protected Person(int personId, string fullName, int telephone, string email, string role)
        {
            PersonId = personId;
            FullName = fullName;
            Telephone = telephone;
            Email = email;
            Role = role;
        }

        // Returns one-line summary of the object
        public virtual string GetInfo()
        {
            return $"{PersonId} - {FullName} ({Role}) | Tel: {Telephone} | Email: {Email}";
        }

        // Displays full formatted information
        public virtual void DisplayInfo()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"ID       : {PersonId}");
            Console.WriteLine($"Name     : {FullName}");
            Console.WriteLine($"Role     : {Role}");
            Console.WriteLine($"Telephone: {Telephone}");
            Console.WriteLine($"Email    : {Email}");
        }

        // Allows user to edit the object's common data with validation
        public virtual void EditInfo()
        {
            Console.WriteLine($"Editing common details for {Role} (ID: {PersonId})");

            // ---- Full name ----
            Console.Write($"Full name ({FullName}): ");
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim();
                // Only letters and spaces, length 3–50
                if (input.Length >= 3 && input.Length <= 50 &&
                    input.All(c => char.IsLetter(c) || c == ' '))
                {
                    FullName = input;
                }
                else
                {
                    Console.WriteLine("Invalid name. Keeping old value.");
                }
            }

            // ---- Telephone ----
            Console.Write($"Telephone ({Telephone}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (int.TryParse(input, out int newTel))
                {
                    // 7–9 digits: between 1,000,000 and 999,999,999
                    if (newTel >= 1_000_000 && newTel <= 999_999_999)
                    {
                        Telephone = newTel;
                    }
                    else
                    {
                        Console.WriteLine("Telephone must be 7–9 digits. Keeping old value.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid telephone. Keeping old value.");
                }
            }

            // ---- Email ----
            Console.Write($"Email ({Email}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim();
                if (IsValidEmailFormat(input))
                {
                    Email = input;
                }
                else
                {
                    Console.WriteLine("Invalid email format. Keeping old value.");
                }
            }
        }

        // Simple reusable email format check used by base and subclasses
        protected bool IsValidEmailFormat(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            email = email.Trim();

            if (email.Length > 50)
                return false;

            int atIndex = email.IndexOf("@", StringComparison.Ordinal);
            int dotIndex = email.LastIndexOf(".", StringComparison.Ordinal);

            return atIndex > 0 &&
                   dotIndex > atIndex + 1 &&
                   dotIndex < email.Length - 1;
        }
    }

    // ============================================================
    // SUBCLASS: TEACHER
    // Adds subject information and salary unique to teachers.
    // ============================================================
    public class Teacher : Person
    {
        public decimal Salary { get; set; }
        public string Subject1 { get; set; }
        public string Subject2 { get; set; }

        public Teacher(
            int personId,
            string fullName,
            int telephone,
            string email,
            decimal salary,
            string subject1,
            string subject2)
            : base(personId, fullName, telephone, email, "Teacher")
        {
            Salary = salary;
            Subject1 = subject1;
            Subject2 = subject2;
        }

        // Displays teacher-specific data
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Salary   : {Salary:C}");
            Console.WriteLine($"Subject1 : {Subject1}");
            Console.WriteLine($"Subject2 : {Subject2}");
        }

        // Editing teacher-specific fields with validation
        public override void EditInfo()
        {
            base.EditInfo();

            // Salary with confirmation on change
            Console.Write($"Salary ({Salary}): ");
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (decimal.TryParse(input, out decimal newSalary) && newSalary >= 0)
                {
                    Console.Write($"Confirm change salary from {Salary} to {newSalary}? (Y/N): ");
                    string? confirm = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(confirm) &&
                        confirm.Trim().Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        Salary = newSalary;
                    }
                    else
                    {
                        Console.WriteLine("Salary change cancelled. Keeping old value.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid salary. Keeping old value.");
                }
            }

            // Subjects: non-empty, max length 30
            Console.Write($"Subject 1 ({Subject1}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim();
                if (input.Length <= 30)
                    Subject1 = input;
                else
                    Console.WriteLine("Subject 1 is too long (max 30 characters). Keeping old value.");
            }

            Console.Write($"Subject 2 ({Subject2}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim();
                if (input.Length <= 30)
                    Subject2 = input;
                else
                    Console.WriteLine("Subject 2 is too long (max 30 characters). Keeping old value.");
            }
        }
    }

    // ============================================================
    // SUBCLASS: STUDENT
    // Contains 3 subjects unique to student records.
    // ============================================================
    public class Student : Person
    {
        public string Subject1 { get; set; }
        public string Subject2 { get; set; }
        public string Subject3 { get; set; }

        public Student(
            int personId,
            string fullName,
            int telephone,
            string email,
            string subject1,
            string subject2,
            string subject3)
            : base(personId, fullName, telephone, email, "Student")
        {
            Subject1 = subject1;
            Subject2 = subject2;
            Subject3 = subject3;
        }

        // Display student info
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Subject1 : {Subject1}");
            Console.WriteLine($"Subject2 : {Subject2}");
            Console.WriteLine($"Subject3 : {Subject3}");
        }

        // Editing student-specific fields, ensuring non-duplicate subjects
        public override void EditInfo()
        {
            base.EditInfo();

            Console.Write($"Subject 1 ({Subject1}): ");
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim();
                if (input.Length <= 30)
                    Subject1 = input;
                else
                    Console.WriteLine("Subject 1 is too long (max 30 characters). Keeping old value.");
            }

            Console.Write($"Subject 2 ({Subject2}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim();
                if (input.Length <= 30)
                {
                    if (!input.Equals(Subject1, StringComparison.OrdinalIgnoreCase))
                        Subject2 = input;
                    else
                        Console.WriteLine("Subject 2 cannot be the same as Subject 1. Keeping old value.");
                }
                else
                {
                    Console.WriteLine("Subject 2 is too long (max 30 characters). Keeping old value.");
                }
            }

            Console.Write($"Subject 3 ({Subject3}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim();
                if (input.Length <= 30)
                {
                    if (!input.Equals(Subject1, StringComparison.OrdinalIgnoreCase) &&
                        !input.Equals(Subject2, StringComparison.OrdinalIgnoreCase))
                    {
                        Subject3 = input;
                    }
                    else
                    {
                        Console.WriteLine("Subject 3 must be different from Subject 1 and Subject 2. Keeping old value.");
                    }
                }
                else
                {
                    Console.WriteLine("Subject 3 is too long (max 30 characters). Keeping old value.");
                }
            }
        }
    }

    // ============================================================
    // SUBCLASS: ADMIN STAFF
    // Adds salary, employment type and weekly hours.
    // ============================================================
    public class AdminStaff : Person
    {
        public decimal Salary { get; set; }
        public string EmploymentType { get; set; } // e.g. Full-time / Part-time
        public int WeeklyHours { get; set; }

        public AdminStaff(
            int personId,
            string fullName,
            int telephone,
            string email,
            decimal salary,
            string employmentType,
            int weeklyHours)
            : base(personId, fullName, telephone, email, "AdminStaff")
        {
            Salary = salary;
            EmploymentType = employmentType;
            WeeklyHours = weeklyHours;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Salary         : {Salary:C}");
            Console.WriteLine($"EmploymentType : {EmploymentType}");
            Console.WriteLine($"Weekly Hours   : {WeeklyHours}");
        }

        public override void EditInfo()
        {
            base.EditInfo();

            // Employment type: only Full-time or Part-time
            Console.Write($"Employment Type ({EmploymentType}): ");
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string normalized = NormalizeEmploymentType(input.Trim());
                if (normalized != "")
                {
                    EmploymentType = normalized;
                }
                else
                {
                    Console.WriteLine("Invalid employment type. Use 'Full-time' or 'Part-time'. Keeping old value.");
                }
            }

            // Salary rule depends on employment type
            Console.Write($"Salary ({Salary}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (decimal.TryParse(input, out decimal newSalary) && newSalary >= 0)
                {
                    if (IsValidAdminSalary(EmploymentType, newSalary))
                    {
                        Console.Write($"Confirm change salary from {Salary} to {newSalary}? (Y/N): ");
                        string? confirm = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(confirm) &&
                            confirm.Trim().Equals("Y", StringComparison.OrdinalIgnoreCase))
                        {
                            Salary = newSalary;
                        }
                        else
                        {
                            Console.WriteLine("Salary change cancelled. Keeping old value.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Salary does not meet minimum requirement for this employment type. Keeping old value.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid salary. Keeping old value.");
                }
            }

            // Weekly hours also depend on employment type
            Console.Write($"Weekly Hours ({WeeklyHours}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (int.TryParse(input, out int newHours) && newHours >= 0)
                {
                    if (IsValidWeeklyHours(EmploymentType, newHours))
                    {
                        WeeklyHours = newHours;
                    }
                    else
                    {
                        Console.WriteLine("Weekly hours out of allowed range for this employment type. Keeping old value.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid hours. Keeping old value.");
                }
            }
        }

        // Helper: normalize employment type string
        private static string NormalizeEmploymentType(string value)
        {
            if (value.Equals("full-time", StringComparison.OrdinalIgnoreCase) ||
                value.Equals("full time", StringComparison.OrdinalIgnoreCase))
                return "Full-time";

            if (value.Equals("part-time", StringComparison.OrdinalIgnoreCase) ||
                value.Equals("part time", StringComparison.OrdinalIgnoreCase))
                return "Part-time";

            return "";
        }

        // Helper: business rules for salary based on employment type
        private static bool IsValidAdminSalary(string employmentType, decimal salary)
        {
            if (employmentType == "Full-time")
            {
                // Example rule: at least 1000 for full-time
                return salary >= 1000m;
            }

            if (employmentType == "Part-time")
            {
                // Example rule: at least 300 for part-time
                return salary >= 300m;
            }

            // Fallback if type is unknown (should not happen if validated)
            return salary >= 0;
        }

        // Helper: weekly hours rule based on employment type
        private static bool IsValidWeeklyHours(string employmentType, int hours)
        {
            if (employmentType == "Full-time")
            {
                // Example: 35–48 hours per week
                return hours >= 35 && hours <= 48;
            }

            if (employmentType == "Part-time")
            {
                // Example: 1–30 hours per week
                return hours >= 1 && hours <= 30;
            }

            // If for any reason type is unknown, fall back to 1–60
            return hours >= 1 && hours <= 60;
        }
    }

    // ============================================================
    // APPLICATION CONTROLLER
    // Handles menu, input, record management & data validation.
    // Uses List<Person> to store records in memory during runtime.
    // ============================================================
    public class Application
    {
        private readonly List<Person> persons = new List<Person>();

        // Main loop controlling program flow
        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                // Menu interface
                Console.WriteLine();
                Console.WriteLine("===== Desktop Information System =====");
                Console.WriteLine("1. Add New Record");
                Console.WriteLine("2. View All Records");
                Console.WriteLine("3. View Records by Role");
                Console.WriteLine("4. Edit Existing Record");
                Console.WriteLine("5. Delete Record");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                int choice = ReadIntInRange(0, 5);
                Console.WriteLine();

                // Menu switch handling selected operation
                switch (choice)
                {
                    case 1: AddNewRecord(); break;
                    case 2: ViewAllRecords(); break;
                    case 3: ViewRecordsByRole(); break;
                    case 4: EditRecord(); break;
                    case 5: DeleteRecord(); break;
                    case 0: exit = true; break;
                }
            }

            Console.WriteLine("Exiting application. Goodbye.");
        }

        // ============================================================
        // ADD NEW RECORD
        // Guides user through creating a Teacher, Student or AdminStaff
        // ============================================================
        private void AddNewRecord()
        {
            Console.WriteLine("Add New Record");
            Console.WriteLine("1. Teacher");
            Console.WriteLine("2. Student");
            Console.WriteLine("3. Admin Staff");
            Console.Write("Select role: ");

            int choice = ReadIntInRange(1, 3);

            // Collecting shared information with validation
            int personId = ReadUniquePersonId();
            string fullName = ReadNonEmptyString("Full name: ");
            int telephone = ReadInt("Telephone (7–9 digits): ");
            string email = ReadUniqueEmail("Email: ");

            // Creating subclass objects based on role
            switch (choice)
            {
                case 1:
                    {
                        decimal salaryT = ReadDecimal("Salary: ");
                        string s1 = ReadNonEmptyString("Subject 1: ");
                        string s2;

                        // Ensure subject2 different from subject1
                        while (true)
                        {
                            s2 = ReadNonEmptyString("Subject 2: ");
                            if (!s2.Equals(s1, StringComparison.OrdinalIgnoreCase))
                                break;

                            Console.WriteLine("Subject 2 cannot be the same as Subject 1.");
                        }

                        persons.Add(new Teacher(personId, fullName, telephone, email, salaryT, s1, s2));
                        break;
                    }

                case 2:
                    {
                        string st1 = ReadNonEmptyString("Subject 1: ");
                        string st2;
                        string st3;

                        // Ensure unique subjects for student
                        while (true)
                        {
                            st2 = ReadNonEmptyString("Subject 2: ");
                            if (!st2.Equals(st1, StringComparison.OrdinalIgnoreCase))
                                break;

                            Console.WriteLine("Subject 2 cannot be the same as Subject 1.");
                        }

                        while (true)
                        {
                            st3 = ReadNonEmptyString("Subject 3: ");
                            if (!st3.Equals(st1, StringComparison.OrdinalIgnoreCase) &&
                                !st3.Equals(st2, StringComparison.OrdinalIgnoreCase))
                                break;

                            Console.WriteLine("Subject 3 must be different from Subject 1 and Subject 2.");
                        }

                        persons.Add(new Student(personId, fullName, telephone, email, st1, st2, st3));
                        break;
                    }

                case 3:
                    {
                        string empType = ReadNonEmptyString("Employment type (Full-time / Part-time): ");
                        empType = NormalizeEmploymentType(empType);
                        while (empType == "")
                        {
                            Console.WriteLine("Invalid employment type. Use 'Full-time' or 'Part-time'.");
                            empType = NormalizeEmploymentType(ReadNonEmptyString("Employment type (Full-time / Part-time): "));
                        }

                        // salary and weekly hours validated by business rules
                        decimal salaryA;
                        while (true)
                        {
                            salaryA = ReadDecimal("Salary: ");
                            if (IsValidAdminSalary(empType, salaryA))
                                break;

                            Console.WriteLine("Salary does not meet minimum requirement for this employment type.");
                        }

                        int hours;
                        while (true)
                        {
                            hours = ReadInt("Weekly hours: ");
                            if (IsValidWeeklyHours(empType, hours))
                                break;

                            Console.WriteLine("Weekly hours out of allowed range for this employment type.");
                        }

                        persons.Add(new AdminStaff(personId, fullName, telephone, email, salaryA, empType, hours));
                        break;
                    }
            }

            Console.WriteLine("Record added successfully.");
        }

        // ============================================================
        // VIEW ALL RECORDS
        // Displays full details for each stored individual
        // ============================================================
        private void ViewAllRecords()
        {
            Console.WriteLine("View All Records");

            if (!persons.Any())
            {
                Console.WriteLine("No records available.");
                return;
            }

            foreach (var p in persons)
                p.DisplayInfo();
        }

        // ============================================================
        // VIEW RECORDS BY ROLE
        // Filters persons list by Teacher / Student / AdminStaff
        // ============================================================
        private void ViewRecordsByRole()
        {
            Console.WriteLine("View Records by Role");
            Console.WriteLine("1. Teacher");
            Console.WriteLine("2. Student");
            Console.WriteLine("3. Admin Staff");
            Console.Write("Select role: ");

            int choice = ReadIntInRange(1, 3);

            string role = choice switch
            {
                1 => "Teacher",
                2 => "Student",
                3 => "AdminStaff",
                _ => ""
            };

            var filtered = persons.Where(p => p.Role == role).ToList();

            if (!filtered.Any())
            {
                Console.WriteLine($"No records for {role}");
                return;
            }

            foreach (var p in filtered)
                p.DisplayInfo();
        }

        // ============================================================
        // EDIT RECORD
        // Allows editing both base fields + subclass-specific fields
        // ============================================================
        private void EditRecord()
        {
            Console.WriteLine("Edit Existing Record");

            if (!persons.Any())
            {
                Console.WriteLine("No records available to edit.");
                return;
            }

            int id = ReadInt("Enter Person ID: ");
            var person = persons.FirstOrDefault(p => p.PersonId == id);

            if (person == null)
            {
                Console.WriteLine("No record found.");
                return;
            }

            person.DisplayInfo();
            Console.WriteLine("Enter new data (blank = keep old value).");

            // Polymorphism: correct EditInfo() executes based on actual object type
            person.EditInfo();

            Console.WriteLine("Record updated (where inputs were valid).");
        }

        // ============================================================
        // DELETE RECORD
        // Confirms deletion before removing object from list
        // ============================================================
        private void DeleteRecord()
        {
            Console.WriteLine("Delete Record");

            if (!persons.Any())
            {
                Console.WriteLine("No records to delete.");
                return;
            }

            int id = ReadInt("Enter Person ID: ");
            var person = persons.FirstOrDefault(p => p.PersonId == id);

            if (person == null)
            {
                Console.WriteLine("No record found.");
                return;
            }

            person.DisplayInfo();
            Console.Write("Confirm delete? (Y/N): ");

            string? confirm = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(confirm) &&
                confirm.Trim().Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                persons.Remove(person);
                Console.WriteLine("Record deleted.");
            }
            else
            {
                Console.WriteLine("Cancelled.");
            }
        }

        // ============================================================
        // HELPER FUNCTIONS: SAFE INPUT + VALIDATION
        // These implement most of the validation rules discussed.
        // ============================================================

        // Integer input, non-negative
        private int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value >= 0)
                {
                    // Extra rule for telephone based on prompt text (7–9 digits)
                    if (prompt.ToLower().Contains("telephone"))
                    {
                        if (value >= 1_000_000 && value <= 999_999_999)
                        {
                            return value;
                        }

                        Console.WriteLine("Telephone must be between 7 and 9 digits.");
                        continue;
                    }

                    return value;
                }

                Console.WriteLine("Invalid number. Please enter a non-negative whole number.");
            }
        }

        // Decimal input, non-negative
        private decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal value) && value >= 0)
                {
                    return value;
                }

                Console.WriteLine("Invalid decimal value. It must be a number and cannot be negative.");
            }
        }

        // String input, non-empty, plus extra rules depending on prompt
        private string ReadNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Value cannot be empty.");
                    continue;
                }

                input = input.Trim();

                // Email validation (format + length)
                if (prompt.ToLower().Contains("email"))
                {
                    if (IsValidEmailFormat(input))
                        return input;

                    Console.WriteLine("Invalid email format. Email must contain '@' and '.' and be reasonably formatted.");
                    continue;
                }

                // Name validation (letters + spaces, length 3–50)
                if (prompt.ToLower().Contains("full name") || prompt.ToLower().Contains("name"))
                {
                    if (input.Length < 3 || input.Length > 50 ||
                        !input.All(c => char.IsLetter(c) || c == ' '))
                    {
                        Console.WriteLine("Invalid name. Only letters and spaces are allowed (3–50 characters).");
                        continue;
                    }
                }

                // Subjects & other fields: basic max length for cleanliness
                if (prompt.ToLower().Contains("subject"))
                {
                    if (input.Length > 30)
                    {
                        Console.WriteLine("Subject name too long (max 30 characters).");
                        continue;
                    }
                }

                if (prompt.ToLower().Contains("employment"))
                {
                    if (input.Length > 30)
                    {
                        Console.WriteLine("Employment type too long (max 30 characters).");
                        continue;
                    }
                }

                return input;
            }
        }

        // Menu / constrained integer
        private int ReadIntInRange(int min, int max)
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int value) && value >= min && value <= max)
                    return value;

                Console.Write($"Enter a number between {min}-{max}: ");
            }
        }

        // Ensures ID uniqueness across all stored records
        private int ReadUniquePersonId()
        {
            while (true)
            {
                int id = ReadInt("Person ID: ");
                if (!persons.Any(p => p.PersonId == id))
                    return id;

                Console.WriteLine("This Person ID already exists. Enter a different ID.");
            }
        }

        // Ensures email valid format and unique across list
        private string ReadUniqueEmail(string prompt)
        {
            while (true)
            {
                string email = ReadNonEmptyString(prompt);

                bool exists = persons.Any(p =>
                    p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (!exists)
                    return email;

                Console.WriteLine("This email is already used by another person. Please enter a different email.");
            }
        }

        // Reusable email format check at application level
        private static bool IsValidEmailFormat(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            email = email.Trim();

            if (email.Length > 50)
                return false;

            int atIndex = email.IndexOf("@", StringComparison.Ordinal);
            int dotIndex = email.LastIndexOf(".", StringComparison.Ordinal);

            return atIndex > 0 &&
                   dotIndex > atIndex + 1 &&
                   dotIndex < email.Length - 1;
        }

        // Normalize employment type, reused in AddNewRecord
        private static string NormalizeEmploymentType(string value)
        {
            if (value.Equals("full-time", StringComparison.OrdinalIgnoreCase) ||
                value.Equals("full time", StringComparison.OrdinalIgnoreCase))
                return "Full-time";

            if (value.Equals("part-time", StringComparison.OrdinalIgnoreCase) ||
                value.Equals("part time", StringComparison.OrdinalIgnoreCase))
                return "Part-time";

            return "";
        }

        // Salary rule for AdminStaff (same as in AdminStaff class)
        private static bool IsValidAdminSalary(string employmentType, decimal salary)
        {
            if (employmentType == "Full-time")
            {
                return salary >= 1000m;
            }

            if (employmentType == "Part-time")
            {
                return salary >= 300m;
            }

            return salary >= 0;
        }

        // Weekly hours rule for AdminStaff
        private static bool IsValidWeeklyHours(string employmentType, int hours)
        {
            if (employmentType == "Full-time")
            {
                return hours >= 35 && hours <= 48;
            }

            if (employmentType == "Part-time")
            {
                return hours >= 1 && hours <= 30;
            }

            return hours >= 1 && hours <= 60;
        }
    }

    // ============================================================
    // PROGRAM ENTRY POINT
    // Creates Application instance and starts the system
    // ============================================================
    class Program
    {
        static void Main()
        {
            Application app = new Application();
            app.Run();   // Start menu loop
        }
    }
}
