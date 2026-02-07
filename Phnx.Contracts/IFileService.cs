using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Contracts
{

    public interface IFileService
    {
        void DeleteFile(string filePath);
        void DeleteFiles(List<string> files);
        Task<string> SaveFileAsync(string prefixName, IFormFile? file, CancellationToken cancellationToken);
        Task<List<string>> SaveFilesAsync(string prefixName, IFormFileCollection? files, CancellationToken cancellationToken);
    }
}