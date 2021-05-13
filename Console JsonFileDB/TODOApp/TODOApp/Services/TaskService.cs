using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TODOApp.Data;
using TODOApp.Entities;

namespace TODOApp.Services
{
    public class TaskService
    {
        private const string StoreFileName = "Tasks.json";

        private readonly FileStorage _fileStorage;

        private readonly List<Task> _applicationTasks = new List<Task>();

        public TaskService()
        {
            _fileStorage = new FileStorage();
            List<Task> tasksFromFile = _fileStorage.Read<List<Task>>(StoreFileName);
            if (tasksFromFile != null)
            {
                _applicationTasks = tasksFromFile;
            }
        }

        public bool CreateTask(string title, string description, int listId, int creatorId)
        {
            if (_applicationTasks.Any(t => t.IdOfCreator == creatorId && t.ListId == listId && t.Title == title))
            {
                return false;
            }

            int nextId;

            if (_applicationTasks.Count == 0)
            {
                nextId = 1;
            }
            else
            {
                nextId = _applicationTasks.Last().Id + 1;
            }

            Task newTask = new Task()
            {
                Id = nextId,
                ListId = listId,
                Title = title,
                Description = description,
                IsComplete = false,
                DateCreated = DateTime.Now,
                IdOfCreator = creatorId,
                LastModifiedAt = DateTime.Now,
                LastModifiedBy = creatorId
            };

            _applicationTasks.Add(newTask);
            SaveToFile();
            return true;
        }

        public List<int> GetTaskIdsByListId(int listId)
        {
            return _applicationTasks.Where(t => t.ListId == listId).Select(t => t.Id).ToList();
        }

        public void DisplayTaskData(int id)
        {
            Task task = _applicationTasks.FirstOrDefault(t => t.Id == id);
            Console.WriteLine($"Id : {task.Id}");
            Console.WriteLine($"Title : {task.Title}");
            Console.WriteLine($"Description : {task.Description}");
            Console.WriteLine($"IsComplete : {task.IsComplete}");
            Console.WriteLine($"Assigned to users with id : {String.Join(" ", task.AssignedTo)}");
        }

        public bool EditTask(string title, string description, bool isComplete, int taskid, int modifiedById)
        {
            if (_applicationTasks.Any(t => t.Title == title && t.Id != taskid))
            {
                Console.WriteLine($"Task with title {title} already exist!");
                return false;
            }

            Task task = _applicationTasks.FirstOrDefault(t => t.Id == taskid);
            task.Title = title;
            task.Description = description;
            task.IsComplete = isComplete;
            task.LastModifiedAt = DateTime.Now;
            task.LastModifiedBy = modifiedById;

            SaveToFile();

            return true;
        }

        public void DeleteTask(int id)
        {
            _applicationTasks.RemoveAll(t => t.Id == id);
            SaveToFile();
        }

        public bool TaskWithIdExists(int id)
        {
            if (!_applicationTasks.Any(t => t.Id == id))
            {
                Console.WriteLine($"Task with Id {id} does not exist.");
                return false;
            }
            return true;
        }

        public void AssignTask(int taskId, int userId, int modifiedById)
        {
            Task task = _applicationTasks.FirstOrDefault(t => t.Id == taskId);
            task.AssignedTo.Add(userId);
            task.LastModifiedAt = DateTime.Now;
            task.LastModifiedBy = modifiedById;
            SaveToFile();
        }

        public void CompleteTask(int taskId, int modifiedById)
        {
            Task task = _applicationTasks.FirstOrDefault(t => t.Id == taskId);
            task.IsComplete = true;
            task.LastModifiedAt = DateTime.Now;
            task.LastModifiedBy = modifiedById;
            SaveToFile();
        }

        private void SaveToFile()
        {
            _fileStorage.Write(StoreFileName, _applicationTasks);
        }
    }
}
