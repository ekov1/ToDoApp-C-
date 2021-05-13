using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TODOApp.Data;
using TODOApp.Entities;

namespace TODOApp.Services
{
    public class ToDoListService
    {
        private const string StoreFileName = "ToDoLists.json";

        private readonly FileStorage _fileStorage;

        private readonly List<ToDoList> _applicationToDoLists = new List<ToDoList>();

        public ToDoListService()
        {
            _fileStorage = new FileStorage();
            List<ToDoList> toDoListsFromFile = _fileStorage.Read<List<ToDoList>>(StoreFileName);
            if (toDoListsFromFile != null)
            {
                _applicationToDoLists = toDoListsFromFile;
            }
        }

        public bool CreateToDolist(string title, int ownerId)
        {
            if (_applicationToDoLists.Any(l => l.Title == title))
            {
                return false;
            }

            int nextId;

            if (_applicationToDoLists.Count == 0)
            {
                nextId = 1;
            }
            else
            {
                nextId = _applicationToDoLists.Last().Id + 1;
            }

            ToDoList newToDoList = new ToDoList()
            {
                Id = nextId,
                Title = title,
                DateCreated = DateTime.Now,
                IdOfCreator = ownerId,
                LastModifiedAt = DateTime.Now,
                LastModifiedBy = ownerId
            };

            _applicationToDoLists.Add(newToDoList);
            SaveToFile();

            return true;
        }

        public List<int> GetAllToDoListIdsByUser(int userId)
        {
            return _applicationToDoLists
                .Where(l => l.IdOfCreator == userId || l.SharedWith.Contains(userId))
                .Select(l => l.Id)
                .ToList();
        }

        public void DisplayListData(int id)
        {
            ToDoList tdl = _applicationToDoLists.FirstOrDefault(l => l.Id == id);
            Console.WriteLine($"Id : {tdl.Id}");
            Console.WriteLine($"Title : {tdl.Title}");
        }

        public bool EditToDoList(int id, string title, int modifiedById)
        {
            if (_applicationToDoLists.Any(l => l.Title == title && l.Id != id))
            {
                Console.WriteLine($"List with title {title} already exist!");
                return false;
            }

            ToDoList tdl = _applicationToDoLists.FirstOrDefault(l => l.Id == id);
            tdl.Title = title;
            tdl.LastModifiedAt = DateTime.Now;
            tdl.LastModifiedBy = modifiedById;

            SaveToFile();

            return true;
        }

        public void DeleteToDoList(int listid, int userId)
        {
            _applicationToDoLists.RemoveAll(l => l.Id == listid && l.IdOfCreator == userId);
            _applicationToDoLists.FirstOrDefault(l => l.Id == listid).SharedWith.RemoveAll(id => id == userId);
            SaveToFile();
        }

        public bool ToDoListWithIdExists(int id)
        {
            if (!_applicationToDoLists.Any(u => u.Id == id))
            {
                Console.WriteLine($"ToDoList with Id {id} does not exist.");
                return false;
            }
            return true;
        }

        public void ShareToDoList(int listId, int userId)
        {
            _applicationToDoLists.FirstOrDefault(l => l.Id == listId).SharedWith.Add(userId);
            SaveToFile();
        }

        public bool CanEditList(int listId, int userId)
        {
            if (_applicationToDoLists.Any(l=>l.IdOfCreator == userId && l.Id == listId))
            {
                return true;
            }
            Console.WriteLine("You dont own this list!");
            return false;
        }

        public bool CanViewList(int listId, int userId)
        {
            if (_applicationToDoLists.Any(l => l.IdOfCreator == userId && l.Id == listId))
            {
                return true;
            }
            Console.WriteLine("You dont own this list!");
            return false;
        }

        public bool IsListSharedWithUser(int listId, int userId)
        {
            if (_applicationToDoLists.FirstOrDefault(l=>l.Id==listId).SharedWith.Contains(userId))
            {
                return true;
            }
            Console.WriteLine($"List with id {listId} not shared with user with {userId}");
            return false;
        }

        private void SaveToFile()
        {
            _fileStorage.Write(StoreFileName, _applicationToDoLists);
        }
    }
}
