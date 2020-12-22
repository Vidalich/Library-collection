using System;
using System.IO;
using System.Collections.Generic;

namespace task5_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Library lib = new Library();
            string[] row;
            try
            {   // считывание данных с файла
                using (var sr = new StreamReader("data.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        row = sr.ReadLine().Split(';');
                        int id = Convert.ToInt32(row[0]);
                        string data = row[1];
                        string genre = row[2];
                        string status = row[3];
                        lib.Add(new Book(id, data, genre, status));
                    }
                }       
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Список книг библиотеки:");
            foreach (Book book in lib)
                Console.WriteLine(book.ToString());

            // Интерфейс приложения
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("Вывести количество экземпляров книг - 1\n" +
                              "Получить статус книг - 2\n" +
                              "Добавить новую книгу - 3\n" +
                              "Выдать книгу читателю - 4\n" +
                              "Удалить книгу - 5\n" +
                              "Подтвердить изменения и выйти - 0");
            
            int choose = Convert.ToInt32(Console.ReadLine());
            while (choose != 0)
            {
                switch (choose)
                {
                    case 1:
                        {
                            Library tmp_lib = new Library();
                            foreach (Book book in lib)
                                if (!tmp_lib.DoesExist(book))
                                {
                                    Console.WriteLine($"{book.Data} - {lib.GetAmountOfCopies(book)} экземпляров");
                                    tmp_lib.Add(book);
                                }
                            
                            break;
                        }
                    case 2:
                        foreach (Book book in lib)
                            Console.WriteLine($"{book.Data} - {book.Status}");
                        break;
                    case 3:
                        {
                            Book new_book = new Book();
                            Random rand = new Random();
                            new_book.Id = rand.Next(1000, 9999);

                            Console.Write("Введите название новой книги: ");
                            new_book.Data = Console.ReadLine();

                            Console.Write("Введите жанр новой книги: ");
                            new_book.Genre = Console.ReadLine();

                            new_book.Status = "В фонде";

                            lib.Add(new_book);

                           break;
                        }
                    case 4:
                        {
                            try
                            {
                                Console.Write("Введите название выдаваемой книги: ");
                                string bookName = Console.ReadLine();
                                Console.Write("Введите фамилию получателя книги: ");
                                string userName = Console.ReadLine();
                                lib.Give(bookName, userName);
                            }
                            catch
                            {
                                Console.WriteLine("Нет такой книги!");
                            }
                            break;
                        }
                    case 5:
                        {
                            try
                            {
                                Console.Write("Введите название удаляемой книги: ");
                                string bookName = Console.ReadLine();
                                lib.Delete(bookName);
                            }
                            catch
                            {
                                Console.WriteLine("Нет такой книги!");
                            }
                            break;
                        }
                }
                choose = Convert.ToInt32(Console.ReadLine());
            }

            // перезапись измененных данных в файл:
            Console.WriteLine("\n\nИзменение данных...");
            using (var sr = new StreamWriter("data.txt", false))
            {
                foreach (Book book in lib)
                {
                    sr.WriteLine(book.ToString());
                }
            }
        }
    }
}
