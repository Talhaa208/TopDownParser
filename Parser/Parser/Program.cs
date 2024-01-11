
using System;

class TopDownParser
{
    //Grammer
    //E->EAE | (E) | -E | id
    //A-> + | - | * | / | !

    private static string input;
    private static int index;
    private static char currentToken;

    static void Main()
    {
        Console.Write("Enter an expression: ");
        input = Console.ReadLine();
        index = 0;

        try
        {
            currentToken = GetNextToken();
            ParseE();

            if (index == input.Length)
            {
                Console.WriteLine("Parsing successful! The expression is valid.");
            }
            else
            {
                Console.WriteLine("Parsing failed! Unexpected token at position " + index);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static char GetNextToken()
    {
        if (index < input.Length)
        {
            return input[index++];
        }
        else
        {
            return '\0'; // End of input
        }
    }

    static void Match(char expectedToken)
    {
        if (currentToken == expectedToken)
        {
            currentToken = GetNextToken();
        }
        else
        {
            throw new Exception("Unexpected token: " + currentToken);
        }
    }

    static void ParseE()
    {
        ParseA();
        ParseEPrime();
    }

    static void ParseEPrime()
    {
        if (IsAOperator(currentToken))
        {
            Match(currentToken);
            ParseA();
            ParseEPrime();
        }
        // else, it's an epsilon production (no operation), do nothing
    }

    static void ParseA()
    {
        if (IsIdStart(currentToken))
        {
            // Match the entire 'id'
            while (IsIdPart(currentToken))
            {
                Match(currentToken);
           }
        }
        else if (currentToken == '(')
        {
            Match('(');
            ParseE();
            Match(')');
        }
        else if (IsAOperator(currentToken))
        {
            Match(currentToken);
        }
        else
        {
            throw new Exception("Unexpected token in expression: " + currentToken);
        }
    }

    static bool IsIdStart(char c)
    {
        if (c == 'i')
            return true;

        return char.IsLetter(c);    }


    static bool IsIdPart(char c)
    {
        currentToken = GetNextToken();
        if (currentToken == 'd')
        {
            currentToken = GetNextToken(); // Consume 'd'
            return true;
        }

        return false;
    }


    static bool IsAOperator(char c)
    {
        return c == '+' || c == '-' || c == '*' || c == '/' || c == '!';
    }
}

