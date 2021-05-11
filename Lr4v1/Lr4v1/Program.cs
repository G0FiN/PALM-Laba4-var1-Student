using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lr4v1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            Console.Write("Оберіть дію:" +
                "\n1 - Записати нові дані у файли." +
                "\n2 - Вивести збережені дані з файлів." +
                "\nВведіть ваш вибір: ");
            int WriteOfRead = int.Parse(Console.ReadLine());

            if (WriteOfRead == 1)
            {
                students = InputData();
                students.Sort();
                students.Reverse();
                SaveData(students);
                Console.WriteLine("\nДані забережено");
            }
            else if (WriteOfRead == 2)
            {
                Console.Write("Оберіть тип файлу:" +
                "\n1 - TXT." +
                "\n2 - XML." +
                "\nВведіть ваш вибір: ");
                int TXTorXML = int.Parse(Console.ReadLine());

                if (TXTorXML == 1)
                    students = ReadTXTData();
                else if (TXTorXML == 2)
                    students = ReadXMLData();
                students.Sort();
                students.Reverse();
            }

            Console.WriteLine("\nВсього {0} студентів", students.Count);
            OutputData(students);

            Console.WriteLine("\nCтуденти з середнім балом більше 4");
            double[] GPA = CulcGPA(students);
            for (int i = 0; i < GPA.Length; i++)
                if (GPA[i] > 4)
                    Console.WriteLine("\n\tСтудент №{0}\n{1} Cередній бал: {2}",
                    i + 1, students[i].ToString(), GPA[i]);
        }

        //Введення данних про студентів з клавіатури в консоль
        public static List<Student> InputData()
        {
            List<Student> data = new List<Student>();
            string input = "";

            Console.Write("\nВводьте дані за зразком:\n" +
                "\tПІБ\tГрупа\tУспішність\n" +
                "Водянов С. О.; КТ - 20; 1 2 3 4 5;\n\n");

            while (true)
            {
                Console.Write("Введіть дані: ");
                input = Console.ReadLine();
                if (input == "")
                    break;
                data.Add(new Student(input));
            }

            return data;
        }

        //Виведення данних про студентів в консоль
        public static void OutputData(List<Student> students)
        {
            for (int i = 0; i < students.Count; i++)
                Console.WriteLine("\n\tСтудент №{0}\n{1}",
                    i + 1, students[i].ToString());
        }

        //Середній бал кожного студента особисто
        public static double[] CulcGPA(List<Student> studs)
        {
            double[] GPA = new double[studs.Count];
            for (int i = 0; i < GPA.Length; i++)
                GPA[i] =
                    ( CharToDoubleConverter(studs[i].Progres[1])
                    + CharToDoubleConverter(studs[i].Progres[3])
                    + CharToDoubleConverter(studs[i].Progres[5])
                    + CharToDoubleConverter(studs[i].Progres[7])
                    + CharToDoubleConverter(studs[i].Progres[9])) / 5;

            return GPA;
        }

        //Конвертує числа від 0 до 9 з типу char в тип int
        public static double CharToDoubleConverter(char a)
        { double b = a - '0'; return b; }

        //Збереження даних в ТХТ на ХМL файли
        public static void SaveData(List<Student> students)
        {
            using (StreamWriter save = new StreamWriter("Students_TXT.txt"))
            {
                foreach (Student data in students)
                    save.WriteLine(data);
            }

            XmlSerializer formatter = new XmlSerializer(typeof(List<Student>));

            using FileStream fs = new FileStream("Students_XML.xml", FileMode.Create);
            formatter.Serialize(fs, students);
        }

        //Читання ТХТ файлу 
        public static List<Student> ReadTXTData()
        {
            List<Student> students = new List<Student>();
            string[] dataFromFile = File.ReadAllLines("Students_TXT.txt");
            for (int i = 0; i < dataFromFile.Length; i++)
                students.Add(new Student(dataFromFile[i]));

            return students;
        }

        //Читання XML файлу 
        public static List<Student> ReadXMLData()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Student>));
            List<Student> students = new List<Student>();

            using (FileStream fs = new FileStream("Students_XML.xml", FileMode.OpenOrCreate))
            {
                students = (List<Student>)formatter.Deserialize(fs);
            }

            return students;
        }
    }
}
