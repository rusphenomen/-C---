using MyCircularList;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THIRDLA3
{
    public static class MethodsTest<T>
    { 

        public static void BasicMethodsTest(IList<T> list)
        {

            bool isChoise = true;

            Console.WriteLine("Select an operation to perform:");
            Console.WriteLine("1. Add Element");
            Console.WriteLine("2. Print Elements");
            Console.WriteLine("3. Remove Element");
            Console.WriteLine("4. Print SubList");
            Console.WriteLine("5. Clear List");
            Console.WriteLine("6. Check if Contains Element");
            Console.WriteLine("7. Get Index of Element");
            Console.WriteLine("8. Insert Element");
            Console.WriteLine("9. Remove Element at Index");
            Console.WriteLine("10. Exit");


            while (isChoise)
            {
                Console.WriteLine("Enter your choice (1-10): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter element to add: ");
                        T elementToAdd = GetUserInput<T>();
                            list.Add(elementToAdd);
                            Console.WriteLine("Element added.");
                        break;

                    case "2":
                        Console.Write("Printing elements: ");
                        list.Print();
                        break;

                    case "3":

                        Console.Write("Enter element to remove: ");
                        T elemToRemove = GetUserInput<T>();
                            try
                            {
                            list.Remove(elemToRemove);
                                    Console.WriteLine("Element removed.");
                                
                            }
                            catch (ListIsEmpty ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        break;

                    case "4":
                        Console.Write("Enter start index of SubList: ");
                        if (int.TryParse(Console.ReadLine(), out int startIndex))
                        {
                            Console.Write("Enter end index of SubList: ");
                            if (int.TryParse(Console.ReadLine(), out int endIndex))
                            {
                                try
                                {
                                    IList<T> subList = list.SubList(startIndex, endIndex);
                                    Console.WriteLine("SubList:");
                                    subList.Print();
                                    Console.WriteLine();
                                }
                                catch (UnableToCreateSubList2 ex)
                                {
                                    Console.WriteLine(ex.Message + " " + ex.index + " " + ex.index2);
                                }
                                catch (UnableToCreateSubList ex)
                                {
                                    Console.WriteLine(ex.Message + " " + ex.index);
                                }
                                catch (ListIsEmpty ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid integer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer.");
                        }
                        break;

                    case "5":
                        try
                        {
                            list.Clear();
                            Console.WriteLine("List cleared.");
                        }
                        catch (ListIsEmpty)
                        {
                            Console.WriteLine("List already clean -_-");
                        }

                        break;

                    case "6":
                        Console.Write("Enter element to check if it exists: ");
                        T elementToCheck = GetUserInput<T>();
                            try
                            {
                                bool contains = list.Contains(elementToCheck);
                                Console.WriteLine("Elem in list: " + contains);
                            }
                            catch (ListIsEmpty ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        break;

                    case "7":
                        Console.Write("Enter element to get its index: ");
                        T elementToRemove = GetUserInput<T>();
                            try
                            {
                                int index = list.IndexOf(elementToRemove);
                                Console.WriteLine(index);
                            }
                            catch (ListIsEmpty ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        break;

                    case "8":
                        Console.Write("Enter index to insert element: ");
                        if (int.TryParse(Console.ReadLine(), out int insertIndex))
                        {
                            Console.Write("Enter element to insert: ");
                            T elementToInsert = GetUserInput<T>();
                            {
                                try
                                {
                                    list.Insert(insertIndex, elementToInsert);
                                    Console.WriteLine("Element inserted after index " + insertIndex);
                                }
                                catch (ListIsEmpty ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                catch (IndexOutOfRangeException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer for the index.");
                        }
                        break;

                    case "9":
                        Console.Write("Enter index to remove element: ");
                        if (int.TryParse(Console.ReadLine(), out int removeIndex))
                        {
                            try
                            {
                                list.RemoveAt(removeIndex);
                                Console.WriteLine("Element removed at index " + removeIndex);
                            }
                            catch (ListIsEmpty ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (IndexOutOfRangeException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer for the index.");
                        }
                        break;

                    case "10":
                        Console.WriteLine("Exiting program.");
                        isChoise = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 10.");
                        break;
                }
            }
        }

        private static T GetUserInput<T>()
        { 
            string input = Console.ReadLine();

            try
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter a valid value for type {0}.", typeof(T).Name);
                return default(T);
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Invalid input type. Please enter a valid value for type {0}.", typeof(T).Name);
                return default(T);
            }
        }

    }
}
