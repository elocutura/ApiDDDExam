using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VY.Rebeld.Data.Contracts.Entities;
using VY.Rebeld.Data.Contracts.Repositories;
using VY.Rebeld.Dtos;
using VY.Rebeld.Infrastructure.Contracts;

// Explose internals to Xunit for testing
[assembly: InternalsVisibleTo("VY.Rebeld.UnitTests.Data")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace VY.Rebeld.Data.Impl.Repositories
{
    public class FileRebeldRepository : IRebeldRepository
    {
        private readonly string _filePath;

        public FileRebeldRepository(string filePath)
        {
            _filePath = filePath;
        }

        // Just for testing
        internal FileRebeldRepository()
        { }

        public async Task<OperationResult<RebeldSightingDto>> GetSightingByNameAsync(string name)
        {
            var toReturn = new OperationResult<RebeldSightingDto>();

            try
            {
                var result = await ParseFileToDict();
                var rebeldFound = result.Result[name];
                toReturn.Result = rebeldFound;
            }
            catch (Exception ex)
            {
                toReturn.AddError(204, "Rebeld not found in file");
            }

            return  toReturn;
        }

        public async Task<OperationResult> SaveRebeldAsync(RebeldEntity rebeldEntity)
        {
            var toReturn = new OperationResult();

            try
            {
                // Saved to file by key,value for easier search
                // Using Guid as key would be better in most cases
                await File.AppendAllTextAsync(_filePath, string.Format("{0},_rebeld {1} on {2} at {3}_\n", rebeldEntity.Name, rebeldEntity.Name, rebeldEntity.Planet, rebeldEntity.Date.ToString())) ;
            }
            catch (Exception ex)
            {
                toReturn.AddError(ex);
            }
            return toReturn;
        }

        // Internal virtual only for testing, otherwise only private
        // This is not optimal for performance, if we wanted optimal performance we would check names here as we are
        // reading the file to avoid creating the dictionary and avoid parsing all lines beyond the name we are searching for
        // I chose this implementation since this way methods are compact and easy to read
        // Optimized version is coded right below, but not used
        internal virtual async Task<OperationResult<Dictionary<string, RebeldSightingDto>>> ParseFileToDict()
        {
            var toReturn = new OperationResult<Dictionary<string, RebeldSightingDto>>();
            Dictionary<string, RebeldSightingDto> dict = new Dictionary<string, RebeldSightingDto>();

            try
            {
                var lines = await File.ReadAllLinesAsync(_filePath);

                for (int i = lines.Length-1; i > 0; i++)
                {
                    string[] fields = lines[i].Split(',');
                    dict.Add(fields[0], new RebeldSightingDto() { SightText = fields[1] });
                }
            }
            catch (Exception ex)
            {
                toReturn.AddError(404, "File Not found");
            }

            if (dict.Count <= 0)
            {
                toReturn.AddError(204, "File is empty");
            }

            toReturn.Result = dict;

            return toReturn;
        }
        // Optimized version of searching for the name
        // No dictionary is made on memory, no further file processing is done when name is found
        private async Task<OperationResult<RebeldSightingDto>> FindRebelByNameAsync(string name)
        {
            var toReturn = new OperationResult<RebeldSightingDto>();

            try
            {
                var lines = await File.ReadAllLinesAsync(_filePath);


                if (lines.Length <= 0)
                {
                    toReturn.AddError(204, "File is empty");
                    return toReturn;
                }

                for (int i = lines.Length - 1; i > 0; i++)
                {
                    string[] fields = lines[i].Split(',');
                    if (fields[0] == name) // found rebeld, no need to keep searching the document
                    {
                        toReturn.Result = new RebeldSightingDto() { SightText = fields[1] };
                        return toReturn;
                    }
                }
            }
            catch (Exception ex)
            {
                toReturn.AddError(404, "File Not found");
            }

            // If rebeld not found but no errors either, return no content
            toReturn.AddError(204, "Rebeld not found in file");

            return toReturn;
        }

    }
}
