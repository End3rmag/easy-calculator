using System;
using System.Collections.Generic;
using System.Text;

StreamWriter sw = new StreamWriter("C://Users/prdb/Desktop/ConsoleApp11/test.txt");
string Colculator = ("C://Users/prdb/Desktop/ConsoleApp11/Colculator.txt");
string exp = File.ReadAllText(Colculator);

try
{
    double result = EE(exp);
    Console.WriteLine("Результат: " + result);
    sw.WriteLine("Результат: " + result);
    sw.Close();
}
catch (Exception ex)
{
    Console.WriteLine("Ошибка: " + ex.Message);
}
 
static double Operation(char operation, double b, double a)
{
    switch (operation)
    {
        case '+': return a + b;
        case '-': return a - b;
        case '*': return a * b;
        case '/':
            if (b == 0)
                throw new DivideByZeroException("Деление на ноль.");
            return a / b;
    }
    return 0;
}

static int Poryadok(char operation)
{
    switch (operation)
    {
        case '+':
        case '-':
            return 1;
        case '*':
        case '/':
            return 2;
    }
    return 0;
}

static bool Operators(char c)
{
    return c == '+' || c == '-' || c == '*' || c == '/';
}

static double EE(string ex)
{
    var nums = new Stack<double>();
    var operations = new Stack<char>();

    for (int i = 0; i < ex.Length; i++)
    {
        if (ex[i] == ' ')
            continue;
        if (char.IsDigit(ex[i]))
        {
            StringBuilder sb = new StringBuilder();
            while (i < ex.Length && (char.IsDigit(ex[i])))
            {
                sb.Append(ex[i]);
                i++;
            }
            nums.Push(Convert.ToDouble(sb.ToString()));
            i--;
        }
        else if (Operators(ex[i]))
        {
            while (operations.Count > 0 && Poryadok(operations.Peek()) >= Poryadok(ex[i]))
            {
                nums.Push(Operation(operations.Pop(), nums.Pop(), nums.Pop()));
            }
            operations.Push(ex[i]); 
        }
    }
    while (operations.Count > 0)
    {
        nums.Push(Operation(operations.Pop(), nums.Pop(), nums.Pop()));
    }

    return nums.Pop();
}


