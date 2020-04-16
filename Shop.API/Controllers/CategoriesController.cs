using Microsoft.AspNetCore.Mvc;
using Shop.API.Domain.Services;
using System.Collections.Generic;
using Shop.API.Domain.Models;
using System.Threading.Tasks;
using AutoMapper;
using Shop.API.Resources;
using Shop.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Shop.API.Resources.Communication;

namespace Shop.API.Controllers
{

    
    [Route("/api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

       
        [HttpGet]
        public async Task<ResponseResult> GetAllAsync()
        {
            var categories = await categoryService.ListAsync();
            var resources = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
            var result = new ResponseResult
            {
                Data = resources,
                Message = "",
                Success = true
            };
            return result;
        }


       
       
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = mapper.Map<SaveCategoryResource, Category>(resource);
            var categoryResponse = await categoryService.SaveAsync(category);
            var categoryResource = mapper.Map<Category, CategoryResource>(categoryResponse.Category);
            var result = categoryResponse.GetResponseResult(categoryResource);
            return Ok(result);

        }

        [Authorize(Roles="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = mapper.Map<SaveCategoryResource, Category>(resource);
            var categoryResponse = await categoryService.UpdateAsync(id, category);
            var categoryResource = mapper.Map<Category, CategoryResource>(categoryResponse.Category);
            var result = categoryResponse.GetResponseResult(categoryResource);
            return Ok(result);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) 
        {
            var categoryResponse = await categoryService.DeleteAsync(id);
            var categoryResource = mapper.Map<Category, CategoryResource>(categoryResponse.Category);
            var result = categoryResponse.GetResponseResult(categoryResource);
            return Ok(result);
        }

    }


}