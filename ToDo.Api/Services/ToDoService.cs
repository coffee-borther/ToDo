using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _iMapper;

        public ToDoService(IUnitOfWork iUnitOfWork, IMapper iMapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _iMapper = iMapper;
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameters queryParameters)
        {
            try
            {
                var repository = _iUnitOfWork.GetRepository<Context.ToDo>();
                var todos = await repository.GetPagedListAsync(predicate:
                    x => string.IsNullOrWhiteSpace(queryParameters.SearchString) || x.Title.Contains(queryParameters.SearchString),
                    pageIndex: queryParameters.PageIndex,
                    pageSize: queryParameters.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new ApiResponse(true, todos);
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync(ToDoParameters queryParameters)
        {
            try
            {
                var repository = _iUnitOfWork.GetRepository<Context.ToDo>();
                var todos = await repository.GetPagedListAsync(predicate:
                    x => (string.IsNullOrWhiteSpace(queryParameters.SearchString) || x.Title.Contains(queryParameters.SearchString)) && (queryParameters.Status == null || x.Status.Equals(queryParameters.Status)),
                    pageIndex: queryParameters.PageIndex,
                    pageSize: queryParameters.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new ApiResponse(true, todos);
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }

        public async Task<ApiResponse> summary()
        {
            try
            {
                var todos = await _iUnitOfWork.GetRepository<Context.ToDo>().GetAllAsync(
                    orderBy: source => source.OrderByDescending(t => t.CreateDate));
                var memos = await _iUnitOfWork.GetRepository<Memo>().GetAllAsync(
                    orderBy: source => source.OrderByDescending(t => t.CreateDate));
                SummaryDto summary = new SummaryDto();
                summary.Sum = todos.Count;
                summary.CompletedCount = todos.Where(t => t.Status == 1).Count();
                summary.CompletedRatio = (summary.CompletedCount / (double) summary.Sum).ToString("0%");
                summary.MemoCount = memos.Count;
                summary.ToDoList =
                    new ObservableCollection<ToDoDto>(_iMapper.Map<List<ToDoDto>>(todos.Where(t => t.Status == 0)));
                summary.MemoList = new ObservableCollection<MemoDto>(_iMapper.Map<List<MemoDto>>(memos));
                return new ApiResponse(true, summary);
            }
            catch (Exception e)
            {
                return new ApiResponse(false, "");
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = _iUnitOfWork.GetRepository<Context.ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ApiResponse(true, todo);
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }

        public async Task<ApiResponse> AddAsync(ToDoDto model)
        {
            try
            {
                var todo = _iMapper.Map<Context.ToDo>(model);
                await _iUnitOfWork.GetRepository<Context.ToDo>().InsertAsync(todo);
                if (await _iUnitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, todo);
                }

                return new ApiResponse("添加数据失败");
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(ToDoDto model)
        {
            try
            {
                var DbToDo = _iMapper.Map<Context.ToDo>(model);
                var repository = _iUnitOfWork.GetRepository<Context.ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(DbToDo.Id));
                todo.Title = DbToDo.Title;
                todo.Content = DbToDo.Content;
                todo.Status = DbToDo.Status;
                todo.UpdateDate = DateTime.Now;
                repository.Update(todo);
                if (await _iUnitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, todo);
                }

                return new ApiResponse("更新数据失败");
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = _iUnitOfWork.GetRepository<Context.ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(todo);
                if (await _iUnitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, "");
                }

                return new ApiResponse("删除数据失败");
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }
    }
}