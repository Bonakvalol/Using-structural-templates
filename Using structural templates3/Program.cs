using System;
using System.Collections.Generic;

namespace CompositePatternExample
{
    
    public abstract class FileSystemItem
    {
        public abstract void Display(int depth);  
        public abstract void Add(FileSystemItem item); 
        public abstract void Remove(FileSystemItem item); 
    }

    public class File : FileSystemItem
    {
        private string name;

        public File(string name)
        {
            this.name = name;
        }

       
        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + name); 
        }

        public override void Add(FileSystemItem item)
        {
            Console.WriteLine("Не удается добавить что-либо в файл");
        }

        public override void Remove(FileSystemItem item)
        {
            Console.WriteLine("Не удается удалить из файла");
        }
    }

    
    public class Directory : FileSystemItem
    {
        private string name;
        private List<FileSystemItem> items = new List<FileSystemItem>();

        public Directory(string name)
        {
            this.name = name;
        }

        
        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + name); 
            foreach (var item in items)
            {
                item.Display(depth + 2);  
            }
        }

        
        public override void Add(FileSystemItem item)
        {
            items.Add(item);
        }

        public override void Remove(FileSystemItem item)
        {
            items.Remove(item);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Directory root = new Directory("Корневой каталог");

            while (true)
            {
                Console.WriteLine("Выберите действие: ");
                Console.WriteLine("1 - Добавить файл");
                Console.WriteLine("2 - Добавить директорию");
                Console.WriteLine("3 - Показать структуру");
                Console.WriteLine("4 - Выход");

                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Введите имя файла: ");
                    var fileName = Console.ReadLine();
                    root.Add(new File(fileName));
                }
                else if (choice == "2")
                {
                    Console.Write("Введите имя директории: ");
                    var dirName = Console.ReadLine();
                    Directory newDir = new Directory(dirName);
                    root.Add(newDir);

                    while (true)
                    {
                        Console.WriteLine($"Добавление в директорию '{dirName}'. Выберите действие:");
                        Console.WriteLine("1 - Добавить файл");
                        Console.WriteLine("2 - Добавить поддиректорию");
                        Console.WriteLine("3 - Завершить добавление");

                        var subChoice = Console.ReadLine();

                        if (subChoice == "1")
                        {
                            Console.Write("Введите имя файла: ");
                            var subFileName = Console.ReadLine();
                            newDir.Add(new File(subFileName));
                        }
                        else if (subChoice == "2")
                        {
                            Console.Write("Введите имя поддиректории: ");
                            var subDirName = Console.ReadLine();
                            newDir.Add(new Directory(subDirName));
                        }
                        else if (subChoice == "3")
                        {
                            break;  
                        }
                    }
                }
                else if (choice == "3")
                {
                    root.Display(0); 
                }
                else if (choice == "4")
                {
                    break; 
                }
                else
                {
                    Console.WriteLine("Неверный выбор");
                }
            }
        }
    }
}
