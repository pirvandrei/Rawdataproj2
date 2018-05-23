using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DataService;
using DataService.Dto.StatisticsDto;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace StackoverflowContext
{
    public class StatisticsRepository : IStatisticsRepository
    {
        public async Task<IList<RankedWordListDto>> RankedWordList(string word)
        {
            using (var db = new StackoverflowDbContext())
            {
                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();

                var cmd = new MySqlCommand
                {
                    Connection = conn
                };

                cmd.Parameters.Add("@param", DbType.String);
                cmd.Parameters["@param"].Value = word;

                cmd.CommandText = "call RankedWordList(@param)";

                var result = new List<RankedWordListDto>();

                using (var reader = cmd.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new RankedWordListDto
                        {
                            Word = (string)reader["word"],
                            Rank = (decimal)reader["rank"]
                        });
                    }
                }

                return result;
            }
        }

        public async Task<IList<WeightedWordListDto>> WeightedWordList(string term)
        {
            using (var db = new StackoverflowDbContext())
            {
                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();

                var cmd = new MySqlCommand
                {
                    Connection = conn
                };

                cmd.Parameters.Add("@param", DbType.String);
                cmd.Parameters["@param"].Value = term;

                cmd.CommandText = "call WeightedWordList(@param)";

                var result = new List<WeightedWordListDto>();

                using (var reader = cmd.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new WeightedWordListDto
                        {
                            Term = (string)reader["term"],
                            Rank = (decimal)reader["rank"]
                        });
                    }
                }

                return result;
            }
        }

        public async Task<IList<AssociationsListDto>> GetAssociations(string word)
        {
            using (var db = new StackoverflowDbContext())
            {
                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();

                var cmd = new MySqlCommand
                {
                    Connection = conn
                };

                cmd.Parameters.Add("@param", DbType.String);
                cmd.Parameters["@param"].Value = word;

                cmd.CommandText = "call GetAssociations(@param)";

                var result = new List<AssociationsListDto>();

                using (var reader = cmd.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new AssociationsListDto
                        {
                            Word = (string)reader["word2"],
                            Grade = (decimal)reader["grade"]
                        });
                    }
                }

                return result;
            }
        }

        public async Task<TermNetworkDto> TermNetwork(string word, double grade)
        {
            using (var db = new StackoverflowDbContext())
            {
                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();

                var cmd = new MySqlCommand
                {
                    Connection = conn
                };

                cmd.Parameters.Add("@w", DbType.String);
                cmd.Parameters.Add("@n", DbType.Double);
                cmd.Parameters["@w"].Value = word;
                cmd.Parameters["@n"].Value = grade;

                cmd.CommandText = "call term_network(@w, @n)";

                var result = new TermNetworkDto();
                var sb = new StringBuilder();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        sb.Append((string)reader["var graph = "]);
                    }
                }
                result.Graph = sb.ToString();

                return result;
            }
        }

    }
}
