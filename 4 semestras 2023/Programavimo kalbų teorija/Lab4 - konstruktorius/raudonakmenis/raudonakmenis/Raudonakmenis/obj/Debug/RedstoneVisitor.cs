using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Raudonakmenis.obj.Debug
{
    public class RedstoneVisitor : GrammarBaseVisitor<object>
    {
        private SymbolTable symbolTable = new SymbolTable();
        private readonly Stack<SymbolTable> tablestack = new Stack<SymbolTable>();

        private Dictionary<string, FunctionDefinition> functions = new Dictionary<string, FunctionDefinition>();

        public class FunctionDefinition
        {
            public string ReturnType { get; }
            public string Name { get; }
            public List<Parameter> Parameters { get; }
            public GrammarParser.MethodBodyContext Body { get; }

            public GrammarParser.MethodDeclarationContext Context { get; }

            public FunctionDefinition(string returnType, string name, List<Parameter> parameters, GrammarParser.MethodBodyContext body, GrammarParser.MethodDeclarationContext context)
            {
                this.ReturnType = returnType;
                this.Name = name;
                this.Parameters = parameters;
                this.Body = body;
                this.Context = context;
            }
        }

        public class Parameter
        {
            public string Type { get; }
            public string Name { get; }

            public Parameter(string type, string name)
            {
                Type = type;
                Name = name;
            }
        }


        public RedstoneVisitor(SymbolTable symbolTbl) {
            this.symbolTable = symbolTbl;
            this.functions = new Dictionary<string, FunctionDefinition>();
        }
        StringBuilder builder = new StringBuilder();
        
        public override object VisitProgram(GrammarParser.ProgramContext context) {
            base.VisitProgram(context);
            return builder.ToString();
        }
        public override object VisitStatement(GrammarParser.StatementContext context) {
            if (context.assignment() != null)
            {
                return VisitAssignment(context.assignment());
            }
            else if (context.variableDeclaration() != null)
            {
                return VisitVariableDeclaration(context.variableDeclaration());

            }
            else if (context.functionCall() != null)
            {
                return VisitFunctionCall(context.functionCall());
            }
            else if (context.printStatement() != null)
            {
                return VisitPrintStatement(context.printStatement());
            }

            else if (context.returnStatement() != null)
            {
                return VisitReturnStatement(context.returnStatement());
            }
            return null;
        }
        public override object VisitLine(GrammarParser.LineContext context) {
            if (context.statement() != null)
            {
                return VisitStatement(context.statement());
            }
            else if (context.ifBlock() != null)
            {
                return VisitIfBlock(context.ifBlock());
            }
            else if (context.whileBlock() != null)
            {
                return VisitWhileBlock(context.whileBlock());
            }
            else if (context.classDeclaration() != null)
            {
                return VisitClassDeclaration(context.classDeclaration());
            }
            else if (context.methodDeclaration() != null)
            {
                return VisitMethodDeclaration(context.methodDeclaration());
            }
            else if (context.classCall() != null)
            {
                return VisitClassCall(context.classCall());
            }
            else if (context.printStatement() != null)
            {
                return VisitPrintStatement(context.printStatement());
            }

            return null;
        }

        public override object VisitConstant([NotNull] GrammarParser.ConstantContext context)
        {
            if (context.INTEGER() != null)
                return int.Parse(context.INTEGER().GetText());

            if (context.FLOAT() != null)
                return float.Parse(context.FLOAT().GetText());

            if (context.STRING() != null)
                return context.STRING().GetText().Substring(1, context.STRING().GetText().Length -2);

            if (context.BOOL() != null)
                return context.BOOL().GetText() == "true";

            if (context.NULL() == null)
                return null;

            throw new Exception("Unknown value type.");
        }

        public override object VisitVariableDeclaration([NotNull] GrammarParser.VariableDeclarationContext context)
        {
            var type = context.type().ToString();
            var varname = context.IDENTIFIER().GetText();
            var value = Visit(context.expression());

            this.symbolTable.DeclareVariable(varname, value);
            return value;
        }

        public override object VisitAssignment(GrammarParser.AssignmentContext context) {
            string varName = context.IDENTIFIER().GetText();
            string funkyop = context.funkyOp().GetText();
            var expressionResult = context.expression();
            var value = expressionResult != null ? Visit(expressionResult) : null;




            if (value !=null && !this.symbolTable.isDeclared(varName))
            {
                
                if( funkyop == "=")
                {
                    this.symbolTable.DeclareVariable(varName, value);
                }
               
            }
            else if (this.symbolTable.isDeclared(varName))
            {
                var tablevalue = this.symbolTable.ResolveVariable(varName);
                if(value == null)
                {
                    if (funkyop == "%%")
                    {
                        if (tablevalue is string)
                        {
                            this.symbolTable.ChangeVariable(varName, Reverse((string)tablevalue));
                        }
                        else
                        {
                            throw new Exception("Unsupported type");
                        }
                    }
                    else if (funkyop == "$$")
                    {
                        if (tablevalue is string)
                        {
                            this.symbolTable.ChangeVariable(varName, float.Parse((string)tablevalue));
                        }
                        else
                        {
                            throw new Exception("Unsupported type");
                        }
                    }
                    else if (funkyop == "@@")
                    {
                        if (tablevalue is int || tablevalue is float)
                        {
                            this.symbolTable.ChangeVariable(varName, tablevalue.ToString());
                        }
                        else
                        {
                            throw new Exception("Unsupported type");
                        }
                    }
                    else if (funkyop == "--")
                    {
                        this.symbolTable.ChangeVariable(varName, Subtract(tablevalue, 1));
                    }
                    else if (funkyop == "++")
                    {
                        this.symbolTable.ChangeVariable(varName, Add(tablevalue, 1));
                    }
                    else if (funkyop == "+++")
                    {
                        if (tablevalue is int)
                        {
                            this.symbolTable.ChangeVariable(varName, (int)tablevalue + 2);
                        }
                        else if (tablevalue is float)
                        {
                            this.symbolTable.ChangeVariable(varName, (float)tablevalue + 2);
                        }
                        else
                        {
                            throw new Exception("Unsupported type");
                        }
                    }
                    else if (funkyop == "---")
                    {
                        if (tablevalue is int)
                        {
                            this.symbolTable.ChangeVariable(varName, (int)tablevalue - 2);
                        }
                        else if (tablevalue is float)
                        {
                            this.symbolTable.ChangeVariable(varName, (float)tablevalue - 2);
                        }
                        else
                        {
                            throw new Exception("Unsupported type");
                        }
                    }
                }
                else
                {
                    if (funkyop == "=")
                    {
                        this.symbolTable.ChangeVariable(varName, value);
                    }
                    else if (funkyop == "+=")
                    {
                        if (tablevalue is int && value is int)
                        {
                            this.symbolTable.ChangeVariable(varName, (int)tablevalue + (int)value);
                        }
                        else if (tablevalue is float && value is float)
                        {
                            this.symbolTable.ChangeVariable(varName, (float)tablevalue + (float)value);
                        }
                        else
                        {
                            throw new Exception("Unsupported type");
                        }

                    }
                    else if (funkyop == "-=")
                    {
                        if (tablevalue is int && value is int)
                        {
                            this.symbolTable.ChangeVariable(varName, (int)tablevalue - (int)value);
                        }
                        else if (tablevalue is float && value is float)
                        {
                            this.symbolTable.ChangeVariable(varName, (float)tablevalue - (float)value);
                        }
                        else
                        {
                            throw new Exception("Unsupported type");
                        }
                    }
                    else if (funkyop == "/=")
                    {
                        if (tablevalue is int && value is int)
                        {
                            this.symbolTable.ChangeVariable(varName, (int)tablevalue / (int)value);
                        }
                        else if (tablevalue is float && value is float)
                        {
                            this.symbolTable.ChangeVariable(varName, (float)tablevalue / (float)value);
                        }
                        else
                        {
                            throw new Exception("Unsupported type");
                        }
                    }
                    else if (funkyop == "*=")
                    {
                        if (tablevalue is int && value is int)
                        {
                            this.symbolTable.ChangeVariable(varName, (int)tablevalue * (int)value);
                        }
                        else if (tablevalue is float && value is float)
                        {
                            this.symbolTable.ChangeVariable(varName, (float)tablevalue * (float)value);
                        }
                        else
                        {
                            throw new Exception("Unsupported type");
                        }
                    }
                    else if (funkyop == "%=")
                    {
                        if (tablevalue is int && value is int)
                        {
                            this.symbolTable.ChangeVariable(varName, (int)tablevalue % (int)value);
                        }
                        else if (tablevalue is float && value is float)
                        {
                            this.symbolTable.ChangeVariable(varName, (float)tablevalue % (float)value);
                        }
                        else
                        {
                            throw new Exception("Unsupported type");
                        }
                    }
                    else if (funkyop == "^=")
                    {
                        if (tablevalue is int && value is int)
                        {
                            int temp = (int)tablevalue;
                            for(int i = 0; i < (int)value; i++)
                            {
                                temp = temp * temp;
                            }
                            this.symbolTable.ChangeVariable(varName, temp);
                        }
                        else if (tablevalue is float && value is int)
                        {
                            double temp = (float)tablevalue;
                            for (int i = 0; i < (int)value; i++)
                            {
                                temp = temp * temp;
                            }
                            this.symbolTable.ChangeVariable(varName, temp);
                        }
                        else
                        {
                            throw new Exception("Unsupported type");
                        }
                    }
                }
                
                
                
               
                
                
            }
            else
            {
                throw new Exception("Undeclared value");
            }
            return null;
        }

        public override object VisitIdExpression(GrammarParser.IdExpressionContext context) {
            var idname = context.IDENTIFIER().GetText();

            return this.symbolTable.ResolveVariable(idname);
        }

        public override object VisitAddExpression(GrammarParser.AddExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            string operatorText = context.addOp().GetText();
            switch (operatorText)
            {
                case "+":
                    return Add(left, right);
                case "-":
                    return Subtract(left, right);
                default:
                    throw new InvalidOperationException($"Unsupported intAddOp: {operatorText}");
            }
        }

        public override object VisitBoolExpression(GrammarParser.BoolExpressionContext context) {
            var val1 = Visit(context.expression(0));
            var val2 = Visit(context.expression(1));
            string operatorText = context.boolOp().GetText();
            switch (operatorText)
            {
                case "and":
                    return (bool)val1 && (bool)val2;
                case "or":
                    return (bool)val1 || (bool)val2;
                case "xor":
                    return (bool)val1 ^ (bool)val2;
                default:
                    throw new InvalidOperationException($"Unsupported intAddOp: {operatorText}");
            }
        }

        public override object VisitCompareExpression([NotNull] GrammarParser.CompareExpressionContext context)
        {

            var val1 = Visit(context.expression(0));
            var val2 = Visit(context.expression(1));
            if(val1 is string && val2 is string )
            {

                if(symbolTable.isDeclared((string)val1) && symbolTable.isDeclared((string)val2))
                {
                    val1 = symbolTable.ResolveVariable((string)val1);
                    val2 = symbolTable.ResolveVariable((string)val2);
                }
                else if(symbolTable.isDeclared((string)val1) && !symbolTable.isDeclared((string)val2))
                {
                    val1 = symbolTable.ResolveVariable((string)val1);
                }
                else if (!symbolTable.isDeclared((string)val1) && symbolTable.isDeclared((string)val2))
                {
                    val2 = symbolTable.ResolveVariable((string)val2);
                }
                
            }
            string operatorText = context.compareOp().GetText();
            switch (operatorText)
            {
                case "==":
                    if (val1 is int && val2 is int)
                    {
                        return (int)val1 == (int)val2;
                    }
                    else if (val1 is float && val2 is float)
                    {
                        return (float)val1 == (float)val2;
                    }
                    else if (val1 is string && val2 is string)
                    {
                        return (string)val1 == (string)val2;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unsupported intAddOp: {operatorText}");
                    }
                    
                case "!=":
                    if (val1 is int && val2 is int)
                    {
                        return (int)val1 != (int)val2;
                    }
                    else if (val1 is float && val2 is float)
                    {
                        return (float)val1 != (float)val2;
                    }
                    else if (val1 is string && val2 is string)
                    {
                        return (string)val1 != (string)val2;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unsupported intAddOp: {operatorText}");
                    }
                    
                case ">":
                    if(val1 is int && val2 is int)
                    {
                        return (int)val1 > (int)val2;
                    } else if(val1 is float && val2 is float)
                    {
                        return (float)val1 > (float)val2;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unsupported intAddOp: {operatorText}");
                    }
                    
                case "<":
                    if (val1 is int && val2 is int)
                    {
                        return (int)val1 < (int)val2;
                    }
                    else if (val1 is float && val2 is float)
                    {
                        return (float)val1 < (float)val2;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unsupported intAddOp: {operatorText}");
                    }
                case ">=":
                    if (val1 is int && val2 is int)
                    {
                        return (int)val1 >= (int)val2;
                    }
                    else if (val1 is float && val2 is float)
                    {
                        return (float)val1 >= (float)val2;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unsupported intAddOp: {operatorText}");
                    }
                case "<=":
                    if (val1 is int && val2 is int)
                    {
                        return (int)val1 <= (int)val2;
                    }
                    else if (val1 is float && val2 is float)
                    {
                        return (float)val1 <= (float)val2;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unsupported intAddOp: {operatorText}");
                    }
                default:
                    throw new InvalidOperationException($"Unsupported intAddOp: {operatorText}");
            }


        }

        public override object VisitClassExpression(GrammarParser.ClassExpressionContext context) {
            string className = context.classFunctionCall().IDENTIFIER().GetText();
            string methodName = context.classFunctionCall().methodName().GetText();
            List<object> arguments = new List<object>();

            if (context.classFunctionCall().expression().Length > 0)
            {
                foreach (var expressionContext in context.classFunctionCall().expression())
                {
                    object argument = Visit(expressionContext);
                    arguments.Add(argument);
                }
            }
            return VisitChildren(context);
        }

        /*public override object VisitChildren([NotNull] IRuleNode node)
        {
           
            var result = this.DefaultResult;
            int childCount = node.ChildCount;
            for (int i = 0; i < childCount && this.ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept<Result>((IParseTreeVisitor<Result>)this);
                result = this.AggregateResult(result, nextResult);
            }
            return result;
        }*/
    


        public override object VisitMultExpression( GrammarParser.MultExpressionContext context) {
            var val1 = Visit(context.expression(0));
            var val2 = Visit(context.expression(1));
            string operatorText = context.multOp().GetText();
            switch (operatorText)
            {
                case "*":
                    if(val1 is int && val2 is int)
                    {
                        return (int)val1 * (int)val2;
                    }
                    else if (val1 is float && val2 is float)
                    {
                        return (float)val1 * (float)val2;
                    }
                    else
                    {
                        return new Exception("Unsupported type");
                    }

                case "/":
                    if (val1 is int && val2 is int)
                    {
                        return (int)val1 / (int)val2;
                    }
                    else if(val1 is float && val2 is float)
                    {
                        return (float)val1 / (float)val2;
                    }
                    else
                    {
                        return new Exception("Unsupported type");
                    }
                case "%":
                    if (val1 is int && val2 is int)
                    {
                        return (int)val1 % (int)val2;
                    }
                    else if( val1 is float && val2 is float)
                    {
                        return (float)val1 % (float)val2;
                    }
                    else
                    {
                        return new Exception("Unsupported type");
                    }
                default:
                    throw new InvalidOperationException($"Unsupported intAddOp: {operatorText}");
            }
        }
        public override object VisitNotExpression( GrammarParser.NotExpressionContext context) {
            var val1 = VisitExpression(context.expression());
            return !(bool)val1;
        }
        public override object VisitFunctionExpression(GrammarParser.FunctionExpressionContext context) {
            var value = Visit(context.functionCall());

            return value;

        }



        public override object VisitWhileBlock([NotNull] GrammarParser.WhileBlockContext context)
        {
            var condition = Visit(context.expression());

            while ((bool)condition)
            {
                // Execute the if block
                Visit(context.block());
                condition = Visit(context.expression());
            }

            return null;
        }

        public override object VisitMethodBody(GrammarParser.MethodBodyContext context)
        {
            object value = base.VisitMethodBody(context);
            if (value is ReturnValue)
            {
                return value;
            }
            return new ReturnValue(0);
        }

        public override object VisitMethodDeclaration([NotNull] GrammarParser.MethodDeclarationContext context)
        {
            string returnType = context.type().GetText();
            string functionName = context.methodName().GetText();
            var parameterContexts = context.parameterList()?.parameter();
            List<Parameter> parameters = new List<Parameter>();

            if (parameterContexts != null)
            {
                foreach (var parameterContext in parameterContexts)
                {
                    string parameterType = parameterContext.type().GetText();
                    string parameterName = parameterContext.parameterName().IDENTIFIER().GetText();
                    parameters.Add(new Parameter(parameterType, parameterName));
                }
            }

            FunctionDefinition functionDefinition = new FunctionDefinition(returnType, functionName, parameters, context.methodBody(), context);

            if (functions.ContainsKey(functionName))
                throw new System.Exception($"Function '{functionName}' already exists");

            functions.Add(functionName, functionDefinition);
            Console.WriteLine("Added");

            return this;
        }

        public override object VisitFunctionCall([NotNull] GrammarParser.FunctionCallContext context)
        {
            string functionName = context.methodName().IDENTIFIER().GetText();

            if (!functions.ContainsKey(functionName))
            {
                throw new Exception($"Function '{functionName}' is not defined.");
            }

            FunctionDefinition functionDefinition = functions[functionName];
            var argumentContexts = context.arguments();

            List<object> arguments = new List<object>();

            if (argumentContexts != null)
            {
                foreach (var argumentContext in argumentContexts.expression())
                {

                    arguments.Add(VisitExpression(argumentContext));
                }
            }

            if (arguments.Count != functionDefinition.Parameters.Count)
            {
                throw new Exception($"Function '{functionName}' expects {functionDefinition.Parameters.Count} arguments, but {arguments.Count} were provided.");
            }
            //Check if types are correct
            for (int i = 0; i < arguments.Count; i++)
            {
                if (arguments[i] == null)
                {
                    throw new Exception($"Function '{functionName}' doesn't accept null arguments");
                }

                if (arguments[i].GetType().Equals(functionDefinition.Parameters[i].GetType()))
                {
                    throw new Exception($"Function '{functionName}' expects {functionDefinition.Parameters[i].Name} to be of type {functionDefinition.Parameters[i].GetType().ToString()}.");
                }
            }

            // Set up a new scope for the function execution
            SymbolTable functionScope = new SymbolTable();

            // Assign the arguments to the corresponding parameters
            if (functionDefinition.Context.parameterList() != null)
            {
                for (int i = 0; i < functionDefinition.Context.parameterList().parameter().Length; i++)
                {
                    String paramName = functionDefinition.Context.parameterList().parameter()[i].parameterName().IDENTIFIER().GetText();
                    functionScope.DeclareVariable(paramName, arguments[i]);
                }
            }

            
            tablestack.Push(symbolTable);
            symbolTable = functionScope;

            ReturnValue result = (ReturnValue)VisitMethodBody(functionDefinition.Body);

            // Get the return value from the scope
            //var returnValue = currentScope.ResolveVariable("return");
            symbolTable = tablestack.Pop();

            // Execute the function and return the result
            return result.GetValue();
        }

        public override object VisitReturnStatement(GrammarParser.ReturnStatementContext context)
        {
            if (context.expression() == null)
            {
                return new ReturnValue(0);
            }
            else
            {
                var value = Visit(context.expression());       
                return new ReturnValue(value);
            }
        }

        public override object VisitMethodName([NotNull] GrammarParser.MethodNameContext context)
        {
            return base.VisitMethodName(context);
        }
        public override object VisitPrintStatement(GrammarParser.PrintStatementContext context) {
            var text = Visit(context.expression()).ToString();

            if (context.ChildCount > 4)
            {
                if (context.GetChild(4).ToString() != null)
                {
                    string path = context.GetChild(4).GetText();
                    path = path.Substring(1, path.Length - 2);


                    if (context.GetChild(6).GetText() == "0")
                    {
                        using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), path)))
                        {
                            writer.WriteLine(text);
                        }
                    }
                    else if (context.GetChild(6).GetText() == "1")
                    {
                        using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), path), append: true))
                        {
                            writer.WriteLine(text);
                        }
                    }
                    else
                    {
                        throw new Exception("Expected values 1 or 0");
                    }
                }
                else
                {
                    throw new Exception("Path was null");
                }
            }
            else
            {
                Console.WriteLine(text);
            }
            return 1;
        }
        public override object VisitParanthesesExpression( GrammarParser.ParanthesesExpressionContext context) {
            return Visit(context.expression());
        }
        public override object VisitIfBlock( GrammarParser.IfBlockContext context) {
            var condition = Visit(context.expression());
            //var ifBlock = VisitBlockas(context.blockas());
            //var elseBlock = context.elseIfBlock() != null ? VisitElseIfBlock(context.elseIfBlock()) : null;

            if ((bool)condition)
            {
                // Execute the if block
                Visit(context.block());
            }
            else
            {
                if (context.elseIfBlock() != null)
                {
                    // Execute the else block
                    Visit(context.elseIfBlock());
                }
            }

            return null;
        }

        public override object VisitBlock(GrammarParser.BlockContext context)
        {
            tablestack.Push(symbolTable);
            symbolTable = new SymbolTable(symbolTable);


            object value = base.VisitBlock(context);

            symbolTable = tablestack.Pop();
            return value;
        }

        public override object VisitElseIfBlock(GrammarParser.ElseIfBlockContext context) {
            if (context.ifBlock() != null)
            {
                return VisitIfBlock(context.ifBlock());
            }
            else if (context.block() != null)
            {
                return Visit(context.block());
            }

            return null;
        }











        private object Add(object left, object right)
        {
            if (left.GetType() == right.GetType())
            {
                if (left is int)
                    return (int)left + (int)right;

                if (left is float)
                    return (float)left + (float)right;

                if (left is string)
                    return $"{left}{right}";
            }

            throw new Exception("Can't add these values!");
        }

        private object Subtract(object left, object right)
        {
            if (left.GetType() == right.GetType())
            {
                if (left is int)
                    return (int)left - (int)right;

                if (left is float)
                    return (float)left - (float)right;
            }

            throw new Exception("Can't subtract these values!");
        }

        private string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }
    }

}
