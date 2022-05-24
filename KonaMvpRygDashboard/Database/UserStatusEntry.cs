using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace KonaMvpRygDashboard.Database
{
    public class UserStatusEntry
    {
        [Key]
        public string Id { get; set; }
        public string Timestamp { get; set; }
        public string Elaboration { get; set; }
        public string Emotion { get; set; }
        public string MeetingHours { get; set; }
        public string Platform { get; set; }
        public string PrivateElaboration { get; set; }
        public string Reactions { get; set; }
        [Required]
        public string Selection { get; set; }
        public string SlackMessageId { get; set; }
        [Required]
        public string SlackOrgId { get; set; }
        [Required]
        public string SlackTeamId { get; set; }
        [Required]
        public string SlackUserId { get; set; }

    }
}
