﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MargotKumelingTools.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private static ConcurrentBag<Todo> todos = new ConcurrentBag<Todo> {
            new Todo { Id = Guid.NewGuid(), Description = "Learn Vue" }
        };

        [HttpGet()]
        public IEnumerable<Todo> GetTodos()
        {
            return todos.Where(t => !t.Done);
        }

        [HttpPost()]
        public Todo AddTodo([FromBody]Todo todo)
        {
            todo.Id = Guid.NewGuid();
            todo.Done = false;
            todos.Add(todo);
            return todo;
        }

        [HttpDelete("{id}")]
        public ActionResult CompleteTodo(Guid id)
        {
            var todo = todos.SingleOrDefault(t => t.Id == id);
            if (todo == null) return NotFound();

            todo.Done = true;
            return StatusCode(204);
        }
    }
    public class Todo
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}
