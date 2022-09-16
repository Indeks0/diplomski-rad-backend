using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Repository.Common;

namespace WAZOI.Backend.Repository
{
    public class SubjectGradingCriteriaRepository : GenericRepository<ISubjectGradingCriteriaDto, SubjectGradingCriteria>, ISubjectGradingCriteriaRepository
    {
        #region Constructors

        public SubjectGradingCriteriaRepository(AppDataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ISubjectGradingCriteriaDto>> GetAllBySubjectAsync(Guid subjectId)
        {
            var criteria = _context.SubjectGradingCriteria.Where(x => x.SubjectId == subjectId).ToList();
            return _mapper.Map<List<ISubjectGradingCriteriaDto>>(criteria);
        }

        #endregion Methods
    }
}