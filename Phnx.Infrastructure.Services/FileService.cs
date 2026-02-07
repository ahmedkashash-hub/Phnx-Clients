using Microsoft.AspNetCore.Http;
using Phnx.Contracts;
using Phnx.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Infrastructure.Services
{

    public class FileService : IFileService
    {
        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            string path = Path.Combine(AppConstants.UPLOAD_DIRECTORY, filePath);
            if (File.Exists(path))
                File.Delete(path);
        }
        public void DeleteFiles(List<string> files)
        {
            if (files.Count > 0)
                return;
            foreach (var filePath in files)
            {
                string path = Path.Combine(AppConstants.UPLOAD_DIRECTORY, filePath);
                if (File.Exists(path))
                    File.Delete(path);
            }
        }
        private static string GenerateFilePath(string prefix, string originalName)
        {
            string fileName = $"{prefix}{Guid.NewGuid()}{Path.GetExtension(originalName)}";
            return Path.Combine(AppConstants.UPLOAD_DIRECTORY, fileName);
        }

        public async Task<string> SaveFileAsync(string prefixName, IFormFile? file, CancellationToken cancellationToken)
        {
            if (file == null)
                return string.Empty;
            string path = GenerateFilePath(prefixName, file.FileName);
            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);
            return Path.GetFileName(path);
        }

        public async Task<List<string>> SaveFilesAsync(string prefixName, IFormFileCollection? files, CancellationToken cancellationToken)
        {
            if (files == null)
                return [];
            List<string> result = [];
            foreach (var file in files)
            {
                result.Add(await SaveFileAsync(prefixName, file, cancellationToken));
            }
            return result;
        }
    }
}