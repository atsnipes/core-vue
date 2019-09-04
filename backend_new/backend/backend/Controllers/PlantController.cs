using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/plants")]
    [ApiController]
    [Authorize]
    public class PlantController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PlantController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plant>>> GetAsync()
        {
            var plants = await _dbContext.PlantRecords.ToListAsync();
            return Ok(plants);
        }

        // GET api/plants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> Get(string id)
        {
            var plant = await _dbContext.PlantRecords.FindAsync(id);
            return Ok(plant);
        }

        // POST api/plants
        [HttpPost]
        public async Task<ActionResult> Post(Plant plantToCreate)
        {
            plantToCreate.CreatedDate = DateTime.Now;
            plantToCreate.UpdatedDate = DateTime.Now;
            await _dbContext.AddAsync(plantToCreate);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("Post", plantToCreate);
        }

        // PUT api/plants/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, Plant plantToUpdate)
        {
            var exists = await _dbContext.PlantRecords.AnyAsync(f => f.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            plantToUpdate.UpdatedDate = DateTime.Now;
            _dbContext.PlantRecords.Update(plantToUpdate);

            await _dbContext.SaveChangesAsync();

            return Ok();

        }

        // DELETE api/plants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var plantToDelete = await _dbContext.PlantRecords.FindAsync(id);
            if (plantToDelete == null)
            {
                return NotFound();
            }

            var entity = await _dbContext.PlantRecords.FindAsync(id);

            _dbContext.PlantRecords.Remove(entity);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
