﻿using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using BusinessLogic.Contracts.Models;
using BusinessLogic.Contracts.Services;
using DataAccess.Contracts.Models;
using DataAccess.Contracts.Services;

namespace BusinessLogic.Services
{
    public class BookService: IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IBookMapper bookMapper;

        public BookService(IBookRepository bookRepository, IBookMapper bookMapper)
        {
            this.bookRepository = bookRepository;
            this.bookMapper = bookMapper;
        }

        public BookModel GetById(int id)
        {
            var book = bookRepository.GetById(id);
            return bookMapper.Map(book);
        }

        public BookModel[] GetAll()
        {
            BookDto[] books = bookRepository.GetAll();
            List<BookModel> result = new List<BookModel>();
            books.ToList().ForEach(x => result.Add(bookMapper.Map(x)));
            return result.ToArray();
        }

        public bool Add(BookModel book)
        {
            var bookDto = bookMapper.Map(book);
            return bookRepository.Add(bookDto);
        }

        public void Update(BookModel book)
        {
            var bookDto = bookMapper.Map(book);
            bookRepository.Update(bookDto);
        }

        public void Delete(int id)
        {
            bookRepository.Delete(id);
        }

        public int GetAgeOfBook(int id)
        {
            var book = bookRepository.GetById(id);
            return DateTime.Now.Year - book.YearOfPublishing;
        }
    }
}
