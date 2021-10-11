using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedListPlus.Library.Models;
using SharedListPlus.WebServer.Data;

namespace SharedListPlus.WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListItemsController : ControllerBase
    {
        private readonly SharedListPlusWebServerContext _context;

        public ListItemsController(SharedListPlusWebServerContext context)
        {
            _context = context;
        }

        // GET: api/ListItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListItem>>> GetListItem()
        {
            return await _context.ListItem.ToListAsync();
        }

        // GET: api/ListItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListItem>> GetListItem(long id)
        {
            var listItem = await _context.ListItem.FindAsync(id);

            if (listItem == null)
            {
                return NotFound();
            }

            return listItem;
        }

        // PUT: api/ListItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListItem(long id, ListItem listItem)
        {
            if (id != listItem.ListItemId)
            {
                return BadRequest();
            }

            _context.Entry(listItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ListItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ListItem>> PostListItem(ListItem listItem)
        {
            _context.ListItem.Add(listItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListItem", new { id = listItem.ListItemId }, listItem);
        }

        // DELETE: api/ListItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListItem(long id)
        {
            var listItem = await _context.ListItem.FindAsync(id);
            if (listItem == null)
            {
                return NotFound();
            }

            _context.ListItem.Remove(listItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListItemExists(long id)
        {
            return _context.ListItem.Any(e => e.ListItemId == id);
        }
    }
}
