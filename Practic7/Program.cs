using System;
public class Program
{
    public static void Main()
    {
        var container = new DynamicContainer<Model>();

        for (int i = 0; i < 1000; i++)
        {
            container.Add(new Model { Name = $"Модель{i}", Weight = 50 + i % 20, Height = 170 + i % 15, });
        }

        Console.WriteLine($"Всего моделей: {container.Count}");
        Console.WriteLine($"[0]: {container[0].Name}, Вес: {container[0].Weight}, Рост: {container[0].Height}");
        Console.WriteLine($"[500]: {container[500].Name}, Вес: {container[500].Weight}, Рост: {container[500].Height}");
        Console.WriteLine($"[999]: {container[999].Name}, Вес: {container[999].Weight}, Рост: {container[999].Height}");
        Console.WriteLine("Проверка некорректных индексов");
        try
        {
            Console.WriteLine("Пытаемся получить элемент с индексом -1:");
            var m = container[-1];
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"  Ошибка: {ex.Message}\n");
        }

        try
        {
            Console.WriteLine("Пытаемся получить элемент с индексом 1000:");
            var m = container[1000];
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"  Ошибка: {ex.Message}\n");
        }

        try
        {
            Console.WriteLine("Пытаемся удалить элемент с индексом -5:");
            container.RemoveAt(-5);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"  Ошибка: {ex.Message}\n");
        }

        try
        {
            Console.WriteLine("Пытаемся вставить элемент на позицию 2000:");
            container.Insert(2000, new Model());
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"  Ошибка: {ex.Message}\n");
        }
    }

}
public class DynamicContainer<T>
{
    private T[] data;
    private int size;
    private int capacity;
    public DynamicContainer(int initialCapacity = 4)
    {
        capacity = initialCapacity;
        size = 0;
        data = new T[capacity];
    }
    public void Add(T item)
    {
        if (size == capacity)
        {
            capacity = capacity * 2;
            T[] values = new T[capacity];
            for (int i = 0; i < size; i++)
            {
                values[i] = data[i];
            }
            data = values;
        }
        data[size] = item;
        size = size + 1;
    }
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Индекс вне диапазона");
            return data[index];
        }
        set
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Индекс вне диапазона");
            data[index] = value;
        }
    }
    public int Count => size;
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException(nameof(index),
                "Индекс вне диапазона");
        for (int i = index; i < size - 1; i++)
            data[i] = data[i + 1];
        size--;
        data[size] = default(T);
    }
    public void Insert(int index, T item)
    {
        if (index < 0 || index > size)
            throw new ArgumentOutOfRangeException(nameof(index),
                "Индекс вне диапазона");
        if (size == capacity)
        {
            capacity *= 2;
            T[] values = new T[capacity];
            for (int i = 0; i < size; i++)
            {
                values[i] = data[i];
            }
            data = values;
        }

        for (int i = size; i > index; i--)
        {
            data[i] = data[i - 1];
        }
        data[index] = item;
        size++;
    }
}
public class Model
{
    public string Name { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public bool HasContract { get; set; }

    public Model() { }

    public Model(int id, string name, double height, double weight, bool hasContract)
    {
        Name = name;
        Height = height;
        Weight = weight;
        HasContract = hasContract;
    }

    public void Print()
    {
        Console.WriteLine($"Имя: {Name}, Рост: {Height}, Вес: {Weight}, Контракт: {(HasContract ? "Да" : "Нет")}");
    }
}
