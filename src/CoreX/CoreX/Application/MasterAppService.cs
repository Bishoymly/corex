using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace CoreX.Application
{
    public class MasterAppService<TEntity, TEntityDto> : AsyncCrudAppService<TEntity, TEntityDto> 
        where TEntity : class, IEntity<int>
        where TEntityDto : IEntityDto<int>
    {
        public MasterAppService(IRepository<TEntity> repository) : base(repository)
        {
            
        }
    }
}
