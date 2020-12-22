using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace task5_1
{
    class Library : IEnumerable
    {
        private Book[] _books;

        public Library(Book[] booksArray)
        {
            _books = new Book[booksArray.Length];
            for (int i = 0; i < booksArray.Length; i++)
                _books[i] = booksArray[i];
        }

        public Library()
        {
            _books = new Book[0];
        }

        // добавить книгу в библиотеку
        public void Add(Book newBook)
        {
            Book[] newBooksArray = new Book[_books.Length + 1];
            for (int i = 0; i < _books.Length; i++)
                newBooksArray[i] = _books[i];
            newBooksArray[_books.Length] = newBook;
            newBooksArray[_books.Length].Status = "В фонде";
            _books = newBooksArray;
        }

        // удалить книгу из библиотеки
        public Book Delete(string bookName)
        {
            Book[] newBooksArray = new Book[_books.Length - 1];

            // определение номера удаляемой книги
            int index = -1;
            for (int i = 0; i < _books.Length; i++)
                if (_books[i].Data == bookName)
                    index = i;
            if (index == -1)
                throw new Exception("Element not found!");

            // извлечение книги из массива книг
            Book removedBook = _books[index];

            for (int i = index; i < _books.Length - 1; i++)
                _books[i] = _books[i + 1];
            for (int i = 0; i < _books.Length - 1; i++)
                newBooksArray[i] = _books[i];

            _books = newBooksArray;

            return removedBook;
        }

        // выдать книгу читателю
        public void Give(string bookName, string userName)
        {
            // определение номера выдаваемой книги
            int index = -1;
            for (int i = 0; i < _books.Length; i++)
                if (_books[i].Data == bookName && _books[i].Status.Substring(0, 6) != "Выдана")
                    index = i;
            if (index == -1)
                throw new Exception("Element not found!");

            DateTime thisDay = DateTime.Today;
            _books[index].Status = $"Выдана: {userName}, Дата выдачи: {thisDay.ToString("d")}";
        }

        // получить текущий статус книги
        public string GetStatus(Book book)
        {
            for (int i = 0; i < _books.Length; i++)
                if (_books[i].Id == book.Id)
                    return book.Status;
            throw new Exception("Book not found!");
        }
        
        // считает количество копий данной книги, находящихся в фонде библиотеки
        public int GetAmountOfCopies(Book book)
        {
            int counter = 0;
            for (int i = 0; i < _books.Length; i++)
                if (_books[i].Data == book.Data && _books[i].Status.Substring(0, 6) != "Выдана")
                    counter++;
            return counter;
        }

        // проверяет есть ли в библиотеке книга с названием таким же, как у книги book
        public bool DoesExist(Book book)
        {
            for (int i = 0; i < _books.Length; i++)
                if (_books[i].Data == book.Data)
                    return true;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LibraryEnum(_books);
        }
    }

    class LibraryEnum : IEnumerator
    {
        private Book[] _books;

        int position = -1;

        public LibraryEnum(Book[] books)
        {
            _books = books;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _books.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Book Current
        {
            get
            {
                try
                {
                    return _books[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

}
