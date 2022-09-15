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
                    .Select(x => new { Data = new {x.Id,  x.Description, x.CreatedDate, x.ModifiedDate, x.IsComplete } })
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

            if (entity is null)
            {
                response.Message = "Unable to find an item";
                return response;
            }

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

        public async  Task<ResponseResult> DeleteAsync(int Id)
        {

            ResponseResult response = new() { HasError = false };
            try
            {
                _toDoRepo.Delete(Id);
                await _toDoRepo.SaveAsync();

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public async  Task<ResponseResult> SetCompletedAsync(int Id)
        {
            ResponseResult response = new() { HasError = false };

            
               
            try
            {
                var entity = await _toDoRepo.QueryAll().AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
                if(entity is null)
                {
                    response.Message = "Unable to find an item";
                    return response;
                }

                if (entity.IsComplete == true)
                    entity.IsComplete = false;
                else
                    entity.IsComplete = true;


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

        public async Task<ResponseResult> FindAsync(string keywoard)
        {
            ResponseResult response = new() { HasError = false };
            try
            {
            var result = await _toDoRepo.QueryAll().AsNoTracking()
                .Where(x => x.Description.Contains(keywoard))
                .Select(x=> new {Data = new {x.Id, x.Description, x.CreatedDate }})
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
    }
}
