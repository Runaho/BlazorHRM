using System;
using BlazorHRM.Shared;

namespace BlazorHRM.Server.Models
{
    public class JobCategoryRepository : IJobCategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public JobCategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public JobCategory AddJobCategory(JobCategory jobCategory)
        {
            var addedEntity = _appDbContext.JobCategories.Add(jobCategory);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }



        public IEnumerable<JobCategory> GetAllJobCategories()
        {
            return _appDbContext.JobCategories;
        }

        public JobCategory GetJobCategoryById(int jobCategoryId)
        {
            return _appDbContext.JobCategories.FirstOrDefault(c => c.JobCategoryId == jobCategoryId);
        }

        public JobCategory UpdateJobCategory(JobCategory JobCategory)
        {
            var foundJobCategory = _appDbContext.JobCategories.FirstOrDefault(f => f.JobCategoryId == JobCategory.JobCategoryId);

            if (foundJobCategory != null)
            {
                foundJobCategory.JobCategoryName = JobCategory.JobCategoryName;

                _appDbContext.SaveChanges();

                return foundJobCategory;
            }

            return null;
        }

        public bool DeleteJobCategory(int jobCategoryId)
        {
            var foundJobCategory = _appDbContext.JobCategories.FirstOrDefault(f => f.JobCategoryId == jobCategoryId);

            if (foundJobCategory == null)
            {
                return false;
            }

            _appDbContext.Remove(foundJobCategory);
            _appDbContext.SaveChanges();
            return true;
        }
    }
}