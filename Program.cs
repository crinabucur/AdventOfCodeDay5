using System.Text;
using System.Text.RegularExpressions;

internal class Program
{
    public static void Main()
    {
        var stacks = new Stack<char>[] { new(), new(), new(), new(), new(), new(), new(), new(), new() };
        var finishedDrawing = false;
        const bool canMoveMultipleCrates = true;
        
        foreach (var line in File.ReadLines(@"C:\Users\CrinaBucur\input.txt"))
        {
            if (line.Contains('[') && line.Contains(']'))
            {
                for (var i = 0; i < stacks.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(line[1 + 4 * i].ToString()))
                        stacks[i].Push(line[1 + 4 * i]);
                }
            }
            else if (string.IsNullOrEmpty(line))
            {
                for (var i = 0; i < stacks.Length -1; i++)
                {
                    stacks[i] = ReverseStack(stacks[i]);
                }
            }
            else
            {
                if (!finishedDrawing)
                {
                    finishedDrawing = true;
                    continue;
                }
                    
                var indicationNumbers = Regex.Matches(line, @"\d+");

                var cratesToMove = int.Parse(indicationNumbers[0].Value);
                var sourceStack = int.Parse(indicationNumbers[1].Value);
                var destinationStack = int.Parse(indicationNumbers[2].Value);

                if (canMoveMultipleCrates)
                {
                    var temp = new Stack<char>();
                    for (var i = 0; i < cratesToMove; i++)
                    {
                        var crate = stacks[sourceStack - 1].Pop();
                        temp.Push(crate);
                    }

                    foreach (var crate in temp)
                    {
                        stacks[destinationStack - 1].Push(crate);
                    }
                }
                else for (var i = 0; i < cratesToMove; i++)
                {
                    var crate = stacks[sourceStack - 1].Pop();
                    stacks[destinationStack - 1].Push(crate);
                }
            }
        }

        Console.WriteLine("Crates ending up on top of each stack: {0} ", GetCratesOnTop(stacks));
        Console.ReadLine();
    }

    private static Stack<char> ReverseStack(Stack<char> input)
    {
        var temp = new Stack<char>();
 
        while (input.Count != 0)
            temp.Push(input.Pop());
 
        return temp;
    }

    private static string GetCratesOnTop(Stack<char>[] stacks)
    {
        var cratesOnTop = new StringBuilder();
        foreach (var stack in stacks)
        {
            if (stack.TryPop(out var topElement))
            {
                cratesOnTop.Append(topElement);
            }
        }

        return cratesOnTop.ToString();
    }
}