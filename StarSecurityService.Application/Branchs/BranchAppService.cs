﻿using StarSecurityService.Application.Commons.Dto;
using StarSecurityService.ApplicationCore.Entities;
using StarSecurityService.ApplicationCore.InterFaces;
using StarSecurityService.EntityFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSecurityService.Application.Branchs
{
    public interface IBrachAppService {
        Task<IEnumerable<Branch>> GetAllBranchs();
        Task<List<ComboboxCommonDto>> GetAllForCombobox();
        Task<Branch> AddAsync(Branch branch);
        Task<Branch> FirstOrDefaultAsync(int? Id);
        Task UpdateAsync(Branch branch);
        Task DeleteAsync(int Id);
            Task<int> AddBranch(Branch branch);
            Task<Branch>  GetByIdBranch(int idBrach);
    }
    public class BranchAppService : IBrachAppService
    {
        private readonly IAsyncRepository<Branch> _branchRepository;
        public BranchAppService()
        {
            _branchRepository = new EfRepository<Branch>(new StarServiceDbContext());
        }
        public async Task<List<ComboboxCommonDto>> GetAllForCombobox()
        {
            return (await _branchRepository.GetAllAsync()).Select(x => new ComboboxCommonDto
            {
                Lable = x.Name,
                Value = x.Id
            }).ToList();
        }
        public async Task<IEnumerable<Branch>> GetAllBranchs()
        {
            var iQueryableEmployee = await _branchRepository.GetAllAsync();
            return iQueryableEmployee.AsEnumerable();
        }
    }
}
