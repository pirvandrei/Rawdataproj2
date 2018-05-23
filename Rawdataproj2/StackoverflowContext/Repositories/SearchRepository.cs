using DataService;
using DataService.Dto.SearchDto;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StackoverflowContext
{
    public class SearchRepository : ISearchRepository
    {
        public async Task<Tuple<IList<SearchResultDto>, int>> BestMatchRanked(string query, PagingInfo pagingInfo, string startDate, string endDate)
        {
            using (var db = new StackoverflowDbContext())
            {             
                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();

                var cmd = InitCommand(conn, query, pagingInfo, startDate, endDate);
                cmd.CommandText = "call BestMatchRanked(@query, @pageSize, @pageNumber, @startDate, @endDate)";

                var result = await ReadFromDatabase(cmd);

                var numberOfRows = await GetNumberOfRows(cmd, "call BestMatchRanked_Count(@query, @startDate, @endDate)");    
                
                return new Tuple<IList<SearchResultDto>, int> (result, numberOfRows);
            }
        }

        public async Task<Tuple<IList<SearchResultDto>, int>> MatchAll(string query, PagingInfo pagingInfo, string startDate, string endDate)
        {
            using (var db = new StackoverflowDbContext())
            {
                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();

                var cmd = InitCommand(conn, query, pagingInfo, startDate, endDate);
                cmd.CommandText = "call MatchAll(@query, @pageSize, @pageNumber, @startDate, @endDate)";

                var result = await ReadFromDatabase(cmd);

                var numberOfRows = await GetNumberOfRows(cmd, "call MatchAll_Count(@query, @startDate, @endDate)");

                return new Tuple<IList<SearchResultDto>, int>(result, numberOfRows);
            }
        }

        public async Task<Tuple<IList<SearchResultDto>, int>> BestMatchWeighted(string query, PagingInfo pagingInfo, string startDate, string endDate)
        {
            using (var db = new StackoverflowDbContext())
            {
                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();

                var cmd = InitCommand(conn, query, pagingInfo, startDate, endDate);
                cmd.CommandText = "call BestMatchWeighted(@query, @pageSize, @pageNumber, @startDate, @endDate)";

                var result = await ReadFromDatabase(cmd);

                var numberOfRows = await GetNumberOfRows(cmd, "call BestMatchWeighted_Count(@query, @startDate, @endDate)");

                return new Tuple<IList<SearchResultDto>, int>(result, numberOfRows);
            }
        }

        /*******************************************************
         * Helpers
         * *****************************************************/

        private async Task<int> GetNumberOfRows(MySqlCommand cmd, string cmdText)
        {
            var numberOfRows = 0;
            cmd.CommandText = cmdText;

            using (var reader = cmd.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                     numberOfRows = (int)(Int64)reader["COUNT(*)"];
                }
            }
            return numberOfRows;
        }

        private MySqlCommand InitCommand(MySqlConnection conn, string query, PagingInfo pagingInfo, string startDate, string endDate)
        {
            var cmd = new MySqlCommand
            {
                Connection = conn
            };

            cmd.Parameters.Add("@pageSize", DbType.Int32);
            cmd.Parameters.Add("@pageNumber", DbType.Int32);
            cmd.Parameters.Add("@query", DbType.String);
            cmd.Parameters.Add("@startDate", DbType.String);
            cmd.Parameters.Add("@endDate", DbType.String);

            cmd.Parameters["@pageSize"].Value = pagingInfo.PageSize;
            cmd.Parameters["@pageNumber"].Value = pagingInfo.Page;
            cmd.Parameters["@query"].Value = query;
            cmd.Parameters["@startDate"].Value = startDate;
            cmd.Parameters["@endDate"].Value = endDate;

            return cmd;
        }

        private async Task<IList<SearchResultDto>> ReadFromDatabase(MySqlCommand cmd)
        {
            var result = new List<SearchResultDto>();

            using (var reader = cmd.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    result.Add(new SearchResultDto
                    {
                        Id = (int)reader["id"],
                        Title = (reader["title"] == DBNull.Value) ? "" : (string)reader["title"],
                        Body = (reader["body"] == DBNull.Value) ? "" : (string)reader["body"],
                        PostType = (int)reader["PostType"],
                        CreationDate = (DateTime)reader["CreationDate"],
                        AcceptedAnswerId = (reader["AcceptedAnswerID"] == DBNull.Value) ? 0 : (int)reader["AcceptedAnswerID"],
                        Rank = (decimal)reader["rank"]
                    });
                }
            }
            return result;
        }

    }
}
