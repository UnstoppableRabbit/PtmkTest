using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PTMKapp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string k = Console.ReadLine();

                switch(k)
                {
                    case "myApp1":
                        myApp1();
                        break;
                    case "myApp2":
                        myApp2();
                        break;
                    case "myApp3":
                        myApp3();
                        break;
                    case "myApp4":
                        myApp4();
                        break;
                    case "myApp5":
                        myApp5();
                        break;
                    default:
                        Console.WriteLine("myApp1 - создать таблицу, myApp2 - добавить строку, myApp3 - выбрать все уникальных строки, myApp4 - заполнить таблицу случайными данными, myApp5 - выборка по полу и имени");
                        break;
                }
            }
        }
        static void myApp1()
        {
            SqlConnection conn = new SqlConnection(
            "Server=(localdb)\\mssqllocaldb;Database=ptmk;Trusted_Connection=True;");
            string sql = @"create table Person(
                            create table Person(
                            FIO nvarchar(150) not null,
                            BirthD Date not null,
                            Gender nvarchar(1) not null)";
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Table has been added");
            }
            catch (Exception)
            {
                Console.WriteLine("Something is going wrong");
            }
            conn.Close();
        }
        static void myApp2()
        {
            Console.WriteLine("Введите поочередно ФИО, дату рожденья( формат даты YYYY-MM-DD(Пример: 2001-05-13)) и ваш пол(формат пола - буква М или Ж)");
            string fio = Console.ReadLine();
            string BirthD = Console.ReadLine();
            string Gender = Console.ReadLine();

            SqlConnection conn = new SqlConnection(
            "Server=(localdb)\\mssqllocaldb;Database=ptmk;Trusted_Connection=True;");

            string sql = $@"INSERT INTO Person(FIO, BirthD, Gender) values(N'{fio}', '{BirthD}',N'{Gender}')";
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Person has been added");
            }
            catch (Exception)
            {
                Console.WriteLine("Something is going wrong");
            }
            conn.Close();
        }
        static void myApp3()
        {
            string sql = @"select distinct FIO, BirthD, Gender, (select FLOOR(datediff(month, BirthD, GETDATE()) / 12)) as 'полных лет'
                            from Person
                            order by FIO";

            SqlConnection conn = new SqlConnection(
            "Server=(localdb)\\mssqllocaldb;Database=ptmk;Trusted_Connection=True;");
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);

            SqlDataReader stringReader = command.ExecuteReader();
            List<Person> pl = new List<Person>();

            while (stringReader.Read())
            {
                pl.Add(new Person(String.Format("{0}", stringReader[0]),
                    String.Format("{0}", stringReader[1]), String.Format("{0}", stringReader[2]),
                    String.Format("{0}", stringReader[3])));
            }
            conn.Close();
            foreach (var ps in pl)
            {
                Console.WriteLine($"{ps.FIO}\t{ps.BirthD}\t{ps.Gender}\t{ps.Years}");
            }

        }
        static void myApp4()
        {
            string sql = "";
            List<Person> pl = new List<Person>();
            for(int i=0; i<100; i++)
            {
                string randomWord = "F";
                Random random = new Random();
                //A-Z (65-90) a-z(97-122) 0-9(48-57)
                for (int j = 0; j < 25; j++)
                {
                        randomWord += (char)random.Next(97, 122);
                }
                string gen = (random.Next(0, 2)==1) ? "М" : "Ж";
                string dt = $"{random.Next(1950,2020)}-{random.Next(1,12)}-{random.Next(1, 28)}";
                pl.Add(new Person(randomWord, dt, gen));
                
            }
            foreach (var person in pl)
            {
                sql += $"Insert into Person(FIO, BirthD, Gender) values(N'{person.FIO}', '{person.BirthD.ToString("yyyy-MM-dd")}',N'{person.Gender}');";
            }

            SqlConnection conn = new SqlConnection(
            "Server=(localdb)\\mssqllocaldb;Database=ptmk;Trusted_Connection=True;");
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Persons was added");
            }
            catch (Exception)
            {
                Console.WriteLine("Something is going wrong");
            }
            conn.Close();
        }
        static void myApp5()
        {
            Stopwatch sw = new Stopwatch();
            string sql = @"select FIO, BirthD, Gender
                                from Person 
                                WHERE FIO LIKE 'F%' AND Gender = N'М'";

            SqlConnection conn = new SqlConnection(
            "Server=(localdb)\\mssqllocaldb;Database=ptmk;Trusted_Connection=True;");
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);

            sw.Start();
            SqlDataReader stringReader = command.ExecuteReader();
            List<Person> pl = new List<Person>();
            while (stringReader.Read())
            {
                pl.Add(new Person(String.Format("{0}", stringReader[0]),
                    String.Format("{0}", stringReader[1]), String.Format("{0}", stringReader[2])));
            }
            sw.Stop();
            conn.Close();
            foreach (var ps in pl)
            {
                Console.WriteLine($"{ps.FIO}\t{ps.BirthD}\t{ps.Gender}\t{ps.Years}");
            }
            Console.WriteLine(sw.ElapsedMilliseconds + " миллисекунд");
        }
    }
}
