Dictionary<string, int> studentGrade = new Dictionary<string, int>();

studentGrade.Add("Lee", 80);
studentGrade.Add("Mark", 30);
studentGrade.Add("Kyle", 0);
studentGrade.Add("Lyle", 100);

// returns grade of entry with name 'Lee'
int student = studentGrade["Lee"];
Console.WriteLine(student);

// Retrieve array position of entry
int arrayLength = 10;
string studentName = "Lee";
int arrayPos = Math.Abs(studentName.GetHashCode()) % arrayLength;
Console.WriteLine(arrayPos);
Console.ReadLine();

