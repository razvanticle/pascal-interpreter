using System.Reflection;
using PascalInterpreter.Parsing;

namespace PascalInterpreter;

public abstract class NodeVisitor
{
    protected object? Visit(Ast node)
    {
        var methodName = $"Visit{node.GetType().Name}";

        var type = GetType();
        var methodInfo = type.GetMethod(methodName, BindingFlags.Instance| BindingFlags.NonPublic);
        if (methodInfo == null)
        {
            throw new Exception($"Could not find visitor for {type} ");

        }

       return methodInfo.Invoke(this, new object[] { node });

        // if (result is int value)
        // {
        //     return value;
        // }

        throw new Exception("Error parsing input");
    }
}