using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raudonakmenis.obj.Debug
{
    public class SymbolTable
    {
        private readonly SymbolTable parent;
        private readonly Dictionary<string, object> variables;

        public SymbolTable()
        {
            variables = new Dictionary<string, object>();
            this.parent = null;
        }
        public SymbolTable(SymbolTable _parent)
        {
            variables = new Dictionary<string, object>();
            this.parent = _parent;
        }

        public void DeclareVariable(string variableName, object value)
        {
            if (isDeclared(variableName))
            {
                throw new Exception($"Variable '{variableName}' is already declared.");
            }
            variables.Add(variableName, value);
        }

        public bool isDeclared(string variableName)
        {
            if (variables.ContainsKey(variableName))
            {
                return true;
            }
            return parent != null && parent.isDeclared(variableName);
        }

        public void ChangeVariable(string variableName, object value)
        {
            if (!isDeclared(variableName))
            {
                throw new Exception($"Variable '{variableName}' is not defined.");
            }
            if (variables.ContainsKey(variableName))
            {
                variables[variableName] = value;
            }
            else
            {
                if (parent != null)
                {
                    parent.ChangeVariable(variableName, value);
                }
            }
        }
        public object ResolveVariable(string variableName)
        {
            if (!isDeclared(variableName))
            {
                throw new Exception($"Variable '{variableName}' is not defined.");
            }
            if (variables.ContainsKey(variableName))
            {
                return variables[variableName];
            }
            else
            {
                if (parent != null)
                    return parent.ResolveVariable(variableName);
                else
                    throw new Exception($"Variable '{variableName}' is not defined.");
            }
        }
    }
}
