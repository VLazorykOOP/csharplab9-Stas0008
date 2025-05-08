//  За бажанням студента для задач можна створювати консольний проект або WinForm
// Бажано для задач лаб. робіт створити окремі класи
// Виконання  виконати в стилі багатозаданості :
//   Lab9T2  lab9task2 = new Lab9T2; lab9task2.Run();
// При бажанні можна створити багатозадачний режим виконання задач.

using System.Text.RegularExpressions;
using Lab9_10CharpT;
using static System.Console;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

void Task1()
{
    Stack<char> stack = new Stack<char>();

    Console.Write("Input text: ");
    string input = Console.ReadLine();

    foreach (char c in input)
    {
        if (c == '#')
        {
            if (stack.Count > 0)
                stack.Pop();
        }
        else
        {
            stack.Push(c);
        }
    }

    char[] result = stack.ToArray();
    Array.Reverse(result);

    string output = new string(result);

    Console.WriteLine($"Output text: {output}");
}
void Task2()
{
    Queue<Student> excellentStudents = new Queue<Student>();
    Queue<Student> otherStudents = new Queue<Student>();
        
    string filePath = "students.txt";

    try
    {
        foreach (string line in File.ReadLines(filePath))
        {
            string[] parts = line.Split(',');
            if (parts.Length != 6)
            {
                Console.WriteLine($"Invalid line format: {line}");
                continue;
            }

            try
            {
                string surname = parts[0].Trim();
                string name = parts[1].Trim();;
                string group = parts[2].Trim();
                int[] grades = new int[] {
                        int.Parse(parts[3].Trim()),
                        int.Parse(parts[4].Trim()),
                        int.Parse(parts[5].Trim())
                    };

                Student student = new Student(surname, name, group, grades);

                if (student.IsExcellent())
                    excellentStudents.Enqueue(student);
                else
                    otherStudents.Enqueue(student);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error parsing grades in line: {line}. {ex.Message}");
            }
        }

        Console.WriteLine("Excellent Students (Grades 4 or 5):");
        while (excellentStudents.Count > 0)
            Console.WriteLine(excellentStudents.Dequeue());

        Console.WriteLine("\nOther Students:");
        while (otherStudents.Count > 0)
            Console.WriteLine(otherStudents.Dequeue());
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine($"File {filePath} not found.");
    }
    catch (IOException ex)
    {
        Console.WriteLine($"Error reading file: {ex.Message}");
    }
}
void Task3()
{
    Backspace back = new Backspace();

    Console.Write("Input text: ");
    string input = Console.ReadLine();

    back.Process(input);
    Console.WriteLine($"Output: {back.GetResult()}");

    Backspace clone = (Backspace)back.Clone();
    Console.WriteLine($"Clone Output: {clone.GetResult()}");

    //Task 2
    StudentProcessor student = new StudentProcessor();
    string filePath = "students.txt";
    student.ProcessStudents(filePath);

    Console.WriteLine("Students:");
    foreach (Student stud in student)
        Console.WriteLine(stud);

    // Demonstrate cloning
    StudentProcessor Sclone = (StudentProcessor)student.Clone();
    Console.WriteLine("\nCloned Students:");
    foreach (Student stud in Sclone)
        Console.WriteLine(stud);
}
void Task4()
{
    MusicCatalog catalog = new MusicCatalog();

    MusicDisk disk1 = new MusicDisk("Rock Hits");
    MusicDisk disk2 = new MusicDisk("Pop Classics");
    catalog.AddDisk(disk1);
    catalog.AddDisk(disk2);

    catalog.AddSongToDisk("Rock Hits", new Song("Smells Like Teen Spirit", "Nirvana", 301));
    catalog.AddSongToDisk("Rock Hits", new Song("Come As You Are", "Nirvana", 219));
    catalog.AddSongToDisk("Pop Classics", new Song("Summertime Sadness", "Lana Del Rey", 264));
    catalog.AddSongToDisk("Pop Classics", new Song("Young and Beautiful", "Lana Del Rey", 236));

    catalog.DisplayCatalog();

    Console.WriteLine("\nDisplaying Rock Hits:");
    catalog.DisplayDisk("Rock Hits");

    Console.WriteLine("\nSearching for Lana Del Rey:");
    catalog.SearchByArtist("Lana Del Rey");

    catalog.RemoveSongFromDisk("Pop Classics", "Summertime Sadness");
    Console.WriteLine("\nAfter removing Summertime Sadness:");
    catalog.DisplayDisk("Pop Classics");

    catalog.RemoveDisk("Rock Hits");
    Console.WriteLine("\nAfter removing Rock Hits:");
    catalog.DisplayCatalog();
}


Thread thread = new Thread(Task4);
thread.Start();