using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Services
{
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _iMapper;

        public MemoService(IUnitOfWork iUnitOfWork, IMapper iMapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _iMapper = iMapper;
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameters parameter)
        {
            try
            {
                var repository = _iUnitOfWork.GetRepository<Memo>();
                var todos = await repository.GetPagedListAsync(predicate:
                    x => string.IsNullOrWhiteSpace(parameter.SearchString) ? true : x.Title.Contains(parameter.SearchString),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new ApiResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = _iUnitOfWork.GetRepository<Context.Memo>();
                var Memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ApiResponse(true, Memo);
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }

        public async Task<ApiResponse> AddAsync(MemoDto model)
        {
            try
            {
                var Memo = _iMapper.Map<Context.Memo>(model);
                await _iUnitOfWork.GetRepository<Context.Memo>().InsertAsync(Memo);
                if (await _iUnitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, Memo);
                }

                return new ApiResponse("添加数据失败");
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(MemoDto model)
        {
            try
            {
                var DbMemo = _iMapper.Map<Context.Memo>(model);
                var repository = _iUnitOfWork.GetRepository<Context.Memo>();
                var Memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(DbMemo.Id));
                Memo.Title = DbMemo.Title;
                Memo.Content = DbMemo.Content;
                Memo.UpdateDate = DateTime.Now;
                repository.Update(Memo);
                if (await _iUnitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, Memo);
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
                var repository = _iUnitOfWork.GetRepository<Context.Memo>();
                var Memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(Memo);
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
