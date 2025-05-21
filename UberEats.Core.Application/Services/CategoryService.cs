using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.Category;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(CategoryViewModel category)
        {
            Category entity = new();
            entity.Id = category.Id;
            entity.Name = category.Name;
            entity.ImgUrl = category.ImgUrl;


            await _categoryRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<List<CategoryViewModel>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var result =  categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ImgUrl = c.ImgUrl
            }).ToList();

            return result;
        }

        public async Task<CategoryViewModel> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if(category == null)
                return null;

            CategoryViewModel categoryVm = new();
            categoryVm.Id = category.Id;
            categoryVm.Name = category.Name;
            categoryVm.ImgUrl = category.ImgUrl;

            return categoryVm;
        }

        public async Task UpdateAsync(CategoryViewModel category)
        {
            Category entity = new();
            entity.Id = category.Id;
            entity.Name = category.Name;
            entity.ImgUrl = category.ImgUrl;

            await _categoryRepository.UpdateAsync(entity);
        }
    }
}
