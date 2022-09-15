using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YB.Data.Repository.Interface;
using YB.Data.ToDo;
using YB.Shared.Models;

namespace YB.Service.ToDoService
{
    public class ToDoService : IToDoService
    {
        private IGenericRepository<ToDo> _toDoRepo;
        public ToDoService(IGenericRepository<ToDo> toDo)
        {
            _toDoRepo = toDo;
        }
        public async Task<ResponseResult> GetAllAsync()
        {
            ResponseResult response = new()
            {
                HasError = false
            };

            try
            {

                var result = await _toDoRepo.QueryAll().AsQueryable()
                    .Select(x => new { Data = new { x.Description, x.CreatedDate, x.ModifiedDate, x.IsComplete } })
                    .ToListAsync();

                response.Data = result;
            }
            catch (Exception ex)
            {

                response.HasError = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseResult> InsertAsync(AddModel model)
        {
            ResponseResult response = new() { HasError = false };


            try
            {
                var entity = new ToDo
                {
                    Description = model.Description,
                };

                _toDoRepo.Insert(entity);
                await _toDoRepo.SaveAsync();

                response.Data = entity.Id;

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseResult> UpdateAsync(UpdateModel model)
        {
            ResponseResult response = new() { HasError = false };
            var entity = await _toDoRepo.QueryAll().AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
            try
            {
                entity.Description = model.Description;
                entity.IsComplete = model.IsCompleted;

                entity.ModifiedDate = DateTime.Now;

                _toDoRepo.Update(entity);
                await _toDoRepo.SaveAsync();

                response.Data = entity.Id;

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
