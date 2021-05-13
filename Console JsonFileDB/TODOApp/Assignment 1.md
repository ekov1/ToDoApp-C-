# SFA DotNet - 1 Assignement
## ToDo Application (1 week)

The ToDo application introduces organization in day-to-day tasks. It is a great performance booster for organizations and individuals, working on multiple projects.

## 1. Assignment Goals
1. Development of ToDo application for management of ToDo lists;
2. Development of machine usable CLI tool for access to core system functionality.

## 2. Assignment Description

### 2.1 Required Tasks

**Authentication**

**Story 1.** 
>As a User I need to be able to log into the ToDo application with my username and password. 
If this is the first execution of the application and there are no users in the database 
I need to be able to log-in with the following: 
``` 
User name: "admin"
Password: "adminpassword"
```

**Story 2.** 
>As a user without administrative privileges my access to the Users Management View needs
to be restricted.

**Users Management**

**Story: 3.** 
>As a user with administrative privileges I need to be able to access 
the Users Management View where I can list all users, create, edit and delete a user.


**Story: 4.** 
>As a User with administrative privileges I need to be able to 
create a user and persist the following information:

```
Id
Username
Password
First Name
Last Name
Role (Admin/RegularUser)
Date of creation
Id of the creator
Date of last change
Id of the user that did the last change
```


**Story: 5.**
>As a User with administrative privileges I need to be able to delete a user by Id

**Story: 6.** 
>As a User with administrative privileges I need to be able to select a user by Id and edit the following information:
```
Username
Password
First Name
Last Name

  * Date of creation and Id of the creator remain unchanged
  * Date of last change and Id of the user that did the last change are updated automatically
```

**ToDo Lists Management**

**Story: 7.** 
>As a User I need to be able to access the ToDo List Management View 
where I can access all ToDo lists that are created by me or are shared with me, 
list all ToDo lists, create, edit and delete a ToDo list.

**Story: 8.** 
>As a User I need to be able to create a ToDo list and persist the following information:
```
Id
Title
Date of creation
Id of the creator
Date of last change
Id of the user that did the last change
```
**Story: 9.** 
>As a User I need to be able to delete a ToDo list by Id. 
If the ToDo list is not created by me (is shared with me by another user) 
then the delete action removes the share but does not actually delete the ToDo list.

**Story: 10.** 
>As a User I need to be able to edit a ToDo list by Id by providing the following information:
```
Id
Title

*Date of creation and Id of the creator remain unchanged
*Date of last change and Id of the user that did the last change are updated automatically
```

**Story: 11.**
>As a User I need to be able to share a ToDo list with other users.

**Task Management**

**Story: 12.**
>As a User I need to be able to access the Tasks Management View where 
I can access all Tasks from a single ToDo list that is either created by
me or is shared with me. I should be able to list all Tasks in the ToDo list, create, edit and delete a Task in the ToDo list.

**Story: 13.**
>As a User I need to be able to create a Task in a ToDo list and persist the following information:
```
Id
Id of the List  (the Id of the ToDo list that the Task belongs to)
Title
Description
IsComplete
Date of creation
Id of the creator
Date of last change
Id of the user that did the last change
```
**Story: 14.**
>As a User I need to be able to delete a Task by Id.

**Story: 15.**
>As a User I need to be able to edit a Task by Id by providing the following information:
```
Id
Title
Description
IsComplete

* Date of creation and Id of the creator remain unchanged
* Date of last change and Id of the user that did the last change are updated automatically
```
**Story: 16.**
>As a User I need to be able to assign a Task to one or more Users in the system that have access to the ToDo list that the Task belongs to.

**Story: 17.**
>As a User I need to be able to complete a Task by Id through a fast access menu in the Task Management View.

**Task Management**

**Story: 18.**
>As a User I need to be able to access through the CLI tool all data related to Tasks
from a given ToDo list that is either created by me or is shared with me. 
I should be able to list all Tasks in the ToDo list, create, edit and delete a Task in the ToDo list.

**Story: 19.**
>As a User I need to be able to create a Task in a ToDo list through the CLI tool and persist the following information:
```
Id
Id of the List  (the Id of the ToDo list that the Task belongs to)
Title
Description
IsComplete
Date of creation
Id of the creator
Date of last changeId of the user that did the last change
```
**Story: 20.**
>As a User I need to be able to delete a Task by Id through the CLI tool.

**Story: 21.**
>As a User I need to be able to edit a Task by Id through the CLI tool by providing the following information:
```Id
Title
Description
IsComplete
* Date of creation and Id of the creator remain unchanged
* Date of last change and Id of the user that did the last change are updated automatically
```
**Story: 22.**
>As a User I need to be able to assign a Task through 
the CLI tool to one or more Users in the system that have access to the ToDo list that the Task belongs to.

**Story: 23.**
>As a User I need to be able to complete a Task by Id through the CLI tool using a fast access command argument.

### 2.2. Extra Credit

**User Management CLI tool** 

**Story: 24.**
>As a User I need to be able to access all the features of the ToDo application through the 
CLI tool by providing my username and password as arguments to the CLI tool.

**Story: 25.**
>As a User with administrative privileges I need to be able to access all User related data in the ToDo application through the CLI tool.

**Story: 26.**
>As a User with administrative privileges I need to be able to create a user through the CLI tool and persist the following information:
```
Id
Username
Password
First Name
Last Name
Dateof creation
Id of the creator
Date of last change
Id of the user that did the last change
```
**Story: 27.**
>As a User with administrative privileges I need to be able to delete a user by Id through the CLI tool.

**Story: 28.**
>As a User with administrative privileges I need to be able to edit a user by Id through the CLI tool by providing the following information:
```
Username
Password
First Name
Last Name
Date of creation and Id of the creator remain unchanged
Date of last change and Id of the user that did the last change are updated automatically
```
**ToDo Lists Management CLI tool**

**Story: 29.**
>As a User I need to be able to access through the CLI tool all data related to ToDo lists that are created by me or are shared with me -list all ToDo lists, create, edit and delete a ToDo list.

**Story: 30.**
>As a User I need to be able to create a ToDo list through the CLI tool and persist the following information:IdTitleDate of creationId of the creatorDate of last changeId of the user that did the last change

**Story: 31.**
>As a User I need to be able to delete a ToDo list by Id through the CLI tool. If the ToDo list is not created by me (is shared with me by another user) then the delete action removes the share.

**Story: 32.**
>As a User I need to be able to edit a ToDo list by Id through the CLI tool by providing the following information:
```
Id
Title
Date of creation and Id of the creator remain unchanged
Date of last change and Id of the user that did the last change are updated automatically
```
**Story: 33.**
>As a User I need to be able to share a ToDo list with other users through the CLI tool.


## 3. Assignment Grading
In all the assignments, writing quality code that builds without warnings or errors, and then testing the resulting application and iterating until it functions properly is the goal. Here are the most common reasons assignments receive low points:
- Project does not build.
- One or more items in the Required functionalities section was not satisfied.
- A fundamental concept was not understood.
- Project does not build without warnings.
- Code Quality - Your solution is difficult (or impossible) for someone reading the code to understand due to: 
- Code is visually sloppy and hard to read (e.g. indentation is not consistent, etc.).
- No meaningful variable, method and class names 
- Not following C# code style guides 
- Over/under used methods, classes, variables, data structures or code comments.
- Assignment is not submitted as per Assignment Submission section below.

## 4. Assignment Submission

You already have access to your personal ScaleFocus Academy repositories in GitLab. Every Assignment is submitted in a separate folder in that repo, on your master branch. Every folder is named by the assignment name and number -ex: Final Exam.

Here is a sample structure of how your master branch of your repo should look like towards the end of your SFA training:
```
.
├── Assignment 1
├── Assignment 2  
├── Assignment 3
├── Assignment 4
├── Midterm 1
├── Assignment 5
├── Midterm 2
├── Assignment 6
├──  └── Workforce-management
├──          └── README.md
├──          └── src
├──              └── WorkforceManagement.sln
├──              └── ConsoleApp
├──                  └── ConsoleApp.csproj
├──                  └── Program.cs
├──                  └── ...
├──              └── ClassLibrary
├──                  └── LibraryClass.cs
├──                  └── ...
├──          └── ...

...
```
Assignments that have not been submitted to the master branch or have incorrect folder structure will not be graded.

<small>How to use Git to submit your assignments for review?<small>

> ⚠️ Make sure you have read the GitLab Reading Materials first, available here.

Let's imagine that the first required task from your assignment is to create a Login View in your project:

1. Make sure you have the latest version of your code;

2. Open a bash terminal (CMD for Windows) in your Assignment folder or navigate there with cd Assignment-1/

3. Create a new branch for the feature you're developing git checkout -b login View, where login Views the name of your new branch.

4. Now you need to add all the file you have changed. You can use git add .when you want to add all files in the current folder. Or use git add file.txt you can define specific files you want to push to yor remote repo.

5. Your next step is to commit the changes you have made. You need to use git commit -m "add README" where the message must be meaningful and is describe the exact reason for change;

6. The last step you need to perform is to push your changes to the remote repo by git push -u origin loginView. Pay close attention that master is your main branch and you are not committing to it directly. Pushes are done ONLY against feature branches(branches other than master)!

7. Create a Merge Request and assign your Tutor to it -Open GitLab and navigate to Merge Requests> Create new Merge Request and select your feature branch login Views source and master as target/destination.

8. Your Tutor/Mentor will now review your code. If you have merge request comments, you will need to resolve them before proceeding: 
		
  * Up vote or write something under the comment, acknowledging that you agree with the comment. If there is something you don't understand, now is your time to discuss it by writing under this comment.
  * If everything is clear with the comment, go back to your source code. Make sure you're on your branch, by calling git checkout loginView
  * Do work here that resolves comments
  * Commit as usual(check above).
  * The merge request will be updated with the new code, so your Tutor/Mentor will see your new changes. If there are additional Merge Request comments repeat step 
	
9. When done with all changes you will be allowed to merge your branch with the master branch. Do not forget to mark the branch to be deleted after the merge. Keep in mind that all versions of your code are kept in git and you don't need the branches in your repo.
