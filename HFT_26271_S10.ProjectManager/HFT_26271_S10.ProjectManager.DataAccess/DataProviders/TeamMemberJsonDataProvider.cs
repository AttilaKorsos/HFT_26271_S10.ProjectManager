using HFT_26271_S10.ProjectManager.DataAccess.Interfaces;
using HFT_26271_S10.ProjectManager.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HFT_26271_S10.ProjectManager.DataAccess.DataProviders
{
    public class TeamMemberJsonDataProvider : ITeamMemberDataProvider
    {
        //3.1. feladat
        private static readonly string FilePath = Path.Combine("Data", "teammembers.json");
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        private List<TeamMember> LoadTeamMembers()
        {
            if (!File.Exists(FilePath)) return new List<TeamMember>();

            try
            {
                string json = File.ReadAllText(FilePath);

                return JsonSerializer.Deserialize<List<TeamMember>>(json, options) ?? new List<TeamMember>();
            }
            catch (JsonException)
            {
                return new List<TeamMember>();
            }
        }
        private void SaveTasks(List<TeamMember> teamMembers)
        {
            string json = JsonSerializer.Serialize(teamMembers, options);

            File.WriteAllText(FilePath, json);
        }

        //3.2. feladat

        // READ - összes projektfeladat
        public List<TeamMember> GetAll()
        {
            return LoadTeamMembers();
        }

        // READ - keresés Id alapján
        public TeamMember? GetById(int id)
        {
            List<TeamMember> teamMembers = GetAll();

            return teamMembers.FirstOrDefault(teamMember => teamMember.Id == id);
        }

        // CREATE
        public void Add(TeamMember teamMember)
        {
            List<TeamMember> teamMembers = GetAll();

            teamMembers.Add(teamMember);

            SaveTasks(teamMembers);
        }

        // UPDATE
        public bool Update(TeamMember updatedteamMember)
        {
            List<TeamMember> teamMembers = GetAll();

            TeamMember? existingteamMember = teamMembers.FirstOrDefault(teamMember => teamMember.Id == updatedteamMember.Id);

            if (existingteamMember == null)
            {
                return false;
            }

            existingteamMember.Name = updatedteamMember.Name;
            existingteamMember.Email = updatedteamMember.Email;
            existingteamMember.Role = updatedteamMember.Role;

            SaveTasks(teamMembers);

            return true;
        }

        // DELETE
        public bool Delete(int id)
        {
            List<TeamMember> teamMembers = GetAll();

            TeamMember? teamMemberToDelete = teamMembers.FirstOrDefault(task => task.Id == id);

            if (teamMemberToDelete == null) return false;

            teamMembers.Remove(teamMemberToDelete);

            SaveTasks(teamMembers);

            return true;
        }
    }
}
