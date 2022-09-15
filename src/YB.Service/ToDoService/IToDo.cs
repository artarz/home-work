using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YB.Shared.Models;

namespace YB.Service.ToDoService
{
    public  interface IToDoService
    {
        Task<ResponseResult> GetAllAsync();
        Task<ResponseResult> InsertAsync(AddModel model);
        Task<ResponseResult> UpdateAsync(UpdateModel model);
        Task<ResponseResult> DeleteAsync(int Id);
        Task<ResponseResult> SetCompletedAsync(int id);
        Task<ResponseResult> FindAsync(string keywoard);
    }
}
