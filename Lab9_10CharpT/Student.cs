using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_10CharpT
{
    internal class Student : IComparable, ICloneable
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public int[] Grades { get; set; }

        public Student(string surname, string name, string group, int[] grades)
        {
            Surname = surname;
            Name = name;
            Group = group;
            Grades = grades;
        }

        public bool IsExcellent()
        {
            return Grades.All(g => g >= 4);
        }

        public override string ToString()
        {
            return $"{Surname} {Name}, Group: {Group}, Grades: {string.Join(", ", Grades)}";
        }

        public int CompareTo(object obj)
        {
            Student other = obj as Student;
            if (other == null) return 1;
            return string.Compare(Surname, other.Surname, StringComparison.Ordinal);
        }

        public object Clone()
        {
            return new Student(Surname, Name, Group, (int[])Grades.Clone());
        }
    }
    class StudentProcessor : ArrayList, IEnumerable, ICloneable
    {
        public void ProcessStudents(string filePath)
        {
            ArrayList excellent = new ArrayList();
            ArrayList others = new ArrayList();

            try
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length != 6) continue;

                    string surname = parts[0].Trim();
                    string name = parts[1].Trim();
                    string group = parts[2].Trim();
                    int[] grades = new int[] { int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]) };

                    Student student = new Student(surname, name, group, grades);

                    if (student.IsExcellent())
                        excellent.Add(student);
                    else
                        others.Add(student);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }

            Clear();
            AddRange(excellent);
            AddRange(others);
        }

        public object Clone()
        {
            StudentProcessor clone = new StudentProcessor();
            foreach (Student student in this)
                clone.Add(student.Clone());
            return clone;
        }
    }
}
