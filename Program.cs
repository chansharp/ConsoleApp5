using System.Collections;

namespace ConsoleApp5;

internal static class Program
{
    private static void Main()
    {
        // var test = new Test();
        // foreach (int o in test)
        // {
        //     Console.WriteLine(o);
        // }
        // var enumerable = testIterator as int[] ?? testIterator.ToArray();
        // var foo = enumerable.Select(i => i.ToString());

        var testIterator = TestIterator();
        using var enumerator1 = testIterator.GetEnumerator();
        while (enumerator1.MoveNext())
        {
            int enumerator1Current = enumerator1.Current;
            Console.WriteLine(enumerator1Current);
        }

        var zoo = new Zoo();
        
        zoo.AddMammal("Whale");
        zoo.AddMammal("Rhinoceros");
        zoo.AddBird("Penguin");
        zoo.AddBird("Warbler");
        
        var theStack = new Stack<int>();
        for (var number = 0; number <= 9; number++)
        {
            theStack.Push(number);
        }
        
        Console.WriteLine(theStack.Last());
        Console.WriteLine(theStack.Last());
    }

    private static IEnumerable<int> TestIterator()
    {
        var test = new[] {1, 2, 3, 4, 5, 6};
        Console.WriteLine("init");
        foreach (int i in test)
        {
            yield return i;
        }
    }

    private class Zoo : IEnumerable
    {
        // Private members.
        private readonly List<Animal> _animals = new List<Animal>();

        // Public methods.
        public void AddMammal(string name)
        {
            _animals.Add(new Animal {Name = name, Type = Animal.TypeEnum.Mammal});
        }

        public void AddBird(string name)
        {
            _animals.Add(new Animal {Name = name, Type = Animal.TypeEnum.Bird});
        }

        public IEnumerator GetEnumerator()
        {
            Console.WriteLine("init");
            foreach (var theAnimal in _animals)
            {
                yield return theAnimal.Name;
            }
        }

        // Public members.
        public IEnumerable Mammals => AnimalsForType(Animal.TypeEnum.Mammal);

        public IEnumerable Birds => AnimalsForType(Animal.TypeEnum.Bird);

        // Private methods.
        private IEnumerable AnimalsForType(Animal.TypeEnum type)
        {
            foreach (var theAnimal in _animals)
            {
                if (theAnimal.Type == type)
                {
                    yield return theAnimal.Name;
                }
            }
        }

        // Private class.
        private class Animal
        {
            public enum TypeEnum
            {
                Bird,
                Mammal
            }

            public string Name { get; set; } = default!;

            public TypeEnum Type { get; set; }
        }
    }

    private class Stack<T> : IEnumerable<T>
    {
        private readonly T[] _values = new T[100];
        private int _top = 0;

        public void Push(T t)
        {
            _values[_top] = t;
            _top++;
        }

        public T Pop()
        {
            _top--;
            return _values[_top];
        }

        // This method implements the GetEnumerator method. It allows
        // an instance of the class to be used in a foreach statement.
        public IEnumerator<T> GetEnumerator()
        {
            for (int index = _top - 1; index >= 0; index--)
            {
                yield return _values[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> TopToBottom => this;

        public IEnumerable<T> BottomToTop
        {
            get
            {
                for (var index = 0; index <= _top - 1; index++)
                {
                    yield return _values[index];
                }
            }
        }

        public IEnumerable<T> TopN(int itemsFromTop)
        {
            // Return less than itemsFromTop if necessary.
            int startIndex = itemsFromTop >= _top ? 0 : _top - itemsFromTop;

            for (int index = _top - 1; index >= startIndex; index--)
            {
                yield return _values[index];
            }
        }
    }

}