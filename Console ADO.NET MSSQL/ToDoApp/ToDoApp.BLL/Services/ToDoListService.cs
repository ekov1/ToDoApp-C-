using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;

namespace ToDoApp.BLL.Services
{
    public class ToDoListService
    {
        private readonly ToDoListRepository _toDoListRepository;

        public ToDoListService(ToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        public bool CreateToDolist(int ownerId, string title)
        {
            return _toDoListRepository.CreateToDoList(ownerId, title);
        }

        public List<ToDoList> GetAllToDoListIdsByUser(int userId)
        {
            return _toDoListRepository.GetAllToDoListIdsByUser(userId);
        }

        public bool ToDoListWithIdExists(int id)
        {
            return _toDoListRepository.ToDoListWithIdExists(id);
        }

        public bool CanEditList(int listId, int userId)
        {
            return _toDoListRepository.CanEditList(listId, userId);
        }

        public ToDoList GetToDoListById(int id)
        {
            return _toDoListRepository.GetToDoListById(id);
        }

        public bool EditToDoList(int listId, string title, int modifiedById)
        {
            return _toDoListRepository.EditToDoList(listId, title, modifiedById);
        }

        public void DeleteToDoList(int listid, int userId)
        {
            _toDoListRepository.DeleteToDoList(listid, userId);
        }
    }
}