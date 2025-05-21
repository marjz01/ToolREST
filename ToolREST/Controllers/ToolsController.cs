using Microsoft.AspNetCore.Mvc;
using ToolLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToolREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private ToolRepository _toolRepo;
        public ToolsController(ToolRepository repo)
        {
            _toolRepo = repo;
        }


        // GET: api/<ToolsController>
        [HttpGet]
        public ActionResult <IEnumerable<Tool>> GetAllTools()
        {
            var tools = _toolRepo.GetAll();
            if (tools == null || tools.Count ==0)
            {
                return NoContent(); //Returnere status kode 204
            }
            return Ok(tools); //Returnere status kode 200
        }

        // GET api/<ToolsController>/5
        [HttpGet("{id}")]
        public ActionResult<Tool> GetToolById(int id)
        {
            var tool = _toolRepo.GetById(id);
            if (tool == null)
            {
                return NotFound(); //Returnere status kode 404
            }
            return Ok(tool); //Returnere status kode 200
        }

        // POST api/<ToolsController>
        [HttpPost]
        public ActionResult<Tool> AddTool([FromBody] Tool newtool)
        {
            if (newtool == null)
            {
                return BadRequest(); //Returnere status kode 400
            }
            var tool = _toolRepo.Add(newtool);
            return CreatedAtAction(nameof(GetToolById), new { id = tool.Id }, tool); //Returnere status kode 201
        }

        //PUT api/<ToolsController>/5
        [HttpPut("{id}")]
        public ActionResult<Tool> Put(int id, [FromBody] Tool value)
        {
            Tool? tool = _toolRepo.Update(id, value);
            if (tool == null)
            {
                return NotFound(); //Returnere status kode 404
            }
            else
            {
                return Ok(tool); //Returnere status kode 200
            }
        }

        // DELETE api/<ToolsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
