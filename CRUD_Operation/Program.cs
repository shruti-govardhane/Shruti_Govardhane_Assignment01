using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Operation
{
    class TaskInfo
    {
        public string title;
        public string description;

        public TaskInfo(string title, string description)
        {
            this.title = title;
            this.description = description;
        }

    }
    class Program
    {
        static List<TaskInfo> tasks = new List<TaskInfo>();


        public void Create()
        {
            //Enter task name and description
            Console.Write("\nEnter Title Of Task:");
            string title = Console.ReadLine();
            Console.Write("\nEnter Description Of Task:");
            string description = Console.ReadLine();

            TaskInfo task = new TaskInfo(title, description);
            tasks.Add(task);

            Console.WriteLine("\n***Task Succesfully Created***\n");


        }

        public void Read()
        {
            //Display task list
            Console.WriteLine("\n ***Task List is as follow***\n");

            foreach (var s in tasks)
            {
                Console.WriteLine($"\nTitle of task is:--->{s.title} \n\nDescription of task is:--->{s.description}\n");

            }
        }

        public void Update()
        {
            //update title or task description
            Console.Write("\nEnter the title of the task to update: \n");
            string title = Console.ReadLine();
            TaskInfo UpdateT = tasks.Find(t => t.title == title);
            if (UpdateT == null)
            {
                Console.WriteLine("\n**Task not found!**\n");
                return;
            }
            Console.Write("\nEnter new title: \n");
            string newTitle = Console.ReadLine();
            Console.Write("\nEnter new description: \n");
            string newDescription = Console.ReadLine();

            UpdateT.title = newTitle;
            UpdateT.description = newDescription;

            Console.WriteLine("\n***Task updated successfully!*** \n");
        }

        public void Delete()
        {
            //delete task by entering title
            Console.Write("\nEnter the title of the task to delete: \n");
            string title = Console.ReadLine();

            TaskInfo DeleteT = tasks.Find(t => t.title == title);
            if (DeleteT == null)
            {
                Console.WriteLine("\n***Task not found***\n");
                return;
            }

            tasks.Remove(DeleteT);
            Console.WriteLine("\n***Task deleted successfully!***\n");
        }


        static void Main(string[] args)
        {
            Program obj = new Program();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Simple Task List Application");
                Console.WriteLine("1.Create a Task");
                Console.WriteLine("2.Read Task");
                Console.WriteLine("3.Update Task");
                Console.WriteLine("4.Delete Task");
                Console.WriteLine("5.Exit");


                Console.WriteLine("Enter Your Choice");
                int choice = int.Parse(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        obj.Create(); //call
                        break;
                    case 2:
                        obj.Read();
                        break;

                    case 3:
                        obj.Update();
                        break;
                    case 4:
                        obj.Delete();
                        break;

                    case 5:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid Choice!! Please Enter Valid Choice");
                        break;
                }
            }
        }
    }
}
