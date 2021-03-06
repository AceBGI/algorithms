﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw5
{
    class Student
    {
        public string Name { get; set; }
        public int Day { get; set; } = Int32.MaxValue;
    }

    class Program
    {
        public static Dictionary<string, Student> Students = new Dictionary<string, Student>();
        public static Dictionary<string, LinkedList<Student>> Friends = new Dictionary<string, LinkedList<Student>>();

        static void Main(string[] args)
        {
            // cities and tolls / vertex
            string studentNumber = Console.ReadLine();
            int n;
            Int32.TryParse(studentNumber, out n);

            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();
                Student newStudent = new Student();
                newStudent.Name = line;
                Students.Add(line, newStudent);
                Friends.Add(line, new LinkedList<Student>());
            }

            string numberOffriends = Console.ReadLine();
            int f;
            Int32.TryParse(numberOffriends, out f);

            for (int i = 0; i < f; i++)
            {
                string line = Console.ReadLine();
                string[] sList = line.Split(new[] { '\t', ' ' });

                Friends[sList[0]].AddLast(Students[sList[1]]);
                Friends[sList[1]].AddLast(Students[sList[0]]);
            }

            string reportNumber = Console.ReadLine();
            int r;
            Int32.TryParse(reportNumber, out r);

            List<string> reports = new List<string>();
            for (int i = 0; i < r; i++)
            {
                string line = Console.ReadLine();
                List<SortedSet<string>> report = Report(line);
                StringBuilder outputLine = new StringBuilder();
                foreach (SortedSet<string> list in report)
                {
                    foreach (string item in list)
                    {
                        outputLine.Append(item);
                        outputLine.Append(" ");
                    }
                }
                reports.Add(outputLine.ToString());
            }

            //Console.WriteLine();
            foreach (string item in reports)
            {
                Console.WriteLine(item);
            }
            //Console.ReadLine();
        }

        private static List<SortedSet<string>> Report(string name)
        {
 
            int inf = Int32.MaxValue;
            int countOfDays = 0;
            foreach (Student s in Students.Values)
            {
                s.Day = inf;
            }

            Queue<Student> Q = new Queue<Student>();
            Student startPoint = Students[name];
            startPoint.Day = 0;
            Q.Enqueue(startPoint);

            while (Q.Count != 0)
            {
                Student u = Q.Dequeue();
               
                foreach (Student edge in Friends[u.Name])
                {
                    if (edge.Day == inf)
                    {
                        edge.Day = u.Day + 1;
                        Q.Enqueue(edge);
                    }
                }
                countOfDays++;
            }

            // create days list
            List<SortedSet<string>> days = new List<SortedSet<string>>();
            for (int i = 0; i < countOfDays + 1; i++)
            {
                days.Add(new SortedSet<string>());
            }

            // go through all of the students and them to the correct day list.
            foreach (Student student in Students.Values)
            {
                bool flag = false;
                for (int i = 0; i < countOfDays; i++)
                {
                    if (student.Day == inf)
                    {
                        days[countOfDays].Add(student.Name);
                        flag = true;
                        break;
                    }
                    else if (student.Day == i)
                    {
                        days[i].Add(student.Name);
                        flag = true;
                        break;
                    }
                }
                if (flag) continue;
            }
            

            return days;
        }
    }
}
