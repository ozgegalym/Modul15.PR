using System;
using System.Reflection;

public class MyClass
{
    private int privateField;
    public string PublicProperty { get; set; }
    private double PrivateProperty { get; set; }

    public MyClass()
    {
        // Конструктор по умолчанию
    }

    public MyClass(int privateField, string publicProperty, double privateProperty)
    {
        this.privateField = privateField;
        PublicProperty = publicProperty;
        PrivateProperty = privateProperty;
    }

    public void PublicMethod()
    {
        Console.WriteLine("PublicMethod called");
    }

    private void PrivateMethod()
    {
        Console.WriteLine("PrivateMethod called");
    }
}

class Program
{
    static void Main()
    {
        // Исследование типа
        Type myClassType = typeof(MyClass);
        Console.WriteLine($"Имя класса: {myClassType.Name}");

        // Конструкторы
        ConstructorInfo[] constructors = myClassType.GetConstructors();
        Console.WriteLine("Конструкторы:");
        foreach (var constructor in constructors)
        {
            Console.WriteLine($"  {constructor}");
        }

        // Поля и свойства
        FieldInfo[] fields = myClassType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        PropertyInfo[] properties = myClassType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("Поля и свойства:");
        foreach (var field in fields)
        {
            Console.WriteLine($"  {field.Name} ({field.FieldType.Name})");
        }
        foreach (var property in properties)
        {
            Console.WriteLine($"  {property.Name} ({property.PropertyType.Name})");
        }

        // Методы
        MethodInfo[] methods = myClassType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("Методы:");
        foreach (var method in methods)
        {
            Console.WriteLine($"  {method.ReturnType.Name} {method.Name}");
        }

        Console.WriteLine("\n-----------------------------------\n");

        // Создание экземпляра
        object myInstance = Activator.CreateInstance(myClassType);
        Console.WriteLine("Экземпляр MyClass создан");

        // Манипуляции с объектом
        PropertyInfo publicPropertyInfo = myClassType.GetProperty("PublicProperty");
        publicPropertyInfo.SetValue(myInstance, "New Value");
        Console.WriteLine($"Измененное значение свойства PublicProperty: {publicPropertyInfo.GetValue(myInstance)}");

        MethodInfo publicMethodInfo = myClassType.GetMethod("PublicMethod");
        publicMethodInfo.Invoke(myInstance, null);

        // Вызов приватного метода
        MethodInfo privateMethodInfo = myClassType.GetMethod("PrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance);
        privateMethodInfo.Invoke(myInstance, null);

        Console.WriteLine("\n-----------------------------------\n");

        // Дополнительное задание (для продвинутого уровня)
        Console.WriteLine("Введите имя метода для вызова:");
        string methodName = Console.ReadLine();
        MethodInfo dynamicMethodInfo = myClassType.GetMethod(methodName);
        if (dynamicMethodInfo != null)
        {
            dynamicMethodInfo.Invoke(myInstance, null);
        }
        else
        {
            Console.WriteLine($"Метод с именем {methodName} не найден.");
        }
    }
}
