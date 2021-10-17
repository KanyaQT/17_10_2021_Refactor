using System;
using System.Collections.Generic;
 
class CharacterStat
{
    private const int OVERHEAD_LIMIT = 10;

    private int _statValue;
    private string _statName;

    public CharacterStat(int statValue, string statName)
    {
        _statValue = statValue;
        _statName = statName;
    }

    public void BuyStat(int value, ref int points)
    {
        Console.WriteLine(points);

        _statValue += value;
        points -= value;
        int overhead = OVERHEAD_LIMIT < _statValue ? _statValue - OVERHEAD_LIMIT :
                    _statValue < 0 ? _statValue : 0;

        _statValue -= overhead;
        points += overhead;
    }

    public string GetVisual()
    {
        string statVisual = string.Empty;
        statVisual = _statName + " " + statVisual.PadLeft(_statValue, '#').PadRight(10, '_');
        return statVisual;
    }
}

class Program
{
    private static Dictionary<string, int> _indexOf = new Dictionary<string, int>()
        {
            { "сила", 0 },
            { "ловкость", 1 },
            { "интеллект", 2 }
        };        
    private static CharacterStat[] _stats =
        {
          new CharacterStat(0, "Сила"),
          new CharacterStat(0, "Ловкость"),
          new CharacterStat(0, "Интеллект")
        };

    private static int _age = 0;
    private static int _points = 25;

    private static void PrintStats()
    {
        Console.Clear();
        foreach(CharacterStat s in _stats)
        {
            Console.WriteLine(s.GetVisual());
        }
    }
    private static void ReadInt(string message, out int result)
    {
        Console.WriteLine(message);
        result = 0;
        string resultRaw;
        do
        {
            resultRaw = Console.ReadLine();
        } while (!int.TryParse(resultRaw, out result));
    }

    static void Main(string[] args)
    {
        Intro();
        StartStatAssignmentLoop();
        Outro();
    }

    private static void Intro()
    {

        Console.WriteLine("Добро пожаловать в меню выбора создания персонажа!");
        Console.WriteLine("У вас есть 25 очков, которые вы можете распределить по умениям");
        Console.WriteLine("Нажмите любую клавишу чтобы продолжить...");
        Console.ReadKey();
    }
    private static void StartStatAssignmentLoop()
    {
        while (_points > 0)
        {
            PrintStats();
            Console.WriteLine(@"Количество очков: {0}", _points);

            Console.WriteLine("Какую характеристику вы хотите изменить?");
            string subject = Console.ReadLine();

            Console.WriteLine(@"Что вы хотите сделать? +\-");
            char operation = Console.ReadKey().KeyChar;

            int operandPoints = 0;
            ReadInt("\n" + "Количество поинтов которое следует " +
                (operation == '+' ? "прибавить" : "отнять"),
                out operandPoints);

            operandPoints *= operation == '+' ? 1 : -1;

            try
            {
                _stats[_indexOf[subject.ToLower()]].BuyStat(operandPoints, ref _points);
            }
            catch(KeyNotFoundException)
            {
                Console.WriteLine("Некорректно введено название характеристики!");
                Console.ReadKey();
            }
        }
    }
    private static void Outro()
    {
        PrintStats();
        ReadInt("\nВы распределили все очки. Введите возраст персонажа:",
                out _age);

        PrintStats();
        Console.WriteLine("\nВаш возраст: " + _age);
        Console.WriteLine("\nСоздание персонажа завершено.");
        Console.ReadKey();
    }
}
