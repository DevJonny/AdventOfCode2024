using Xunit.Abstractions;

namespace App.Days;

public class Day7
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    private List<(long, List<long>)> _equations = [];
    private string[] _operators = [];
    
    public Day7(string dataFile, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        
        foreach (var line in File.ReadAllLines(dataFile))
        {
            var split = line.Split(":");
            _equations.Add((long.Parse(split[0]), split[1].Trim().Split(" ").Select(long.Parse).ToList()));
        }
    }

    public Day7 SetEquations(params (long, List<long>)[] equations)
    {
        _equations = equations.ToList();
        return this;
    }
    
    public long Run(params string[] setOperators)
    {
        _operators = setOperators;
        
        var total = 0L;
        
        foreach ((long lhs, List<long> rhs) equation in _equations)
        {
            var operatorSets = GenerateOperators(equation.rhs.Count - 1);

            foreach (var operatorSet in operatorSets)
            {
                var operators = GenerateOperatorStack(operatorSet);
                var totalForOperators = 0L;
                
                var rhs = equation.rhs.Select(r => r).ToList();

                for (var i = 1; i < rhs.Count; i++)
                {
                    var left = totalForOperators == 0 ? rhs[i-1] : totalForOperators;
                    var right = rhs[i];

                    Func<long> applyOperator = operators.Pop() switch
                    {
                        "+" => () => left + right,
                        "*" => () => left * right,
                        "||" => () => long.Parse($"{left}{right}"),
                        _ => () =>0
                    };
                    
                    totalForOperators = applyOperator();
                }

                if (totalForOperators != equation.lhs) 
                    continue;
                
                total += totalForOperators;
                break;
            }
        }
        
        return total;
    }

    private List<string> GenerateOperators(int length)
    {
        return length == 1
            ? _operators.Select(c => c).ToList()
            : GenerateOperators(length - 1)
                .SelectMany(
                    combo => _operators,
                    (combo, c) => combo + c
                )
                .ToList();
    }

    private static Stack<string> GenerateOperatorStack(string operators)
    {
        Stack<string> operatorsStack = new();
        var opsAsChars = operators.ToCharArray();
        
        foreach (var op in opsAsChars)
        {
            operatorsStack.Push(op switch
            {
                '+' => "+",
                '*' => "*",
                '|' => "||",
                _ => throw new ArgumentOutOfRangeException(nameof(operators), operators, null)
            });
        }

        return operatorsStack;
    }
}