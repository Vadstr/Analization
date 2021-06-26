using System.Collections.Generic;

namespace Analization
{
    public class AnalizationNode<T> : INode<T>
    {
        public string formula = string.Empty;
        private T data = default(T);
        private char operation = default(char);
        private List<AnalizationNode<T>> childs = default(List<AnalizationNode<T>>);
        private bool variable = false;

        public T Data { get { return this.data; } }
        public List<AnalizationNode<T>> Childs { get { return this.childs; } }
        public char Operation { get { return this.operation; } }
        public bool HasVariable { get { return this.variable; } }

        public AnalizationNode(string formula, char operation)
        {
            this.formula = formula;
            this.operation = operation;
        }
        public AnalizationNode(T value, char operation)
        {
            this.data = value;
            this.operation = operation;
        }

        public void AddChild(AnalizationNode<T> child)
        {
            if (this.childs == default(List<AnalizationNode<T>>))
                this.childs = new List<AnalizationNode<T>>();
            this.childs.Add(child);
        }
        public void ChangeData(T newDate) => this.data = newDate;
    }
}
