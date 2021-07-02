using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCDemo.Data;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private ApplicationDbContext _db;
        public CategoryAPIController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return _db.Category.ToList();
        }
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _db.Category.Find(id);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _db.Category.Remove(Get(id));
            _db.SaveChanges();
        }
        [HttpPost]
        public void Create([FromBody]Category category)
        {
            var temp = new Category()
            {
                Name = category.Name,
                DisplayOrder = category.DisplayOrder
            };
            _db.Category.Add(temp);
            _db.SaveChanges();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            Category temp = _db.Category.Find(id);
            if (temp == null)
            {
                return BadRequest();
            }

            temp.Name = category.Name;
            temp.DisplayOrder = category.DisplayOrder;

            _db.SaveChanges();

            return NoContent();
        }
    }
}
