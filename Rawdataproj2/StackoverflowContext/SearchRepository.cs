using DataRepository;
using DataRepository.Dto.SearchDto;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackoverflowContext
{
    public class SearchRepository : ISearchRepository
    {
        public async Task<IList<SearchResultDto>> MatchAll(string query, PagingInfo pagingInfo, string method, string sortby, string orderby)
        {
            using (var db = new StackoverflowDbContext())
            {
                var result = new List<SearchResultDto>();

                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Add("@query", DbType.String);
                cmd.Parameters["@query"].Value = query;

                cmd.CommandText = "call MatchAll(@query)";

                var reader = cmd.ExecuteReader();

                while (await reader.ReadAsync())
                {
                    result.Add(new SearchResultDto
                    {
                        Id = (int)reader["id"],
                        Rank = (decimal)reader["rank"],
                        Body = (string)reader["body"]
                    });
                }

                // TODO: perhaps move to stored procedure AND/OR create helper for methods ? 
                if(!string.IsNullOrEmpty(orderby) && orderby == "\"asc\"" || orderby == "\"desc\"")
                {
                    result = orderby == "\"desc\""
                    ? result.OrderBy(x => x.Rank).ToList()
                    : result.OrderBy(x => x.Rank).Reverse().ToList();
                }

                // TODO: fetch correct data and implement it
                //if (!string.IsNullOrEmpty(sortby))
                //{
                //    result = result.Where(date is within sortby date);
                //}
                
                return result;
            }
        }


        public async Task<IList<SearchResultDto>> Bestmatch(string query, PagingInfo pagingInfo, string method, string sortby, string orderby)
        {
            using (var db = new StackoverflowDbContext())
            {
                var result = new List<SearchResultDto>();

                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.Add("@pageSize", DbType.Int32);
                cmd.Parameters.Add("@pageNumber", DbType.Int32);
                cmd.Parameters.Add("@query", DbType.String);

                cmd.Parameters["@pageSize"].Value = pagingInfo.PageSize;
                cmd.Parameters["@pageNumber"].Value = pagingInfo.Page;
                cmd.Parameters["@query"].Value = query;

                cmd.CommandText = "call bestmatch(@query, @pageSize, @pageNumber)";

                var reader = cmd.ExecuteReader();

                while (await reader.ReadAsync())
                {

                    Console.WriteLine("{0}, {1}", reader.GetInt32(0), reader.GetInt32(1));
                    result.Add(new SearchResultDto
                    {
                        Id = (int)reader["id"],
                        Rank = (decimal)reader["rank"]
                    });
                }
                

                return result;
            }
        }

        public async Task<IList<BestMatchRankedDto>> BestMatchRanked(string query)
        {
            using (var db = new StackoverflowDbContext())
            {

                var result = new List<BestMatchRankedDto>();

                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Add("@query", DbType.String);
                cmd.Parameters["@query"].Value = query; 
                cmd.CommandText = "call BestMatchRanked(@query)";

                var reader = cmd.ExecuteReader(); 
                while (await reader.ReadAsync())
                {
                    result.Add(new BestMatchRankedDto
                    {
                        Id = (int)reader["id"],
                        Rank = (decimal)reader["rank"] 
                    });
                }

                return result;
            }
        }

        public async Task<IList<BestMatchWeightedDto>> BestMatchWeighted(string query)
        {
            using (var db = new StackoverflowDbContext())
            {
                var result = new List<BestMatchWeightedDto>();

                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Add("@query", DbType.String);
                cmd.Parameters["@query"].Value = query;

                cmd.CommandText = "call BestMatchWeighted(@query)";

                var reader = cmd.ExecuteReader();

                while (await reader.ReadAsync())
                {
                    result.Add(new BestMatchWeightedDto
                    {
                        Id = (int)reader["id"],
                        Rank = (decimal)reader["rank"]
                    });
                }

                return result;
            }
        }
    }
}
