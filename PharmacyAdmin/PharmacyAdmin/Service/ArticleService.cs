using PharmacyAdmin.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PharmacyAdmin.Service
{
    public interface IArticleService
    {
        Task<int> Create(Article article);
        Task<int> Delete(int Id);
        Task<int> Count(string search);
        Task<int> Update(Article article);
        Task<Article> GetById(int Id);
        Task<List<Article>> ListAll(int skip, int take, string orderBy, string direction, string search);
    }

    public class ArticleService : IArticleService
    {
        private readonly IDapperService _dapperManager;

        public ArticleService(IDapperService dapperManager)
        {
            this._dapperManager = dapperManager;
        }

        public Task<int> Create(Article article)
        {
            var Params = new DynamicParameters();
            Params.Add("Title", article.Title, DbType.String);
            var articleId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_Article]",
                            Params,
                            commandType: CommandType.StoredProcedure));
            return articleId;
        }

        public Task<Article> GetById(int id)
        {
            var article = Task.FromResult(_dapperManager.Get<Article>($"select * from [Article] where ID = {id}", null,
                    commandType: CommandType.Text));
            return article;
        }

        public Task<int> Delete(int id)
        {
            var deleteArticle = Task.FromResult(_dapperManager.Execute($"Delete [Article] where ID = {id}", null,
                    commandType: CommandType.Text));
            return deleteArticle;
        }

        public Task<int> Count(string search)
        {
            var totArticle = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [Article] WHERE Title like '%{search}%'", null,
                    commandType: CommandType.Text));
            return totArticle;
        }

        public Task<List<Article>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            var articles = Task.FromResult(_dapperManager.GetAll<Article>
                ($"SELECT * FROM [Article] WHERE Title like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text));
            return articles;
        }

        public Task<int> Update(Article article)
        {
            var Params = new DynamicParameters();
            Params.Add("Id", article.ID);
            Params.Add("Title", article.Title, DbType.String);

            var updateArticle = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_Article]",
                            Params,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }
    }
}
