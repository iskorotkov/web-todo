using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTodo.Db;
using WebTodo.Models;

namespace WebTodo.Controllers
{
    [ApiController, Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<TodoItem[]>> Get()
        {
            return await _context.TodoItems.ToArrayAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> Get(long id)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post(TodoItem item)
        {
            var created = await _context.TodoItems.AddAsync(item);
            try
            {
                await _context.SaveChangesAsync();
                return Created($"todo/{created.Entity.Id}", created.Entity);
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
                return Conflict();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(item);

            try
            {
                await _context.SaveChangesAsync();
                return Accepted();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
                return Conflict();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(TodoItem item)
        {
            _context.TodoItems.Update(item);

            try
            {
                await _context.SaveChangesAsync();
                return Accepted();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
                return Conflict();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
    }
}
