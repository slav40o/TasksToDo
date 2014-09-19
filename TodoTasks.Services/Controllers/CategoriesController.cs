namespace TodoTasks.Services.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;
    using System.Web.Http;

    using Antlr.Runtime.Tree;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using TodoTasks.Data;
    using TodoTasks.Models;
    using TodoTasks.Services.Models;

    [Authorize]
    public class CategoriesController : ApiController
    {
        private readonly ITodoTasksData data;

        public CategoriesController()
            : this(new TodoTasksData())
        {

        }

        public CategoriesController(ITodoTasksData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var userId = User.Identity.GetUserId();
            var categories = this.data
                .Categories
                .All()
                .Where(c => c.UserId == userId)
                .Select(CategoryModel.FromCategory);

            return Ok(categories);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var userId = User.Identity.GetUserId();
            var category = this.data
                .Categories
                .All()
                .Where(c => c.UserId == userId)
                .Where(c => c.Id == id)
                .Select(CategoryModel.FromCategory)
                .FirstOrDefault();

            if (category == null)
            {
                return BadRequest("Category does not exist - invalid id");
            }

            return Ok(category);
        }

        [HttpPost]
        public IHttpActionResult Create(CategoryModel category)
        {
            var userId = User.Identity.GetUserId();
            var newCategory = new Category 
            {
                Name = category.Name,
                UserId = userId
            };
            this.data.Categories.Add(newCategory);
            this.data.SaveChanges();

            var categoryDataModel =
                this.data.Categories.All()
                    .Where(c => c.Id == newCategory.Id)
                    .Select(CategoryModel.FromCategory)
                    .FirstOrDefault();

            return this.Ok(categoryDataModel);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, CategoryModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCategory = this.data.Categories.All().FirstOrDefault(c => c.Id == id);
            if (existingCategory == null)
            {
                return BadRequest("Such category does not exists!");
            }

            existingCategory.Name = category.Name;
            this.data.SaveChanges();

            category.Id = id;
            return Ok(category);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingCategory = this.data.Categories.All().FirstOrDefault(c => c.Id == id);
            if (existingCategory == null)
            {
                return BadRequest("Such category does not exists!");
            }

            this.data.Categories.Delete(existingCategory);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
