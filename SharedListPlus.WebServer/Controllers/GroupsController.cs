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
    public class GroupsController : ControllerBase
    {
        private readonly SharedListPlusWebServerContext _context;

        public GroupsController(SharedListPlusWebServerContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroup()
        {
            return await _context.Group.ToListAsync();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(long id)
        {
            var @group = await _context.Group.FindAsync(id);

            if (@group == null)
            {
                return NotFound();
            }

            return @group;
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(long id, Group @group)
        {
            if (id != @group.GroupId)
            {
                return BadRequest();
            }

            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group @group)
        {
            _context.Group.Add(@group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = @group.GroupId }, @group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(long id)
        {
            var @group = await _context.Group.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Group.Remove(@group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(long id)
        {
            return _context.Group.Any(e => e.GroupId == id);
        }
        
        //this some good shit
        [Route("{id}/List/create/")]
        [HttpPost]
        public async Task<int> CreateGroupShoppingItem(long id, ListItem item)
		{
            var group = _context.Group.Find(id);
            group.ListItems.Add(item);
            var response = await _context.SaveChangesAsync();
            return response;
        }

        [Route("{id}/people/create/")]
        [HttpPost]
        public async Task<int> CreateGroupMember(long id, Person person)
		{
            var family = _context.Group.Find(id);
            family.GroupMembers.Add(person);
            var response = await _context.SaveChangesAsync();
            return response;
		}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(long id, Group group)
		{
            if (id != group.GroupId)
			{
                return BadRequest();
			}
            _context.Update(group);

			try
			{
                await _context.SaveChangesAsync();
			}
            catch(DbUpdateConcurrencyException)
			{
                if(!GroupExists(id))
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
    }
}
