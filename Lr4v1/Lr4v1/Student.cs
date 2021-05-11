using System;

namespace Lr4v1
{
    [Serializable]
    public struct Student : IComparable<Student>
    {
        public string SurnameAndInitials;
        public string GroupName;
        public string Progres;

        public Student(string LineWithStudentData)
        {
            string[] dataSplitted = LineWithStudentData.Split(';');

            SurnameAndInitials = dataSplitted[0];
            GroupName = dataSplitted[1];
            Progres = dataSplitted[2];
        }

        public int CompareTo(Student other)
        {
            SearchNumInStr(GroupName);

            double a = SearchNumInStr(this.GroupName);
            double b = SearchNumInStr(other.GroupName);

            int name = this.GroupName.Split('-')[0].CompareTo(other.GroupName.Split('-')[0]);

            if (a > b)
                return 1;
            if (a < b)
                return -1;
            else
            {
                if (name > 0)
                    return 1;
                if (name < 0)
                    return -1;
                else
                    return 0;
            }
        }

        public double SearchNumInStr(string GroupName)
        {
            string b1 = "";
            double num = 0;

            for (int i = 0; i < GroupName.Length; i++)
                if (Char.IsDigit(GroupName[i]))
                    b1 += GroupName[i];

            if (b1.Length > 0)
            {
                if (b1.Length > 2)
                {
                    b1 = b1.Remove(2);
                    num = double.Parse(b1) + 0.1;
                }
                else
                    num = double.Parse(b1);
            }

            return num;
        }

        public override string ToString()
        { return SurnameAndInitials + ";" + GroupName + ";" + Progres + ";"; }
    }
}
