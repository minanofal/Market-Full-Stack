using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers.MarketControler
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        

        [HttpGet]
        public async Task<IActionResult> getallproducts()
        {
            var products = await  _unitOfWork.products.GetProducts();
            if (products == null)
            {
                return Ok(new List<ProductDisplayDto>());
            }
            return Ok(products.OrderByDescending(p => p.Updated).OrderByDescending(p => p.Created));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getproduct(Guid id)
        {
            var products = await _unitOfWork.products.GetProducts(p=>p.Id == id);
            if (!products.Any())
            {
                return Ok(new List<ProductDisplayDto>());
            }
            return Ok(products.First());
        }

        [HttpGet("Category/{id}")]
        public async Task<IActionResult> getCategoryproducts(Guid id)
        {
            var products = await _unitOfWork.products.GetProducts(p => p.CategoryId== id);
            if (!products.Any())
            {
                return Ok(new List<ProductDisplayDto>());
            }
            return Ok(products.OrderByDescending(p => p.Updated).OrderByDescending(p => p.Created));
        }

        [HttpGet("type/company/{tid}/{cid}")]
        public async Task<IActionResult> gettypecompanyproducts(Guid tid , Guid cid)
        {
            var products = await _unitOfWork.products.GetProducts(p => p.TypeId == tid && p.CompanyId == cid);
            if (!products.Any())
            {
                return Ok(new List<ProductDisplayDto>());
            }
            return Ok(products.OrderByDescending(p => p.Updated).OrderByDescending(p => p.Created));
        }

        [HttpGet("Type/{id}")]
        public async Task<IActionResult> gettypeproducts(Guid id)
        {
            var products = await _unitOfWork.products.GetProducts(p => p.TypeId == id);
            if (!products.Any())
            {
                return Ok(new List<ProductDisplayDto>());
            }
            return Ok(products.OrderByDescending(p => p.Updated).OrderByDescending(p => p.Created));
        }

        [HttpGet("company/{id}")]
        public async Task<IActionResult> getcompanyproducts(Guid id)
        {
            var products = await _unitOfWork.products.GetProducts(p => p.CompanyId == id);
            if (!products.Any())
            {
                return Ok(new List<ProductDisplayDto>());
            }
            return Ok(products.OrderByDescending(p=>p.Updated).OrderByDescending(p=>p.Created));
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _unitOfWork.products.DeleteProduct(id);
            if (result.Message != null)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Id);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPut("UpdateProdduct/{id}")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductUpdateDto dto , Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data = await _unitOfWork.products.UpdateProduct(dto, id);
            if (data.Message != null)
            {
                return BadRequest(data.Message);

            }
            return Ok(data);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductFormDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data = await _unitOfWork.products.CreateProduct(dto);
            if (data.Message != null)
            {
                return BadRequest(data.Message);
            }
            return Ok(data);
        }
    }
}
