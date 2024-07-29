using Microsoft.AspNetCore.Mvc;
using Vsoft_share_document.DTO;
using Vsoft_share_document.InterfaceBUS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vsoft_share_document.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentWatchersController : ControllerBase
    {
        private readonly IDocumentWatchers_BUS _documentWatchers_BUS;
        public DocumentWatchersController(IDocumentWatchers_BUS documentWatchers_BUS) { 
            _documentWatchers_BUS = documentWatchers_BUS;
        }
        // GET: api/<DocumentWatchersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _documentWatchers_BUS.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<DocumentWatchersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var data = await _documentWatchers_BUS.GetById(id);
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return Ok($"Không tồn tại chia sẻ tài liệu có Id = {id}");
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<DocumentWatchersController>
        [HttpPost] 
        public async Task<IActionResult> Post([FromBody] ENT_CreateDocumentWatchers body)
        {
            try
            {
                string msgErr = await _documentWatchers_BUS.Create(body);
                if (msgErr == null || msgErr == "") {
                    return Ok("Chia sẻ tài liệu cho người dùng thành công");
                }
                return BadRequest(msgErr);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<DocumentWatchersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DocumentWatchersController>/guid-Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                string msgErr = await _documentWatchers_BUS.DeleteById(id);
                if (msgErr == null || msgErr == "")
                {
                    return Ok("Xóa chia sẻ tài liệu thành công");
                }
                return BadRequest(msgErr);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
