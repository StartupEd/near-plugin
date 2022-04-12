using StartupEd.UX.One;
using System;
using System.Collections.Generic;

namespace StartupEd.UX.Incubation.Pages.InProgress
{
    public partial class ToDo : BaseComponent
    {
        public string todoInput { get; set; }
        public List<string> todos = new List<string>();
        public void AddTodo()
        {
            Console.WriteLine(todoInput);
            todos.Add(todoInput);
        }
        public void RemoveTodo(int index)
        {
            todos.RemoveAt(index);
        }
    }
}
