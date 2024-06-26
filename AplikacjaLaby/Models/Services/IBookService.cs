﻿using AplikacjaLaby.Classes;
using AplikacjaLabyData.Entities;

namespace AplikacjaLaby.Models.Services
{
    public interface IBookService
    {
        int Add(Book item);

        void Delete(int id);

        void Update(Book item);

        List<Book> GetAll();

        Book? GetById(int id);

        List<OwnerEntity> GetAllOwners();

        public PagingList<Book> FindPage(int page, int size);
    }
}
