using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FinalProject
{
    public class ShoppingItem
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Gramovka { get; set; }
        public string Obem { get; set; }
    }


    public class ShoppingList
    {
        public List<ShoppingItem> Items { get; set; }

        public ShoppingList()
        {
            Items = new List<ShoppingItem>();
        }
        public void AddItem(ShoppingItem x)
        {
            Items.Add(x);
            Console.WriteLine("Товар успешно добавлен.");
        }
        public void RemoveItem(string name)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name == name)
                {
                    Items.RemoveAt(i);
                    Console.WriteLine("Товар успешно удален.");
                    return;
                }
            }
        }

        public void SaveToXml(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("ShoppingList");
            doc.AppendChild(root);
        }
        public static ShoppingList LoadFromXml(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ShoppingList));
            using (TextReader reader = new StreamReader(filename))
            {
                return (ShoppingList)serializer.Deserialize(reader);
            }
        }


        public void DisplayItems()
        {
            Console.WriteLine("Список покупок:");
            foreach (var item in Items)
            {
                Console.WriteLine("Название: " + item.Name + "Цена: " + item.Price + " Грамовка: " + item.Gramovka + "Объем: " + item.Obem);

            }
        }

        internal class Program
        {
            static void Main(string[] args)
            {
                ShoppingList shoppingList = new ShoppingList();

                while (true)
                {
                    Console.WriteLine("\nВыберите действие:");
                    Console.WriteLine("1. Добавить товар");
                    Console.WriteLine("2. Удалить товар");
                    Console.WriteLine("3. Сохранить список в XML");
                    Console.WriteLine("4. Загрузить список из XML");
                    string x = Console.ReadLine();
                    int y;
                    if (int.TryParse(x, out y))
                    {
                        switch (y)
                        {
                            case 1:
                                ShoppingItem newItem = new ShoppingItem();
                                Console.Write("Введите имя товара: ");
                                newItem.Name = Console.ReadLine();
                                Console.Write("Введите цену: ");
                                newItem.Price = Console.ReadLine();
                                Console.Write("Введите вес: ");
                                newItem.Gramovka = Console.ReadLine();
                                Console.Write("Введите объем: ");
                                newItem.Obem = Console.ReadLine();
                                shoppingList.AddItem(newItem);
                                break;
                            case 2:
                                Console.Write("Введите имя товара для удаления: ");
                                string removeName = Console.ReadLine();
                                shoppingList.RemoveItem(removeName);
                                break;
                            case 3:
                                Console.Write("Введите имя файла для сохранения: ");
                                string saveFile = Console.ReadLine();
                                shoppingList.SaveToXml(saveFile);
                                Console.WriteLine("Сохранено.");
                                break;
                            case 4:
                                Console.Write("Файл из которого загрузить: ");
                                string loadFile = Console.ReadLine();
                                shoppingList = ShoppingList.LoadFromXml(loadFile);
                                Console.WriteLine("Выполнено.");
                                break;
                            case 5:
                                shoppingList.DisplayItems();
                                break;
                            default:
                                Console.WriteLine("Неверный выбор.");
                                break;
                        }
                    }
                }
            }
        }
    }
}