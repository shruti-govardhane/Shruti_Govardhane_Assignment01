using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InventoryManagementSystem
{

    //Description Mention in comments

    //OOPs Concepts:

    //Inheritance :=  Super Class ItemSuper and Child Class Item,Item inheriting property of ItemSuper Class

    //Absraction := Item and ItemSuper classes abstract the concept of an inventory item.
    
    //Encapsulation :=The properties (Id, Name, Price, Quantity) of the Item and ItemSuper classes are encapsulated within those classes
    class ItemSuper
    {
        public int Id { get; }
        public string Name { get; set; }
        public double Price { get; set; }



        //Parameterized Constructor
        public ItemSuper(int Id, string Name, double Price)
        {
            this.Id=Id; //As Here using same variable Id ,name, thats why to point those varibale this keyword is use
            this.Name =Name;
            this.Price = Price;
        }
    }

    class Item : ItemSuper //inheritance
    {
        public int Quantity { get; set; }

        public Item(int Id, string Name, double Price, int Quantity) : base(Id, Name, Price)//calling constructor of base class
        //base keyword is used to access members of the base class
        {
            this.Quantity = Quantity;
        }
    }

    class Inventory
    {
        private List<Item> items = new List<Item>();  //List to store items


        //// Method to add items to the inventory
        public void AddItems(Item item)
        {
            items.Add(item);
        }

        // Method to find an item by its ID
        public Item FindItem(int Id)
        {
            return items.Find(item => item.Id == Id);

            //Find is the method use to find element in the list
            //item => item.Id == Id: This is a lambda expression passed as an argument to the Find method.
           // item: This represents each element in the items
           // It comparing the Id property of each item in the list with the Id passed as argument.
        }

        // Method to update item information Here option are provided to update Name,Price,Quantity or 
        //Both name and price or name and quantity or all three by using swtich case 
        public void UpdateItem(int Id) //here passing argument id
        {
            Item item = FindItem(Id);
            if (item != null)
            {

                //choices
                Console.WriteLine("Select what you want to update:");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. Price");
                Console.WriteLine("3. Quantity");
                Console.WriteLine("4. Name and Price");
                Console.WriteLine("5. Name and Quantity");
                Console.WriteLine("6. Price and Quantity");
                Console.WriteLine("7. All (Name, Price, and Quantity)\n");
                Console.WriteLine("\nEnter Your Choice:\n");


              
                if (int.TryParse(Console.ReadLine(), out int ch))
                    //int.TryParse(): This is a method which convert the input string to an integer
                   // out int ch: This is output parameter for result
                {
                    switch (ch)
                    {
                        case 1:
                            Console.WriteLine("Enter New Name:");
                            item.Name = Console.ReadLine();
                            Console.WriteLine("Name Updated");
                            break;
                        case 2:
                            Console.WriteLine("Enter new Price:");
                            //double.TryParse()  convert the input string to a double
                            if (double.TryParse(Console.ReadLine(), out double newPrice) && newPrice >= 0)

                            //newprice >=0 This is a condition checking if the newPrice is greater than 0 or equal.for validation
                            {
                                item.Price = newPrice;
                                Console.WriteLine("Price Updated");
                            }
                            else
                            {
                                Console.WriteLine("Please Enter a Valid Price");
                            }
                            break;
                        case 3:
                            Console.WriteLine("Enter new Quantity:");
                            if (int.TryParse(Console.ReadLine(), out int newQuantity) && newQuantity >= 0)
                            {
                                item.Quantity = newQuantity;
                                Console.WriteLine("Quantity Updated");
                            }
                            else
                            {
                                Console.WriteLine("Please Enter a Valid Quantity");
                            }
                            break;
                        case 4:
                            Console.WriteLine("Enter New Name:");
                            item.Name = Console.ReadLine();
                            Console.WriteLine("Enter new Price:");
                            if (double.TryParse(Console.ReadLine(), out double newPrice4) && newPrice4 >= 0)
                            {
                                item.Price = newPrice4;
                                Console.WriteLine("Name and Price Updated");
                            }
                            else
                            {
                                Console.WriteLine("Please Enter a Valid Price");
                            }
                            break;
                        case 5:
                            Console.WriteLine("Enter New Name:");
                            item.Name = Console.ReadLine();
                            Console.WriteLine("Enter new Quantity:");
                            if (int.TryParse(Console.ReadLine(), out int newQuantity5) && newQuantity5 >= 0)
                            {
                                item.Quantity = newQuantity5;
                                Console.WriteLine("Name and Quantity Updated");
                            }
                            else
                            {
                                Console.WriteLine("Please Enter a Valid Quantity");
                            }
                            break;
                        case 6:
                            Console.WriteLine("Enter new Price:");
                            if (double.TryParse(Console.ReadLine(), out double newPrice6) && newPrice6 >= 0)
                            {
                                item.Price = newPrice6;
                                Console.WriteLine("Enter new Quantity:");
                                if (int.TryParse(Console.ReadLine(), out int newQuantity6) && newQuantity6 >= 0)
                                {
                                    item.Quantity = newQuantity6;
                                    Console.WriteLine("Price and Quantity Updated");
                                }
                                else
                                {
                                    Console.WriteLine("Please Enter a Valid Quantity");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please Enter a Valid Price");
                            }
                            break;
                        case 7:
                            Console.WriteLine("Enter New Name:");
                            item.Name = Console.ReadLine();
                            Console.WriteLine("Enter new Price:");
                            if (double.TryParse(Console.ReadLine(), out double newPrice7) && newPrice7 >= 0)
                            {
                                item.Price = newPrice7;
                                Console.WriteLine("Enter new Quantity:");
                                if (int.TryParse(Console.ReadLine(), out int newQuantity7) && newQuantity7 >= 0)
                                {
                                    item.Quantity = newQuantity7;
                                    Console.WriteLine("Name, Price, and Quantity Updated");
                                }
                                else
                                {
                                    Console.WriteLine("Please Enter a Valid Quantity");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please Enter a Valid Price");
                            }
                            break;
                        default:
                            Console.WriteLine("Enter Valid Choice");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }
            else
            {
                Console.WriteLine("Item Not Found");
            }
        }

        // Method to display all items in the inventory
        public void Display()
        {
            foreach (Item item in items)//for each loop for iterating through list 
            {
                Console.WriteLine($"ID:{item.Id},Name : {item.Name},Price : {item.Price},Quantity:{item.Quantity}");
            }
        }
        // Method to delete an item from the inventory
        public void Delete(int id)
        {
            Item item = FindItem(id);
            if (item !=null)//if present 
            {
                items.Remove(item);
                Console.WriteLine("Item Deleted");

            }
            else
            {
                Console.WriteLine("Item not found");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Object Creation
            Inventory obj = new Inventory();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n***** Inventory Management System *****\n");
                Console.WriteLine(" 1. Add new Item");
                Console.WriteLine(" 2. Display Item");
                Console.WriteLine(" 3. Find item by Providing Id");
                Console.WriteLine(" 4. Update item information");
                Console.WriteLine(" 5. Delete Item");
                Console.WriteLine("6. Exit");
                Console.Write("\nEnter your choice:\n");
                // Input validation and performing actions based on user choice
                if (int.TryParse(Console.ReadLine(), out int ch))
                {
                    switch (ch)
                    {
                        case 1:
                            // Logic  add a new item
                            Console.Write("Enter Id of item:");
                            if (int.TryParse(Console.ReadLine(), out int Id))
                            {
                                Console.Write("Enter item name: ");
                                string Name = Console.ReadLine();
                                Console.Write("Enter item price: ");
                                if (double.TryParse(Console.ReadLine(), out double Price) && Price >= 0)
                                {
                                    Console.Write("Enter item quantity: ");
                                    if (int.TryParse(Console.ReadLine(), out int Quantity) && Quantity >= 0)
                                    {
                                        obj.AddItems(new Item(Id, Name, Price, Quantity));
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid quantity input");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid price input");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID input");
                            }
                            break;
                        case 2:
                            obj.Display();
                            break;
                        case 3:
                            // Logic to find an item by ID
                            Console.Write("Enter item ID to find: ");
                            if (int.TryParse(Console.ReadLine(), out int searchId))
                            {
                                Item foundItem = obj.FindItem(searchId);
                                if (foundItem != null) // If item present 
                                {
                                    Console.WriteLine($"Item found - ID: {foundItem.Id}, Name: {foundItem.Name}, Price: {foundItem.Price}, Quantity: {foundItem.Quantity}");
                                }
                                else
                                {
                                    Console.WriteLine("Item not found!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID input");
                            }
                            break;
                        case 4:

                            Console.Write("Enter item ID to update: ");
                            if (int.TryParse(Console.ReadLine(), out int updateId))
                            {
                                obj.UpdateItem(updateId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID input");
                            }
                            break;
                        case 5:
                            Console.Write("Enter item ID to delete: ");
                            if (int.TryParse(Console.ReadLine(), out int deleteId))
                            {
                                obj.Delete(deleteId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID input");
                            }
                            break;
                        case 6:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice! Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice! Please try again.");
                }
            }
        }

                
    }

   

}
