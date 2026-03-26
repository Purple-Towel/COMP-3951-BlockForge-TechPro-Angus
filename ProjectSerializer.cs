using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

/// <summary>
/// BlockForge ProjectSerializer 
/// Author: Angus Grewal
/// Date: Mar 25 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Handles serialization and deserialization of Projects.
    /// Used in the ProjectFileHandler to handle read/write disk operations with serialized data.
    /// </summary>
    public class ProjectSerializer
    {
        /// <summary>
        /// Contains the options for the JsonSerializer.
        /// </summary>
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
        };

        /// <summary>
        /// Serialize a Project.
        /// </summary>
        /// <param name="project">Project to serialize</param>
        /// <returns>serialized Project.</returns>
        public static string Serialize(Project project)
        {
            return JsonSerializer.Serialize(project, Options);
        }

        /// <summary>
        /// Deserialize JSON data to a Project.
        /// </summary>
        /// <param name="json">the JSON data to deserialize</param>
        /// <returns>deserialized Project</returns>
        /// <exception cref="InvalidOperationException">thrown when JSON data is invalid</exception>
        public static Project Deserialize(string json)
        {
            var project = JsonSerializer.Deserialize<Project>(json);

            if (project == null)
            {
                throw new InvalidOperationException("Invalid JSON data for Project.");
            }

            return project;
        }
    }
}
