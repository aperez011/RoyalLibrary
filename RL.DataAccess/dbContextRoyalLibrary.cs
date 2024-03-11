using Microsoft.EntityFrameworkCore;
using RL.Entity;
using RL.Utility;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace RL.DataAccess
{
    public class dbContextRoyalLibrary : DbContext
    {
        public dbContextRoyalLibrary(DbContextOptions<dbContextRoyalLibrary> options)
        : base(options)
        {
        }

        DbSet<User> Users { get; set; }
        DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Library;Trusted_Connection=True;");
        }

        public async Task<HashSet<T>> GetAllAsync<T>() where T : BaseProperties, new()
        {
            try
            {
                var data = this.Set<T>().ToHashSet();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : BaseProperties, new()
        {
            try
            {
                var data = await this.Set<T>().SingleOrDefaultAsync(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HashSet<T>> GetByAsync<T>(Expression<Func<T, bool>> condition) where T : BaseProperties, new()
        {
            try
            {
                var data = await Task.FromResult(this.Set<T>().Where(condition).ToHashSet());
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<OperationResult<int>> PostAsync<T>(T entity) where T : BaseProperties, new()
        {
            try
            {
                await this.Set<T>().AddAsync(entity);
                _ = await this.SaveChangesAsync();

                return OperationResult<int>.Success(entity.Id);
            }
            catch (Exception ex)
            {
                return OperationResult<int>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult> PustAsync<T>(T entity) where T : BaseProperties, new()
        {
            try
            {
                var orgEnt = await this.Set<T>().FirstOrDefaultAsync(c => c.Id == entity.Id);

                if (orgEnt == null)
                    return OperationResult.Fail($"The id: '{entity.Id}' doesn't exists on {nameof(T)}.");

                bool changes = await this.RowUpdate(orgEnt, entity);

                if(changes) _ = await this.SaveChangesAsync();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        public async Task<OperationResult> DeleteAsync<T>(int id) where T : BaseProperties
        {
            try
            {

                var orgEnt = await this.Set<T>().SingleOrDefaultAsync(c => c.Id == id);

                if (orgEnt == null)
                    return OperationResult.Fail($"The id: '{id}' doesn't exists on {nameof(T)}.");

                _ = this.Remove(orgEnt);

                var data = await this.SaveChangesAsync();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }


        #region [ Internal Methods]
        private async Task<bool> RowUpdate<T>(T orgEntity, T newEntity) where T : BaseProperties
        {
            string[] invalidProperties = { "Id", "LogDate" };

            try
            {
                //Buscando las propiedades que sean GUID
                bool propertyChange = false;
                string dbOriginal = JsonSerializer.Serialize(orgEntity);
                string newValues = JsonSerializer.Serialize(newEntity);

                //Get class properties can be updated
                Type entity = orgEntity.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(entity.GetProperties().Where(p => !invalidProperties.Contains(p.Name)));

                foreach (PropertyInfo prop in props)
                {
                    object dbValue = prop.GetValue(orgEntity, null);
                    object appValue = prop.GetValue(newEntity, null);

                    if (appValue != null && !dbValue.Equals(appValue))
                    {
                        entity.GetProperty(prop.Name).SetValue(orgEntity, appValue);
                        propertyChange = true;
                    }
                }

                return propertyChange;
            }
            catch
            {
                return false;
            }
        }

        #endregion [ Internal Methods]

    }
}
