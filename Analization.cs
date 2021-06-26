using System;
using System.Collections.Generic;
using System.Linq;

namespace Analization
{
    /// <summary>
    /// 
    /// </summary>
    public class Analizationn
    {
        /// <summary>
        /// Return root of the tree
        /// </summary>
        public AnalizationNode<double> Root { get; }

        /// <summary>
        /// Return result of expression
        /// </summary>
        public double Result { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formula"></param>
        public Analizationn(string formula)
        {
            this.Root = CreateTree(formula);
            this.Result = Calculate(this.Root);
        }

        /// <summary>
        /// Create tree for analization expression
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns></returns>
        private AnalizationNode<double> CreateTree(string expression)
        {
            if (expression[0] == '-')
                expression = this.SetBreaks(expression);

            var operationList = new[] { '+', '-', '*', '/' , '^'};

            if (expression[0] == '(' && expression[expression.Length - 1] == ')' && !HasManyBreaks(expression))
            {
                expression = expression.Substring(1, expression.Length - 1);
                expression = expression.Substring(0, expression.Length - 1);
            }

            if (expression.Split(operationList).Length > 1 && !IsNumber(expression))
                foreach (var operation in operationList)
                {
                    var expresions = Split(expression, operation);
                    if (expresions.Count > 1)
                    {
                        var newNode = new AnalizationNode<double>(expression, operation);
                        foreach (var expresion in expresions)
                            newNode.AddChild(CreateTree(expresion));

                        return newNode;
                    }
                }
            else
                return new AnalizationNode<double>(expression.Replace(")", " "), default(char));
            return null;
        }
        /// <summary>
        /// Calculate expression
        /// </summary>
        /// <param name="node">Node, from which to begin calculating</param>
        /// <returns></returns>
        private double Calculate(AnalizationNode<double> node)
        {
            var res = 0.0;
            if (node.Childs != null)
            {
                foreach (var child in node.Childs)
                    res = res == 0 ? Calculate(child) : ApplyOperation((node.Operation == '*' || node.Operation == '/') && res == 0 ? 1.0 : res, node.Operation, Calculate(child));
                node.ChangeData(res);
                return node.Data;
            }
            else
            {
                node.ChangeData(float.Parse(node.formula));
                return node.Data;
            }
        }
        /// <summary>
        /// Return result of operation by two values
        /// </summary>
        /// <param name="value">First value</param>
        /// <param name="operation">Operation</param>
        /// <param name="secondValue">Second value</param>
        /// <returns></returns>
        private double ApplyOperation(double value, char operation, double secondValue)
        {
            switch (operation)
            {
                case '+': return value + secondValue;
                case '-': return value - secondValue;
                case '*': return value * secondValue;
                case '/': return value / secondValue;
                case '^': return Math.Pow(value, secondValue);
                default: return default(double);
            }
        }

        /// <summary>
        /// Split expression by the symbol and return collection expressions
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="symbol">Symbol by split</param>
        /// <returns></returns>
        private List<string> Split(string expression, char symbol)
        {
            var expresion = string.Empty;
            var expresions = new List<string>();
            for (var i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                {
                    var count = 0;
                    i++;
                    expresion += "(";
                    for (var j = i; j < expression.Length; j++)
                    {
                        if (expression[j] == '(')
                        {
                            count++;
                            expresion += "(";
                        }
                        else if (expression[j] == ')')
                        {
                            if (count == 0)
                            {
                                i = j;
                                break;
                            }
                            else
                            {
                                count--;
                                expresion += ")";
                            }
                        }
                        else
                            expresion += expression[j];
                    }
                }

                if (expression[i] != symbol)
                    expresion += expression[i];
                else if (!string.IsNullOrEmpty(expresion))
                {
                    expresions.Add(expresion);
                    expresion = string.Empty;
                }
                else
                    expresion += expression[i];
            }
            expresions.Add(expresion);
            return expresions;
        }
        /// <summary>
        /// Cheak that expression has many polynomial
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns></returns>
        private bool HasManyBreaks(string expression)
        {
            var count = 0;
            var res = 0;
            for (var i = 0; i < expression.Length; i++)
            {
                if (expression[i] != '(') continue;

                i++;
                for (var j = i; j < expression.Length; j++)
                {
                    if (expression[j] == '(')
                        count++;
                    else if (expression[j] == ')')
                    {
                        if (count == 0)
                        {
                            res++;
                            i = j;
                            break;
                        }

                        count--;
                    }
                }
            }

            return res > 1;
        }
        /// <summary>
        /// Set break to first number if their minus
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns></returns>
        private string SetBreaks(string expression)
        {
            var result = expression;
            var symbols = new[] { '+', '-', '*', '/', '(', ')' };

            result = '(' + result;
            for (var i = 2; i < expression.Length; i++)
                if (symbols.Contains(result[i]))
                {
                    result = result.Substring(0, i) + ')' + result.Substring(i);
                    return result;
                }

            return result;
        }
        /// <summary>
        /// Check, if the expression is a number
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns></returns>
        private bool IsNumber(string expression)
        {
            var symbols = new[] { '+', '-', '*', '/', '(', ')' };

            if (!new[] { '(', ')' }.Contains(expression[0]) && expression[0] == '-')
            {
                for (var i = 2; i < expression.Length; i++)
                    if (symbols.Contains(expression[i]))
                        return false;
            }
            else
                return false;


            return true;
        }
    }
}
