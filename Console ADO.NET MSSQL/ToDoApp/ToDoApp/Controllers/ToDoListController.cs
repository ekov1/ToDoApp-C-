using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoApp.BLL.Services;
using ToDoApp.Core.Providers.Contracts;
using ToDoApp.DAL.Entities;

namespace ToDoApp.Controllers
{
    public class ToDoListController
    {
        private readonly ToDoListService _toDoListService;
        private readonly UserService _userService;

        public readonly IWriter _writer;
        public readonly IReader _reader;

        public ToDoListController(ToDoListService toDoListService, UserService userService, IWriter writer, IReader reader)
        {
            _toDoListService = toDoListService;
            _userService = userService;
            _writer = writer;
            _reader = reader;
        }

        public void ListToDoLists()
        {
            List<ToDoList> toDoLists = _toDoListService.GetAllToDoListIdsByUser(_userService.CurrentUser.Id);
            if (toDoLists.Any())
            {
                _writer.WriteLine("=======================================================");
                foreach (ToDoList tdl in toDoLists)
                {
                    _writer.WriteLine($"Id : {tdl.Id}");
                    _writer.WriteLine($"Title : {tdl.Title}");
                    _writer.WriteLine("=======================================================");
                }
            }
            else
            {
                _writer.WriteLine("=======================================================");
                _writer.WriteLine("No ToDoList found");
                _writer.WriteLine("=======================================================");
            }
            _writer.WriteLine("");
        }

        public void CreateToDoList()
        {
            _writer.WriteLine("Enter List Title:");
            string title = _reader.ReadLine();

            bool created = _toDoListService.CreateToDolist(_userService.CurrentUser.Id, title);
            if (created)
            {
                _writer.WriteLine($"ToDoList with title '{title}' added");
            }
            else
            {
                _writer.WriteLine($"ToDoList with title '{title}' already exists");
            }
            _writer.WriteLine("");
        }

        public void EditToDoList()
        {
            _writer.WriteLine("Enter Id of ToDoList for Edit");
            string input = Console.ReadLine();
            int listId;
            if (int.TryParse(input, out listId) && _toDoListService.ToDoListWithIdExists(listId)
                && _toDoListService.CanEditList(listId, _userService.CurrentUser.Id))
            {
                ToDoList tdl = _toDoListService.GetToDoListById(listId);
                DisplayListData(tdl);
                _writer.WriteLine("Enter List Title:");
                string title = _reader.ReadLine();
                bool edited = _toDoListService.EditToDoList(listId, title, _userService.CurrentUser.Id);
                if (edited)
                {
                    _writer.WriteLine($"ToDoList with id {listId} edited");
                }
                else
                {
                    _writer.WriteLine("Edit FAIL");
                }
            }
            else
            {
                _writer.WriteLine("Invalid id");
            }
            _writer.WriteLine("");
        }

        public void DeleteToDoList()
        {

            _writer.WriteLine("Enter Id of ToDoList for Delete");
            string input = _reader.ReadLine();
            int listId;
            if (int.TryParse(input, out listId) && _toDoListService.ToDoListWithIdExists(listId) 
                /*&& _toDoListService.CanEditList(listId,_userService.CurrentUser.Id)*/)
            {
                _toDoListService.DeleteToDoList(listId, _userService.CurrentUser.Id);
                _writer.WriteLine("Deleted");
            }
            else
            {
                _writer.WriteLine("Invalid id");
            }
            _writer.WriteLine("");
        }

        private void ShareToDoList()
        {
            Console.WriteLine("Enter Id of ToDoList to Share:");
            string inputListId = Console.ReadLine();
            int listId;

            Console.WriteLine("Enter Id of User you want to Share with:");
            string inputUserId = Console.ReadLine();
            int userId;
            if (int.TryParse(inputListId, out listId) && int.TryParse(inputUserId, out userId)
                && _toDoListService.ToDoListWithIdExists(listId) && _userService.UserWithIdExists(userId))
            {
               // _toDoListService.ShareToDoList(listId, userId);
                Console.WriteLine($"List with id {listId} Shared with user with id {userId}");
            }
            else
            {
                Console.WriteLine("Share FAILED");
            }
            Console.WriteLine();
        }

        private void DisplayListData(ToDoList list)
        {
            _writer.WriteLine($"Id : {list.Id}");
            _writer.WriteLine($"Title : {list.Title}");
            _writer.WriteLine($"IdOfCreator : {list.IdOfCreator} ");
            _writer.WriteLine($"DateCreated : {list.DateCreated}");
            _writer.WriteLine($"LastModifiedBy : {list.LastModifiedBy}");
            _writer.WriteLine($"LastModifiedAt : {list.LastModifiedAt}");
        }
    }


}
