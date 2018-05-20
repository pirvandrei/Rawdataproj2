using DataRepository;
using DataRepository.Dto.SearchDto;
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
        public async Task<Tuple<IList<SearchResultDto>, int>> MatchAll(string query, PagingInfo pagingInfo, string method, string startDate, string endDate)
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
                cmd.Parameters.Add("@startDate", DbType.String);
                cmd.Parameters.Add("@endDate", DbType.String);

                cmd.Parameters["@pageSize"].Value = pagingInfo.PageSize;
                cmd.Parameters["@pageNumber"].Value = pagingInfo.Page;
                cmd.Parameters["@query"].Value = query;
                cmd.Parameters["@startDate"].Value = startDate;
                cmd.Parameters["@endDate"].Value = endDate;

                cmd.CommandText = "call MatchAll(@query, @pageSize, @pageNumber, @startDate, @endDate)";

                var reader = cmd.ExecuteReader();

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

                string queryString = "SELECT FOUND_ROWS()";
                int numberOfRows = 0;
                var command = new MySqlCommand(queryString, conn);
                using (conn)
                {
                    // Create the Command and Parameter objects.
                   
                    //command.Parameters.AddWithValue("@pricePoint", paramValue);

                    // Open the connection in a try/catch block. 
                    // Create and execute the DataReader, writing the result
                    // set to the console window.
                    try
                    {
                        conn.Open();
                        var r = command.ExecuteReader();
                        while (r.Read())
                        {
                            Console.WriteLine("{0}",
                                r[0]);
                            numberOfRows = (int)r[0];
                        }
                        r.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadLine();
                }

                return new Tuple<IList<SearchResultDto>, int> (result, numberOfRows);
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

                cmd.CommandText = "call BestMatchRanked(@query, @pageSize, @pageNumber)";

                var reader = cmd.ExecuteReader();

                while (await reader.ReadAsync())
                {
                    result.Add(new SearchResultDto
                    {
                        Id = (int)reader["id"],
                        Title = (reader["title"] == DBNull.Value) ? "" : (string)reader["title"],
                        Body = (reader["body"] == DBNull.Value) ? "" : (string)reader["body"],
                        PostType = (int)reader["PostType"],
                        CreationDate = (DateTime)reader["CreationDate"],
                        AcceptedAnswerId = (reader["AcceptedAnswerId"] == DBNull.Value) ? 0 : (int)reader["AcceptedAnswerId"],
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
