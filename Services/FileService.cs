using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using GraphOptimizer.Models;
using GraphOptimizer.Models.Persistence;
using GraphOptimizer.ViewModels.GraphCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphOptimizer.Services
{
    public class FileService: IFileService
    {
        private readonly ISerializationService SerializationService;

        public FileService()
        {
            SerializationService = new SerializationService();
        }

        public async Task SaveProjectAsync(Visual visualRoot, GraphViewModel graphVM)
        {
            var topLevel = TopLevel.GetTopLevel(visualRoot);
            if (topLevel == null)
            {
                return;
            }

            try
            {
                var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
                {
                    Title = "Зберегти проект",
                    FileTypeChoices = new List<FilePickerFileType>
                {
                    FileTypes.GraphOptimizerProject
                },
                    DefaultExtension = "gop",
                    SuggestedFileName = "go_project.gop"
                });

                if (file == null)
                {
                    return;
                }

                await using var stream = await file.OpenWriteAsync();
                await using var writer = new StreamWriter(stream);

                string json = SerializationService.SerializeProject(graphVM);

                await writer.WriteAsync(json);
                await writer.FlushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Помилка: {ex.Message}");
                return;
            }
        }

        public async Task SaveResultAsync(Visual visualRoot, GraphViewModel graphVM, AnalysisResult analysisResult)
        {
            var topLevel = TopLevel.GetTopLevel(visualRoot);
            if (topLevel == null)
            {
                return;
            }

            try
            {
                var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
                {
                    Title = "Зберегти результати",
                    FileTypeChoices = new List<FilePickerFileType>
                {
                    FileTypes.GraphOptimizerResult
                },
                    DefaultExtension = "gor",
                    SuggestedFileName = "go_result.gor"
                });

                if (file == null)
                {
                    return;
                }

                await using var stream = await file.OpenWriteAsync();
                await using var writer = new StreamWriter(stream);

                string json = SerializationService.SerializeResult(graphVM, analysisResult);

                await writer.WriteAsync(json);
                await writer.FlushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Помилка: {ex.Message}");
                return;
            }
        }

        public async Task<ProjectDto?> LoadProjectAsync(Visual visualRoot)
        {
            var topLevel = TopLevel.GetTopLevel(visualRoot);
            if (topLevel == null)
            {
                return null;
            }

            try
            {
                var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Відкрити проєкт",
                    FileTypeFilter = new[] { FileTypes.GraphOptimizerProject, FileTypes.GraphOptimizerResult },
                    AllowMultiple = false
                });

                if (files.Count == 0)
                {
                    return null;
                }

                await using var stream = await files[0].OpenReadAsync();
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();

                return SerializationService.DeserializeAny(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Помилка: {ex.Message}");
                return null;
            }
        }
    }

    public static class FileTypes
    {
        public static FilePickerFileType GraphOptimizerProject { get; } = new("Graph Optimizer Project (*.gop)")
        {
            Patterns = new[] { "*.gop" },
            MimeTypes = new[] { "application/gop" }
        };

        public static FilePickerFileType GraphOptimizerResult { get; } = new("Graph Optimizer Result (*.gor)")
        {
            Patterns = new[] { "*.gor" },
            MimeTypes = new[] { "application/gor" }
        };
    }
}
