using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// BlockForge ProjectFileManager 
/// Author: Angus Grewal
/// Date: Apr 7 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// File IO handler to save and load a Project from application to disk or vice-versa.
    /// </summary>
    public class ProjectFileManager
    {
        /// <summary>
        /// Requires a payload transformer to ensure our pipeline encrypts data at the file level.
        /// </summary>
        private PayloadTransformer _transformer;

        /// <summary>
        /// Constructor that will start the file manager with a supplied PayloadTransformer for encrypting or decrypting the data.
        /// </summary>
        /// <param name="transformer">which transformer to use.</param>
        public ProjectFileManager(PayloadTransformer transformer)
        {
            _transformer = transformer;
        }

        /// <summary>
        /// Saves the Project as a file on disk.
        /// Project (DTO) -> JSON -> Encrypted -> UTF-8 encoded bytes -> [ProjectName].bfg
        /// </summary>
        /// <param name="project">project to save.</param>
        /// <exception cref="InvalidOperationException">thrown when CodeBlocks fail to validate.</exception>
        /// <exception cref="InvalidDataException">thrown if the project is attempted to be saved without a proper name.</exception>
        public void SaveFile(Project project)
        {
            List<string> errors = CodeBlockValidator.Validate(project.CodeBlocks);
            if (errors.Count > 0)
            {
                string message = "Validation failed:\n" + string.Join("\n", errors);
                throw new InvalidOperationException(message);
            }

            if (project.ProjectName == null)
            {
                throw new InvalidDataException("Project name cannot be null when saving.");
            }

            string json = ProjectSerializer.Serialize(project);
            string scrambled = _transformer.Scramble(json);
            byte[] encoded = Encoding.UTF8.GetBytes(scrambled);

            File.WriteAllBytes(project.ProjectName + ".bfg", encoded);
        }

        /// <summary>
        /// Loads a Project from a file on disk.
        /// [ProjectName].bfg -> UTF-8 encoded bytes -> Encrypted -> JSON -> Project (DTO)
        /// </summary>
        /// <param name="filepath">the file to load.</param>
        /// <returns>a reconstructed Project.</returns>
        /// <exception cref="InvalidDataException">thrown if somehow a Project was saved without its name metadata.</exception>
        /// <exception cref="InvalidOperationException">thrown when CodeBlocks fail to validate.</exception>
        public Project LoadFile(string filepath)
        {
            byte[] data = File.ReadAllBytes(filepath);
            string scrambled = Encoding.UTF8.GetString(data);
            string json = _transformer.Unscramble(scrambled);

            Project project = ProjectSerializer.Deserialize(json);

            if (project.ProjectName == null)
            {
                throw new InvalidDataException("Project name was null when loading.");
            }

            List<string> errors = CodeBlockValidator.Validate(project.CodeBlocks);

            if (errors.Count > 0)
            {
                string message = "Validation failed:\n" + string.Join("\n", errors);
                throw new InvalidOperationException(message);
            }

            return project;
        }

        /// <summary>
        /// From project, output code. For now, uses PseudoCodeGenerator.
        /// </summary>
        /// <param name="project">The project to generate output code from.</param>
        /// <param name="filepath">the file to write to.</param>
        /// <exception cref="ArgumentNullException">thrown when a project is null.</exception>
        /// <exception cref="ArgumentException">thrown when there is no filepath to write to.</exception>
        public void OutputCode(Project project, string filepath)
        {
            List<string> errors = CodeBlockValidator.Validate(project.CodeBlocks);
            if (errors.Count > 0)
            {
                string message = "Validation failed:\n" + string.Join("\n", errors);
                throw new InvalidOperationException(message);
            }

            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentException("Filepath can't be null or empty.", nameof(filepath));
            }


            PseudoCodeGenerator generator = new PseudoCodeGenerator();
            string output = generator.Generate(project);

            File.WriteAllText(filepath, output);
        }
    }
}
